using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RankAchievers : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            bindRank();

        }

        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    private void bindRank(DateTime? d1 = null, DateTime? d2 = null, string uname = null)
    {
        var rankAchieved = from c in db.tblRankAchieveds
                           join d in db.tblRepurchaseRanks
                           on c.rankId equals d.RankId
                           join f in db.tblMembers
                           on c.username equals f.username
                           select new
                           {
                               c.username,
                               d.RankName,
                               c.dateAchieved,
                               f.name,
                               c.termLeft,
                               d.MatchingIncome,
                               d.MaintenanceBv,
                               f.mobile,
                               f.state
                           };
        if (d1 != null && d2 != null)
        {
            rankAchieved = rankAchieved.Where(x => x.dateAchieved >= d1 && x.dateAchieved <= d2);
        }
        if (uname != null)
            rankAchieved = rankAchieved.Where(x => x.username == uname);
        GridView1.DataSource = rankAchieved;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string uname = null;

            DateTime? d1 = null, d2 = null;
            if (TextBox2.Text != string.Empty && TextBox3.Text != string.Empty)
            {
                d1 = Convert.ToDateTime(TextBox2.Text);
                d2 = Convert.ToDateTime(TextBox3.Text);
            }
            if (TextBox1.Text != string.Empty)
                uname = TextBox1.Text;

            bindRank(d1, d2, uname);
        }
        catch
        {

        }

    }
}