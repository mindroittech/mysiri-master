using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class admin_view_members : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms send = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataSource1.Selecting += (s, ev) =>
        {
            ev.Command.CommandTimeout = 120; // Timeout set to 120 seconds
        };
        Label2.Visible = false;
        try
        {
            string admin = Session["admin"].ToString();

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
    protected void Button1_Click(object sender, EventArgs e)
    {
        Export();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void Export()
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Members.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        }
        GridView1.RenderControl(htw);

        GridView1.AllowPaging = true;
        GridView1.DataBind();
        Response.Write(sw.ToString());
        Response.End();
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
                up.isActive = 0;
                db.SubmitChanges();
            }

        }

        Response.Redirect("view-members.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (TextBox1.Text == string.Empty || TextBox2.Text == string.Empty)
            {
                Label2.Text = "Select Date";
                Label2.Visible = true;
                return;
            }
            var get = from c in db.tblMembers
                      where c.joindate.Value.Date >= Convert.ToDateTime(TextBox1.Text) && c.joindate.Value.Date <= Convert.ToDateTime(TextBox2.Text) && c.isActive == 1
                      orderby c.id descending
                      select new { c.username, c.name, c.renewal, c.joindate, c.status, c.mobile, c.amount, c.plan, kyc = c.isPanVerifed == true ? "Yes" : "No" };
            GridView1.DataSourceID = null;
            GridView1.AllowPaging = false;
            GridView1.DataSource = get;
            GridView1.DataBind();
        }
        catch
        {
            Label2.Text = "Select Date";
            Label2.Visible = true;

        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("view-members.aspx");
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
                  where (c.username == TextBox3.Text || c.name.Contains(TextBox3.Text) || c.mobile == TextBox3.Text || c.pan == TextBox3.Text) && c.isActive == 1
                  orderby c.id descending
                  select new { c.username, c.name, c.status, c.renewal, c.joindate, c.mobile, c.amount, c.plan, kyc = c.isPanVerifed == true ? "Yes" : "No" };
        GridView1.DataSourceID = null;
        GridView1.AllowPaging = false;
        GridView1.DataSource = get;
        GridView1.DataBind();
    }
    protected void btnSms_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chk = row.FindControl("CheckBox1") as CheckBox;

            if (chk.Checked)
            {
                string uname = row.Cells[1].Text.Trim();

                var insert = (from c in db.tblMembers
                              where c.username == uname
                              select c).SingleOrDefault();

                string msg = "Dear " + insert.name + ", Congratulations! Welcome to  DLS Biotech. Your Login ID:" + insert.username + " Password:" + insert.password + " Login at: https://www.dlsbiotech.com All the Best!";
                send.sms(insert.mobile, msg, "1407172845276587925");

            }

        }

        Response.Redirect("view-members.aspx");
    }
}