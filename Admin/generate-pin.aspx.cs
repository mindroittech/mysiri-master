using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_generate_pin : System.Web.UI.Page
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
                var plan = from c in db.tblPlans where c.status==true
                           select c;
                drpPlan.DataSource = plan;
                drpPlan.DataTextField = "PlanName";
                drpPlan.DataValueField = "PlanAmount";
                drpPlan.DataBind();

                //drpPlan.Items.Insert(0, new ListItem("Select Plan", "0"));
            }

            if (Request.QueryString["uname"] != null)
            {
                TextBox1.Text = Request.QueryString["uname"];
                TextBox2.Text = Request.QueryString["sil"];

                drpPlan.Items.FindByText(Request.QueryString["plan"]).Selected = true;

                findName(TextBox1.Text);
                CalculateAmount(null, null);
            }

        }

        catch
        {

            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void txtUser_Changed(object sender, EventArgs e)
    {
        findName(TextBox1.Text);
    }

    private void findName(string uname)
    {
        try
        {
            var get = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
            if (get != null)
                txtName.Text = get.name;

            else
            {
                var franch = db.tblVendors.Where(x => x.Username == uname).SingleOrDefault();
                if (franch != null)
                {
                    txtName.Text = franch.VendorName;
                }
                else
                    txtName.Text = "";
            }
        }
        catch
        {
            txtName.Text = "";
        }

    }

    protected void GenerateOTP_Click(object sender, EventArgs e)
    {
        try
        {
            sendsms sm = new sendsms();
            decimal amt = 0;

            amt = Convert.ToDecimal(txtAmount.Text);
            string otp = Helper.GenerateOTP();
            Session["PinOTP"] = otp;
            lblOtpMessage.Text = "Otp has been sent to your phone number";
            string msg = otp + " is the OTP to approve the PIN Request for the ID : " + TextBox1.Text + " Name : " + txtName.Text +
                " for the amount Rs. " + amt + ". DLS Biotech";
          //  sm.sms("8208595348", msg, "1407169591317075523");
            sm.sms("9380393866,9448415584", msg, "1407169591317075523");

            //9380393866,9448415584
        }
        catch
        {

        }
    }

    protected void DropDownList2_Changed(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue == "Online")
            divUTR.Visible = true;
        else
            divUTR.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string uname = "";
        string mobile = "";
        string name = "";
        var find = (from c in db.tblMembers
                    where c.username == TextBox1.Text.Trim()
                    select c).SingleOrDefault();
        bool isFranch = false;
        if (find == null)
        {
            var franch = db.tblVendors.Where(x => x.Username == TextBox1.Text.Trim()).SingleOrDefault();
            if (franch == null)
            {
                Label1.Visible = true;
                Label1.Text = "ID not found";
                return;
            }
            else
            {
                isFranch = true;
                uname = franch.Username;
                mobile = franch.Phone;
                name = franch.VendorName;
            }
        }
        else
        {
            uname = find.username;
            mobile = find.mobile;
            name = find.name;
        }

        if (Session["PinOTP"] == null || Session["PinOTP"].ToString() != txtOTP.Text)
        {
            Label1.Text = "Invalid OTP";
            Label1.Visible = true;
            return;
        }

        if (TextBox2.Text != string.Empty)
        {
            int n = Convert.ToInt32(TextBox2.Text);

            string pin = GeneratePin();
            for (int i = 0; i < n; i++)
            {
                var f = from c in db.PinManagaers
                        where c.pinnumbers == pin
                        select c;
                if (f.Count() == 1)
                {
                    pin = (Convert.ToDecimal(pin) + 1).ToString();
                }

                var ins = new PinManagaer();
                ins.date = DateTime.UtcNow.AddHours(5.5).Date;
                ins.issued = TextBox1.Text;
                ins.status = "Not used";
                ins.pinnumbers = pin;
                ins.package = drpPlan.SelectedItem.Text;
                ins.transferby = "Admin"; // System.Configuration.ConfigurationManager.AppSettings["companyname"];
                ins.initialTransfer = TextBox1.Text;
                ins.IsFreePin = CheckBox1.Checked;
                db.PinManagaers.InsertOnSubmit(ins);
                db.SubmitChanges();

                if (CheckBox1.Checked == false)
                    ProductRequest(TextBox1.Text, 0, ins.package, ins.pinnumbers);

                pin = (Convert.ToDecimal(pin) + 1).ToString();
            }

            Session["PinOTP"] = null;
            var insert = new PinTransferHistory
            {
                transferDate = DateTime.UtcNow.AddHours(5.5),
                transferedFrom = "Admin",
                transferedTo = TextBox1.Text,
                pinnumbers = n.ToString(),
                package = drpPlan.SelectedItem.Text,
                UTR = txtUTR.Text,
                PayMode = DropDownList2.SelectedValue
            };

            string pinType = "Paid";
            if (CheckBox1.Checked == true)
                pinType = "Free";

            db.PinTransferHistories.InsertOnSubmit(insert);
            db.SubmitChanges();

            //var plan = (from c in db.tblPlans
            //            where c.PlanName == drpPlan.SelectedItem.Text
            //            select c).SingleOrDefault();
            //decimal totalAmt = n * plan.PlanAmount.Value;

            //if (CheckBox1.Checked == false && isFranch)
            //// for franch no min condition
            //{
            //    decimal percentage = 3;
            //    if (totalAmt < 100000)
            //        percentage = 2;
            //    decimal amt = totalAmt * percentage / 100;
            //    payout(amt, uname, uname, "Pin Commission");
            //}

            try
            {
                var up = (from c in db.PinRequests
                          where c.pid.ToString() == Request.QueryString["id"]
                          select c).SingleOrDefault();
                up.status = "Sent";

                db.SubmitChanges();
            }
            catch { }


            string msg = string.Format("Dear {0}, Admin has transferred {1} PINs to your ID {2}. DLS Biotech", name, n, uname);
            sm.sms(mobile, msg, "1407169591308544358");

            Response.Redirect("admin.aspx?pl=sub&n=" + n + "&pt=" + pinType);
        }
    }

    protected void payout(Decimal amt, string uname, string incomefrom, string desc)
    {

        var insert = new Payout();
        insert.username = uname;
        insert.date = DateTime.UtcNow.Date;
        insert.desc = desc;
        insert.tax = 0;
        insert.credit = amt;
        insert.debit = 0;
        insert.status = "Pending";
        insert.incomefrom = incomefrom;
        insert.remarks = desc;
        db.Payouts.InsertOnSubmit(insert);
        db.SubmitChanges();
    }

    private void ProductRequest(string uname, int pid, string plan, string pin)
    {
        RequestProduct up = new RequestProduct();
        up.username = uname;
        up.status = "Pending";
        up.date = DateTime.UtcNow.AddHours(5.5);
        up.name = plan;
        up.pin_number = pin;
        up.pinReqId = pid;
        db.RequestProducts.InsertOnSubmit(up);
        db.SubmitChanges();
    }


    public string GenerateNumber()
    {
        Random RndNum = new Random();
        int RnNum = RndNum.Next(100000000, 999999999);
        string pinnm = DateTime.Today.ToString("MMddyyyy") + RnNum.ToString();
        return pinnm;
    }

    public void CalculateAmount(object sender, EventArgs e)
    {
        if (TextBox2.Text != string.Empty && drpPlan.SelectedValue != string.Empty)
        {
            int pins = Convert.ToInt32(TextBox2.Text);
            decimal amt = Convert.ToDecimal(drpPlan.SelectedValue);

            decimal totalAmt = pins * amt;

            txtAmount.Text = totalAmt.ToString();
        }
        else
            txtAmount.Text = "";
    }

    public string GeneratePin()
    {
        string strPwdchar = "0123456789";
        string strPwd = DateTime.Today.ToString("Mddyyyy");
        Random rnd = new Random();
        for (int i = 0; i <= 8; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }

        return strPwd;
    }
}