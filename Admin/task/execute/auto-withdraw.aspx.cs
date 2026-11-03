using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_task_execute_auto_withdraw : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == "auto")
        {
            DateTime date = DateTime.UtcNow.AddHours(5.5);
            if (Request.QueryString["d"] != null)
            {
                date = Convert.ToDateTime(Request.QueryString["d"]);
            }

            db.CommandTimeout = 7200;


            MemberPayout();
            MemberRepurchasePayout();

            if (date.Day == 1)
            {
                FranchiseCommission(date);
               // ConsistencyOffer(date);
                GenerateFranchiseCommission(date);
                FranchiseReferralPayout();

            }          
        }
    }
   
    //private void ConsistencyOffer(DateTime date)
    //{
    //    DateTime lastDate = date.AddDays(-1).Date;
    //    DateTime startDate = new DateTime(lastDate.Year, lastDate.Month, 1);

    //    var bvs = from c in db.tblBVs
    //              where c.date >= startDate && c.date <= lastDate
    //              group c by c.username into grp
    //              select new
    //              {
    //                  username = grp.Key,
    //                  leftBv = grp.Sum(x => x.leftBv),
    //                  rightBv = grp.Sum(x => x.rightBv),
    //                  selfBv = grp.Sum(x => x.Bv)
    //              };
    //    bool querterly = false;  // 3 months 1 product
    //    bool offer, selfBvEligible;
    //    decimal productWorth = 0;
    //    foreach (var bv in bvs)
    //    {
    //        offer = selfBvEligible = false;

    //        RequestProduct prod = db.RequestProducts.Where(x => x.username == bv.username && x.name == "Consistency Offer").OrderByDescending(x => x.date).FirstOrDefault();
    //        if (prod == null || (prod != null && prod.date.Value.AddMonths(3).Date <= date.Date))
    //        {
    //            querterly = true;
    //        }
    //        else
    //            querterly = false;

    //        if (querterly)
    //        {
    //            if (bv.leftBv >= 10000 && bv.rightBv >= 10000)
    //            {
    //                productWorth = 5000;
    //                selfBvEligible = bv.selfBv >= 500;
    //                var tuple = CheckForPreviosBv(bv.username, 10000, lastDate, 500);
    //                offer = tuple.Item1;
    //                if (selfBvEligible == false)
    //                {
    //                    selfBvEligible = tuple.Item2;
    //                }
    //            }
    //            else if (bv.leftBv >= 25000 && bv.rightBv >= 25000)
    //            {
    //                productWorth = 10000;
    //                selfBvEligible = bv.selfBv >= 1000;
    //                var tuple = CheckForPreviosBv(bv.username, 25000, lastDate, 1000);
    //                offer = tuple.Item1;
    //                if (selfBvEligible == false)
    //                {
    //                    selfBvEligible = tuple.Item2;
    //                }
    //            }
    //            else if (bv.leftBv >= 50000 && bv.rightBv >= 50000)
    //            {
    //                productWorth = 20000;
    //                selfBvEligible = bv.selfBv >= 1500;
    //                var tuple = CheckForPreviosBv(bv.username, 50000, lastDate, 1500);
    //                offer = tuple.Item1;
    //                if (selfBvEligible == false)
    //                {
    //                    selfBvEligible = tuple.Item2;
    //                }
    //            }

    //            if (selfBvEligible && offer)
    //            {
    //                ProductRequest(bv.username, "Consistency Offer", productWorth);
    //            }
    //        }
    //    }
    //    db.SubmitChanges();
    //}

    private void ProductRequest(string uname, string plan, decimal cost)
    {
        RequestProduct up = new RequestProduct();
        up.username = uname;
        up.status = "Pending";
        up.date = DateTime.UtcNow.AddHours(5.5);
        up.name = plan;
        up.cost = cost;
        db.RequestProducts.InsertOnSubmit(up);
    }

    //private Tuple<Boolean, Boolean> CheckForPreviosBv(string username, decimal cutoff, DateTime date, decimal selfCutoff)
    //{
    //    bool offerEligible = false;
    //    bool selfBvEligible = false;
    //    for (int i = 1; i <= 2; i++)
    //    {
    //        DateTime lastDate = date.AddMonths(-i);
    //        DateTime startDate = new DateTime(lastDate.Year, lastDate.Month, 1);

    //        var bv = (from c in db.tblBVs
    //                  where c.date >= startDate && c.date <= lastDate && c.username == username
    //                  group c by c.username into grp
    //                  select new
    //                  {
    //                      username = grp.Key,
    //                      leftBv = grp.Sum(x => x.leftBv),
    //                      rightBv = grp.Sum(x => x.rightBv),
    //                      selfBv = grp.Sum(x => x.Bv)
    //                  }).SingleOrDefault();
    //        if (bv != null && bv.leftBv >= cutoff && bv.rightBv >= cutoff)
    //        {
    //            offerEligible = true;
    //            selfBvEligible = bv.selfBv >= selfCutoff;  //any one month
    //        }
    //        else
    //            offerEligible = false;
    //    }
    //    return new Tuple<bool, bool>(offerEligible, selfBvEligible);
    //}

    private void MemberRepurchasePayout(DateTime? date = null)
    {
        if (date == null)
            date = DateTime.UtcNow.AddHours(5.5).Date;
        var getpayout = from c in db.tblMembers
                        where c.isActive == 1 && c.RepIncome >= 100 //&& c.isPanVerifed == true
                        select c;
        foreach (var g in getpayout)
        {
            decimal deduction = 0;
            int? loanId = null;
            decimal credit = g.RepIncome * 90 / 100;
            var checkLoan = db.tblLoans.FirstOrDefault(x => x.status == "Active" && x.username == g.username);
            if (checkLoan != null)
            {
                decimal remaining = checkLoan.loanAmount.Value - checkLoan.recoveredAmount.Value;
                decimal deductionAmt = credit * checkLoan.recoveryPercent.Value / 100;
                if (deductionAmt > remaining)
                {
                    deduction = remaining;
                }
                else
                {
                    deduction = deductionAmt;
                }

                loanId = checkLoan.loanId;
                checkLoan.recoveredAmount += deduction;
                if (checkLoan.loanAmount == checkLoan.recoveredAmount)
                    checkLoan.status = "Closed";
            }
            decimal amt = g.RepIncome;
            var insert = new PaymentRequest
            {
                date = date,
                username = g.username,
                cashavailable = g.credit,
                amount = amt,
                tax = amt * 2 / 100,
                sc = amt * 8 / 100,
                deduction = deduction,
                netpay = ((amt * 90 / 100) - deduction),
                status = "Pending",
                comments = "Repurchase Pay"
            };
            if (deduction > 0)
            {
                insert.loanId = loanId;
            }
            db.PaymentRequests.InsertOnSubmit(insert);
            g.RepIncome = 0;

        }
        db.SubmitChanges();
    }

    private void MemberPayout(DateTime? date = null)
    {
        if (date == null)
            date = DateTime.UtcNow.AddHours(5.5).Date;
        var getpayout = from c in db.tblMembers
                        where c.isActive == 1 && c.credit >= 100 //&& c.isPanVerifed == true
                        select c;
        foreach (var g in getpayout)
        {

            decimal credit = g.credit.Value * 90 / 100;
            decimal deduction = 0;
            int? loanId = null;
            var checkLoan = db.tblLoans.FirstOrDefault(x => x.status == "Active" && x.username == g.username);
            if (checkLoan != null)
            {
                decimal remaining = checkLoan.loanAmount.Value - checkLoan.recoveredAmount.Value;
                decimal deductionAmt = credit * checkLoan.recoveryPercent.Value / 100;
                if (deductionAmt > remaining)
                {
                    deduction = remaining;
                }

                else
                {
                    deduction = deductionAmt;
                }

                loanId = checkLoan.loanId;
                checkLoan.recoveredAmount += deduction;
                if (checkLoan.loanAmount == checkLoan.recoveredAmount)
                    checkLoan.status = "Closed";
            }
            decimal amt = g.credit.Value;
            var insert = new PaymentRequest
            {
                date = date,
                username = g.username,
                cashavailable = g.credit,
                amount = amt,
                tax = amt * 2 / 100,
                sc = amt * 8 / 100,
                deduction = deduction,
                netpay = ((amt * 90 / 100) - deduction),
                status = "Pending",
                comments = "Auto Pay"
            };
            if (deduction > 0)
            {
                insert.loanId = loanId;
            }
            db.PaymentRequests.InsertOnSubmit(insert);
            g.credit = 0;

        }
        db.SubmitChanges();
    }

    private void FranchiseReferralPayout(DateTime? date = null)
    {
        if (date == null)
            date = DateTime.UtcNow.AddHours(5.5).Date;
        var getpayout = from c in db.tblVendors
                        where c.IsActive == true && c.KitWallet > 0 //&& c.isPanVerifed == true
                        select c;
        foreach (var g in getpayout)
        {
            var insert = new PaymentRequest
            {
                date = date,
                username = g.Username,
                cashavailable = g.KitWallet,
                amount = g.KitWallet,
                tax = g.KitWallet * 2 / 100,
                sc = g.KitWallet * 8 / 100,
                netpay = g.KitWallet * 90 / 100,
                status = "Pending",
                comments = "Franchisee Pay"
            };
            db.PaymentRequests.InsertOnSubmit(insert);
            g.KitWallet = 0;
          
        }
        db.SubmitChanges();
    }

    //private void FranchiseCommission(DateTime date)
    //{
    //    DateTime lastDate = date.AddDays(-1).Date;
    //    DateTime startDate = new DateTime(lastDate.Year, lastDate.Month, 1);

    //    //For Franchisee
    //    var invoices = from c in db.tblInvoices
    //                   where c.PurchaseDate >= startDate && c.PurchaseDate <= lastDate && c.FranchiseeId != 1
    //                   group c by c.FranchiseeId into grp
    //                   select new { franchiseeId = grp.Key, amount = grp.Sum(x => x.Amount) };
    //    foreach (var inv in invoices)
    //    {
    //        var franch = db.tblVendors.Single(x => x.Id == inv.franchiseeId);
    //        var role = db.tblRoles.SingleOrDefault(x => x.RoleId == franch.RoleId);

    //        decimal commissionPercent = 0;
    //        decimal totalOrderAmt = 0;
    //        var allOrders = from c in db.tblInvoices
    //                        where c.FranchiseeId == inv.franchiseeId
    //                        select c;
    //        if (allOrders.Any())
    //            totalOrderAmt = allOrders.Sum(x => x.Amount.Value);
    //        if (totalOrderAmt >= role.amount)
    //        {
    //            decimal orderAmt = inv.amount.Value;
    //            if (orderAmt > 0 && orderAmt <= 49999)
    //            {
    //                commissionPercent = 4;
    //            }
    //            else if (orderAmt > 49999 && orderAmt <= 99999)
    //            {
    //                commissionPercent = 5;
    //            }
    //            else if (orderAmt > 99999 && orderAmt <= 149999)
    //            {
    //                commissionPercent = 5.5m;
    //            }
    //            else if (orderAmt > 149999)
    //            {
    //                commissionPercent = 6;
    //            }
    //        }
    //        else
    //        {
    //            commissionPercent = role.bonusPercent.Value;
    //        }
    //        decimal commission = inv.amount.Value * commissionPercent / 100;
    //        if (commission > 0)
    //        {
    //            franch.RepurchaseWallet += commission;
    //            Payout(franch.Username, commission, "Franchisee Commission", "Commission from Members Invoice", date);
    //        }
    //    }
    //    db.SubmitChanges();
    //}


    private void FranchiseCommission(DateTime date)
    {
        DateTime lastDate = date.AddDays(-1).Date;
        DateTime startDate = new DateTime(lastDate.Year, lastDate.Month, 1);

        var invoices = from c in db.tblInvoices
                       where c.PurchaseDate >= startDate
                          && c.PurchaseDate < lastDate.AddDays(1)
                          && c.FranchiseeId != 1
                       group c by c.FranchiseeId into grp
                       select new
                       {
                           franchiseeId = grp.Key,
                           amount = grp.Sum(x => x.Amount ?? 0)
                       };

        foreach (var inv in invoices)
        {
            var franch = db.tblVendors.SingleOrDefault(x => x.Id == inv.franchiseeId);

            if (franch == null)
                continue;

            var role = db.tblRoles.SingleOrDefault(x => x.RoleId == franch.RoleId);

            if (role == null || !role.amount.HasValue || role.amount.Value <= 0)
                continue;


            // Calculate sales percentage
            decimal salesPercentage = (inv.amount / role.amount.Value) * 100;

            if (salesPercentage >= 30)
            {
                decimal commissionPercent = role.bonusPercent ?? 0;
            
                decimal commission = inv.amount * commissionPercent / 100;

                if (commission > 0)
                {
                    franch.RepurchaseWallet += commission;

                    Payout(
                        franch.Username,
                        commission,
                        "Franchisee Commission",
                        "Commission from Members Invoice",
                        date
                    );
                }
            }
            else
            {
                // Record lapsed commission eligibility
                var lapsed = new tblFranchiseCommissionLapsed
                {
                    Username = franch.Username,
                    TransactionDate = date,
                    Amount = inv.amount,
                    SalesPercentage = salesPercentage
                };

                db.tblFranchiseCommissionLapseds.InsertOnSubmit(lapsed);
            }
        }

        db.SubmitChanges();
    }
    private void Payout(string uname, decimal Amount, string desc, string remarks, DateTime date)
    {
        var history = new Payout
        {
            username = uname,
            credit = Amount,
            netpayout = Amount,
            debit = 0,
            date = date.Date,
            confirmdate = date,
            desc = desc,
            remarks = remarks,
            status = "Send",
        };
        db.Payouts.InsertOnSubmit(history);
        
    }

    private void GenerateFranchiseCommission(DateTime? date = null)
    {
        if (date == null)
            date = DateTime.UtcNow.AddHours(5.5).Date;
        var getpayout = from c in db.tblVendors
                        where c.IsActive == true && c.RepurchaseWallet >= 100 //&& c.isPanVerifed == true
                        select c;
        foreach (var g in getpayout)
        {
            decimal credit = g.RepurchaseWallet.Value * 90 / 100;
            decimal deduction = 0;
            int? loanId = null;
            var checkLoan = db.tblLoans.FirstOrDefault(x => x.status == "Active" && x.username == g.Username);
            if (checkLoan != null)
            {
                decimal remaining = checkLoan.loanAmount.Value - checkLoan.recoveredAmount.Value;
                decimal deductionAmt = credit * checkLoan.recoveryPercent.Value / 100;
                if (deductionAmt > remaining)
                {
                    deduction = remaining;
                }
                else
                {
                    deduction = deductionAmt;
                }

                loanId = checkLoan.loanId;
                checkLoan.recoveredAmount += deduction;
                if (checkLoan.loanAmount == checkLoan.recoveredAmount)
                    checkLoan.status = "Closed";
            }
            decimal amt = g.RepurchaseWallet.Value;

            var insert = new PaymentRequest
            {
                date = date,
                username = g.Username,
                cashavailable = g.RepurchaseWallet,
                amount = g.RepurchaseWallet,
                deduction = deduction,
                tax = amt * 2 / 100,
                sc = amt * 8 / 100,
                netpay = ((amt * 90 / 100) - deduction),
                status = "Pending",
                comments = "Franchisee Commission"
            };
            if (deduction > 0)
            {
                insert.loanId = loanId;
            }

            db.PaymentRequests.InsertOnSubmit(insert);
            g.RepurchaseWallet = 0;
           
        }
        db.SubmitChanges();
    }

}