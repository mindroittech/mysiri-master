using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_dashboard : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            string admin = Session["admin"].ToString();

            if (admin != "admin")
                divAdmin.Visible = false;

            var pinreq = from c in db.PinRequests
                         where c.status == "Pending"
                         select c;
            if (pinreq.Any())
            {
                lblPin.Text = pinreq.Count().ToString();

            }

            var products = from c in db.tblProducts
                           where c.IsActive == true && c.Quantity != null
                          && c.Quantity < 200
                           select new { c.ProductName, c.Quantity };

            Repeater1.DataSource = products;
            Repeater1.DataBind();

        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (Session["role"].ToString() == "admin")
            Response.Redirect("activate.aspx");
        else
            Response.Redirect("op-activatemember.aspx");
    }
}