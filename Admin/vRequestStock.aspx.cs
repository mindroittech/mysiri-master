using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vRequestStock : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string vendor = Session["vendor"].ToString();
            string role = Session["role"].ToString();
            if (!IsPostBack)
            {
                var roles = (from c in db.tblVendors
                             where c.Username == vendor
                             select c).SingleOrDefault();
                var reporter = from c in db.tblVendors
                               join d in db.tblRoles
                               on c.RoleId equals d.RoleId
                               where (d.role == "Admin" || d.role == "GoDown") && c.IsActive == true
                               select c;

                drpReporter.DataSource = reporter;
                drpReporter.DataTextField = "VendorName";
                drpReporter.DataValueField = "Id";
                drpReporter.DataBind();

                var me = db.tblVendors.Single(x => x.Username == vendor);
                if (me.ReportingId != null)
                    drpReporter.SelectedValue = me.ReportingId.ToString();
                drpReporter.Enabled = false;

                lblWallet.Text = me.CashWallet.ToString();

                var products = from c in db.tblProducts.AsEnumerable()
                               join d in db.tblVendorStocks.Where(x => x.VendorId == me.Id)
                               on c.ProductId equals d.ProductId into grp
                               from g in grp.DefaultIfEmpty()
                               where c.IsActive == true && c.AwardEligible == false && c.ActiveToFanchisee==true
                               select new
                               {
                                   c.ProductName,
                                   Stock = g != null ? g.Stock : 0,
                                   Cost = c.DP,
                                   c.ImageUrl,
                                   c.ProductId
                               };
                GridView1.DataSource = products;
                GridView1.DataBind();

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void txtQuanity_Changed(object sender, EventArgs e)
    {

        Label lblQty = GridView1.FooterRow.FindControl("lblTotalQty") as Label;
        Label lbltotal = GridView1.FooterRow.FindControl("lblTotalAmount") as Label;
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
            lblQty.Text = totalQuantity.ToString();
            lbltotal.Text = totalamt.ToString();
        }
        catch
        {
            lblCost.Text = "0";
            lblQty.Text = "0";
            lbltotal.Text = "0";
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string vendor = Session["vendor"].ToString();
        var me = (from c in db.tblVendors
                  where c.Username == vendor
                  select c).SingleOrDefault();

        decimal totalAmt = Convert.ToDecimal(lblCost.Text);
        if (totalAmt == 0)
        {
            lblErrorMessage.Text = "Please select atleast one product.";
            lblErrorMessage.Visible = true;
            return;
        }
        if (drpPayMode.SelectedItem.Text == "Cash Wallet" && totalAmt > me.CashWallet)
        {
            lblErrorMessage.Text = "You don't have sufficient balance in Cash Wallet.";
            lblErrorMessage.Visible = true;
            return;
        }

        //decimal totalStock = (from p in db.tblProducts
        //                      join s in db.tblVendorStocks
        //                      on p.ProductId equals s.ProductId
        //                      where s.VendorId == me.Id
        //                      select (decimal?)(p.DP * s.Stock)).Sum() ?? 0;

        //// ✅ Safe wallet handling
        //decimal cashWallet = me.CashWallet;

        //decimal totalValue = cashWallet + totalStock;

        //// ✅ Correct percentage calculation
        //decimal target = (me.Amount ?? 0) * 0.50m;


        //if(totalValue<target && me.RoleId >2)
        //{
        //    decimal shortage = target - totalValue;

        //    lblErrorMessage.Text = "You need ₹" + shortage + " more to proceed. Please topup your CashWallet";
           
        //    lblErrorMessage.Visible = true;
        //    return;
        //}

        var req = new StockRequest
        {
            VendorId = me.Id,
            AmountPaid = totalAmt,
            Requestdate = DateTime.UtcNow.AddHours(5.5),
            Status = "Requested",
            PayMode = drpPayMode.SelectedItem.Text,
            Remarks = txtRemarks.Text,
            RequestTo = Convert.ToInt32(drpReporter.SelectedValue),
            RequestType = "Product",
            IsActive = true,
        };
        db.StockRequests.InsertOnSubmit(req);

        // deducting Bill Amount while placing order dt. 20/7/2024 (araju)

        if (drpPayMode.SelectedItem.Text == "Cash Wallet")
        {
            me.CashWallet -= totalAmt;
            Debit(me, totalAmt, "Debit", "Towards Stock Purchase");
        }
        db.SubmitChanges();

        var vendorToRequest = (from c in db.tblVendors
                                where c.Id ==Convert.ToInt32(drpReporter.SelectedValue)
                                select c).SingleOrDefault();

        vendorToRequest.CashWallet += totalAmt;
        Credit(vendorToRequest, totalAmt, "Credit", "Towards Stock Sale-" + me.VendorName, vendorToRequest.Username);

        DateTime date = DateTime.UtcNow.AddHours(5.5);

        List<StockRequestProduct> lstPurchase = new List<StockRequestProduct>();
        foreach (GridViewRow row in GridView1.Rows)
        {
            int prodId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
            TextBox txtNos = (TextBox)row.FindControl("txtNos");
            Label lblAmount = (Label)row.FindControl("lblAmount");
            if (txtNos.Text != "" && txtNos.Text != "0")
            {
                int quant = Convert.ToInt32(txtNos.Text);
                var repurchase = new StockRequestProduct
                {
                    ProductId = prodId,
                    QtyReq = quant,
                    SrId = req.SrId
                };
                lstPurchase.Add(repurchase);
            }
        }
        db.StockRequestProducts.InsertAllOnSubmit(lstPurchase);
        db.SubmitChanges();

        Response.Redirect("vStockRequestHistory.aspx?v=sub");
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