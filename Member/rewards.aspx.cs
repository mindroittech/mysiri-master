using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_rewards : System.Web.UI.Page
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
        var data = from c in db.AchievementBonusLists
                   join d in db.tblMembers
                   on c.Username equals d.username
                   where c.Username == username && c.Remarks!= "Level 1"
                   select new
                   {
                       c.Username,
                       c.Id,
                       d.name,
                       c.Date,
                       c.Match,
                       c.Remarks,
                       c.status,
                       c.Bonus,
                       d.mobile,
                       c.Award,
                       c.senddate
                   };
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
}