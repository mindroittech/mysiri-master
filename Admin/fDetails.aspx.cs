using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_fDetails : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            if (Request.QueryString["uname"] != null)
            {
                string uname = Request.QueryString["uname"];

                var find = (from c in db.tblVendors
                            where c.Username == uname
                            select c).SingleOrDefault();
                lblID.Text = find.Username;
                lblName.Text = find.VendorName;
                Label2.Text = find.bankname;
                Label3.Text = find.branch;
                Label4.Text = find.account;
                Label5.Text = find.ifsc;
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }
}