using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_kyc_detail : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (!IsPostBack)
            {
                string uname = Request.QueryString["uname"].ToString();

                if (uname.Length == 7)
                {
                    var get = (from c in db.tblVendors
                               where c.IsActive == true
                               && c.Username == uname
                               select new { c.Username, c.VendorName, c.PAN, c.UploadPAN, c.UploadBank }).SingleOrDefault();
                    txtName.Text = get.VendorName;
                    TextBox1.Text = get.Username;
                    txtPAN.Text = get.PAN;
                    Image1.ImageUrl = get.UploadPAN;
                    Image2.ImageUrl = get.UploadBank;
                    
                }
                else
                {
                    var get = (from c in db.tblMembers
                               where c.isActive == 1
                               && c.username == uname
                               select new { c.username, c.name, c.pan, c.UploadPan, c.UploadBank }).SingleOrDefault();
                    txtName.Text = get.name;
                    TextBox1.Text = get.username;
                    txtPAN.Text = get.pan;
                    Image1.ImageUrl = get.UploadPan;
                    Image2.ImageUrl = get.UploadBank;
                }
            }
        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length == 7)
        {
            var member = db.tblVendors.Where(c => c.Username == TextBox1.Text).SingleOrDefault();
            member.isKyc = true;
            member.EligibleDate = DateTime.UtcNow.AddHours(5.5);
            db.SubmitChanges();
        }
        else
        {
            var member = db.tblMembers.Where(c => c.username == TextBox1.Text).SingleOrDefault();
            member.isPanVerifed = true;
            member.EligibleDate = DateTime.UtcNow.AddHours(5.5);
            db.SubmitChanges();
        }
        Response.Redirect("kyc-pan.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text.Length == 7)
        {
            var member = db.tblVendors.Where(c => c.Username == TextBox1.Text).SingleOrDefault();
            member.isKyc = false;
            member.EligibleDate = DateTime.UtcNow.AddHours(5.5);
            db.SubmitChanges();
        }
        else
        {
            var member = db.tblMembers.Where(c => c.username == TextBox1.Text).SingleOrDefault();
            member.isPanVerifed = false;
            member.EligibleDate = DateTime.UtcNow.AddHours(5.5);
            db.SubmitChanges();
        }
        Response.Redirect("kyc-pan.aspx");
    }
}