using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_fInvoice : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();
            string inv = "";
            if (Request.QueryString["inv"] != null)
            {
                inv = Request.QueryString["inv"];
            }
            var invoice = db.StockRequests.SingleOrDefault(x => x.InvoiceNo == inv);
            var member = db.tblVendors.SingleOrDefault(x => x.Id == invoice.VendorId);
            lblName.Text = member.VendorName;
            lblAddress.Text = member.Address;
            string state = "";
            if (member != null)
            {
                lblCity.Text = member.City;
                lblState.Text = member.State;
                lblPincode.Text = member.Pincode;
                state = member.State;
                lblGST.Text = member.GSTIN;
                lblMobile.Text = member.Phone;
            }
            lblUser.Text = member.Username;
            lblInv.Text = invoice.InvoiceNo;
            lblInvDate.Text = invoice.SentDate.Value.ToString("dd/MM/yyyy");

            var products = from c in db.StockRequestProducts
                           join f in db.tblProducts
                           on c.ProductId equals f.ProductId
                           where c.SrId == invoice.SrId && c.QtySent != null
                           let DP = c.amount / c.QtySent
                           let GST = c.gst.HasValue ? c.gst : f.GST
                           select new
                           {
                               f.HSNCode,
                               f.ProductName,
                               PV = c.QtySent * f.PV,
                               DP,
                               f.MRP,
                               GST,
                               //c.amount,
                               Amount = c.QtySent * (DP * 100 / (GST + 100)),
                               GSTAmount = c.QtySent * (DP - (DP * 100 / (GST + 100))),
                               QTY = c.QtySent,
                               IGST = state == "Karnataka" ? 0 : GST,
                               IGSTAmount = state == "Karnataka" ? 0 : c.QtySent * (DP - (DP * 100 / (GST + 100))),
                               CGST = state == "Karnataka" ? GST / 2 : 0,
                               SGST = state == "Karnataka" ? GST / 2 : 0,
                               CGSTAmount = state == "Karnataka" ? c.QtySent * (DP - (DP * 100 / (GST + 100))) / 2 : 0,
                               SGSTAmount = state == "Karnataka" ? c.QtySent * (DP - (DP * 100 / (GST + 100))) / 2 : 0,
                               TotalAmount = c.QtySent * f.DP

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
            //  lblNetPayable.Text = products.Sum(x => x.TotalAmount.Value).ToString("0");
            decimal grossAmount = products.Sum(x => x.Amount.Value);

            // Calculate the CGST amount
            decimal cgstAmount = products.Sum(x => x.CGSTAmount.Value);

            // Calculate the SGST amount
            decimal sgstAmount = products.Sum(x => x.SGSTAmount.Value);

            // Calculate the IGST amount
            decimal igstAmount = products.Sum(x => x.IGSTAmount.Value);
            decimal netPayable = grossAmount + cgstAmount + sgstAmount + igstAmount;
            // Update the net payable label
            lblNetPayable.Text = netPayable.ToString("0.00");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}