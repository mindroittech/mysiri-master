using System; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_ReferAndEarn : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            if (!IsPostBack)
            {

                txtReferredBy.Text = uname;
                txtUser_Changed(null, null);
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

            }
        }
        catch
        {
            Response.Redirect("../login.aspx");
        }

    }
    protected void txtUser_Changed(object sender, EventArgs e)
    {
        findName(txtReferredBy.Text);
    }

    private void findName(string uname)
    {
        try
        {
            var get = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
            if (get != null)
                lblName.Text = get.name;
            else
            {
                var franch = db.tblVendors.Where(x => x.Username == uname).SingleOrDefault();
                if (franch != null)
                {
                    lblName.Text = franch.VendorName;
                }
                else
                    lblName.Text = "";
            }
        }
        catch
        {
            lblName.Text = "";
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        var referedBy = txtReferredBy.Text;
        if (referedBy != string.Empty)
        {
            var franch = db.tblVendors.SingleOrDefault(x => x.Username == referedBy);
            if (franch == null)
            {
                var member = db.tblMembers.SingleOrDefault(x => x.username == referedBy);
                if (member == null)
                {
                    Label1.Text = "Sponsor Id is not valid";
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
            UTR2 = txtUTR2.Text,
            UTR3 = txtUTR3.Text,
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
        if (FileUpload2.HasFile)
        {
            string fileName = referedBy + "_Slip2_" + FileUpload2.FileName;
            FileUpload2.SaveAs(Server.MapPath("~/Proof/" + fileName));
            string link = "~/Proof/" + fileName;
            vendor.slip2 = link;
        }
        if (FileUpload3.HasFile)
        {
            string fileName = referedBy + "_Slip3_" + FileUpload3.FileName;
            FileUpload3.SaveAs(Server.MapPath("~/Proof/" + fileName));
            string link = "~/Proof/" + fileName;
            vendor.slip3 = link;
        }

        vendor.State = drpstates.SelectedItem.Text;
        vendor.district = drpdist.SelectedItem.Text;

        db.tblReferrals.InsertOnSubmit(vendor);
        db.SubmitChanges();

        Response.Redirect("franchiseeReferralHistory.aspx");
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
}