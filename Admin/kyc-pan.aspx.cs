using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_kyc_pan : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            if (!IsPostBack)
            {
                var members = from c in db.tblMembers
                              where c.isActive == 1 && c.isPanVerifed == null && c.UploadPan != null
                              select new
                              {
                                  c.username,
                                  c.name,
                                  c.pan,
                                  c.account,
                                  c.ifsc,
                                  c.UploadPan,
                                  c.UploadBank,
                                  c.UsedDate,
                                  c.EligibleDate
                              };

                var vendors = from c in db.tblVendors
                              where c.IsActive == true && c.isKyc == null && c.UploadPAN != null
                              select new
                              {
                                  username = c.Username,
                                  name = c.VendorName,
                                  pan = c.PAN,
                                  c.account,
                                  c.ifsc,
                                  UploadPan = c.UploadPAN,
                                  UploadBank = c.UploadBank,
                                  c.UsedDate,
                                  c.EligibleDate
                              };

                GridView1.DataSource = members.Union(vendors);
                GridView1.DataBind();

                if (Request.QueryString["s"] != null)
                {
                    Label1.Visible = true;
                    Label1.Text = "KYC " + Request.QueryString["s"];
                }
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "edit")
        {
            string username = e.CommandArgument.ToString();
            Response.Redirect("kyc-detail.aspx?uname=" + username);
        }
        if (e.CommandName == "reject")
        {
            string username = e.CommandArgument.ToString();
            Response.Redirect("kyc-detail.aspx?uname=" + username);
        }
    }
}