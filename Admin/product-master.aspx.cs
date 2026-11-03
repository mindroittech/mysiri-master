using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_product_master : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["role"].ToString();

            if (!IsPostBack)
            {
                if (Request.QueryString["ptype"] != null)
                    bind(null, Request.QueryString["ptype"]);
                else
                    bind();
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected bool IsVisible(int prodId)
    {
        bool visible = true;
        var stocks = db.tblVendorStocks.Where(x => x.ProductId == prodId && x.Stock > 0);
        if (stocks.Any())
            visible = false;
        return visible;
    }
    private int globalStock(int prodId)
    {
        int total = 0;
        var prod = (from c in db.tblProducts
                    where c.ProductId == prodId
                    select c).SingleOrDefault();
        total += prod.Quantity.HasValue ? prod.Quantity.Value : 0;

        var vendorStock = (from c in db.tblVendorStocks
                           where c.ProductId == prodId
                           select c);
        if (vendorStock.Any())
            total += vendorStock.Sum(x => x.Stock);
        return total;
    }
    private void bind(string prod = null, string ptype = null)
    {
        var get = from c in db.tblProducts
                  //join d in db.tblPlans
                  //on c.PlanId equals d.PlanId
                  where c.IsActive == true
                  select new
                  {
                      c.ProductName,
                      c.ProductId,
                      c.FranchisePrice,
                      Free = c.FreeEligible.Value ? "Yes" : "No",
                      AW = c.AwardEligible.Value ? "Yes" : "No",
                      c.GST,
                      c.ExpiryDate,
                      c.ImageUrl,
                      c.MRP,

                      c.Quantity,
                      c.DP,
                      c.PV,
                      c.BasePrice
                  };
        if (ptype == "free")
        {
            get = get.Where(x => x.Free == "Yes");
        }
        if (prod != null)
            get = get.Where(x => x.ProductName.Contains(prod));

        GridView1.DataSource = get;
        GridView1.DataBind();

        var totalQty = get.Sum(x => x.Quantity);
        var totalDP = get.Sum(x => x.Quantity * x.DP);
        var totalBv = get.Sum(x => x.Quantity * x.PV);

        if (totalQty.HasValue)
            lblTotalQty.Text = totalQty.ToString();
        if (totalBv.HasValue)
            lblTotalBV.Text = Math.Round(totalBv.Value).ToString();
        if (totalDP.HasValue)
            lblTotalAmt.Text = Math.Round(totalDP.Value).ToString();
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        bind(TextBox1.Text);
    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //}
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("product-master.aspx");
        }
        catch (Exception ex)
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Products.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //GridView1.AllowPaging = false;
        //Button3_Click(null, null);
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        for (int j = 0; j < GridView1.Columns.Count; j++)
        {
            if (j == 2 || j == 9)
                GridView1.Columns[j].Visible = false;

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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "delete")
        {
            int ProductId = Convert.ToInt32(e.CommandArgument);

            var get = db.tblProducts.Where(x => x.ProductId == ProductId).SingleOrDefault();

            get.IsActive = false;
            db.SubmitChanges();

            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
        }
        else if (e.CommandName == "edit")
        {
            int ProductId = Convert.ToInt32(e.CommandArgument);

            Response.Redirect("add-product-master.aspx?Id=" + ProductId);
        }

    }
}