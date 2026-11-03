using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{

    DataClassesDataContext db = new DataClassesDataContext(); sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    private void DistrictFranchiseeSalary(DateTime date)
    {
        var districts = from c in db.tblVendors
                        where c.IsActive == true && c.RoleId == 2  // District Franchisee
                        select c;

        foreach (var dist in districts)
        {
            // ✅ Salary = 1% of Amount
            decimal salary = (decimal)dist.Amount * 0.01m;

            // ✅ Count existing salary payouts
            int count = db.Payouts
                          .Count(x => x.username == dist.Username && x.desc == "Salary Income");

            // ✅ Only for 10 salaries
            if (count < 11)
            {
                dist.salaryWallet += salary;

                db.Payouts.InsertOnSubmit(new Payout
                {
                    username = dist.Username,
                    credit = salary,
                    netpayout = salary,
                    debit = 0,
                    tax = 0,
                    desc = "Salary Income",
                    remarks = "District Franchisee Salary",
                    status = "Send",
                    date = date.Date,
                    confirmdate = date
                });

                // ✅ Last month date range
                DateTime startDate = new DateTime(date.Year, date.Month, 1).AddMonths(-1);
                DateTime endDate = new DateTime(date.Year, date.Month, 1).AddDays(-1);

                // ✅ Calculate last month’s stock selling for THIS dist
                decimal lastMonthStock = (from c in db.StockRequests
                                          where c.Status != "Requested"
                                                && c.RequestType == "Product"
                                                && c.VendorId == dist.Id
                                                && c.SentDate >= startDate && c.SentDate <= endDate
                                          select (decimal?)c.AmountPaid).Sum() ?? 0;

                if (!string.IsNullOrEmpty(dist.SponsoredBy) && lastMonthStock >= 400000)
                {
                    // find sponsor in Members or Vendors
                    var sponsorMember = db.tblMembers.SingleOrDefault(x => x.username == dist.SponsoredBy);
                    var sponsorVendor = db.tblVendors.SingleOrDefault(x => x.Username == dist.SponsoredBy);

                    string sponsorName = (sponsorMember != null) ? sponsorMember.username :
                        (sponsorVendor != null) ? sponsorVendor.Username : null;

                    if (sponsorName != null)
                    {
                        decimal bonus = salary * 0.5m; // 50% bonus

                        // credit to correct wallet
                        if (sponsorMember != null)
                            sponsorMember.salaryWallet += bonus;
                        else
                            sponsorVendor.salaryWallet += bonus;


                        db.Payouts.InsertOnSubmit(new Payout
                        {
                            username = sponsorName,
                            credit = bonus,
                            netpayout = bonus,
                            debit = 0,
                            tax = 0,
                            desc = "Franchisee Referral Income",
                            remarks = string.Format("Last month stock sellings: {0}", lastMonthStock),
                            status = "Send",
                            date = date.Date,
                            confirmdate = date
                        });

                    }

                }
            }
        }

        db.SubmitChanges();
    }
    private void FranchiseeSalary(DateTime date)
    {
        var districts = from c in db.tblVendors
                        where c.IsActive == true && (c.RoleId != 2 && c.RoleId != 1) // other Franchisee
                        select c;

        foreach (var dist in districts)
        {
            // ✅ Salary = 1% of Amount
            decimal salary = (decimal)dist.Amount * 0.01m;

            // ✅ Count existing salary payouts
            int count = db.Payouts
                          .Count(x => x.username == dist.Username && x.desc == "Salary Income");

            // ✅ Only for 10 salaries
            if (count < 11)
            {
                if (dist.isFirst == true && dist.isSecond == true)
                {
                    dist.salaryWallet += salary;

                    db.Payouts.InsertOnSubmit(new Payout
                    {
                        username = dist.Username,
                        credit = salary,
                        netpayout = salary,
                        debit = 0,
                        tax = 0,
                        desc = "Salary Income",
                        remarks = "Monthly Franchisee Salary",
                        status = "Send",
                        date = date.Date,
                        confirmdate = date
                    });
                }

                // ✅ Last month date range
                DateTime startDate = new DateTime(date.Year, date.Month, 1).AddMonths(-1);
                DateTime endDate = new DateTime(date.Year, date.Month, 1).AddDays(-1);

                // ✅ Calculate last month’s stock selling for THIS dist
                decimal lastMonthStock = (from c in db.StockRequests
                                          where c.Status != "Requested"
                                                && c.RequestType == "Product"
                                                && c.VendorId == dist.Id
                                                && c.SentDate >= startDate && c.SentDate <= endDate
                                          select (decimal?)c.AmountPaid).Sum() ?? 0;

                var required = dist.Amount * 10 / 100;

                if (!string.IsNullOrEmpty(dist.SponsoredBy) && lastMonthStock >= required)
                {
                    var sponsorMember = db.tblMembers.SingleOrDefault(x => x.username == dist.SponsoredBy);
                    var sponsorVendor = db.tblVendors.SingleOrDefault(x => x.Username == dist.SponsoredBy);

                    string sponsorName = sponsorMember != null ? sponsorMember.username :
                                         sponsorVendor != null ? sponsorVendor.Username : null;

                    if (sponsorName != null)
                    {
                        decimal bonus = salary * 0.5m; // 50% bonus

                        if (sponsorMember != null)
                            sponsorMember.salaryWallet += bonus;
                        else
                            sponsorVendor.salaryWallet += bonus;

                        db.Payouts.InsertOnSubmit(new Payout
                        {
                            username = sponsorName,
                            credit = bonus,
                            netpayout = bonus,
                            debit = 0,
                            tax = 0,
                            desc = "Franchisee Referral Income",
                            remarks = string.Format("Last month stock sellings: {0}", lastMonthStock),
                            status = "Send",
                            date = date.Date,
                            confirmdate = date
                        });

                    }

                }
            }

            dist.isFirst = false;
            dist.isSecond = false;

        }

        db.SubmitChanges();
    }
    private void SalaryPayoutMembers(DateTime? date = null)
    {
        if (date == null)
            date = DateTime.UtcNow.AddHours(5.5).Date;
        var getpayout = from c in db.tblMembers
                        where c.isActive == 1 && c.salaryWallet > 0
                        select c;
        foreach (var g in getpayout)
        {
            decimal amt = g.salaryWallet.Value;
            var insert = new PaymentRequest
            {
                date = date,
                username = g.username,
                cashavailable = g.salaryWallet,
                amount = amt,
                tax = amt * 2 / 100,
                sc = amt * 8 / 100,
                deduction = 0,
                netpay = (amt * 90 / 100),
                status = "Pending",
                comments = "Salary Pay"
            };

            db.PaymentRequests.InsertOnSubmit(insert);
            g.salaryWallet = 0;
        }
        db.SubmitChanges();
    }
    private void SalaryPayoutFranchisee(DateTime? date = null)
    {
        if (date == null)
            date = DateTime.UtcNow.AddHours(5.5).Date;
        var getpayout = from c in db.tblVendors
                        where c.IsActive == true && c.salaryWallet > 0
                        select c;
        foreach (var g in getpayout)
        {
            var insert = new PaymentRequest
            {
                date = date,
                username = g.Username,
                cashavailable = g.salaryWallet,
                amount = g.salaryWallet,
                tax = g.salaryWallet * 2 / 100,
                sc = g.salaryWallet * 8 / 100,
                netpay = g.salaryWallet * 90 / 100,
                status = "Pending",
                comments = "Salary Pay"
            };
            db.PaymentRequests.InsertOnSubmit(insert);
            g.salaryWallet = 0;

        }
        db.SubmitChanges();
    }

}

