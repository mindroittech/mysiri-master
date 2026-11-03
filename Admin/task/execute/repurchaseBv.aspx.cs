using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_task_execute_repurchaseBv : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime date = DateTime.UtcNow.AddHours(5.5);
        if (Request.QueryString["id"] == "monthly")
        {
            CalculateRepurchaseBv(date);

            CalculateRepurchasePayout(date);

            //var admin = db.Admins.First();
            //if (admin.selfPurchaseEnabled == true)
            //{
            //    CalculateSelfPurchaseWallet(date);
            //}

          
        }
    }
 
    private void CalculateRepurchaseBv(DateTime date)
    {
        var findBv = from c in db.tblBVs
                     join d in db.tblMembers
                     on c.username equals d.username
                     where c.status == "Pending" && c.bvType == "Repurchase"
                     && d.status == "Paid"
                     group c by c.username into grp
                     select new
                     {
                         username = grp.Key,
                         leftBv = grp.Sum(x => x.leftBv),
                         rightBv = grp.Sum(x => x.rightBv),
                         selfBv = grp.Sum(x => x.Bv)
                     };
        foreach (var f in findBv)
        {
            var member = db.tblMembers.SingleOrDefault(x => x.username == f.username);
            member.selfBv += f.selfBv;

            decimal oldLeft = member.leftBv.Value;
            decimal oldRight = member.rightBv.Value;
            decimal newleft = 0, newright = 0;

            newleft = f.leftBv.Value;
            newright = f.rightBv.Value;
            member.leftBv += f.leftBv;
            member.rightBv += f.rightBv;
            member.leftTotalBv += f.leftBv;
            member.rightTotalBv += f.rightBv;

            db.SubmitChanges();

            BVReport(member.username, date, oldLeft, oldRight, f.selfBv.Value, newleft, newright, 0);

            var update = from c in db.tblBVs
                         join d in db.tblMembers
                         on c.username equals d.username
                         where c.status == "Pending" && c.username == f.username
                         && d.status == "Paid"
                         select c;
            update.ToList().ForEach(x => x.status = "Send");
            db.SubmitChanges();
        }

    }
    private void BVReport(string uname, DateTime lastDate, decimal oldLeft, decimal oldRight, decimal selfBv, decimal newLeft, decimal newRight, decimal maintenance)
    {
        var bv = new BVReport
        {
            username = uname,
            date = lastDate,
            oldLeft = oldLeft,
            oldRight = oldRight,
            selfBv = selfBv,
            newLeft = newLeft,
            newRight = newRight,
            maintenanceBv = maintenance
        };
        db.BVReports.InsertOnSubmit(bv);
        db.SubmitChanges();
    }
    private void CalculateRepurchasePayout(DateTime date)
    {
         // open this stored procedure for logic
        // db.spAddRanks();

        Decimal Bv = 0;
        var repurchases = from c in db.tblMembers
                          where c.leftBv > 0 && c.rightBv > 0
                          && c.isActive == 1 && c.status == "Paid"
                          select c;
        foreach (var g in repurchases)
        {
            Bv = 0;
            Decimal left = (decimal)g.leftBv;
            Decimal right = (decimal)g.rightBv;

            if (left < right)
            {
                Bv = (decimal)g.leftBv;
                g.rightBv = g.rightBv - g.leftBv;
                g.leftBv = 0;
            }
            else if (left > right)
            {
                Bv = (decimal)g.rightBv;
                g.leftBv = g.leftBv - g.rightBv;
                g.rightBv = 0;
            }
            else
            {
                Bv = (decimal)g.leftBv;
                g.leftBv = 0;
                g.rightBv = 0;
            }

          
            decimal amt = Bv * 20m / 100;

            g.RepIncome += amt;
            Payout(amt, g.username, "Repurchase Income", date, "Repurchase Incentive", "Send");

            // Bonus Income based on matched BV
            decimal bonusIncome = 0;

            bonusIncome += Math.Floor(Bv / 10000m) * 500m;
            bonusIncome += Math.Floor(Bv / 25000m) * 1000m;
            bonusIncome += Math.Floor(Bv / 50000m) * 1500m;
            bonusIncome += Math.Floor(Bv / 100000m) * 2500m;

            string bonusRemark = (int)(Bv / 10000m) + " x 500 + "
                   + (int)(Bv / 25000m) + " x 1000 + "
                   + (int)(Bv / 50000m) + " x 1500 + "
                   + (int)(Bv / 100000m) + " x 2500";

            // Bonus Income payout
            if (bonusIncome > 0)
            {
                g.credit += bonusIncome;

                Payout(
                    bonusIncome,
                    g.username,
                    "Bonus Income",
                    date,
                    bonusRemark,
                    "Send"
                );
            }

        }

    }

    private bool IsEligible(tblMember member, DateTime payoutDate)
    {
        // First two ranks always eligible
        if (member.rankid==null ||member.rankid == 1 || member.rankid == 2)
            return true;

        var rank = db.tblRepurchaseRanks.SingleOrDefault(x => x.RankId == member.rankid);

        if (rank == null || rank.rep_Slab == null)
            return false;

        decimal slab = rank.rep_Slab.Value;

        //======================
        // 6th Payout
        //======================

        if (payoutDate.Day == 6)
        {
            // Carry Forward Available
            if (member.RepCarryCycle > 0 &&
                member.RepCarryDate.HasValue &&
                member.RepCarryDate.Value.Date == payoutDate.Date)
            {
                member.RepCarryCycle = 0;
                member.RepCarryDate = null;

                db.SubmitChanges();

                return true;
            }

            DateTime fromDate = new DateTime(payoutDate.AddMonths(-1).Year,
                                             payoutDate.AddMonths(-1).Month,
                                             21);

            DateTime toDate = new DateTime(payoutDate.Year,
                                           payoutDate.Month,
                                           6);

            decimal selfBV = db.tblInvoices
                .Where(x => x.username == member.username
                         && x.Name == "Repurchase"
                         && x.PurchaseDate.Value.Date >= fromDate
                         && x.PurchaseDate.Value.Date < toDate)
                .Sum(x => (decimal?)x.BV) ?? 0;

            return selfBV >= slab;
        }

        //======================
        //21st Payout
        //======================
        else if (payoutDate.Day == 21)
        {
            DateTime fromDate = new DateTime(payoutDate.Year,
                                             payoutDate.Month,
                                             6);

            DateTime toDate = new DateTime(payoutDate.Year,
                                           payoutDate.Month,
                                           21);

            decimal selfBV = db.tblInvoices
                .Where(x => x.username == member.username
                         && x.Name == "Repurchase"
                         && x.PurchaseDate.Value.Date >= fromDate
                         && x.PurchaseDate.Value.Date < toDate)
                .Sum(x => (decimal?)x.BV) ?? 0;

            if (selfBV >= slab * 2)
            {
                member.RepCarryCycle = 1;
                member.RepCarryDate = new DateTime(
                    payoutDate.AddMonths(1).Year,
                    payoutDate.AddMonths(1).Month,
                    6);

                db.SubmitChanges();
            }

            return selfBV >= slab;
        }

        return false;
    }
    protected void Payout(Decimal amt, string uname, string desc, DateTime date, string remarks, string status, int reward = 0)
    {

        var insert = new Payout();
        insert.username = uname;
        insert.date = date.Date;
        insert.confirmdate = date;
        insert.desc = desc;
        insert.tax = 0;
        insert.credit = amt;
        insert.netpayout = amt;
        insert.debit = 0;
        insert.status = status;
        insert.remarks = remarks;
        insert.reward = reward;
        db.Payouts.InsertOnSubmit(insert);
        db.SubmitChanges();
    }

}


