using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_deactivated : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms send = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        Label2.Visible = false;
        try
        {
            string admin = Session["admin"].ToString();

            if (Request.QueryString["v"] == "sub")
            {
                Label1.Text = "ID is successfully activated";
                Label1.Visible = true;
            }
        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string uname = e.CommandArgument.ToString();

            if (e.CommandName == "genealogy")
            {
                Response.Redirect("genealogy.aspx?uname=" + uname);
            }
            if (e.CommandName == "profile")
            {
                Response.Redirect("mprofile.aspx?uname=" + uname);
            }
            if (e.CommandName == "downline")
            {
                Response.Redirect("downline.aspx?uname=" + uname);
            }


        }
        catch
        { }
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text == string.Empty)
        {
            Label2.Text = "Enter Username or Name or Mobile";
            Label2.Visible = true;
            return;
        }
        var get = from c in db.tblMembers
                  where (c.username == TextBox3.Text || c.name.Contains(TextBox3.Text) || c.mobile == TextBox3.Text) &&
                  (c.status == "Inactive")
                  select new { c.username, c.name, c.joindate, c.mobile, c.status, c.amount, c.plan, kyc = c.isPanVerifed == true ? "Yes" : "No" };
        GridView1.DataSourceID = null;
        GridView1.AllowPaging = false;
        GridView1.DataSource = get;
        GridView1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chk = row.FindControl("CheckBox1") as CheckBox;

            if (chk.Checked)
            {
                string uname = row.Cells[1].Text;

                var up = (from c in db.tblMembers
                          where c.username == uname
                          select c).SingleOrDefault();
                up.isActive = 1;
                if (up.status == "Inactive")
                    up.status = "Free";
                db.SubmitChanges();
            }

        }

        Response.Redirect("deactivated.aspx?v=sub");
    }
}