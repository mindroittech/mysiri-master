using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vStockHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string vendor = Session["vendor"].ToString();
            if (!IsPostBack)
            {
                bind();
                if (Request.QueryString["v"] == "sub")
                    lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void GridView_PageIndex(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
    private void bind()
    {
        string vendor = Session["vendor"].ToString();

        var history1 = (from c in db.StockRequests
                        join f in db.tblVendors
                            on c.VendorId equals f.Id
                        join d in db.tblVendors
                        on c.RequestTo equals d.Id into grp
                        from g in grp.DefaultIfEmpty()
                        where f.Username == vendor && c.IsActive == true && c.RequestType == "Product"
                        orderby c.SrId descending
                        select new
                        {

                            c.VendorId,
                            c.Remarks,
                            c.Requestdate,
                            c.RequestTo,
                            c.PayMode,
                            c.AmountPaid,
                            c.SentBy,
                            c.SentDate,
                            c.SrId,
                            Reporter = g != null ? g.VendorName : "",
                            c.Status,
                            c.Delivery,
                            Courier = c.Delivery != "Courier" ? c.Delivery : c.Courier,
                            c.InvoiceNo,
                            c.AwbNo,
                            url = "finvoice.aspx"
                        });
        GridView1.DataSource = history1.ToList();
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "products")
        {
            int srid = Convert.ToInt32(e.CommandArgument);
            var products = from c in db.StockRequestProducts
                           join d in db.tblProducts
                           on c.ProductId equals d.ProductId
                           where c.SrId == srid
                           select new { d.ProductId, d.ProductName, d.ImageUrl, c.QtyReq, c.QtySent };
            GridView2.DataSource = products;
            GridView2.DataBind();
            ImageButton2_ModalPopupExtender.Show();
        }
        if (e.CommandName == "accept")
        {

            int srid = Convert.ToInt32(e.CommandArgument);

            var stockReq = db.StockRequests.SingleOrDefault(x => x.SrId == srid);
            if (stockReq.Status != "Received")
            {
                stockReq.ReceivedDate = DateTime.UtcNow.AddHours(5.5);
                stockReq.Status = "Received";
                db.SubmitChanges();

                var stockProducts = from c in db.StockRequestProducts
                                    where c.SrId == srid && c.QtySent != null
                                    select c;
                foreach (var lst in stockProducts)
                {
                    var vendorstock = db.tblVendorStocks.Where(x => x.VendorId == stockReq.VendorId && x.ProductId == lst.ProductId).SingleOrDefault();
                    if (vendorstock != null)
                    {
                        vendorstock.Stock += lst.QtySent.Value;
                        db.SubmitChanges();
                    }
                    else
                    {
                        var stockInsert = new tblVendorStock
                        {
                            VendorId = stockReq.VendorId,
                            ProductId = lst.ProductId,
                            Stock = lst.QtySent.Value
                        };
                        db.tblVendorStocks.InsertOnSubmit(stockInsert);
                        db.SubmitChanges();
                    }
                }
            }

            Response.Redirect("vStockRequestHistory.aspx");
        }
        if (e.CommandName == "delete")
        {
            int srid = Convert.ToInt32(e.CommandArgument);
            var history1 = db.StockRequests.Where(x => x.SrId == srid).SingleOrDefault();
            history1.IsActive = false;

            var me = (from c in db.tblVendors
                      where c.Id == history1.VendorId
                      select c).SingleOrDefault();

            if (history1.PayMode == "Product Wallet")
            {
                me.RepurchaseWallet += history1.AmountPaid;
                me.RepurchaseWalletDebit -= history1.AmountPaid;
            }
            db.SubmitChanges();
            Response.Redirect("vStockRequestHistory.aspx");
        }
    }
}