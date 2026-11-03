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
        Response.Redirect("ACHachievers.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "send")
        {
            int id = Convert.ToInt32(e.CommandArgument);


            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

            TextBox txtcomment = row.FindControl("txtComments") as TextBox;

            var find = db.AchievementBonusLists.SingleOrDefault(x => x.Id == id);

            find.status = "Completed";
            find.senddate = DateTime.UtcNow.AddHours(5.5);
            find.comments = txtcomment.Text;
            db.SubmitChanges();

            Response.Redirect("ACHachievers.aspx");
        }

    }
    private void bind(string uname = null)
    {
        var data = from c in db.AchievementBonusLists
                   join d in db.tblMembers
                   on c.Username equals d.username                  
                   where c.status == "Pending"
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
                       c.Award
                      
                   };
        if (uname != null)
            data = data.Where(x => x.Username == uname);

        GridView1.DataSource = data;
        GridView1.DataBind();
    }
}