using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Admin_vOrderHistory : System.Web.UI.Page
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
                if (Request.QueryString["o"] != null)
                {
                    lblMessage.Text = "Order has been successfully created";
                    lblMessage.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("vOrderHistory.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            bind(TextBox1.Text, TextBox2.Text, RadioButtonList1.SelectedValue, true);
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

    private void bind(string uname = "", string order = "", string orderType = "", bool isClicked = false)
    {
        string vendor = Session["vendor"].ToString();
        var history1 = (from c in db.tblInvoices
                        join f in db.tblMembers
                            on c.username equals f.username
                        join k in db.tblVendors
                        on c.FranchiseeId equals k.Id
                        where c.status != "Requested" && k.Username == vendor && f.isActive == 1    //courier
                        orderby c.InvoiceId descending
                        select new
                        {
                            c.InvoiceId,
                            f.country,
                            Type = c.Name,
                            c.PurchaseDate,
                            f.username,
                            c.PayMode,
                            f.name,
                            c.status,
                            c.DeliveredOn,
                            c.DispachedOn,
                            c.AWBNo,
                            c.CourierAddress,
                            c.InvoiceNo,
                            c.Amount,
                            c.invType,
                            url = "../invoice.aspx?inv=" + c.InvoiceNo
                        });

        if (uname != "")
        {
            history1 = history1.Where(x => x.username == uname);
        }
        if (order != "")
        {
            history1 = history1.Where(x => x.InvoiceNo == order);
        }
        if (orderType != "")
        {
            history1 = history1.Where(x => x.invType == orderType);
        }
        if (isClicked)
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
                           select new { d.ProductId, d.ProductName, d.ImageUrl, c.Qty };
            GridView2.DataSource = products;


            GridView2.DataBind();
            ImageButton2_ModalPopupExtender.Show();
        }

    }
}