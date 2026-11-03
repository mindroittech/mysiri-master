using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vProfile : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string vendor = Session["admin"] != null ? Session["admin"].ToString() : Session["vendor"].ToString();

            if (!IsPostBack)
            {

                var get = db.tblVendors.SingleOrDefault(x => x.Username == vendor);
                if (get != null)
                {
                    txtCompany.Text = get.VendorName;

                    txtEmail.Text = get.Email;
                    txtPhone.Text = get.Phone;
                    txtCity.Text = get.City;
                    txtPincode.Text = get.Pincode;
                    drpState.SelectedValue = get.State;


                    txtPAN.Text = get.PAN;
                    txtNominee.Text = get.Nominee;
                    txtAge.Text = get.NomineeAge.ToString();
                    txtRelation.Text = get.NomineeRelation;

                    txtAddress.Text = get.Address;
                    txtAccount.Text = get.account;
                    txtBank.Text = get.bankname;
                    txtBranch.Text = get.branch;
                    txtIfsc.Text = get.ifsc;
                }
            }
            if (Request.QueryString["v"] == "s")
                Panel1.Visible = true;
        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string vendor = Session["admin"] != null ? Session["admin"].ToString() : Session["vendor"].ToString();

        var get = db.tblVendors.Where(x => x.Username == vendor).SingleOrDefault();

        get.CompanyName = get.VendorName = txtCompany.Text;
        get.Phone = txtPhone.Text;
        get.City = txtCity.Text;
        get.Email = txtEmail.Text;
        get.Nominee = txtNominee.Text;
        if (txtAge.Text != string.Empty)
            get.NomineeAge = Convert.ToInt32(txtAge.Text);
        get.NomineeRelation = txtRelation.Text;
        get.State = drpState.SelectedValue;
        get.Pincode = txtPincode.Text;
        get.PAN = txtPAN.Text;
        get.Address = txtAddress.Text;
        get.bankname = txtBank.Text;
        get.branch = txtBranch.Text;
        get.account = txtAccount.Text;
        get.ifsc = txtIfsc.Text;
        db.SubmitChanges();

        Response.Redirect("vProfile.aspx?v=s");
    }


}