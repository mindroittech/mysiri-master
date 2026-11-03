using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_news : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");

        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        var n = new New();
        //n.title = TextBox1.Text;
        n.desc = TextBox2.Text;
        n.status = DropDownList1.SelectedValue;
        n.createDate = DateTime.UtcNow.AddHours(5.5).Date;
        db.News.InsertOnSubmit(n);
        db.SubmitChanges();

        Response.Redirect("news.aspx");

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        var q = (from c in db.News
                 where c.nid == Convert.ToInt32(Session["id"])
                 select c).SingleOrDefault();
        // q.title = TextBox1.Text;
        q.desc = TextBox2.Text;
        q.status = DropDownList1.SelectedValue;

        db.SubmitChanges();
        Response.Redirect("news.aspx");

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ed")
        {

            int id = Convert.ToInt32(e.CommandArgument);
            var q = (from c in db.News
                     where c.nid == id
                     select c).SingleOrDefault();
            //TextBox1.Text = q.title;
            TextBox2.Text = q.desc;
            DropDownList1.SelectedValue = q.status;

            Session["id"] = q.nid;

            Button1.Visible = false;
            Button2.Visible = true;

        }

        if (e.CommandName == "delete")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            var q = (from c in db.News
                     where c.nid == id
                     select c).SingleOrDefault();
            if (q.status == "Active")
            {
                q.status = "Inactive";
                q.toDate = DateTime.UtcNow.AddHours(5.5).Date;
            }
            else
            {
                q.status = "Active";
                // q.toDate = DateTime.UtcNow.AddHours(5.5).Date;
            }
            db.SubmitChanges();

            Response.Redirect("news.aspx");
        }
    }
}