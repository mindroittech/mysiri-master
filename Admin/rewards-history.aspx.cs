using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_awards_history : System.Web.UI.Page
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

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("awards-history.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bind(TextBox1.Text);
    }
    private void bind(string username = null)
    {
        var data = from c in db.tblRewards
                   join d in db.tblMembers
                   on c.username equals d.username
                   join f in db.tblRepurchaseRanks
                   on c.awardId equals f.RankId
                   where c.status == "Completed"
                   select new
                   {
                       c.username,
                       c.Id,
                       d.name,
                       f.RankName,
                       c.achievedate,
                       f.BvRequired,
                       f.Reward,
                       c.comments,
                       c.sentDate,
                       d.mobile,
                       d.state
                   };

        if (username != null)
        {
            data = data.Where(x => x.username == username);
        }
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
}