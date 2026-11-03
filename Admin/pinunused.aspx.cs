using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Admin_pinunused : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            if (!IsPostBack)
            {

                bind();

            }
        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            PinManagaer add = db.PinManagaers.Where(x => x.id == id).SingleOrDefault();
            db.PinManagaers.DeleteOnSubmit(add);
            db.SubmitChanges();

            Response.Redirect("pinunused.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text == string.Empty)
        {
            GridView1.AllowPaging = true;
            bind();
        }
        else
        {
            GridView1.AllowPaging = false;
            bind(TextBox1.Text);
        }
    }

    private void bind(string uname = null)
    {
        var get = from c in db.PinManagaers
                  join d in db.tblMembers
                  on c.issued equals d.username
                  select new
                  {
                      c.id,
                      c.pinnumbers,
                      c.package,
                      c.issued,
                      d.name,
                      c.date,
                      c.status,
                      c.usedby,
                      c.useDate,
                      isfree = c.IsFreePin == true ? "Yes" : "No"
                  };

        var franch = from c in db.PinManagaers
                     join d in db.tblVendors
                     on c.issued equals d.Username
                     select new
                     {
                         c.id,
                         c.pinnumbers,
                         c.package,
                         c.issued,
                         name = d.VendorName,
                         c.date,
                         c.status,
                         c.usedby,
                         c.useDate,
                         isfree = c.IsFreePin == true ? "Yes" : "No"
                     };

        var res = get.Union(franch).Where(x => x.status == "Not used");

        if (uname != null)
        {
            res = res.Where(x => x.issued == uname);
        }
        GridView1.DataSource = res.OrderByDescending(x => x.id);
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("pinunused.aspx");
    }
}