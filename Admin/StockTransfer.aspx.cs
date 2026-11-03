using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Admin_stock_transfer : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    private static string role = "";
    private static string fcountry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string adminUSer = Session["admin"].ToString();
            if (!IsPostBack)
            {
                var vendors = from c in db.tblVendors
                              join d in db.tblRoles
                              on c.RoleId equals d.RoleId
                              where c.IsActive == true && d.RoleName != "Admin"
                              select new { c.Username, c.CompanyName };
                drpVendor.DataSource = vendors;
                drpVendor.DataTextField = "CompanyName";
                drpVendor.DataValueField = "Username";
                drpVendor.DataBind();
                drpVendor.Items.Insert(0, new ListItem("--Select--", ""));
                if (Request.QueryString["Id"] != null)
                {
                    int SrId = Convert.ToInt32(Request.QueryString["Id"]);


                    var get = (from c in db.StockRequests
                               join d in db.tblVendors
                               on c.VendorId equals d.Id
                               where c.SrId == SrId
                               select new { d.Username, d.RoleId, d.CompanyName, d.CashWallet, c.AmountPaid }).SingleOrDefault();
                    if (get != null)
                    {
                        txtAmount.Text = get.CashWallet.ToString();
                        drpVendor.SelectedValue = get.Username;
                        txtVendor_Changed(null, null);
                        drpVendor.Enabled = false;
                        //lblName.Text = get.CompanyName;
                    }
                    bind(SrId);
                }
                else
                {
                    bind();
                }

                if (Request.QueryString["p"] == "sub")
                {
                    lblMessage.Text = "Stock transferred successfully.";
                    lblMessage.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    private void bind(int SrId)
    {
        string vendor = Session["admin"].ToString();
        var stockRequest = from c in db.StockRequestProducts
                           join f in db.StockRequests
                           on c.SrId equals f.SrId
                           join d in db.tblProducts
                           on c.ProductId equals d.ProductId
                           where c.SrId == SrId
                           select new
                           {
                               c.ProductId,
                               d.ProductName,
                               d.ImageUrl,
                               Cost = d.DP,
                               Quantity = d.Quantity.HasValue ? d.Quantity.Value : 0,
                               c.QtyReq,
                               c.QtySent
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
        string vendor = Session["admin"].ToString();
        var stockRequest =
                           from d in db.tblProducts
                           where d.IsActive == true && d.AwardEligible == false
                           select new
                           {
                               d.ProductId,
                               d.ProductName,
                               d.ImageUrl,
                               Cost = d.DP,
                               Quantity = d.Quantity.HasValue ? d.Quantity.Value : 0,
                               QtyReq = "",
                               QtySent = ""

                           };
        GridView1.DataSource = stockRequest.ToList();
        GridView1.DataBind();
    }

    protected void txtVendor_Changed(object sender, EventArgs e)
    {
        try
        {
            string username = drpVendor.SelectedValue;
            var user = db.tblVendors.SingleOrDefault(x => x.Username == username);
            if (user != null)
            {
                lblName.Text = user.Username;
                txtBilling.Text = user.Address + " " + user.Pincode;
                Button1.Enabled = true;
                txtType.Text = role = db.tblRoles.Single(x => x.RoleId == user.RoleId).RoleName;
                fcountry = user.Country;
                txtAmount.Text = user.CashWallet.ToString();
                bind();
            }
            else
            {
                lblName.Text = "";
                txtBilling.Text = "";
                Button1.Enabled = false;
                txtType.Text = role = "";
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
        {
            divCourier.Visible = true;
        }
        else
        {
            divCourier.Visible = false;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = lblMessage.Visible = false;
        string admin = Session["admin"].ToString();
        var me = db.tblVendors.Single(x => x.Username == admin);

        string username = drpVendor.SelectedValue;

        var vendor = (from c in db.tblVendors
                      where c.Username == username
                      select c).SingleOrDefault();
        if (vendor == null)
        {
            lblErrorMessage.Text = "ID not found";
            lblErrorMessage.Visible = true;
            return;
        }
        if (vendor.Username == me.Username)
        {
            lblErrorMessage.Text = "Cannot transfer to your ID";
            lblErrorMessage.Visible = true;
            return;
        }
        decimal totalAmt = Convert.ToDecimal(lblCost.Text);

        //if (totalAmt > vendor.CashWallet)
        //{
        //    lblErrorMessage.Text = "Franchisee doesn't have required balance in Cash Wallet.";
        //    lblErrorMessage.Visible = true;
        //    return;
        //}


        int inv = 1001;
        var checkInv = (from c in db.StockRequests
                        where c.InvoiceNo != null
                        orderby c.InvoiceNo descending
                        select c).FirstOrDefault();
        if (checkInv != null)
        {
            inv = Convert.ToInt32(checkInv.InvoiceNo) + 1;
        }

        if (Request.QueryString["Id"] != null)
        {
            int SrId = Convert.ToInt32(Request.QueryString["Id"]);

            var sr = db.StockRequests.SingleOrDefault(x => x.SrId == SrId);
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
                sr.billingAddress = txtBilling.Text;
                sr.isFree = CheckBox1.Checked;
                List<StockRequestProduct> lstPurchase = new List<StockRequestProduct>();
                foreach (GridViewRow row in GridView1.Rows)
                {
                    int prodId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    var prod = db.tblProducts.SingleOrDefault(x => x.ProductId == prodId);
                    TextBox txtNos = (TextBox)row.FindControl("txtNos");
                    if (txtNos.Text != "" && txtNos.Text != "0")
                    {
                        int quant = Convert.ToInt32(txtNos.Text);
                        int availableQuant = Convert.ToInt32(row.Cells[4].Text);
                        string product = row.Cells[1].Text;
                        Label lblAmount = (Label)row.FindControl("lblAmount");
                        if (availableQuant < quant)
                        {
                            lblErrorMessage.Text = "Product " + product + " doesn't have sufficient stock to transfer.";
                            lblErrorMessage.Visible = true;
                            return;
                        }
                        var purchase = new StockRequestProduct
                        {
                            ProductId = prodId,
                            QtySent = quant,
                            SrId = SrId,
                            amount = Convert.ToDecimal(lblAmount.Text),
                            gst = prod.GST
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

                // uncommented on 20/7/2024 (araju)

                var stockCost = Convert.ToDecimal(lblCost.Text);
                if (sr.AmountPaid > stockCost)
                {
                    //   vendor.CashWallet += (sr.AmountPaid - stockCost);  // add extra remaining amount to cash wallet
                    decimal refund = sr.AmountPaid - stockCost;
                    vendor.CashWallet += refund;

                    Credit(vendor, refund, "Credit", "Towards Stock Refund-" + " "+ me.VendorName, me.Username);
                }
                var SRProduct = from d in lstPurchase
                                join c in db.StockRequestProducts
                                on d.ProductId equals c.ProductId
                                where c.SrId == SrId
                                select new { c, d };
                SRProduct.ToList().ForEach(x => { x.c.QtySent = x.d.QtySent; x.c.amount = x.d.amount; });
                // admin stock update
                var products = from d in lstPurchase
                               join c in db.tblProducts
                               on d.ProductId equals c.ProductId
                               select new { c, d };
                products.ToList().ForEach(x => x.c.Quantity = x.c.Quantity - x.d.QtySent);
                db.SubmitChanges();
                //vendor stock update once he accepts product

               // Commenting the deduct code bcz already we deduct the bill amount while user placing the order dt.20/7/2024 (araju)    

                //vendor.CashWallet -= totalAmt;
                //Debit(vendor, totalAmt, "Debit", "Towards Stock Purchase");
            }
            Response.Redirect("StockRequests.aspx?p=sub");
        }
        else
        {
            var req = new StockRequest
            {
                VendorId = vendor.Id,
                AmountPaid = totalAmt,
                SentDate = DateTime.UtcNow.AddHours(5.5),
                Status = "Processed",
                Delivery = drpDelivery.SelectedItem.Text,
                PayMode = "Cash",
                SentBy = me.Id,
                RequestType = "Product",
                IsActive = true,
                billingAddress = txtBilling.Text,
                InvoiceNo = inv.ToString(),
                isFree = CheckBox1.Checked
            };
            if (req.Delivery == "Courier")
            {
                req.Courier = drpCourier.SelectedItem.Text;
                req.AwbNo = txtAWB.Text;
            }
            db.StockRequests.InsertOnSubmit(req);

            List<StockRequestProduct> lstPurchase = new List<StockRequestProduct>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                int prodId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                TextBox txtNos = (TextBox)row.FindControl("txtNos");

                if (txtNos.Text != "" && txtNos.Text != "0")
                {
                    int quant = Convert.ToInt32(txtNos.Text);
                    var prod = db.tblProducts.SingleOrDefault(x => x.ProductId == prodId);
                    int availableQuant = Convert.ToInt32(row.Cells[4].Text);
                    string product = row.Cells[1].Text;
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    if (availableQuant < quant)
                    {
                        lblErrorMessage.Text = "Product " + product + " doesn't have sufficient stock to transfer.";
                        lblErrorMessage.Visible = true;
                        return;
                    }
                    var repurchase = new StockRequestProduct
                    {
                        ProductId = prodId,
                        QtySent = quant,
                        amount = Convert.ToDecimal(lblAmount.Text),
                        gst = prod.GST
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
            db.SubmitChanges();
            lstPurchase.ForEach(x => x.SrId = req.SrId);
            db.StockRequestProducts.InsertAllOnSubmit(lstPurchase);
            //Less admin stock
            var products = from d in lstPurchase
                           join c in db.tblProducts
                           on d.ProductId equals c.ProductId
                           select new { c, d };
            products.ToList().ForEach(x => x.c.Quantity = x.c.Quantity - x.d.QtySent);
            db.SubmitChanges();
            
            
            //vendor stock update
            //foreach (var lst in lstPurchase)
            //{
            //    var vendorstock = db.tblVendorStocks.Where(x => x.VendorId == vendor.Id && x.ProductId == lst.ProductId).SingleOrDefault();
            //    if (vendorstock != null)
            //    {
            //        vendorstock.Stock = vendorstock.Stock + lst.QtySent.Value;
            //        db.SubmitChanges();
            //    }
            //    else
            //    {
            //        var stockInsert = new tblVendorStock
            //        {
            //            VendorId = vendor.Id,
            //            ProductId = lst.ProductId,
            //            Stock = lst.QtySent.Value
            //        };
            //        db.tblVendorStocks.InsertOnSubmit(stockInsert);
            //        db.SubmitChanges();
            //    }
            //}

            //vendor.CashWallet -= totalAmt;
            //Debit(vendor, totalAmt, "Debit", "Towards Stock Purchase");

            Response.Redirect("StockTransfer.aspx?p=sub");
        }
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
}