using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ClubAwards : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["admin"].ToString();
            if (!IsPostBack)
                bind();
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            bind(TextBox1.Text);
        }
        catch
        {

        }

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClubAwards.aspx");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "send")
        {
            int id = Convert.ToInt32(e.CommandArgument);


            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

            var find = db.tblRankAchieveds.SingleOrDefault(x => x.rid == id);

            find.rewardStatus = "Completed";
            find.rewardDate = DateTime.UtcNow.AddHours(5.5);
            db.SubmitChanges();

            Response.Redirect("ClubAwards.aspx");
        }

    }
    private void bind(string uname = null)
    {
        var data = from c in db.tblRankAchieveds
                   join d in db.tblMembers
                   on c.username equals d.username
                   join f in db.tblRepurchaseRanks
                   on c.rankId equals f.RankId
                   where c.rewardStatus == null
                   && c.rankType == null
                   select new
                   {
                       c.rid,
                       c.username,
                       c.rankId,
                       d.name,
                       f.RankName,
                       c.dateAchieved,
                       f.Reward,
                       d.mobile,
                       d.state
                   };
        if (uname != null)
            data = data.Where(x => x.username == uname);
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
}