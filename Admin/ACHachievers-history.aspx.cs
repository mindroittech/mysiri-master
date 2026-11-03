using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
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
        Response.Redirect("ACHachievers-history.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bind(TextBox1.Text);
    }
    private void bind(string username = null)
    {
        var data = from c in db.AchievementBonusLists
                   join d in db.tblMembers
                   on c.Username equals d.username
                   where c.status == "Completed"
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
                       c.comments,
                       c.senddate

                   };
        if (username != null)
            data = data.Where(x => x.Username == username);

        GridView1.DataSource = data;
        GridView1.DataBind();
    }
}