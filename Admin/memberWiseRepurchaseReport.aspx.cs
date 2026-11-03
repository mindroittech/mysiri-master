using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_memberWiseRepurchaseReport : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    private static bool isclicked = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();
            if (role != "GoDown" && role != "Admin")
            {
                throw new Exception("Access denied");
            }
            if (!IsPostBack)
            {
                //bind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            DateTime? d1 = null, d2 = null;
            if (TextBox2.Text != string.Empty)
            {
                d1 = Convert.ToDateTime(TextBox2.Text);
            }
            if (TextBox3.Text != string.Empty)
            {
                d2 = Convert.ToDateTime(TextBox3.Text);
            }
            bind(TextBox1.Text, d1, d2);
            bindRank(TextBox1.Text);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    private void bind(string uname, DateTime? startDate = null, DateTime? endDate = null)
    {
        lblSelfBv.Text = "0";
        lblTotalLeftBv.Text = "0";
        lblTotalRightBv.Text = "0";

        var totalBVs = from c in db.tblBVs
                       join d in db.tblMembers
                       on c.username equals d.username
                       where c.username == uname
                       && d.status == "Paid"
                       select c;
        var self = totalBVs.Where(x => x.desc == "Self");
        var totalleft = totalBVs.Where(x => x.desc == "Left");
        var totalright = totalBVs.Where(x => x.desc == "Right");
        if (startDate != null && endDate != null)
        {
            totalleft = totalleft.Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate);
            self = self.Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate);
            totalright = totalright.Where(x => x.date.Value.Date >= startDate && x.date.Value.Date <= endDate);
        }
        if (self.Any())
            lblSelfBv.Text = self.Sum(x => x.Bv).ToString();
        if (totalleft.Any())
            lblTotalLeftBv.Text = totalleft.Sum(x => x.leftBv).ToString();
        if (totalright.Any())
            lblTotalRightBv.Text = totalright.Sum(x => x.rightBv).ToString();
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