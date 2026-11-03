using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

public partial class Admin_CreateOrder : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string role = Session["role"].ToString();
            if (!IsPostBack)
            {
                RadioButtonList1.SelectedIndex = 0;
                bind("Member");

            }
        }
        catch (Exception)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void bind(string type)
    {
        var products = from c in db.tblProducts
                       where c.IsActive == true && c.Quantity > 0
                       select new
                       {
                           c.ProductName,
                           Cost = type == "Member" ? c.DP : c.MRP,
                           PV = c.PV,
                           FranchiseePrice = c.DP,
                           c.ImageUrl,
                           c.ProductId,
                           Stock = c.Quantity,
                           Free = c.FreeEligible.Value ? "Yes" : "No"

                       };
        GridView1.DataSource = products;
        GridView1.DataBind();
    }


    protected void drpPayMode_Changed(object sender, EventArgs e)
    {
        tmOnline.Visible = false;
        tmWallet.Visible = false;
        if (drpPayMode.SelectedItem.Text == "Product Wallet")
        {
            tmWallet.Visible = true;
        }
        else if (drpPayMode.SelectedItem.Text == "Online")
        {
            tmOnline.Visible = true;
        }
    }

    private static string GenerateOTP()
    {
        string strPwdchar = "1234567890";
        string otp = "";
        Random rnd = new Random();
        for (int i = 0; i <= 5; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            otp += strPwdchar.Substring(iRandom, 1);
        }

        return otp;
    }



    //protected void GenerateOTP_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        var d = (from c in db.tblMembers
    //                 where c.username == txtUser.Text.Trim()
    //                 select c).SingleOrDefault();
    //        sendsms sm = new sendsms();
    //        EmailClass email = new EmailClass();
    //        if (d != null)
    //        {
    //            string otp = GenerateOTP();
    //            Cache.Insert("OTP", otp, null, DateTime.UtcNow.AddHours(5.5).AddMinutes(20), TimeSpan.Zero);
    //            lblOtpMessage.Text = "Otp has been sent to your phone number";
    //            string msg = "Dear " + d.name + ", OTP for your Wallet Debit is " + otp + ". DLS Biotech";
    //            sm.sms(d.mobile, msg, "1207162479530567574");
    //        }
    //        else
    //        {

    //        }
    //    }
    //    catch
    //    {

    //    }
    //}


    protected void RadioButtonList1_Changed(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue != "Member")
        {
            guestName.Visible = true;
            payMode.Visible = false;
            Div1.Visible = false;
            Div2.Visible = false;
        }
        else
        {
            guestName.Visible = false;
            payMode.Visible = true;
            Div1.Visible = true;
            Div2.Visible = true;
        }
        bind(RadioButtonList1.SelectedValue);
        lblCost.Text = "0";
        lblTotalBv.InnerText = "0";
        lblTotalQty.Text = "0";

    }
    protected void txtUser_Changed(object sender, EventArgs e)
    {
        try
        {
            var user = db.tblMembers.SingleOrDefault(x => x.username == txtUser.Text && x.isActive == 1);
            lblName.Text = user.name;
            lblErrorMessage.Visible = false;
            lblTMWallet.Text = user.RepurchaseWallet.ToString();

            var bvs = db.tblBVs.Where(x => x.username == user.username && x.status == "Pending" && x.bvType == "Repurchase");

            var left = bvs.Where(x => x.desc == "Left");
            var right = bvs.Where(x => x.desc == "Right");

            decimal currentLeftBv = 0;
            decimal currentRightBv = 0;
            if (left.Any())
            {
                currentLeftBv = left.Sum(x => x.leftBv.Value);
                lblleft.Text = currentLeftBv.ToString();
            }
            if (right.Any())
            {
                currentRightBv = right.Sum(x => x.rightBv.Value);
                lblright.Text = currentRightBv.ToString();
            }

        }
        catch
        {
            lblName.Text = "";
            lblErrorMessage.Text = "ID does not exist.";
            lblErrorMessage.Visible = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string uname = txtUser.Text;
        string userType = RadioButtonList1.SelectedValue;
        string admin = Session["admin"].ToString();
        var franch = db.tblVendors.Where(x => x.Username == admin).SingleOrDefault();
        var user = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
        if (user == null)
        {
            lblErrorMessage.Text = "ID does not exist.";
            lblErrorMessage.Visible = true;
            return;
        }

        decimal totalAmt = Convert.ToDecimal(lblCost.Text);

        if (totalAmt == 0)
        {
            lblErrorMessage.Text = "Please select atleast one product.";
            lblErrorMessage.Visible = true;
            return;
        }

        if (drpPayMode.SelectedItem.Text == "Product Wallet" && totalAmt > user.RepurchaseWallet)
        {
            lblErrorMessage.Text = "User does not have sufficient balance in Product Wallet.";
            lblErrorMessage.Visible = true;
            return;
        }

        bool freeEligible = true;
        foreach (GridViewRow row in GridView1.Rows)
        {
            int prodId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
            TextBox txtNos = (TextBox)row.FindControl("txtNos");
            if (txtNos.Text != "" && txtNos.Text != "0")
            {
                int quant = Convert.ToInt32(txtNos.Text);
                int availableQuant = Convert.ToInt32(row.Cells[5].Text);
                string product = row.Cells[1].Text;
                if (availableQuant < quant)
                {
                    lblErrorMessage.Text = "Product " + product + " doesn't have sufficient stock to transfer.";
                    lblErrorMessage.Visible = true;
                    return;
                }
                if (row.Cells[6].Text == "No")
                {
                    freeEligible = false;
                }
            }
        }
        DateTime date = DateTime.UtcNow.AddHours(5.5);
        decimal PV = Convert.ToDecimal(lblTotalBv.InnerText);


        if (drpPayMode.SelectedItem.Text == "Product Wallet")
        {
            if (freeEligible == false)
            {
                lblErrorMessage.Text = "Selected Products are not eligible for Wallet Purchase.";
                lblErrorMessage.Visible = true;
                return;
            }

            user.RepurchaseWallet -= totalAmt;
            var history = new Payout
            {
                username = uname,
                credit = 0,
                debit = totalAmt,
                date = date.Date,
                confirmdate = date,
                desc = "Product Wallet Debit",
                remarks = "Towards Repurchase",
                status = "Send"
            };

            db.Payouts.InsertOnSubmit(history);
            db.SubmitChanges();
        }

        int number = 10000;
        var getLast = db.tblInvoices.Where(x => x.InvoiceNo != null).OrderByDescending(x => x.InvoiceNo).FirstOrDefault();
        if (getLast != null)
        {
            number = Convert.ToInt32(getLast.InvoiceNo);
        }

        string purchaseType = "Repurchase";
        string address = null;
        string dispatched = user.name;
        string payMode = drpPayMode.SelectedItem.Text;
        string mobile = user.mobile;

        if (userType == "Guest")
        {
            purchaseType = "Order";
            address = txtAddress.Text;
            dispatched = txtDispatched.Text;
            mobile = txtMobile.Text;
            payMode = "Cash";
        }

        var invoice = new tblInvoice
        {
            username = uname,
            Amount = totalAmt,
            PurchaseDate = date.Date,
            Name = purchaseType,
            DeliveredOn = date.Date,
            DispachedOn = date,
            PayMode = payMode,
            status = "Delivered",
            DeliveryType = 1,
            InvoiceNo = (number + 1).ToString(),
            BV = PV,
            userType = userType,
            RepurchaseCommission = 0,
            dispatchedTo = dispatched,
            CourierAddress = address,
            AWBNo = mobile,
            invType = "Paid",
            FranchiseeId = franch.Id,
            remarks = txtUTR.Text
        };

        if (FileUpload1.HasFile)
        {
            string fileName = uname + "_Inv_" + FileUpload1.FileName;
            invoice.attachment = "../Files/" + fileName;
            FileUpload1.SaveAs(Server.MapPath("~/Files/" + fileName));
        }

        db.tblInvoices.InsertOnSubmit(invoice);
        db.SubmitChanges();
        //franchPay(franchisee.Username, FranchCommission, invoice.InvoiceId.ToString(), "Repurchase Commission");

        List<tblRepurchase> lstPurchase = new List<tblRepurchase>();
        foreach (GridViewRow row in GridView1.Rows)
        {
            int prodId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
            TextBox txtNos = (TextBox)row.FindControl("txtNos");
            Label lblAmount = (Label)row.FindControl("lblAmount");
            Label lblPV = (Label)row.FindControl("lblPV");
            if (txtNos.Text != "" && txtNos.Text != "0")
            {
                int quant = Convert.ToInt32(txtNos.Text);
                var repurchase = new tblRepurchase
                {
                    username = uname,
                    purchaseDate = date.Date,
                    amount = Convert.ToDecimal(lblAmount.Text),
                    RepurchaseBV = Convert.ToDecimal(lblPV.Text),
                    Qty = quant,
                    product_id = prodId,
                    InvoiceId = invoice.InvoiceId
                };
                lstPurchase.Add(repurchase);
            }
        }
        db.tblRepurchases.InsertAllOnSubmit(lstPurchase);
        db.SubmitChanges();

        // my stock update
        var products = from d in lstPurchase
                       join c in db.tblProducts
                       on d.product_id equals c.ProductId
                       select new { c, d };
        products.ToList().ForEach(x => x.c.Quantity = x.c.Quantity - x.d.Qty.Value);
        db.SubmitChanges();

        Task.Factory.StartNew(() =>
        {
            var gen = db.tblMembers.Where(x => x.username == user.username).SingleOrDefault();
            if (gen != null && PV > 0)
            {
                //self BV
                AddBv(gen.username, PV, date, "Self", "", gen.username, invoice.InvoiceId, "Repurchase");

                string selectedLeg = "";

                if (rbLeft.Checked)
                    selectedLeg = "Left";
                else if (rbRight.Checked)
                    selectedLeg = "Right";

                //self BV
                AddBv(gen.username, PV, date, selectedLeg, "", gen.username, invoice.InvoiceId, "Repurchase");

                //Level BV
                int refid = gen.refid.Value;
                string leg = gen.leg;
                int i = 0;
                while (refid > 0)
                {
                    i++;
                    var bv = (from c in db.tblMembers
                              where c.id == refid
                              select c).SingleOrDefault();
                   
                    if(bv.status=="Paid")
                        AddBv(bv.username, PV, date, leg, i.ToString(), gen.username, invoice.InvoiceId, "Repurchase");

                    refid = bv.refid.Value;
                    leg = bv.leg;
                    if (refid == 0)
                    {
                        break;
                    }
                }
            }
        });

        string msg = "Dear " + dispatched + ", You have purchased products worth Rs " + totalAmt + ". Thanks for shopping with DLS Biotech";
        sm.sms(mobile, msg, "1407169591270393749");

        Response.Redirect("mPurchaseHistory.aspx?o=" + invoice.InvoiceId);
    }

    private void AddBv(string uname, decimal pv, DateTime date, string desc, string remarks, string bvfrom, int invId, string type)
    {
        var history = new tblBV
        {
            username = uname,
            Bv = 0,
            leftBv = 0,
            rightBv = 0,
            bvFrom = bvfrom,
            date = date.Date,
            desc = desc,
            remarks = remarks,
            status = "Pending",
            invId = invId,
            lcarry = 0,
            rcarry = 0,
            bvType = type
        };
        if (desc == "Self")
            history.Bv = pv;
        else if (desc == "Left")
            history.leftBv = pv;
        else if (desc == "Right")
            history.rightBv = pv;

        db.tblBVs.InsertOnSubmit(history);
        db.SubmitChanges();
    }

    private void Payout(string uname, Decimal amt, string desc, string remarks, string incomefrom, Decimal Pv, DateTime date,
        Decimal invoiceId, Decimal tax = 0)
    {
        var history = new Payout
        {
            username = uname,
            credit = amt,
            debit = 0,
            netpayout = amt - tax,
            tax = tax,
            date = date.Date,
            desc = desc,
            remarks = remarks,
            incomefrom = incomefrom,
            status = "Pending",
            reward = invoiceId,

        };

        db.Payouts.InsertOnSubmit(history);
        db.SubmitChanges();
    }

    protected void txtQuanity_Changed(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

            TextBox txtNos = (TextBox)row.FindControl("txtNos");
            Label lblAmount = (Label)row.FindControl("lblAmount");
            Label lblPV = (Label)row.FindControl("lblPV");
            Label lblVAT = (Label)row.FindControl("lblVAT");
            Label lblExcVAT = (Label)row.FindControl("lblExcVAT");
            if (txtNos.Text.Trim() != string.Empty)
            {
                int quantity = Convert.ToInt32(txtNos.Text);
                Decimal cost = Convert.ToDecimal(row.Cells[3].Text.Trim());
                Decimal pv = Convert.ToDecimal(row.Cells[4].Text.Trim());
                Decimal amt = quantity * cost;

                lblAmount.Text = Math.Round(amt, 2).ToString();
                lblPV.Text = (quantity * pv).ToString();
            }
            else
            {
                lblAmount.Text = "0";
                lblPV.Text = "";
            }

            Decimal totalamt = 0;
            int totalQuantity = 0;
            Decimal totalPV = 0;

            foreach (GridViewRow r in GridView1.Rows)
            {
                Label lblAMt = (Label)r.FindControl("lblAmount");
                Label lblPV1 = (Label)r.FindControl("lblPV");
                Label lblTotalExcVAT = (Label)r.FindControl("lblExcVAT");
                TextBox txtQuant = (TextBox)r.FindControl("txtNos");
                if (lblAMt.Text != "" && lblAMt.Text != "0")
                {
                    totalamt += Convert.ToDecimal(lblAMt.Text);
                    totalQuantity += Convert.ToInt32(txtQuant.Text);
                    totalPV += Convert.ToDecimal(lblPV1.Text);
                }
            }
            lblCost.Text = totalamt.ToString();
            lblTotalBv.InnerText = totalPV.ToString();
            lblTotalQty.Text = totalQuantity.ToString();
        }
        catch
        {
            lblCost.Text = "0";
            lblTotalBv.InnerText = "";
        }
    }
}