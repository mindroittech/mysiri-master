using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_mprofile : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string admin = Session["admin"].ToString();

            string uname = Request.QueryString["uname"].ToString();

            if (!IsPostBack)
            {

                var get = (from c in db.tblMembers
                           where c.username == uname
                           select c).SingleOrDefault();
                TextBox2.Text = get.name;
                TextBox1.Text = get.username;
                TextBox3.Text = get.email;
                TextBox4.Text = get.mobile;

                TextBox14.Text = get.username;
                TextBox15.Text = get.password;

                if (get.dob != null)
                {
                    TextBox5.Text = get.dob.Value.ToString("MM/dd/yyyy");
                }
                DropDownList1.SelectedValue = get.gender;
                TextBox6.Text = get.address;
                TextBox7.Text = get.pincode;
                DropDownList2.SelectedValue = get.state;
                TextBox13.Text = get.city;

                TextBox8.Text = get.bankname;
                TextBox9.Text = get.branch;
                TextBox10.Text = get.account;
                TextBox11.Text = get.ifsc;
                TextBox12.Text = get.pan;

                txtaccNAme.Value = get.accHolderName;
                ddAccountType.Value = get.accType;
                txtNominee.Text = get.Nominee;
                txtAge.Text = get.NomineeAge.ToString();
                txtRelation.Text = get.NomineeRelation;

                txtAdhar.Text = get.aadhar;

                var sponsor = db.tblMembers.Where(x => x.username == get.sid).SingleOrDefault();

               if(sponsor!=null)
                    {
                    Label1.Text = get.sid;
                    Label2.Text = sponsor.name;

                }

                var findReferrer = db.tblMembers.Where(x => x.id == get.refid).SingleOrDefault();

                Label3.Text = findReferrer != null ? findReferrer.username : "";
                Label4.Text = findReferrer != null ? findReferrer.name : "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("view-members.aspx");
        }
    }

    protected void txtMobile_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var get = (from c in db.tblMembers
                       where c.mobile == TextBox4.Text
                       select c);

            if (get.Count() > 0)
            {
                TextBox2.Text = get.First().name;
                TextBox3.Text = get.First().email;
                TextBox7.Text = get.First().pincode;
                if (get.First().dob != null)
                {
                    TextBox5.Text = get.First().dob.Value.ToString("MM/dd/yyyy");
                }

                TextBox6.Text = get.First().address;
                DropDownList1.SelectedValue = get.First().gender;
                TextBox13.Text = get.First().city;

                txtNominee.Text = get.First().Nominee;
                txtRelation.Text = get.First().NomineeRelation;
                if (get.First().NomineeAge.HasValue)
                    txtAge.Text = get.First().NomineeAge.Value.ToString();

                txtaccNAme.Value = get.First().accHolderName;

                txtAdhar.Text = get.First().aadhar;
                TextBox12.Text = get.First().pan;
                TextBox10.Text = get.First().account;
                TextBox11.Text = get.First().ifsc;
                TextBox8.Text = get.First().bankname;
                TextBox9.Text = get.First().branch;
                ddAccountType.Value = get.First().accType;
            }
        }
        catch
        { }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            string uname = Request.QueryString["uname"].ToString();

            var get = (from c in db.tblMembers
                       where c.username == uname
                       select c).SingleOrDefault();

            var pan = (from c in db.tblMembers
                       where c.pan == TextBox12.Text.Trim()
                       && c.username != uname
                       select c);
            if (pan.Any())
            {
                Label5.Text = "PAN number already exists. Please use diff PAN";
                Label5.Visible = true;
                return;
            }

            var mobile = (from c in db.tblMembers
                          where c.username == TextBox4.Text.Trim()
                          && c.username != uname
                          select c);
            if (mobile.Any())
            {
                Label5.Text = "Mobile number already exists. Please use diff number";
                Label5.Visible = true;
                return;
            }


            get.name = TextBox2.Text;

            get.email = TextBox3.Text;
            get.mobile = TextBox4.Text;

            if (!string.IsNullOrEmpty(TextBox5.Text))
                get.dob = Convert.ToDateTime(TextBox5.Text);

            get.gender = DropDownList1.SelectedValue;
            get.address = TextBox6.Text;
            get.pincode = TextBox7.Text;

            if (DropDownList2.SelectedIndex != 0)
                get.state = DropDownList2.SelectedValue;

            get.city = TextBox13.Text;

            get.bankname = TextBox8.Text;
            get.branch = TextBox9.Text;
            get.account = TextBox10.Text;
            get.ifsc = TextBox11.Text;
            get.pan = TextBox12.Text;
            get.aadhar = txtAdhar.Text;

            get.accHolderName = txtaccNAme.Value;
            get.accType = ddAccountType.Value;

            get.Nominee = txtNominee.Text;
            get.NomineeRelation = txtRelation.Text;
            get.NomineeAge = txtAge.Text.Trim() != "" ? Convert.ToInt32(txtAge.Text) : 0;

            get.password = TextBox15.Text;

            db.SubmitChanges();

            Panel1.Visible = true;

        }

        catch (Exception ex)
        {

        }
    }
}