using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class profile : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();
            if (!IsPostBack)
            {
                var get = (from c in db.tblMembers
                           where c.username == uname
                           select c).SingleOrDefault();
                TextBox2.Text = get.name.ToUpper();
                txtuser.Value = get.username;
                TextBox3.Text = get.email;
                TextBox4.Text = get.mobile;

                //if (get.dob != null)
                //{
                //    TextBox5.Text = get.dob.Value.ToString("MM/dd/yyyy");
                //}
                //DropDownList1.SelectedValue = get.gender;
                TextBox6.Text = get.address;
                TextBox7.Text = get.pincode;
                TextBox13.Text = get.city;
             
                joinDate.Value = get.joindate.Value.ToString("dd/MM/yyyy");

                var sponsor = db.tblMembers.Where(x => x.username == get.sid).SingleOrDefault();
                if (sponsor != null)
                {
                    sponsorId.Value = sponsor.username;
                    sponsorName.Value = sponsor.name;
                }
               
                if (!string.IsNullOrEmpty(get.state))
                {
                    drpState.Items.FindByText(get.state).Selected = true;
                }
                TextBox8.Text = get.bankname;
                TextBox9.Text = get.branch;
                TextBox10.Text = get.account;
                TextBox11.Text = get.ifsc;
                TextBox12.Text = get.pan;
                drpGender.SelectedValue = get.gender;
                txtDistrict.Text = get.district;

                if (!string.IsNullOrEmpty(get.account) && !string.IsNullOrEmpty(get.ifsc))
                {
                    TextBox8.Enabled = false;
                    TextBox9.Enabled = false;
                    TextBox10.Enabled = false;
                    TextBox11.Enabled = false;
                }

                if (!string.IsNullOrEmpty(get.accHolderName))
                    txtaccNAme.Disabled = true;

                // paytmNumber.Value = get.payment;
                txtaccNAme.Value = get.accHolderName;
                ddAccountType.Value = get.accType;
                nomineeName.Value = get.Nominee;
                nomineeRelation.Value = get.NomineeRelation;

                // txtAdhar.Text = get.aadhar;
                if (get.UploadId != null)
                    cntMainContent_img_banner.Src = get.UploadId;
            }

            if (Request.QueryString["v"] == "sub")
                Panel1.Visible = true;
        }
        catch
        {
            Response.Redirect("../login.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();

            var pan = (from c in db.tblMembers
                       where c.pan == TextBox12.Text.Trim()
                       && c.username != uname
                       select c);
            if (pan.Any())
            {
                Label2.Text = "PAN number already exists. Please use diff PAN";
                Label2.Visible = true;
                return;
            }

            var mobile = (from c in db.tblMembers
                          where c.username == TextBox6.Text.Trim()
                          && c.username != uname
                          select c);
            if (mobile.Any())
            {
                Label2.Text = "Mobile number already exists. Please use diff number";
                Label2.Visible = true;
                return;
            }

            var get = (from c in db.tblMembers
                       where c.username == uname
                       select c).SingleOrDefault();

            get.name = TextBox2.Text;

            get.email = TextBox3.Text;
            get.mobile = TextBox4.Text;

            get.gender = drpGender.SelectedValue;
            //if(!string.IsNullOrEmpty(TextBox5.Text))
            //get.dob = Convert.ToDateTime(TextBox5.Text);

            get.district = txtDistrict.Text;
            get.address = TextBox6.Text;
            get.pincode = TextBox7.Text;
            get.city = TextBox13.Text;
            if (drpState.SelectedIndex != 0)
                get.state = drpState.SelectedItem.Text;
            get.accHolderName = txtaccNAme.Value;
            get.accType = ddAccountType.Value;
            get.bankname = TextBox8.Text;
            get.branch = TextBox9.Text;
            get.account = TextBox10.Text;
            get.ifsc = TextBox11.Text;
            get.pan = TextBox12.Text;
            // get.payment = paytmNumber.Value;
            // get.aadhar = txtAdhar.Text;

            get.Nominee = nomineeName.Value;
            get.NomineeRelation = nomineeRelation.Value;
            //  get.NomineeAge = txtAge.Text.Trim() != "" ? Convert.ToInt32(txtAge.Text) : 0;

            if (FileUpload1.HasFile)
            {
                string fileName = uname + "_" + FileUpload1.FileName;

                FileUpload1.SaveAs(Server.MapPath("~/Photos/" + fileName));
                get.UploadId = "~/Photos/" + fileName;
            }

            db.SubmitChanges();
            Response.Redirect("profile.aspx?v=sub");
        }
        catch
        {

        }
    }

    protected void TextBox11_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var get = (from c in db.Ifscs
                       where c.ifsc1 == (TextBox11.Text)
                       select c).SingleOrDefault();

            TextBox8.Text = get.bankname;
            TextBox9.Text = get.branchname;
        }
        catch
        { }
    }
}