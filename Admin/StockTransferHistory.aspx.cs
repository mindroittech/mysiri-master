using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StockTransferHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["role"].ToString() != "Admin")
                {
                    Button2.Visible = false;

                    bind(null, null, null, Session["vendor"].ToString());
                }
                else
                    bind();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime? d1 = null;
            DateTime? d2 = null;
            string uname = null;
            if (txtStart.Text != string.Empty && txtEnd.Text != string.Empty)
            {
                d1 = Convert.ToDateTime(txtStart.Text);
                d2 = Convert.ToDateTime(txtEnd.Text);
            }
            if (TextBox1.Text != string.Empty)
            {
                uname = TextBox1.Text;
            }

            if (Session["vendor"] != null)
                bind(uname, d1, d2, Session["vendor"].ToString());
            else
                bind(uname, d1, d2);

        }
        catch (Exception ex)
        {

        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string admin = Session["admin"].ToString();  // only for admin user
        bind(null, null, null, admin);

    }
    protected void GridView_PageIndex(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        if (Session["role"].ToString() != "Admin")
        {
            bind(null, null, null, Session["vendor"].ToString());
        }
        else
            bind();
    }
    private void bind(string uname = null, DateTime? d1 = null, DateTime? d2 = null, string transferedBy = null)
    {

        var history1 = (from c in db.StockRequests
                        join f in db.tblVendors
                            on c.VendorId equals f.Id
                        join d in db.tblVendors
                            on c.SentBy equals d.Id into grp
                        from g in grp.DefaultIfEmpty()
                        where c.Status != "Requested" && (c.RequestType == "Product")
                        orderby c.SrId descending
                        select new
                        {
                            c.VendorId,
                            f.Username,
                            f.CompanyName,
                            c.Remarks,
                            c.Requestdate,
                            c.SentDate,
                            c.RequestTo,
                            c.PayMode,
                            c.AmountPaid,
                            c.SrId,
                            SentByUserName = g.Username,
                            SentBy = g.Username != null ? g.Username + " " + g.CompanyName : "",
                            c.Status,
                            c.Delivery,
                            Courier = c.Delivery != "Courier" ? c.Delivery : c.Courier,
                            c.AwbNo,
                            c.InvoiceNo,
                            Free = c.isFree.HasValue ? c.isFree.Value ? "Yes" : "No" : "",
                            url = "finvoice.aspx"
                        });
        if (transferedBy != null)
        {
            history1 = history1.Where(x => x.SentByUserName == transferedBy);
        }
        if (d1 != null && d2 != null)
        {
            history1 = history1.Where(x => x.SentDate >= d1 && x.SentDate <= d2);
        }
        if (uname != null)
        {
            history1 = history1.Where(x => x.Username == uname);
        }

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
        if (e.CommandName == "delete")
        {
            int srid = Convert.ToInt32(e.CommandArgument);
            var sreq = db.StockRequests.SingleOrDefault(x => x.SrId == srid);
            sreq.Status = "Requested";
            if (sreq.Requestdate != null)
            {
                var res = db.StockRequestProducts.Where(x => x.SrId == srid);
                var products = from d in res
                               join c in db.tblProducts
                               on d.ProductId equals c.ProductId
                               select new { c, d };
                products.ToList().ForEach(x => x.c.Quantity = x.c.Quantity + x.d.QtySent);

                res.ToList().ForEach(x => x.QtySent = null);
                db.SubmitChanges();
            }
            else
            {
                var res = db.StockRequestProducts.Where(x => x.SrId == srid);
                var products = from d in res
                               join c in db.tblProducts
                               on d.ProductId equals c.ProductId
                               select new { c, d };
                products.ToList().ForEach(x => x.c.Quantity = x.c.Quantity + x.d.QtySent);
                db.StockRequestProducts.DeleteAllOnSubmit(res);
                db.SubmitChanges();
            }
            Response.Redirect("StockTransferHistory.aspx");
        }

    }
}