using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PinTransferLog : System.Web.UI.Page
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

        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != string.Empty)
        {

            bind(TextBox1.Text, null);
        }
        else if (TextBox2.Text != string.Empty)
        {

            bind(null, TextBox2.Text);
        }
        else
        {
            bind(null, null);
        }
    }
    private void bind(string from = null, string to = null)
    {
        var history1 = from c in db.PinTransferHistories

                       join g in db.tblMembers
                       on c.transferedTo equals g.username
                       where c.transferedFrom == "Admin"
                       select new
                       {
                           c.transferDate,
                           TransferedFrom = c.transferedFrom,
                           c.package,
                           TransferedTo = g.username + " - " + g.name,
                           pins = c.pinnumbers,
                           c.UTR
                       };

        var history2 = from c in db.PinTransferHistories
                       join g in db.tblVendors
                       on c.transferedTo equals g.Username
                       where c.transferedFrom == "Admin"
                       select new
                       {
                           c.transferDate,
                           TransferedFrom = c.transferedFrom,
                           c.package,
                           TransferedTo = g.Username + " - " + g.VendorName,
                           pins = c.pinnumbers,
                           c.UTR
                       };

        var history = history1.Union(history2);
        if (from != null)
            history = history.Where(x => x.TransferedFrom.Contains(from));
        else if (to != null)
            history = history.Where(x => x.TransferedTo.Contains(to));

        GridView1.DataSource = history.OrderByDescending(x => x.transferDate).ToList();
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}