using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_products : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    private static bool isclicked = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();
            if (role != "GoDown" && role != "Admin")
            {
                throw new Exception("Access denied");
            }
            if (!IsPostBack)
            {
                isclicked = false;
                bind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("mPurchaseHistory.aspx");
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
            bind(TextBox1.Text, TextBox2.Text, d1, d2, isclicked);
        }
        catch (Exception)
        {

        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Export();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void Export()
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "FranchiseeWiseSalesReport.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.AllowPaging = false;
        if (isclicked)
        {
            Button1_Click(null, null);
        }
        else
        {
            bind();
        }
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        for (int j = 0; j < GridView1.Columns.Count; j++)
        {
            if (j == 11)
            {
                GridView1.Columns[j].Visible = false;
            }
        }
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        }
        GridView1.RenderControl(htw);
        GridView1.AllowPaging = true;
        if (isclicked)
        {
            Button1_Click(null, null);
        }
        else
        {
            bind();
        }

        Response.Write(sw.ToString());
        Response.End();
    }

    protected void GridView_PageIndex(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    private void bind(string uname = "", string vendor = "", DateTime? d1 = null, DateTime? d2 = null, bool clicked = false)
    {

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
                            f.country,
                            Type = c.Name,
                            c.PurchaseDate,
                            f.username,
                            c.PayMode,
                            f.name,
                            f.mobile,
                            c.status,
                            c.DeliveredOn,
                            c.DispachedOn,
                            c.AWBNo,
                            c.CourierAddress,
                            c.InvoiceNo,
                            c.Amount,
                            franchiseId = k.Username,
                            franchise = k.Username + "-" + k.VendorName,
                            url = "../invoice.aspx?inv=" + c.InvoiceNo
                        });
        if (uname != "")
        {
            history1 = history1.Where(x => x.username == uname);
        }

        if (vendor != "")
        {
            history1 = history1.Where(x => x.franchiseId == vendor);
        }
        if (d1 != null && d2 != null)
        {
            history1 = history1.Where(x => x.DispachedOn.Value.Date >= d1 && x.DispachedOn.Value.Date <= d2);
        }
        if (isclicked)
        {
            GridView1.AllowPaging = false;
        }

        var sum = history1.Sum(x => x.Amount) ?? 0;
        lblTotal.Text = sum.ToString();
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