using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_myRepurchases : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    private static bool isclicked = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            if (!IsPostBack)
            {
                isclicked = false;
                bind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../login.aspx");
        }

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("myRepurchases.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            isclicked = true;
            DateTime? d1 = null, d2 = null;
            if (TextBox3.Text != string.Empty)
            {
                d1 = Convert.ToDateTime(TextBox3.Text);
            }
            if (TextBox4.Text != string.Empty)
            {
                d2 = Convert.ToDateTime(TextBox4.Text);
            }
            bind(d1, d2, isclicked);
        }
        catch (Exception)
        {

        }

    }

    protected void GridView_PageIndex(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    private void bind(DateTime? d1 = null, DateTime? d2 = null, bool isClicked = false)
    {
        string uname = Session["username"].ToString();
        var history1 = (from c in db.tblInvoices
                        join f in db.tblMembers
                         on c.username equals f.username
                        join k in db.tblVendors
                        on c.FranchiseeId equals k.Id
                        where c.status != "Requested" && f.isActive == 1    //courier
                        orderby c.InvoiceId descending
                        select new
                        {
                            c.InvoiceId,
                            Type = c.Name,
                            c.PurchaseDate,
                            f.username,
                            c.PayMode,
                            f.name,
                            f.mobile,
                            c.status,
                            c.AWBNo,
                            c.BV,
                            c.CourierAddress,
                            c.CourierName,
                            c.DeliveryType,
                            c.InvoiceNo,
                            c.Amount,
                            franchiseId = k.Username,
                            k.CompanyName,
                            url = "../invoice.aspx?inv=" + c.InvoiceNo
                        });
        if (uname != "")
        {
            history1 = history1.Where(x => x.username == uname);
        }


        if (d1 != null && d2 != null)
        {
            history1 = history1.Where(x => x.PurchaseDate.Value.Date >= d1 && x.PurchaseDate.Value.Date <= d2);
        }
        if (isclicked)
        {
            GridView1.AllowPaging = false;
        }
        GridView1.DataSource = history1.ToList();
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "products")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            var invoice = db.tblInvoices.SingleOrDefault(x => x.InvoiceId == id);

            var products = from c in db.tblRepurchases
                           join d in db.tblProducts
                           on c.product_id equals d.ProductId
                           where c.InvoiceId == id
                           select new { d.ProductId, d.ProductName, d.DP, d.ImageUrl, c.Qty };
            GridView2.DataSource = products;
            GridView2.DataBind();
            ImageButton2_ModalPopupExtender.Show();
        }

    }
}