using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_admin : System.Web.UI.MasterPage
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //string username = Session["admin"].ToString();

            string username = Session["admin"] != null ? Session["admin"].ToString() : Session["vendor"].ToString();
            lblName.Text = username;
            var find = db.tblVendors.SingleOrDefault(x => x.Username == username);
            string role = Session["role"].ToString();
            if (role == "Admin")
            {
                liVendorDash.Visible = false;
                if (username == "admin1")
                {
                    admin4.Visible = admin5.Visible = false;
                    adminAccounts.Visible = adminFranchisee.Visible = adminUtilities.Visible = false;
                }
                if (username == "admin2")
                {
                    admin1.Visible = admin3.Visible = admin2.Visible = false;
                    adminAccounts.Visible = adminFranchisee.Visible = adminUtilities.Visible = false;
                }
                if (username == "admin3")
                {
                    admin1.Visible = admin2.Visible = admin4.Visible = admin5.Visible = false;
                    adminAccounts.Visible = adminFranchisee.Visible = adminUtilities.Visible = false;
                }
                if (username == "admin4")
                {
                    admin1.Visible = admin2.Visible = admin3.Visible = admin5.Visible = false;
                    adminAccounts.Visible = adminFranchisee.Visible = adminUtilities.Visible = false;
                }
                if (username == "admin5")
                {
                    admin1.Visible = admin2.Visible = admin3.Visible = admin4.Visible = false;
                    adminAccounts.Visible = adminFranchisee.Visible = adminUtilities.Visible = false;
                }
                if (username == "charan")
                {
                    admin1.Visible = admin2.Visible = admin3.Visible = admin4.Visible = admin5.Visible=false;
                    adminAccounts.Visible = adminFranchisee.Visible = adminUtilities.Visible = false;
                    admin6.Visible = true;

                    admin1.Style.Add("pointer-events", "none");
                    admin2.Style.Add("pointer-events", "none");
                    admin3.Style.Add("pointer-events", "none");
                    admin4.Style.Add("pointer-events", "none");
                    admin5.Style.Add("pointer-events", "none");
                    adminAccounts.Style.Add("pointer-events", "none");
                    adminFranchisee.Style.Add("pointer-events", "none");
                    adminUtilities.Style.Add("pointer-events", "none");

                }
            }
            else
            {
                liAdminDash.Visible = false;
                if (role == "Franchisee")
                {
                    stockTransfer.Visible = false;
                }
                if (role == "GoDown")
                {
                    vendorRepurchase.Visible = false;
                }
            }
        }
        catch
        { }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/index.html");
    }
}
