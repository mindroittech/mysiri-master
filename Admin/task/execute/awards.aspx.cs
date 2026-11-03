using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_task_execute_awards : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == "award")
        {
            BinaryAward();

            //ClubAwards();

            //NewClubAwards();

            RepurchaseRank();
        }
    }

    private void ClubAwards()
    {
        db.spAddRanks();
    }

    private void NewClubAwards()
    {
        db.spAddClubRanks();
    }

    //private void BinaryAward()
    //{

    //    decimal pv = 1000;
    //    var eligibleMembers = from c in db.tblMembers
    //                          where c.isActive == 1 && c.status == "Paid" && c.diamondlcarry / pv >= 40 && c.diamondrcarry / pv >= 40
    //                          select new { c.username, c.mobile, c.pan, c.account, leftBv = c.diamondlcarry / pv, rightBv = c.diamondrcarry / pv };
    //    foreach (var member in eligibleMembers)
    //    {
    //        if (member.leftBv >= 60 && member.rightBv >= 60)
    //        {
    //            addAward(member.username, 60);
    //        }
    //        if (member.leftBv >= 160 && member.rightBv >= 160)
    //        {
    //            addAward(member.username, 160);
    //        }
    //        if (member.leftBv >= 400 && member.rightBv >= 400)
    //        {
    //            addAward(member.username, 400);
    //        }
    //        if (member.leftBv >= 850 && member.rightBv >= 850)
    //        {
    //            addAward(member.username, 850);
    //        }
    //        if (member.leftBv >= 1800 && member.rightBv >= 1800)
    //        {
    //            addAward(member.username, 1800);
    //        }
    //        if (member.leftBv >= 4000 && member.rightBv >= 4000)
    //        {
    //            addAward(member.username, 4000);
    //        }
    //        if (member.leftBv >= 8000 && member.rightBv >= 8000)
    //        {
    //            addAward(member.username, 8000);
    //        }
    //        if (member.leftBv >= 16000 && member.rightBv >= 16000)
    //        {
    //            addAward(member.username, 16000);
    //        }
    //        if (member.leftBv >= 32000 && member.rightBv >= 32000)
    //        {
    //            addAward(member.username, 32000);
    //        }
    //        if (member.leftBv >= 65000 && member.rightBv >= 65000)
    //        {
    //            addAward(member.username, 65000);
    //        }
    //        if (member.leftBv >= 135000 && member.rightBv >= 135000)
    //        {
    //            addAward(member.username, 135000);
    //        }
    //        if (member.leftBv >= 270000 && member.rightBv >= 270000)
    //        {
    //            addAward(member.username, 270000);
    //        }
    //        if (member.leftBv >= 550000 && member.rightBv >= 550000)
    //        {
    //            addAward(member.username, 550000);
    //        }
    //    }
    //}

    private void RepurchaseRank()
    {
        var awards = db.tblRepurchaseRanks
                       .Where(x => x.isActive == true)
                       .OrderBy(x => x.RankId)
                       .ToList();

        var eligibleMembers = from c in db.tblMembers
                              where c.isActive == 1
                                 && c.status == "Paid"
                              let totalAmount = db.PaymentRequests
                                  .Where(r => r.username == c.username
                                           && r.status == "Paid")
                                  .Sum(r => (decimal?)r.amount) ?? 0
                              where totalAmount > 0
                              select new
                              {
                                  c.username,
                                  TotalAmount = totalAmount
                              };

        foreach (var member in eligibleMembers)
        {
            foreach (var award in awards)
            {
                if (member.TotalAmount >= award.BvRequired)
                {
                    addAwardReward(member.username, award.RankId);
                }
            }
        }
    }

    private void addAwardReward(string username, int rewardPoint)
    {
        var award = db.tblRepurchaseRanks
                      .FirstOrDefault(x => x.RankId == rewardPoint);

        if (award == null)
            return;

        bool alreadyExists = db.tblRewards.Any(
            x => x.username == username &&
                 x.awardId == award.RankId);

        if (alreadyExists)
            return;

        var m = db.tblMembers.FirstOrDefault(x => x.username == username);

        if (m == null)
            return;

        var insert = new tblReward
        {
            username = username,
            awardId = award.RankId,
            status = "Pending",
            achievedate = DateTime.UtcNow.AddHours(5.5).Date
        };

        m.rankid = m.rankid ?? award.RankId;

        db.tblRewards.InsertOnSubmit(insert);
        db.SubmitChanges();
    }

    private void BinaryAward()
    {
        var awards = db.MasterAwards
                       .OrderBy(x => x.RewardPoint)
                       .ToList();

        var eligibleMembers = from c in db.tblMembers
                              where c.isActive == 1
                                    && c.status == "Paid"
                              select new
                              {
                                  c.username,
                                  c.tlcount,
                                  c.trcount
                              };

        foreach (var member in eligibleMembers)
        {
            decimal leftMatch = Convert.ToDecimal(member.tlcount) / 10m;
            decimal rightMatch = Convert.ToDecimal(member.trcount) / 10m;

            decimal pvMatch = Math.Min(leftMatch, rightMatch);

            foreach (var award in awards)
            {
                if (pvMatch >= award.RewardPoint)
                {
                    addAward(member.username, award.RewardPoint);
                }
            }
        }
    }

    private void addAward(string username, int rewardPoint)
    {
        var award = db.MasterAwards
                      .FirstOrDefault(x => x.RewardPoint == rewardPoint);

        if (award == null)
            return;

        bool alreadyExists = db.tblAwards.Any(
            x => x.username == username &&
                 x.awardId == award.RewardId);

        if (alreadyExists)
            return;

        var insert = new tblAward
        {
            username = username,
            awardId = award.RewardId,
            status = "Pending",
            achievedate = DateTime.UtcNow.Date
        };

        db.tblAwards.InsertOnSubmit(insert);
        db.SubmitChanges();
    }

}