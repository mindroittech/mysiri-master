using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vStockTransfer : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    private static string role = "";
    private static string fcountry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string vendor = Session["vendor"].ToString();
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    int SrId = Convert.ToInt32(Request.QueryString["Id"]);
                    var get = (from c in db.StockRequests
                               join d in db.tblVendors
                               on c.VendorId equals d.Id
                               where c.SrId == SrId
                               select new { d.Username, d.VendorName, d.CashWallet, d.CompanyName, c.AmountPaid }).SingleOrDefault();
                    if (get != null)
                    {
                        txtAmount.Text = get.CashWallet.ToString();
                        txtVendor.Text = get.Username;
                        lblName.Text = get.VendorName;
                    }
                    bind(SrId);
                }
                else
                    bind();

                if (Request.QueryString["p"] == "sub")
                {
                    lblMessage.Text = "Stock transferred successfully.";
                    lblMessage.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    private int findStock(int prodId, string vendor)
    {
        int st = 0;
        var findStock = (from c in db.tblVendorStocks
                         join d in db.tblVendors
                         on c.VendorId equals d.Id
                         where d.Username == vendor && c.ProductId == prodId
                         select c).SingleOrDefault();
        if (findStock != null)
            st = findStock.Stock;

        return st;
    }
    private void bind(int SrId)
    {
        string vendor = Session["vendor"].ToString();
        var stockRequest = from d in db.tblProducts
                           join c in db.StockRequestProducts
                           on d.ProductId equals c.ProductId into grp
                           from g in grp.DefaultIfEmpty()
                           where g.SrId == SrId
                           select new
                           {
                               d.ProductId,
                               d.ProductName,
                               d.ImageUrl,
                               Cost = d.DP,
                               Quantity = findStock(d.ProductId, vendor),
                               g.QtyReq,
                               g.QtySent
                           };
        GridView1.DataSource = stockRequest.ToList();
        GridView1.DataBind();

        Decimal totalamt = 0;
        int totalQuantity = 0;
        foreach (GridViewRow r in GridView1.Rows)
        {
            Decimal cost = Convert.ToDecimal(r.Cells[3].Text);
            TextBox txtQuant = (TextBox)r.FindControl("txtNos");
            Label lblAMt = (Label)r.FindControl("lblAmount");
            if (txtQuant.Text != "" && txtQuant.Text != "0")
            {
                int quant = Convert.ToInt32(txtQuant.Text);
                totalamt += quant * cost;
                totalQuantity += quant;
                lblAMt.Text = (quant * cost).ToString();
            }
        }
        lblCost.Text = totalamt.ToString();
        lblTotalQty.Text = totalQuantity.ToString();
    }
    private void bind()
    {
        string vendor = Session["vendor"].ToString();
        var stockRequest = from d in db.tblProducts
                           join c in db.tblVendorStocks
                           on d.ProductId equals c.ProductId
                           join f in db.tblVendors
                           on c.VendorId equals f.Id
                           where d.IsActive == true && f.Username == vendor
                           select new
                           {
                               d.ProductId,
                               d.ProductName,
                               d.ImageUrl,
                               Cost = d.DP,
                               Quantity = c.Stock,
                               QtyReq = "",
                               QtySent = ""

                           };
        GridView1.DataSource = stockRequest.ToList();
        GridView1.DataBind();
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
                Decimal cost = Convert.ToDecimal(row.Cells[3].Text.Trim());

                Decimal amt = quantity * cost;
                lblAmount.Text = amt.ToString();

            }
            else
            {
                lblAmount.Text = "0";
            }

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
            lblCost.Text = totalamt.ToString();
            lblTotalQty.Text = totalQuantity.ToString();
            txtNos.Focus();
        }
        catch
        {
            lblCost.Text = "0";
        }
    }
    protected void txtVendor_Changed(object sender, EventArgs e)
    {
        try
        {
            string username = txtVendor.Text;
            var user = db.tblVendors.SingleOrDefault(x => x.Username == username);
            if (user != null)
            {
                lblName.Text = user.CompanyName;
                txtBilling.Text = user.Address + " " + user.Pincode;
                Button1.Enabled = true;
                role = db.tblRoles.Single(x => x.RoleId == user.RoleId).RoleName;
                fcountry = user.Country;

                txtAmount.Text = user.CashWallet.ToString();
                bind();
            }
            else
            {
                lblName.Text = "";
                txtBilling.Text = "";
                Button1.Enabled = false;
                role = "";
                fcountry = "";
                txtAmount.Text = "";
            }
        }
        catch
        {

        }
    }

    protected void drpDelivery_Changed(object sender, EventArgs e)
    {
        if (drpDelivery.SelectedValue == "Courier")
            divCourier.Visible = true;
        else
            divCourier.Visible = false;
    }

    protected void GenerateOTP_Click(object sender, EventArgs e)
    {
        try
        {
            sendsms sm = new sendsms();
            decimal amt = 0;

            amt = Convert.ToDecimal(lblCost.Text);
            string otp = Helper.GenerateOTP();
            string vendor = Session["vendor"].ToString();
            var me = db.tblVendors.Single(x => x.Username == vendor);
            Cache.Insert("StockOTP", otp, null, DateTime.UtcNow.AddHours(5.5).AddMinutes(20), TimeSpan.Zero);
            lblOtpMessage.Text = "Otp has been sent to your phone number";
            string msg = otp + " is the OTP to approve the Stock Request for the ID : " + txtVendor.Text + " Name : " + lblName.Text +
                " for the amount Rs. " + amt + ". DLS Biotech";
           // sm.sms("9380393866,9448415584", msg, "1407169591337595193");
            sm.sms(me.Phone, msg, "1407169591337595193");
            //sm.sms("8208595348", msg, "1407169591337595193");

        }
        catch
        {

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = lblMessage.Visible = false;
        string vendor = Session["vendor"].ToString();
        var me = db.tblVendors.Single(x => x.Username == vendor);
        var role = db.tblRoles.SingleOrDefault(x => x.RoleId == me.RoleId);
        var vendorToTransfer = (from c in db.tblVendors
                                where c.Username == txtVendor.Text
                                select c).SingleOrDefault();
        if (vendorToTransfer == null)
        {
            lblErrorMessage.Text = "ID not found";
            lblErrorMessage.Visible = true;
            return;
        }
        if (vendorToTransfer.Username == me.Username)
        {
            lblErrorMessage.Text = "Cannot transfer to your ID";
            lblErrorMessage.Visible = true;
            return;
        }
        decimal totalAmt = Convert.ToDecimal(lblCost.Text);

        //if (totalAmt > vendorToTransfer.CashWallet)
        //{
        //    lblErrorMessage.Text = "Franchisee doesn't have required balance in Cash Wallet.";
        //    lblErrorMessage.Visible = true;
        //    return;
        //}

        if (Cache["StockOTP"] == null || Cache["StockOTP"].ToString() != txtOTP.Text)
        {
            lblErrorMessage.Text = "Invalid OTP";
            lblErrorMessage.Visible = true;
            return;
        }

        int inv = 1001;
        var checkInv = (from c in db.StockRequests
                        where c.InvoiceNo != null
                        orderby c.InvoiceNo descending
                        select c).FirstOrDefault();
        if (checkInv != null)
        {
            inv = Convert.ToInt32(checkInv.InvoiceNo) + 1;
        }

        decimal commission = 0;
        if (Request.QueryString["Id"] != null)
        {
            int SrId = Convert.ToInt32(Request.QueryString["Id"]);

            var sr = db.StockRequests.SingleOrDefault(x => x.SrId == SrId && x.RequestTo == me.Id);
            if (sr != null)
            {
                sr.SentDate = DateTime.UtcNow.AddHours(5.5);
                sr.SentBy = me.Id;

                sr.Status = "Processed";
                sr.Delivery = drpDelivery.SelectedItem.Text;
                sr.InvoiceNo = inv.ToString();

                if (sr.Delivery == "Courier")
                {
                    sr.Courier = drpCourier.SelectedItem.Text;
                    sr.AwbNo = txtAWB.Text;
                }
                List<StockRequestProduct> lstPurchase = new List<StockRequestProduct>();
                decimal billedAmt = 0;
                decimal refund = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    int prodId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    TextBox txtNos = (TextBox)row.FindControl("txtNos");
                    if (txtNos.Text != "" && txtNos.Text != "0")
                    {
                        int quant = Convert.ToInt32(txtNos.Text);
                        decimal cost = Convert.ToDecimal(row.Cells[3].Text);
                        int availableQuant = Convert.ToInt32(row.Cells[4].Text);
                        int requestedQuant = Convert.ToInt32(row.Cells[5].Text);
                        string product = row.Cells[1].Text;
                        Label lblAmount = (Label)row.FindControl("lblAmount");
                        if (availableQuant < quant)
                        {
                            lblErrorMessage.Text = "Product " + product + " doesn't have sufficient stock to transfer.";
                            lblErrorMessage.Visible = true;
                            return;
                        }
                        if (requestedQuant > quant)
                        {
                            int remaining = requestedQuant - quant;
                            refund = cost * remaining;
                            me.CashWallet -= refund;
                            Debit(me, refund, "Debit", "Towards Stock Refund "+ product);

                            vendorToTransfer.CashWallet += refund;
                            Credit(vendorToTransfer, refund, "Credit", "Towards Stock Refund-"+" "+product + me.VendorName, me.Username);
                        }
                        billedAmt += (quant * cost);
                        var purchase = new StockRequestProduct
                        {
                            ProductId = prodId,
                            QtySent = quant,
                            SrId = SrId,
                            amount = Convert.ToDecimal(lblAmount.Text)
                        };
                        lstPurchase.Add(purchase);
                    }
                }
                if (lstPurchase.Count == 0)
                {
                    lblErrorMessage.Text = "No product is selected.";
                    lblErrorMessage.Visible = true;
                    return;
                }

                commission = billedAmt * role.bonusPercent.Value / 100;

                sr.commission = commission;
                sr.billedAmt = billedAmt;
                var SRProduct = from d in lstPurchase
                                join c in db.StockRequestProducts
                                on d.ProductId equals c.ProductId
                                where c.SrId == SrId
                                select new { c, d };
                SRProduct.ToList().ForEach(x => { x.c.QtySent = x.d.QtySent; x.c.amount = x.d.amount; });
                // my stock update
                var products = from d in lstPurchase
                               join c in db.tblVendorStocks
                               on d.ProductId equals c.ProductId
                               where c.VendorId == me.Id
                               select new { c, d };
                products.ToList().ForEach(x => x.c.Stock = x.c.Stock - x.d.QtySent.Value);
                db.SubmitChanges();


                me.RepurchaseWallet += commission; // commission only for C&F

                db.SubmitChanges();
                //me.CashWallet += billedAmt;
                //Credit(me, totalAmt, "Credit", "Towards Stock Sale-" + vendorToTransfer.VendorName, vendorToTransfer.Username);

                //vendorToTransfer.CashWallet -= billedAmt;
                //Debit(vendorToTransfer, totalAmt, "Debit", "Towards Stock Purchase");

            }
            Response.Redirect("StockRequests.aspx?p=sub");
        }
        else
        {
            var req = new StockRequest
            {
                VendorId = vendorToTransfer.Id,
                AmountPaid = totalAmt,
                SentDate = DateTime.UtcNow.AddHours(5.5),
                Status = "Processed",
                Delivery = drpDelivery.SelectedItem.Text,
                PayMode = "Cash",
                SentBy = me.Id,
                IsActive = true,
                RequestType = "Product",
                InvoiceNo = inv.ToString()
            };
            if (req.Delivery == "Courier")
            {
                req.Courier = drpCourier.SelectedItem.Text;
                req.AwbNo = txtAWB.Text;
            }
            db.StockRequests.InsertOnSubmit(req);
            db.SubmitChanges();
            List<StockRequestProduct> lstPurchase = new List<StockRequestProduct>();
            decimal billedAmt = 0;
            decimal refund = 0;
            foreach (GridViewRow row in GridView1.Rows)
            {
                int prodId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                TextBox txtNos = (TextBox)row.FindControl("txtNos");

                if (txtNos.Text != "" && txtNos.Text != "0")
                {
                    int quant = Convert.ToInt32(txtNos.Text);
                    decimal cost = Convert.ToDecimal(row.Cells[3].Text);
                    int availableQuant = Convert.ToInt32(row.Cells[4].Text);
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    int requestedQuant = Convert.ToInt32(row.Cells[5].Text);
                    string product = row.Cells[1].Text;
                    if (availableQuant < quant)
                    {
                        lblErrorMessage.Text = "Product " + product + " doesn't have sufficient stock to transfer.";
                        lblErrorMessage.Visible = true;
                        return;
                    }
                    if (requestedQuant > quant)
                    {
                        int remaining = requestedQuant - quant;
                        refund = cost * remaining;
                        me.CashWallet -= refund;
                        Debit(me, refund, "Debit", "Towards Stock Refund " + product);

                        vendorToTransfer.CashWallet += refund;
                        Credit(vendorToTransfer, refund, "Credit", "Towards Stock Refund-" + " " + product + me.VendorName, me.Username);
                    }
                    billedAmt += (quant * cost);
                    var repurchase = new StockRequestProduct
                    {
                        ProductId = prodId,
                        QtySent = quant,
                        SrId = req.SrId,
                        amount = Convert.ToDecimal(lblAmount.Text)
                    };
                    lstPurchase.Add(repurchase);
                }
            }
            if (lstPurchase.Count == 0)
            {
                lblErrorMessage.Text = "No product is selected.";
                lblErrorMessage.Visible = true;
                return;
            }
            req.billedAmt = billedAmt;

            commission = billedAmt * role.bonusPercent.Value / 100;
            req.commission = commission;
            db.StockRequestProducts.InsertAllOnSubmit(lstPurchase);
            /// my stock update
            var products = from d in lstPurchase
                           join c in db.tblVendorStocks
                           on d.ProductId equals c.ProductId
                           where c.VendorId == me.Id
                           select new { c, d };
            products.ToList().ForEach(x => x.c.Stock = x.c.Stock - x.d.QtySent.Value);
            db.SubmitChanges();

            me.RepurchaseWallet += commission; // commission only for C&F
            db.SubmitChanges();
            //me.CashWallet += billedAmt;
            //Credit(me, totalAmt, "Credit", "Towards Stock Sale-" + vendorToTransfer.VendorName, vendorToTransfer.Username);

            //vendorToTransfer.CashWallet -= billedAmt;
            //Debit(vendorToTransfer, totalAmt, "Debit", "Towards Stock Purchase");

            Response.Redirect("vStockTransfer.aspx?p=sub");
        }
    }

    private void Credit(tblVendor franch, decimal Amount, string desc, string remarks, string incomeFrom)
    {
        var history = new FranchiseeWallet
        {
            username = franch.Username,
            credit = Amount,
            debit = 0,
            date = DateTime.UtcNow.AddHours(5.5).Date,
            confirmdate = DateTime.UtcNow.AddHours(5.5),
            desc = desc,
            remarks = remarks,
            status = "Send",
            IncomeFrom = incomeFrom
        };
        db.FranchiseeWallets.InsertOnSubmit(history);
        db.SubmitChanges();

        string msg = string.Format("Dear {0}, Your Cash Wallet is credited with Rs {1}. Current Balance is Rs {2}. DLS Biotech", franch.VendorName, Amount, franch.CashWallet);
        sm.sms(franch.Phone, msg, "1407169591301077821");
    }

    private void Debit(tblVendor franch, decimal Amount, string desc, string remarks)
    {
        var history = new FranchiseeWallet
        {
            username = franch.Username,
            credit = 0,
            debit = Amount,
            date = DateTime.UtcNow.AddHours(5.5).Date,
            confirmdate = DateTime.UtcNow.AddHours(5.5),
            desc = desc,
            remarks = remarks,
            status = "Send",
        };
        db.FranchiseeWallets.InsertOnSubmit(history);
        db.SubmitChanges();

        string msg = string.Format("Dear {0}, Your Cash Wallet is debited with Rs {1}. Current Balance is Rs {2}. DLS Biotech", franch.VendorName, Amount, franch.CashWallet);
        sm.sms(franch.Phone, msg, "1407169591304792948");
    }
}