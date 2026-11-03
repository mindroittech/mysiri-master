using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_purchases : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            if (!IsPostBack)
            {
                bind();
            }

        }
        catch (Exception)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    private void bind(string prod = null, DateTime? d1 = null, DateTime? d2 = null)
    {
        var get = from c in db.tblPurchases
                  join d in db.tblSuppliers
                  on c.SupplierId equals d.SupplierId
                  join f in db.tblProducts
                  on c.ProductId equals f.ProductId
                  where c.IsActive == true
                  select new
                  {
                      c.PurchaseId,
                      c.PurchaseDate,
                      f.ProductCode,
                      f.UOM,
                      c.InviceNo,
                      c.GSTAmt,
                      c.Qty,
                      c.Rate,
                      c.Total,
                      c.NetTotal,
                      d.SupplierName,
                      f.HSNCode,
                      f.GST,
                      f.ProductName
                  };
        if (d1 != null && d2 != null)
        {
            get = get.Where(x => x.PurchaseDate.Value.Date >= d1 && x.PurchaseDate.Value.Date <= d2);
        }
        if (prod != null)
        {
            get = get.Where(x => x.ProductName.Contains(prod));
        }
        GridView1.DataSource = get;
        GridView1.DataBind();

        var totalPurchases = get.Sum(x => x.NetTotal);
        var totalQty = get.Sum(x => x.Qty);
        if (totalPurchases.HasValue)
            lblTotalAmt.Text = totalPurchases.ToString();
        if (totalQty.HasValue)
            lblTotalQty.Text = totalQty.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string product = null;
            DateTime? d1 = null, d2 = null;
            if (TextBox1.Text != string.Empty)
            {
                product = TextBox1.Text;
            }
            if (txtStart.Text != string.Empty && txtEnd.Text != string.Empty)
            {
                d1 = Convert.ToDateTime(txtStart.Text);
                d2 = Convert.ToDateTime(txtEnd.Text);
            }
            bind(product, d1, d2);
        }
        catch (Exception)
        {

        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            bind();
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Purchases.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.AllowPaging = false;
        //Button3_Click(null, null);
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        for (int j = 0; j < GridView1.Columns.Count; j++)
        {
            if (j > 10)
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


        Response.Write(sw.ToString());
        Response.End();
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "delete")
        {
            int purchaseId = Convert.ToInt32(e.CommandArgument);

            var get = db.tblPurchases.Where(x => x.PurchaseId == purchaseId).SingleOrDefault();

            get.IsActive = false;

            var stocks = db.tblProducts.Where(x => x.ProductId == get.ProductId).SingleOrDefault();
            if (stocks != null)
            {
                stocks.Quantity = stocks.Quantity - get.Qty;
                if (stocks.Quantity < 0)
                {
                    stocks.Quantity = 0;
                }
            }

            db.SubmitChanges();

            Response.Redirect("purchases.aspx");
        }

    }
}