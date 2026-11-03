using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class invoice : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"] != null ? Session["username"].ToString() : Session["role"].ToString();
            string inv = "";
            if (Request.QueryString["inv"] != null)
            {
                inv = Request.QueryString["inv"];
            }
            var invoice = db.tblInvoices.SingleOrDefault(x => x.InvoiceNo == inv);
            var member = db.tblMembers.SingleOrDefault(x => x.username == invoice.username && x.status== "Paid");

            if (member == null)
            {
                lblerror.Text = "Sorry Invoice is Not Available";
                lblerror.Visible = true;
                printableArea.Visible = false;
                return;
            }
            lblName.Text = invoice.dispatchedTo;
            lblAddress.Text = invoice.CourierAddress;
            lblMobile.Text = invoice.AWBNo;
            lblUser.Text = invoice.AWBNo;
            string state = "";
            if (invoice.userType == "Member")
            {
                lblCity.Text = member.city;
                lblState.Text = member.state;
                lblPincode.Text = member.pincode;
                state = member.state;
                lblMobile.Text = member.mobile;
                lblUser.Text = invoice.username;
            }

            lblInv.Text = invoice.InvoiceNo;

            lblInvDate.Text = invoice.PurchaseDate.Value.ToString("dd/MM/yyyy");

            var products = from c in db.tblRepurchases
                           join f in db.tblProducts
                           on c.product_id equals f.ProductId
                           where c.InvoiceId == invoice.InvoiceId
                           let DP = c.amount / c.Qty
                           select new
                           {
                               f.HSNCode,
                               f.ProductName,
                               PV = c.RepurchaseBV,
                               DP,
                               f.MRP,
                               f.GST,
                               //c.amount,
                               Amount = c.Qty * (DP * 100 / (f.GST + 100)),
                               GSTAmount = c.Qty * (DP - (DP * 100 / (f.GST + 100))),
                               QTY = c.Qty,
                               IGST = state == "Karnataka" ? 0 : f.GST,
                               IGSTAmount = state == "Karnataka" ? 0 : c.Qty * (DP - (DP * 100 / (f.GST + 100))),
                               CGST = state == "Karnataka" ? f.GST / 2 : 0,
                               SGST = state == "Karnataka" ? f.GST / 2 : 0,
                               CGSTAmount = state == "Karnataka" ? c.Qty * (DP - (DP * 100 / (f.GST + 100))) / 2 : 0,
                               SGSTAmount = state == "Karnataka" ? c.Qty * (DP - (DP * 100 / (f.GST + 100))) / 2 : 0

                           };
            GridView1.DataSource = products;
            GridView1.DataBind();
            var gst5 = products.Where(x => x.GST == 5);
            var gst12 = products.Where(x => x.GST == 12);
            var gst18 = products.Where(x => x.GST == 18);
            var gst28 = products.Where(x => x.GST == 28);
            if (gst5.Any())
            {
                lblTaxable5.Text = gst5.Sum(x => x.Amount.Value).ToString("0.00");
                lblCGST5.Text = gst5.Sum(x => x.CGSTAmount.Value).ToString("0.00");
                lblSGST5.Text = gst5.Sum(x => x.SGSTAmount.Value).ToString("0.00");
                lblIGST5.Text = gst5.Sum(x => x.IGSTAmount.Value).ToString("0.00");
            }
            if (gst12.Any())
            {
                lblTaxable12.Text = gst12.Sum(x => x.Amount.Value).ToString("0.00");
                lblCGST12.Text = gst12.Sum(x => x.CGSTAmount.Value).ToString("0.00");
                lblSGST12.Text = gst12.Sum(x => x.SGSTAmount.Value).ToString("0.00");
                lblIGST12.Text = gst12.Sum(x => x.IGSTAmount.Value).ToString("0.00");
            }
            if (gst18.Any())
            {
                lblTaxable18.Text = gst18.Sum(x => x.Amount.Value).ToString("0.00");
                lblCGST18.Text = gst18.Sum(x => x.CGSTAmount.Value).ToString("0.00");
                lblSGST18.Text = gst18.Sum(x => x.SGSTAmount.Value).ToString("0.00");
                lblIGST18.Text = gst18.Sum(x => x.IGSTAmount.Value).ToString("0.00");
            }

            if (gst28.Any())
            {
                lblTaxable28.Text = gst28.Sum(x => x.Amount.Value).ToString("0.00");
                lblCGST28.Text = gst28.Sum(x => x.CGSTAmount.Value).ToString("0.00");
                lblSGST28.Text = gst28.Sum(x => x.SGSTAmount.Value).ToString("0.00");
                lblIGST28.Text = gst28.Sum(x => x.IGSTAmount.Value).ToString("0.00");
            }

            lblGrossAmount.Text = products.Sum(x => x.Amount.Value).ToString("0.00");
            lblCGSTAmount.Text = lblTotalCGST.Text = products.Sum(x => x.CGSTAmount.Value).ToString("0.00");
            lblSGSTAmount.Text = lblTotalSGST.Text = products.Sum(x => x.SGSTAmount.Value).ToString("0.00");
            lblIGSTAmount.Text = lblTotalIGST.Text = products.Sum(x => x.IGSTAmount.Value).ToString("0.00");
            lblNetPayable.Text = invoice.Amount.Value.ToString("0");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}