//private void CalculateSelfPurchaseWallet(DateTime date)
//{
//    DateTime lastDate = date.Date;
//    DateTime firstDate = new DateTime();
//    if (lastDate.Day == 6)
//    {
//        firstDate = new DateTime(lastDate.Year, lastDate.AddMonths(-1).Month, 21);  //21 st to 5th
//    }
//    else if (lastDate.Day == 21)
//        firstDate = new DateTime(lastDate.Year, lastDate.Month, 6); //6th to 20st

//    var invoices = from c in db.tblInvoices
//                   join d in db.tblMembers
//                   on c.username equals d.username
//                   where c.PurchaseDate.Value.Date >= firstDate && c.PurchaseDate.Value.Date < lastDate
//                   && c.invType == "Paid" && d.status == "Paid"
//                   group c by c.username into grp
//                   select new { uname = grp.Key, amount = grp.Sum(x => x.Amount) };
//    decimal selfwallet = 0;
//    foreach (var inv in invoices)
//    {
//        selfwallet = 0;
//        if (inv.amount >= 3000 && inv.amount < 6000)
//            selfwallet = 800;
//        else if (inv.amount >= 6000 && inv.amount < 10000)
//            selfwallet = 2000;
//        else if (inv.amount >= 10000)
//            selfwallet = 3200;
//        if (selfwallet > 0)
//        {
//            var mem = db.tblMembers.SingleOrDefault(x => x.username == inv.uname);
//            mem.selfPurchaseWallet += selfwallet;
//            Payout(selfwallet, inv.uname, "Self Purchase Wallet", date, "Repurchase Worth Rs " + inv.amount);

//        }
//    }
//}