using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class dashboard : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();

            var get = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
            var plan = db.tblPlans.SingleOrDefault(x => x.PlanName == get.plan);
            lblPlan.Text = get.plan;
            if (plan != null)
            {
                lblDailyCap.Text = plan.DailyCap.ToString();
            }

            lblDebit.Text = get.debit.Value.ToString();

            var todaypayout = db.Payouts.Where(x => x.username == uname && x.date == DateTime.UtcNow.AddHours(5.5).Date && x.credit != 0).Sum(x => x.credit);
            var totalPayout = db.Payouts.Where(x => x.username == uname && x.status == "Send").Sum(x => x.credit);

            lblWallet.Text = totalPayout.HasValue ? totalPayout.Value.ToString() : "0";
            decimal todays = todaypayout.HasValue ? todaypayout.Value : 0;
            lblTodays.Text = todays.ToString();

            int lcount = get.silverLeft.Value;
            int rcount = get.silverRight.Value;

            lblBinaryLeft.Text = get.lcarry.ToString();
            lblBinaryRight.Text = get.rcarry.ToString();

            //lblbonanza.Text = get.RepurchaseWallet.ToString();

            lblLeft.Text = lcount.ToString();
            lblRIght.Text = rcount.ToString();
            lblSelfPurchase.Text = get.selfPurchaseWallet.ToString();
            //lblDirect.Text = db.tblMembers.Where(x => x.sid == uname).Count().ToString();

            var payout = db.Payouts.Where(x => x.username == uname && x.credit != 0);

            var silver = payout.Where(x => x.remarks == "Pair Match Commission").Sum(x => x.credit);
           // var pincommission = payout.Where(x => x.desc == "Pin Commission").Sum(x => x.credit);
            var repIncome = payout.Where(x => x.desc == "Repurchase Income").Sum(x => x.credit);
            // var performanceBonus = payout.Where(x => x.desc == "Performance Bonus").Sum(x => x.credit);
            // var achievementBonus = payout.Where(x => x.desc == "Achievement Bonus").Sum(x => x.credit);
            // var salary = payout.Where(x => x.desc == "Salary Income").Sum(x => x.credit);
            // var mfa = payout.Where(x => x.desc == "Monthly Field Allowance").Sum(x => x.credit);

            var sponsor = payout.Where(x => x.desc == "Sponsor Income").Sum(x => x.credit);
            if (sponsor.HasValue)
                lblsponsorIncome.Text = sponsor.Value.ToString();

            var booster = payout.Where(x => x.desc == "Booster Income").Sum(x => x.credit);
            if (booster.HasValue)
                lblbooster.Text = booster.Value.ToString();

            var royalty = payout.Where(x => x.desc == "Sponsor Royalty Income").Sum(x => x.credit);
            if (royalty.HasValue)
                lblsponsorroyalty.Text = royalty.Value.ToString();

            var bonus = payout.Where(x => x.desc == "Bonus Income").Sum(x => x.credit);
            if (bonus.HasValue)
                lblbonus.Text = bonus.Value.ToString();


            if (silver.HasValue)
                lblSilver.Text = silver.Value.ToString();
            if (repIncome.HasValue)
                lblRepIncome.Text = repIncome.ToString();

            //if (pincommission.HasValue)
            //    lblPinCommission.Text = pincommission.ToString();
            //if (performanceBonus.HasValue)
            //    lblReferral.Text = performanceBonus.ToString();
            //  if (achievementBonus.HasValue)
            //    lblachievebonus.Text = achievementBonus.ToString();
            //  if (salary.HasValue)
            //    lblsalary.Text = salary.ToString();
            //  if (mfa.HasValue)
            //    lblmfa.Text = mfa.ToString();

            lblDownline.Text = (lcount + rcount).ToString();
            lblEWallet.Text = get.credit.Value.ToString();

            DateTime today = DateTime.UtcNow.AddHours(5.5).Date;
            DateTime startdate = new DateTime(today.Year, today.Month, 1);
            var totalBVs = db.tblBVs.Where(x => x.username == uname && x.bvType == "Repurchase");

            var totalleft = totalBVs.Where(x => x.desc == "Left");
            var totalright = totalBVs.Where(x => x.desc == "Right");
            var totalSelf = totalBVs.Where(x => x.desc == "Self");

            lblTotalSelf.Text = get.cumbv_self.ToString();
            lblTotalLeftBv.Text = get.cumbv_left.ToString();
            lblTotalRightBv.Text = get.cumbv_right.ToString();

            var todayLeft = totalleft.Where(x => x.date == today);
            var todayRight = totalright.Where(x => x.date == today);
            if (todayLeft.Any())
                lblTodayRepLeft.Text = todayLeft.Sum(x => x.leftBv).ToString();
            if (todayRight.Any())
                lblTodayRepRight.Text = todayRight.Sum(x => x.rightBv).ToString();

            var todayBinaryBVs = db.tblBVs.Where(x => x.username == uname && x.bvType == "Binary" && x.date == today);
            var todayLeftBinary = todayBinaryBVs.Where(x => x.desc == "Left");
            var todayRightBinary = todayBinaryBVs.Where(x => x.desc == "Right");
            if (todayLeftBinary.Any())
                lblTodayBinaryLeft.Text = todayLeftBinary.Sum(x => x.lcarry).ToString();
            if (todayRightBinary.Any())
                lblTodayBinaryRight.Text = todayRightBinary.Sum(x => x.rcarry).ToString();

            var bvs = db.tblBVs.Where(x => x.username == uname && x.status == "Pending" && x.bvType == "Repurchase");
            var self = bvs.Where(x => x.desc == "Self");
            var left = bvs.Where(x => x.desc == "Left");
            var right = bvs.Where(x => x.desc == "Right");
            decimal currentSelfBv = 0;
            decimal currentLeftBv = 0;
            decimal currentRightBv = 0;
            if (self.Any())
            {
                currentSelfBv = self.Sum(x => x.Bv.Value);

            }
            if (left.Any())
            {
                currentLeftBv = left.Sum(x => x.leftBv.Value);
                lblLeftBv.Text = currentLeftBv.ToString();
            }
            if (right.Any())
            {
                currentRightBv = right.Sum(x => x.rightBv.Value);
                lblRightBv.Text = currentRightBv.ToString();
            }
            if (get.rankid != null)
            {
                var rank = db.tblRepurchaseRanks.SingleOrDefault(x => x.RankId == get.rankid);
                lblRank.Text = rank.RankName; 
            }

            //lcachievdpv.Text = get.alcarry.ToString();
            //Rcachievdpv.Text = get.arcarry.ToString();

            //ltachievedpv.Text = get.alcount.ToString();
            //Rtachievedpv.Text = get.arcount.ToString();

            var news = from c in db.News
                       where c.status == "Active"
                       select new { c.title, c.desc, c.createDate };
            if (news.Any())
            {
                Repeater1.DataSource = news;
                Repeater1.DataBind();
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("../login.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.ContentType = "Application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=BusinessPlan.pdf");
        Response.TransmitFile(Server.MapPath("~/Member/RMS.pdf"));
        Response.End();

    }
}