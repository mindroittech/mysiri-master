using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_assign_pin : System.Web.UI.Page
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
                int id = Convert.ToInt32(Request.QueryString["id"]);
                var req = db.PinRequests.SingleOrDefault(x => x.pid == id);

                TextBox1.Text = req.username;
                TextBox2.Text = req.amount.ToString();
                TextBox3.Text = req.paymode;
                TextBox4.Text = req.transactionno;
                Image1.ImageUrl = req.counterfoil;
                findName(req.username);

                var plan = from c in db.tblPlans
                           join d in db.PinRequestByPlans
                           on c.PlanId equals d.planId
                           where d.pId == id
                           select new { c.PlanId, c.PlanName, c.PlanAmount, d.QtyReq, Amount = d.QtyReq.Value * c.PlanAmount.Value };
                GridView1.DataSource = plan;
                GridView1.DataBind();

                footer();
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void GenerateOTP_Click(object sender, EventArgs e)
    {
        try
        {
            sendsms sm = new sendsms();
            decimal amt = 0;
            Label lblTotal = GridView1.FooterRow.FindControl("lblTotalAmount") as Label;
            if (lblTotal != null)
                amt = Convert.ToDecimal(lblTotal.Text);
            string otp = Helper.GenerateOTP();
            Cache.Insert("PinOTP", otp, null, DateTime.UtcNow.AddHours(5.5).AddMinutes(20), TimeSpan.Zero);
            lblOtpMessage.Text = "Otp has been sent to your phone number" + " " + otp;
            string msg = otp + " is the OTP to approve the PIN Request for the ID : " + TextBox1.Text + " Name : " + txtName.Text +
                " for the amount Rs. " + amt + ". DLS Biotech";
            sm.sms("9380393866,9448415584", msg, "1407169591317075523");
           
        }
        catch
        {

        }
    }

    protected void txtQuanity_Changed(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

            TextBox txtNos = (TextBox)row.FindControl("txtNos");
            Label lblAmount = (Label)row.FindControl("lblAmount");

            if (txtNos.Text.Trim() != string.Empty)
            {
                int quantity = Convert.ToInt32(txtNos.Text);
                Decimal cost = Convert.ToDecimal(row.Cells[2].Text.Trim());
                Decimal amt = quantity * cost;
                lblAmount.Text = amt.ToString();
            }
            else
            {
                lblAmount.Text = "0";
            }

            footer();
        }
        catch
        {

        }
    }

    private void footer()
    {
        Label lblQty = GridView1.FooterRow.FindControl("lblTotalQty") as Label;
        Label lbltotal = GridView1.FooterRow.FindControl("lblTotalAmount") as Label;

        Decimal totalamt = 0;
        int totalQuantity = 0;
        foreach (GridViewRow r in GridView1.Rows)
        {
            Label lblAMt = (Label)r.FindControl("lblAmount");
            TextBox txtQuant = (TextBox)r.FindControl("txtNos");
            if (lblAMt.Text != "" && lblAMt.Text != "0")
            {
                totalamt += Convert.ToDecimal(lblAMt.Text);
                totalQuantity += Convert.ToInt32(txtQuant.Text);
            }
        }

        lblQty.Text = totalQuantity.ToString();
        lbltotal.Text = totalamt.ToString();
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

        if (Cache["PinOTP"] == null || Cache["PinOTP"].ToString() != txtOTP.Text)
        {
            Label1.Text = "Invalid OTP";
            Label1.Visible = true;
            return;
        }

        int pid = Convert.ToInt32(Request.QueryString["id"]);
        var pinReq = db.PinRequests.SingleOrDefault(x => x.pid == pid && x.status!= "Sent");

        if (pinReq == null)
        {
            Label1.Visible = true;
            Label1.Text = "Already Approved";
            return;
        }

        int totalpins = 0;
        decimal totalAmt = 0;
        foreach (GridViewRow row in GridView1.Rows)
        {
            int planId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
            string plan = row.Cells[1].Text;
            TextBox txtNos = (TextBox)row.FindControl("txtNos");

            if (txtNos.Text != string.Empty)
            {
                int n = Convert.ToInt32(txtNos.Text);
                decimal amt = Convert.ToDecimal(row.Cells[2].Text);
                totalAmt += (n * amt);
                totalpins += n;
                var pinByPlan = db.PinRequestByPlans.Where(x => x.pId == pid && x.planId == planId).SingleOrDefault();
                pinByPlan.QtySent = n;
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
                    ins.date = DateTime.UtcNow.Date;
                    ins.issued = TextBox1.Text;
                    ins.status = "Not used";
                    ins.pinnumbers = pin;
                    ins.package = plan;
                    ins.transferby = "Admin"; // System.Configuration.ConfigurationManager.AppSettings["companyname"];
                    ins.initialTransfer = TextBox1.Text;
                    ins.IsFreePin = false;
                    db.PinManagaers.InsertOnSubmit(ins);
                    db.SubmitChanges();

                    ProductRequest(pinReq.username, pid, ins.package, ins.pinnumbers);

                    pin = (Convert.ToDecimal(pin) + 1).ToString();
                }

                var insert = new PinTransferHistory
                {
                    transferDate = DateTime.UtcNow.AddHours(5.5),
                    transferedFrom = "Admin",
                    transferedTo = TextBox1.Text,
                    pinnumbers = n.ToString(),
                    package = plan
                };
                string pinType = "Paid";

                db.PinTransferHistories.InsertOnSubmit(insert);
                db.SubmitChanges();
            }
        }
        //if (isFranch)
        //{
        //    TimeSpan time=  pinReq.createdDate.Value.TimeOfDay;
        //    if (time.Hours < 18)
        //    {
        //        decimal percentage = 3;
        //        if (totalAmt < 100000)
        //            percentage = 2;
        //        decimal amt = totalAmt * percentage / 100;
        //        payout(amt, uname, uname, "Pin Commission");
        //    }
        //}

        pinReq.status = "Sent";
        pinReq.actionDate = DateTime.UtcNow.AddHours(5.5);
        db.SubmitChanges();

        string msg = string.Format("Dear {0}, Admin has transferred {1} PINs to your ID {2}. DLS Biotech", name, totalpins, uname);
        sm.sms(mobile, msg, "1407169591308544358");

        Response.Redirect("admin.aspx?pl=sub");
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

}