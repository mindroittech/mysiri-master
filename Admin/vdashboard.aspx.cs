using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vdashboard : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            string vendor = Session["vendor"].ToString();

            string role = Session["role"].ToString();
            //var orders = (from c in db.tblInvoices
            //              join d in db.tblVendors
            //              on c.FranchiseeId equals d.Id
            //              where c.status == "Requested" && c.DeliveryType == 1
            //              && d.Username == vendor && c.KitId == null
            //              select c).Count();
            //lblOrders.Text = orders.ToString();

            //var kits = (from c in db.tblInvoices
            //            join d in db.tblVendors
            //            on c.FranchiseeId equals d.Id
            //            where c.status == "Requested" && c.DeliveryType == 1
            //            && d.Username == vendor && c.KitId != null
            //            select c).Count();
            //lblKitRequests.Text = kits.ToString();

            var me = db.tblVendors.SingleOrDefault(x => x.Username == vendor);
            if (role == "Franchisee")
            {
                vgodown.Visible = false;

            }
            lblID.Text = me.Username;
            lblName.Text = me.VendorName;

            lblCashWallet.Text = me.CashWallet.ToString();
            lblKitCommission.Text = (me.KitWallet).ToString();
            lblRepurchase.Text = (me.RepurchaseWallet).ToString();
            //var stock = (from c in db.StockRequests
            //             join d in db.tblVendors
            //               on c.RequestTo equals d.Id
            //             where c.Status == "Requested" && d.Username == vendor
            //             select c).Count();
            string currentUser = Session["role"].ToString() == "Admin" ? Session["admin"].ToString() : Session["vendor"].ToString();
            int totalCount = (from c in db.StockRequests
                              join f in db.tblVendors on c.VendorId equals f.Id
                              join d in db.tblVendors on c.RequestTo equals d.Id
                              where d.Username == currentUser
                                    && c.Status == "Requested"
                                    && c.RequestType == "Product"
                                    && c.IsActive == true
                                    && f.IsActive == true
                              select c).Count();

            lblStockReq.Text = totalCount.ToString();


            var stockData = from c in db.tblProducts
                            join d in db.tblVendorStocks
                                on c.ProductId equals d.ProductId
                            join f in db.tblVendors
                                on d.VendorId equals f.Id
                            where c.IsActive == true
                                  && f.Username == vendor
                            select new
                            {
                                Quantity = d.Stock,
                                DP = c.DP,
                                PV = c.PV
                            };

            decimal totalStockValue = 0m;
            decimal totalBVValue = 0m;

            foreach (var item in stockData)
            {
                decimal qty = Convert.ToDecimal(item.Quantity);
                decimal dp = Convert.ToDecimal(item.DP ?? 0);
                decimal pv = Convert.ToDecimal(item.PV ?? 0);

                totalStockValue += qty * dp;
                totalBVValue += qty * pv;
            }

            lblstocks.Text = totalStockValue.ToString("N2");
            lblbv.Text = totalBVValue.ToString("N2");


        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
}