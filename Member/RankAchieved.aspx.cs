using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_RankAchieved : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();
            if (!IsPostBack)
            {
                bind(uname);
                bindRank(uname);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../login.aspx");
        }
    }

    private void bind(string uname, DateTime? startDate = null, DateTime? endDate = null)
    {
        lblSelfBv.Text = "0";
        lblTotalLeftBv.Text = "0";
        lblTotalRightBv.Text = "0";

        decimal selfBVRequired = 0;
        var member = db.tblMembers.SingleOrDefault(x => x.username == uname);
        var rank = db.tblRepurchaseRanks.SingleOrDefault(x => x.RankId == member.rankid);

        if (rank != null)
            selfBVRequired = rank.SelfBv.Value;

        lblSelfBv.Text = (member.selfBv.Value - selfBVRequired).ToString();

        lblTotalLeftBv.Text = member.leftBv.ToString();

        lblTotalRightBv.Text = member.rightBv.ToString();
    }

    private void bindRank(string uname)
    {
        var rankAchieved = from c in db.tblRankAchieveds
                           join d in db.tblRepurchaseRanks
                           on c.rankId equals d.RankId
                           where c.username == uname
                           select new { c.username, d.RankName, c.dateAchieved, c.termLeft, d.MatchingIncome, d.MaintenanceBv };
        GridView1.DataSource = rankAchieved;
        GridView1.DataBind();
    }
}