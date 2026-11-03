using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_add_vendors : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();
            if (role != "GoDown" && role != "Admin")
            {
                throw new Exception("Access denied");
            }
            if (!IsPostBack)
            {

                    var reporter = from c in db.tblVendors
                                   join d in db.tblRoles
                                   on c.RoleId equals d.RoleId
                                   where (d.role == "Admin" || d.role == "GoDown") && c.IsActive == true
                                   select c;
                    drpReporter.DataSource = reporter;
                    drpReporter.DataTextField = "VendorName";
                    drpReporter.DataValueField = "Id";
                    drpReporter.DataBind();


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


                var roles = from c in db.tblRoles
                            where c.IsActive == true
                            select new { RoleName = c.RoleName + " (Rs " + c.amount + " and above)", c.RoleId };
                drpRole.DataSource = roles;
                drpRole.DataTextField = "RoleName";
                drpRole.DataValueField = "RoleId";
                drpRole.DataBind();
                drpRole.Items.Insert(0, new ListItem("Select Vendor", ""));

                var districts = from c in db.tbldistricts
                                select new
                                {
                                    DistrictId = c.id,
                                    DistrictName = c.district
                                };

                // Bind districts to drpDistrict dropdown
                drpdist.DataSource = districts;
                drpdist.DataTextField = "DistrictName";
                drpdist.DataValueField = "DistrictId";
                drpdist.DataBind();

                // Add a default option
                drpdist.Items.Insert(0, new ListItem("Select District", ""));
                if (Request.QueryString["Id"] != null)
                {

                    var roles1 = from c in db.tblRoles                       
                                select new { RoleName = c.RoleName + " (Rs " + c.amount + " and above)", c.RoleId };
                    drpRole.DataSource = roles1;
                    drpRole.DataTextField = "RoleName";
                    drpRole.DataValueField = "RoleId";
                    drpRole.DataBind();
                    drpRole.Items.Insert(0, new ListItem("Select Vendor", ""));

                    //divEdit.Visible = true;
                    int id = Convert.ToInt32(Request.QueryString["Id"]);
                    var get = db.tblVendors.Where(x => x.Id == id).SingleOrDefault();

                    txtCompanay.Value = get.VendorName;
                    drpRole.Items.FindByValue(get.RoleId.ToString()).Selected = true;
                    drpRole.Enabled = false;
                    txtAmount.Text = get.Amount.ToString();
                    txtPhone.Value = get.Phone;
                    txtCity.Text = get.City;
                    txtPincode.Text = get.Pincode;
                    txtEmail.Text = get.Email;
              
                    txtUser.Text = get.Username;
                    txtPass.Text = get.Password;
                    txtReferedBy.Enabled = false;
                    txtReferedBy.Text = get.SponsoredBy;
                    txtAddress.Text = get.Address;
                    txtGSTNo.Text = get.GSTIN;
                    Button1.Text = "Update";
                    drpReporter.SelectedValue = get.ReportingId.ToString();
                    divUpdate.Visible = true;
                    txtNominee.Text = get.Nominee;
                    txtAge.Text = get.NomineeAge.ToString();
                    txtRelation.Text = get.NomineeRelation;

                    txtBank.Text = get.bankname;
                    txtBranch.Text = get.branch;
                    txtIfsc.Text = get.ifsc;
                    txtAccount.Text = get.account;
                    txtPAN.Text = get.PAN;
                    chkActive.Checked = get.isStatusActive.Value;
                   
                    //chsalary.Checked = get.allowSalary.Value;

                  
                    //  string stateName = !string.IsNullOrEmpty(get.state) ? get.state : null;
                    string stateName = get.State;
                   
                    var stateId = db.TBLstates
                                    .Where(s => s.stateName == stateName)
                                    .Select(s => s.ID)
                                    .SingleOrDefault();

                    if (drpstates.Items.FindByValue(stateId.ToString()) != null)
                    {
                        drpstates.SelectedValue = stateId.ToString();                       
                    }

                    string district = !string.IsNullOrEmpty(get.district) ? get.district : null;

                    var distid = db.tbldistricts.Where(s => s.district == district)
                                    .Select(s => s.id)
                                    .SingleOrDefault();

                    if (drpdist.Items.FindByValue(distid.ToString()) != null)
                    {
                        drpdist.SelectedValue = distid.ToString();
                    }

                    SetDistrict(stateId);

                    HyperLink1.Visible = true;
                    HyperLink2.Visible = true;
                    if (get.UploadPAN != null)
                        HyperLink1.NavigateUrl = get.UploadPAN;
                    if (get.UploadBank != null)
                        HyperLink2.NavigateUrl = get.UploadBank;
                }
            }      
        }
        catch (Exception ex)
        {
             Response.Redirect("../vendorlogin.aspx");
          
        }

    }

    protected void txtIfsc_changed(object sender, EventArgs e)
    {
        try
        {
            var get = (from c in db.Ifscs
                       where c.ifsc1 == (txtIfsc.Text)
                       select c).SingleOrDefault();

            txtBank.Text = get.bankname;
            txtBranch.Text = get.branchname;
        }
        catch
        { }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {
            int userid = 1000;
            var find = db.tblVendors.OrderByDescending(x => x.Id).FirstOrDefault();
            if (find != null)
                userid = userid + find.Id;
            string uname = "MF" + userid;

            var referedBy = txtReferedBy.Text;
            if (referedBy != string.Empty)
            {
                var franch = db.tblVendors.SingleOrDefault(x => x.Username == referedBy);
                if (franch == null)
                {
                    var member = db.tblMembers.SingleOrDefault(x => x.username == referedBy);
                    if (member == null)
                    {
                        lblMessage.Text = "Sponsor Id is not a valid";
                        lblMessage.Visible = true;
                        return;
                    }
                }
            }
            var mobile = (from c in db.tblVendors
                          where c.Phone == txtPhone.Value
                          select c);
            if (mobile.Any())
            {
                lblMessage.Text = "Mobile number already exists. Please use diff number";
                lblMessage.Visible = true;
                return;
            }
            //var statesname = db.TBLstates.SingleOrDefault(x => x.ID == drpstates.SelectedIndex);

            //var districtsname = db.tbldistricts.SingleOrDefault(x => x.id == drpdist.SelectedIndex);

            var vendor = new tblVendor
            {
                Username = uname,
                Password = GeneratePassword(),
                CreatedDate = DateTime.UtcNow.AddHours(5.5).Date,
                VendorName = txtCompanay.Value,
                CompanyName = txtCompanay.Value,
                IsActive = true,
                Phone = txtPhone.Value,
                ReportingId = Convert.ToInt32(drpReporter.SelectedValue),
                RoleId = Convert.ToInt32(drpRole.SelectedValue),
                City = txtCity.Text,
                Pincode = txtPincode.Text,
                Address = txtAddress.Text,
                GSTIN = txtGSTNo.Text,
                Amount = Convert.ToDecimal(txtAmount.Text),
                KitWallet = 0,
                KitWalletDebit = 0,
                RepurchaseWallet = 0,
                RepurchaseWalletDebit = 0,
                SponsoredBy = referedBy,
                State = drpstates.SelectedItem.Text,
                district = drpdist.SelectedItem.Text,
                bankname = txtBank.Text,
                branch = txtBranch.Text,
                ifsc = txtIfsc.Text,
                account = txtAccount.Text,
                PAN = txtPAN.Text,
                isStatusActive = true,
                isEligiable = false,
               // allowSalary = chsalary.Checked
            };

            vendor.Nominee = txtNominee.Text;
            if (txtAge.Text != string.Empty)
                vendor.NomineeAge = Convert.ToInt32(txtAge.Text);
            vendor.NomineeRelation = txtRelation.Text;

            if (FileUpload1.HasFile)
            {
                string fileName = uname + "_PAN_" + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/Files/" + fileName));
                string link = "~/Files/" + fileName;
                vendor.UploadPAN = link;
            }
            if (FileUpload2.HasFile)
            {
                string bankPass = uname + "_Pass_" + FileUpload2.FileName;
                FileUpload2.SaveAs(Server.MapPath("~/Files/" + bankPass));
                string passbooklink = "~/Files/" + bankPass;
                vendor.UploadBank = passbooklink;

            }

            //if (Convert.ToInt32(drpRole.SelectedValue) > 2)
            //    vendor.fcount = 24;

            db.tblVendors.InsertOnSubmit(vendor);

            // Franchisee Sponsor Income 

            decimal amt = Convert.ToDecimal(txtAmount.Text);

            var role = db.tblRoles.SingleOrDefault(
                     x => x.RoleId == Convert.ToInt32(drpRole.SelectedValue) && x.IsActive == true);

            decimal sponsorPercent = role.sponsorPercent.HasValue
                       ? role.sponsorPercent.Value : 0;

            decimal sponsorIncome = amt * sponsorPercent / 100;

            if (referedBy != string.Empty)
            {

                var franch = db.tblVendors.SingleOrDefault(
                    x => x.Username == referedBy);

                if (franch != null)
                {
                    franch.KitWallet = (franch.KitWallet ?? 0) + sponsorIncome;
                    payout(sponsorIncome, franch.Username, vendor.Username);
                }
                else
                {
                    var member = db.tblMembers.SingleOrDefault(
                        x => x.username == referedBy);

                    if (member == null)
                    {
                        lblMessage.Text = "Sponsor Id is not valid.";
                        lblMessage.Visible = true;
                        return;
                    }

                    member.credit = (member.credit ?? 0) + sponsorIncome;
                    payout(sponsorIncome, member.username, vendor.Username);
                }
            }

            db.SubmitChanges();

            string msg = "Dear " + vendor.VendorName + ", Congratulations! Welcome to DLS Biotech. Your Franchisee Login ID:" + vendor.Username + " Password:" + vendor.Password + " Login at: https://www.dlsbiotech.com";
            sm.sms(vendor.Phone, msg, "1407172845282824396");

        }
        else if (Button1.Text == "Update")
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);

            var get = db.tblVendors.Where(x => x.Id == id).SingleOrDefault();

            var mobile = (from c in db.tblVendors
                          where c.Phone == txtPhone.Value
                          && c.Id != id
                          select c);
            if (mobile.Any())
            {
                lblMessage.Text = "Mobile number already exists. Please use diff number";
                lblMessage.Visible = true;
                return;
            }   

            get.VendorName = txtCompanay.Value;
            get.CompanyName = txtCompanay.Value;
            get.Phone = txtPhone.Value;
            get.City = txtCity.Text;
           //if (get.Country == "India")
            get.State = drpstates.SelectedItem.Text;
            get.district = drpdist.SelectedItem.Text;
            get.Pincode = txtPincode.Text;
            get.RoleId = Convert.ToInt32(drpRole.SelectedValue);
            get.Address = txtAddress.Text;
            get.GSTIN = txtGSTNo.Text;
            get.Password = txtPass.Text;
            get.ReportingId = Convert.ToInt32(drpReporter.SelectedValue);
            get.Email = txtEmail.Text;
            get.Nominee = txtNominee.Text;
            if (txtAge.Text != string.Empty)
                get.NomineeAge = Convert.ToInt32(txtAge.Text);
            get.NomineeRelation = txtRelation.Text;
            get.bankname = txtBank.Text;
            get.branch = txtBranch.Text;
            get.ifsc = txtIfsc.Text;
            get.account = txtAccount.Text;
            get.PAN = txtPAN.Text;
            get.isStatusActive = chkActive.Checked;

            //get.allowSalary = chsalary.Checked;

            if (FileUpload1.HasFile)
            {
                string fileName = get.Username + "_PAN_" + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/Files/" + fileName));
                string link = "~/Files/" + fileName;
                get.UploadPAN = link;
            }
            if (FileUpload2.HasFile)
            {
                string bankPass = get.Username + "_Pass_" + FileUpload1.FileName;
                FileUpload2.SaveAs(Server.MapPath("~/Files/" + bankPass));
                string passbooklink = "~/Files/" + bankPass;
                get.UploadBank = passbooklink;

            }

            db.SubmitChanges();
        }
        Response.Redirect("vendors.aspx");
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

    protected void payout(Decimal amt, string uname, string incomefrom)
    {

        var insert = new Payout();
        insert.username = uname;
        insert.date = DateTime.UtcNow.Date;
        insert.confirmdate = DateTime.UtcNow.AddHours(5.5);
        insert.desc = "Franchisee Sponsor Income";
        insert.tax = 0;
        insert.netpayout = amt;
        insert.credit = amt;
        insert.debit = 0;
        insert.status = "Send";
        insert.incomefrom = incomefrom;
        insert.remarks = "Franchisee Referral Income";
        db.Payouts.InsertOnSubmit(insert);
        db.SubmitChanges();
    }
    protected void drpstates_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedStateId = Convert.ToInt32(drpstates.SelectedValue);

        // Fetch districts based on the selected state ID
        var districts = from d in db.tbldistricts
                        where d.stateid == selectedStateId
                        select new
                        {
                            DistrictId = d.id,
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

    protected void drpRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(drpRole.SelectedValue))
        {
            int selectedRoleId = Convert.ToInt32(drpRole.SelectedValue);

            var selectedRole = db.tblRoles.FirstOrDefault(r => r.RoleId == selectedRoleId);

            if (selectedRole.RoleId ==2)
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
            else
            {
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
        else
        {
            
        }

    }
    private void SetDistrict(int id)
    {
        var districts = from d in db.tbldistricts
                        where d.stateid == id
                        select new
                        {
                            DistrictId = d.id,
                            DistrictName = d.district
                        };

        // Bind districts to drpDistrict dropdown
        drpdist.DataSource = districts;
        drpdist.DataTextField = "DistrictName";
        drpdist.DataValueField = "DistrictId";
        drpdist.DataBind();

    }
    protected void drpdist_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(drpRole.SelectedValue))
        {
            int selectedRoleId = Convert.ToInt32(drpRole.SelectedValue);

            var selectedRole = db.tblRoles.FirstOrDefault(r => r.RoleId == selectedRoleId);
            var selectDistrict = db.tbldistricts.SingleOrDefault(x => x.district == drpdist.SelectedItem.Text );

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