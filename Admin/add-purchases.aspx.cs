using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_add_purchases : System.Web.UI.Page
{

    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            if (!IsPostBack)
            {
                var product = from c in db.tblProducts
                              where c.IsActive == true
                              select c;

                GridView1.DataSource = product;
                GridView1.DataBind();

                var supplier = from c in db.tblSuppliers
                               where c.IsActive == true
                               select c;
                drpSupplier.DataSource = supplier;
                drpSupplier.DataTextField = "SupplierName";
                drpSupplier.DataValueField = "SupplierId";
                drpSupplier.DataBind();

                drpSupplier.Items.Insert(0, new ListItem("Select Supplier", ""));
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }
    protected void txtQuanity_Changed(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

            TextBox txtNos = (TextBox)row.FindControl("txtNos");
            Label lblAmount = (Label)row.FindControl("lblAmount");
            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            Label lblGstAmt = (Label)row.FindControl("lblGstAmt");
            Label lblNetTotal = (Label)row.FindControl("lblNetTotal");
            if (txtNos.Text.Trim() != string.Empty && txtRate.Text.Trim() != string.Empty)
            {
                Decimal gst = Convert.ToDecimal(row.Cells[2].Text);
                int quantity = Convert.ToInt32(txtNos.Text);
                Decimal cost = Convert.ToDecimal(txtRate.Text);
                Decimal amt = quantity * cost;
                lblAmount.Text = amt.ToString();
                Decimal gstAmt = (amt * gst / 100);
                lblGstAmt.Text = gstAmt.ToString();
                Decimal netTotal = amt + gstAmt;
                lblNetTotal.Text = netTotal.ToString();
            }
            else
            {
                lblAmount.Text = "0";
                lblGstAmt.Text = "0";
                lblNetTotal.Text = "0";

            }

            decimal totalQty = 0, totalPrice = 0, totalgst = 0, finalprice = 0;
            foreach (GridViewRow r in GridView1.Rows)
            {
                TextBox txtNo = (TextBox)r.FindControl("txtNos");
                if (txtNo.Text.Trim() != string.Empty && txtNo.Text != "0")
                {
                    totalQty += Convert.ToInt32(txtNo.Text);
                    Label lblTotalAmount = (Label)r.FindControl("lblAmount");
                    Label lblTotalGstAmt = (Label)r.FindControl("lblGstAmt");
                    Label lblTotalNet = (Label)r.FindControl("lblNetTotal");

                    totalPrice += Convert.ToDecimal(lblTotalAmount.Text);
                    totalgst += Convert.ToDecimal(lblTotalGstAmt.Text);
                    finalprice += Convert.ToDecimal(lblTotalNet.Text);
                }

                lblTotalQty.Text = totalQty.ToString();
                lblTotalPrice.Text = totalPrice.ToString();
                lblTotalGST.Text = totalgst.ToString();
                lblFinalPrice.Text = finalprice.ToString();
            }
        }
        catch
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Label1.Visible = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                TextBox txtNos = (TextBox)row.FindControl("txtNos");
                if (txtNos.Text.Trim() != string.Empty && txtNos.Text != "0")
                {
                    int productId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
                    var findProduct = db.tblProducts.FirstOrDefault(x => x.ProductId == productId);

                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    TextBox txtRate = (TextBox)row.FindControl("txtRate");
                    Label lblGstAmt = (Label)row.FindControl("lblGstAmt");
                    Label lblNetTotal = (Label)row.FindControl("lblNetTotal");
                    var product = new tblPurchase
                    {
                        PurchaseDate = Convert.ToDateTime(txtDate.Text),
                        SupplierId = Convert.ToInt32(drpSupplier.SelectedValue),
                        ProductId = productId,
                        InviceNo = invoiceNo.Value,
                        IsActive = true,
                        Qty = Convert.ToInt32(txtNos.Text),
                        Rate = Convert.ToDecimal(txtRate.Text),
                        Total = Convert.ToDecimal(lblAmount.Text),
                        GSTAmt = Convert.ToDecimal(lblGstAmt.Text),
                        NetTotal = Convert.ToDecimal(lblNetTotal.Text)
                        //LKNMrp = Convert.ToDecimal(txtlknmrp.Value),
                        //LKNDp = Convert.ToDecimal(txtlknDp.Value),
                        //LKNCv = Convert.ToDecimal(txtlknCV.Value)
                    };

                    db.tblPurchases.InsertOnSubmit(product);
                    //Update Stock
                    findProduct.Quantity = findProduct.Quantity >= 0 ? findProduct.Quantity + product.Qty : product.Qty;
                    findProduct.GrandTotal = findProduct.GrandTotal >= 0 ? findProduct.GrandTotal + product.NetTotal : product.NetTotal;
                    db.SubmitChanges();
                }
            }
            Response.Redirect("purchases.aspx");
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Label1.Visible = true;
        }
    }
}