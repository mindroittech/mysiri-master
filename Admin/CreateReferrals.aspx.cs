using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ReferAndEarn : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["vendor"].ToString();

            if (!IsPostBack)
            {
                string admin = Session["admin"] != null ? Session["admin"].ToString() : Session["vendor"].ToString();

                if (Session["vendor"] != null)
                    txtReferredBy.Value = Session["vendor"].ToString();

                var roles = from c in db.tblRoles
                            where c.IsActive == true
                            select new { RoleName = c.RoleName + " (Rs " + c.amount + " and above)", c.RoleId };
                drpRole.DataSource = roles;
                drpRole.DataTextField = "RoleName";
                drpRole.DataValueField = "RoleId";
                drpRole.DataBind();
                drpRole.Items.Insert(0, new ListItem("Select Vendor", ""));


                var state = from c in db.TBLstates
                            select new
                            {
                                SId = c.ID, // Assuming ID from TBLstates is SId
                                StateName = c.stateName // Assuming state from TBLstates is StateName
                            };

                drpstates.DataSource = state;
                drpstates.DataTextField = "StateName";
                drpstates.DataValueField = "SId";
                drpstates.DataBind();
                drpstates.Items.Insert(0, new ListItem("Select State", ""));

                var reporter = from c in db.tblVendors
                               join d in db.tblRoles
                               on c.RoleId equals d.RoleId
                               where (d.role == "Admin" || d.role == "GoDown") && c.IsActive == true
                               select c;
                drpReporter.DataSource = reporter;
                drpReporter.DataTextField = "VendorName";
                drpReporter.DataValueField = "Id";
                drpReporter.DataBind();
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        var referedBy = txtReferredBy.Value;
        if (referedBy != string.Empty)
        {
            var franch = db.tblVendors.SingleOrDefault(x => x.Username == referedBy);
            if (franch == null)
            {
                var member = db.tblMembers.SingleOrDefault(x => x.username == referedBy);
                if (member == null)
                {
                    Label1.Text = "Sponsor Id is not a valid";
                    Label1.Visible = true;
                    return;
                }
            }
        }
        var vendor = new tblReferral
        {
            Username = referedBy,
            CreatedDate = DateTime.UtcNow.AddHours(5.5).Date,
            VendorName = txtCompanay.Value,
            CompanyName = txtCompanay.Value,
            Phone = txtPhone.Value,
            RoleId = Convert.ToInt32(drpRole.SelectedValue),
            City = txtCity.Text,
            Pincode = txtPincode.Text,
            Address = txtAddress.Text,
            GSTIN = txtGSTNo.Text,
            Amount = Convert.ToDecimal(txtAmount.Text),
            status = "Pending",
            UTR = txtUTR.Text,
            bank = txtBank.Text,
            depositeDate = Convert.ToDateTime(txtDate.Text),
                 
        };
        if (FileUpload1.HasFile)
        {
            string fileName = referedBy + "_Slip_" + FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath("~/Proof/" + fileName));
            string link = "~/Proof/" + fileName;
            vendor.slip = link;
        }

      //  vendor.State = drpState.Value;

        db.tblReferrals.InsertOnSubmit(vendor);
        db.SubmitChanges();

        Response.Redirect("ReferralHistory.aspx");
    }

    private string GeneratePassword()
    {
        string strPwdchar = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 5; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }
        return strPwd;
    }

    protected void drpstates_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedStateId = Convert.ToInt32(drpstates.SelectedValue);

        // Fetch districts based on the selected state ID
        var districts = from d in db.tbldistricts
                        where d.stateid == selectedStateId
                        select new
                        {
                            DistrictId = d.stateid,
                            DistrictName = d.district
                        };

        // Bind districts to drpDistrict dropdown
        drpdist.DataSource = districts;
        drpdist.DataTextField = "DistrictName";
        drpdist.DataValueField = "DistrictId";
        drpdist.DataBind();

        // Add a default option
        drpdist.Items.Insert(0, new ListItem("Select District", ""));

    }
    protected void drpdist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(drpRole.SelectedValue))
        {
            int selectedRoleId = Convert.ToInt32(drpRole.SelectedValue);

            var selectedRole = db.tblRoles.FirstOrDefault(r => r.RoleId == selectedRoleId);
            var selectDistrict = db.tbldistricts.SingleOrDefault(x => x.district == drpdist.SelectedItem.Text);

            if (selectedRole.RoleId != 2 && selectedRole.RoleId != 1)
            {
                //var reporter = from c in db.tblVendors
                //               join d in db.tblRoles
                //               on c.RoleId equals d.RoleId
                //               where (d.role == "GoDown") && c.IsActive == true
                //               select c;

                var reporter1 = from c in db.tblVendors
                                join d in db.tblRoles
                                on c.RoleId equals d.RoleId
                                where d.role == "GoDown"
                                      && c.district == selectDistrict.district
                                      && c.IsActive == true
                                select c;

                var result = reporter1.ToList();

                if (result.Count > 0)
                {

                    drpReporter.DataSource = reporter1;
                    drpReporter.DataTextField = "VendorName";
                    drpReporter.DataValueField = "Id";
                    drpReporter.DataBind();

                }
                else
                {
                    var reporter = from c in db.tblVendors
                                   join d in db.tblRoles
                                   on c.RoleId equals d.RoleId
                                   where (d.role == "GoDown") && c.IsActive == true
                                   select c;
                    drpReporter.DataSource = reporter;
                    drpReporter.DataTextField = "VendorName";
                    drpReporter.DataValueField = "Id";
                    drpReporter.DataBind();
                }
            }
            else
            {
                var reporter = from c in db.tblVendors
                               join d in db.tblRoles
                               on c.RoleId equals d.RoleId
                               where (d.role == "Admin") && c.IsActive == true
                               select c;
                drpReporter.DataSource = reporter;
                drpReporter.DataTextField = "VendorName";
                drpReporter.DataValueField = "Id";
                drpReporter.DataBind();
            }
        }
    }
}