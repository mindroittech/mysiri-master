using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_club_awards : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();
            if (!IsPostBack)
                bind(uname);
        }
        catch (Exception ex)
        {
            Response.Redirect("../login.aspx");
        }
    }


    private void bind(string username = null)
    {
        var data = from c in db.tblRankAchieveds
                   join d in db.tblMembers
                   on c.username equals d.username
                   join f in db.tblRepurchaseRanks
                   on c.rankId equals f.RankId
                   where c.rewardStatus == null
                   && c.rankType == null &&
                    c.username == username
                   select new
                   {
                       c.rid,
                       c.username,
                       c.rankId,
                       d.name,
                       f.RankName,
                       c.dateAchieved,
                       f.Reward,
                       c.rewardDate,
                       c.rewardStatus
                   };
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
}