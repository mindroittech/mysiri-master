using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

public partial class Admin_task_execute_task : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void bvpayout(Decimal amt, Decimal deduction, string uname, string desc, DateTime date, string remarks, int totalMatch,
        decimal oldleft, decimal oldright, decimal newLeft, decimal newRight, decimal match, decimal paid, decimal flush)
    {

        var insert = new Payout();
        insert.username = uname;
        insert.date = date.Date;
        insert.confirmdate = date;
        insert.desc = desc;
        insert.tax = deduction;
        insert.credit = amt;
        insert.debit = 0;
        insert.netpayout = amt - deduction;
        insert.status = "Send";
        insert.incomefrom = uname;
        insert.remarks = remarks;
        insert.reward = totalMatch;
        insert.Left_Previous = oldleft;
        insert.Right_Previous = oldright;
        insert.Left_New = newLeft;
        insert.Right_New = newRight;
        insert.match = match;
        insert.paid = paid;
        insert.flush = flush;
        db.Payouts.InsertOnSubmit(insert);
       
    }

    protected void bvDebit(Decimal amt, string uname, string desc, DateTime date, string remarks)
    {
        var getCredit = db.tblMembers.SingleOrDefault(x => x.username == uname);
        var insert = new Payout();
        insert.username = uname;
        insert.date = date.Date;
        insert.desc = desc;
        insert.tax = 0;
        insert.credit = 0;
        insert.debit = amt;
        insert.status = "Send";
        insert.confirmdate = date;
        insert.incomefrom = uname;
        insert.reward = getCredit.credit;
        insert.remarks = remarks;
        db.Payouts.InsertOnSubmit(insert);
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //job is scheduled to run 1 times daily 12 AM
        if (Request.QueryString["id"] == "generate")
        {
            DateTime date = DateTime.UtcNow.AddHours(5.5);
            SilverPlanCutoff(date);
          
           
            Inactive(date);
          
            InactivePan(date);

            // GeneratePinCommission was used to execute on 1st of every month 
            // Changed on 10-10-2024 by arzoo removed date validation 
            //GeneratePinCommissionFranchisee(date);
        }
    }

   

    private void GeneratePinCommission(DateTime date)
    {

        var commision = from c in db.Payouts
                        where c.desc == "Pin Commission"
                        && c.status == "Pending"
                        select c;
        foreach (var c in commision)
        {
            var m = db.tblMembers.SingleOrDefault(x => x.username == c.username);
            if (m != null)
            {
                m.credit += c.credit;
                SendWalletSms(m.name, c.credit.Value, m.mobile);
            }
            else
            {
                var v = db.tblVendors.SingleOrDefault(x => x.Username == c.username);
                v.KitWallet += c.credit;
                SendWalletSms(v.VendorName, c.credit.Value, v.Phone);
            }

            c.status = "Send";
            c.confirmdate = date;

            db.SubmitChanges();


        }
    }
    private void GeneratePinCommissionFranchisee(DateTime date)
    {

        try
        {
            var commissions = from c in db.Payouts
                              where c.desc == "Pin Commission" && c.status == "Pending"
                              select c;

            foreach (var c in commissions)
            {
                var vendor = db.tblVendors.SingleOrDefault(x => x.Username == c.username);
                if (vendor != null && c.credit.HasValue)
                {
                    vendor.KitWallet += c.credit.Value;
                    SendWalletSms(vendor.VendorName, c.credit.Value, vendor.Phone);
                    c.status = "Send";
                    c.confirmdate = date;                  
                }
               
            }
            db.SubmitChanges();
        }
        catch (Exception ex)
        {
            var q = new tblQuery
            {
                Message = ex.Message,

            };
            db.tblQueries.InsertOnSubmit(q);
            db.SubmitChanges();
        }
    }

    private void Inactive(DateTime date)
    {
        var old = from c in db.tblMembers
                  where c.status == "Free" && c.joindate.Value.AddDays(30).Date < date.Date
                  select c;
        old.ToList().ForEach(x => { x.status = "Inactive"; });
        db.SubmitChanges();

    }

    private void InactivePan(DateTime date)
    {
        DateTime cutoff = date.AddHours(-48);

        var old = db.tblMembers.Where(x =>
            x.status == "Free" &&
            x.joindate != null &&
            x.joindate < cutoff);

        foreach (var member in old)
        {
            member.pan = null;
        }

        db.SubmitChanges();
    }

    private void SilverPlanCutoff(DateTime date)
    {
        Decimal amt = 0;
        var getall = from c in db.tblMembers
                     join d in db.tblPlans on c.plan equals d.PlanName
                     where( c.lcarry > 0 && c.rcarry > 0
                     && c.isActive == 1 && c.status == "Paid" )
                     select c;
        foreach (var g in getall)
        {
            amt = 0;
            Decimal left = (decimal)g.lcarry;
            Decimal right = (decimal)g.rcarry;

            decimal oldLeft = (decimal)g.goldlcarry;
            decimal oldRight = (decimal)g.goldrcarry;
            decimal newLeft = (decimal)g.lcarry - oldLeft;
            decimal newRight = (decimal)g.rcarry - oldRight;

            if (left < right)
            {
                amt = (decimal)g.lcarry;
                g.rcarry = g.rcarry - g.lcarry;
                g.lcarry = 0;
                g.goldrcarry = g.rcarry;
                g.goldlcarry = 0;
            }
            else if (left > right)
            {
                amt = (decimal)g.rcarry;
                g.lcarry = g.lcarry - g.rcarry;
                g.rcarry = 0;
                g.goldlcarry = g.lcarry;
                g.goldrcarry = 0;
            }
            else
            {
                amt = (decimal)g.lcarry;
                g.rcarry = 0;
                g.lcarry = 0;
                g.goldrcarry = 0;
                g.goldlcarry = 0;
            }

            var findPlan = db.tblPlans.Where(x => x.PlanName == g.plan).SingleOrDefault();
            int totalMatch = (int)(amt / findPlan.BVAmount);
            Decimal actual = amt;
            decimal dailyCap = (decimal)findPlan.DailyCap;

            //var binary = db.Payouts.Where(x => x.username == g.username && x.desc == "Matching Bonus");
            //if (binary.Any() && binary.Sum(x => x.credit) >= 600000)
            //{
            //    dailyCap = 20000;
            //}

            if (amt > dailyCap)
            {
                amt = (decimal)dailyCap;
            }

            decimal match = actual;
            decimal paid = amt;
            decimal flush = match - paid;

            g.credit = g.credit + amt;
            bvpayout(amt, 0, g.username, "Matching Bonus", date, "Pair Match Commission", totalMatch,
             oldLeft, oldRight, newLeft, newRight, match, paid, flush);


            // sponsor royalty

            // Pass the direct sponsor's refid
            int currentRefId = g.refid.Value;

            decimal matchingIncome = amt; // Actual matching income earned
            decimal[] royaltyPercentages = { 5m, 10m, 15m, 20m };

            for (int level = 1; level <= 4; level++)
            {
                var sponsor = db.tblMembers.SingleOrDefault(
                    x => x.id == currentRefId
                );

                if (sponsor == null)
                    break;

                if (sponsor.status == "Paid")
                {
                    decimal royalty = matchingIncome *
                        royaltyPercentages[level - 1] / 100m;

                    if (royalty > 0)
                    {
                        sponsor.credit += royalty;

                        payout(
                            royalty,
                            sponsor.username,
                            g.username,
                            "Sponsor Royalty Income",
                            "Level " + level 
                        );
                    }
                }

                // Move to the next upline
                currentRefId = sponsor.refid.Value;
            }

            // SendWalletSms(g.name, amt, g.mobile);
        }
        db.SubmitChanges();
    }

    protected void payout(Decimal amt, string uname, string incomefrom, string desc, string remarks)
    {

        var insert = new Payout();
        insert.username = uname;
        insert.date = DateTime.UtcNow.Date;
        insert.confirmdate = DateTime.UtcNow.AddHours(5.5);
        insert.desc = desc;
        insert.tax = 0;
        insert.netpayout = amt;
        insert.credit = amt;
        insert.debit = 0;
        insert.status = "Send";
        insert.incomefrom = incomefrom;
        insert.remarks = remarks;
        db.Payouts.InsertOnSubmit(insert);
      
    }

    private void SendWalletSms(string name, Decimal amt, string mob)
    {
        string msg = "Dear " + name + ", Congratulations! You have earned Rs " + amt + ". DLS Biotech";
        sm.sms(mob, msg, "1407169591262331056");
    }
}