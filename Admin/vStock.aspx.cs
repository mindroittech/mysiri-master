using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_vStock : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string vendor = Session["vendor"].ToString();
            if (!IsPostBack)
            {
                bind(vendor);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    private void bind(string uname = null)
    {
        var vendor = db.tblVendors.SingleOrDefault(x => x.Username == uname);
        var get =
                  from d in db.tblProducts
                  join c in db.tblVendorStocks.Where(x => x.VendorId == vendor.Id)
                  on d.ProductId equals c.ProductId
                  where d.IsActive == true && c.Stock > 0
                  select new
                  {
                      d.ProductName,
                      d.MRP,
                      FranchisePrice = d.DP,
                      DP = d.DP,
                      d.ImageUrl,
                      PV = d.PV,
                      d.ProductId,
                      Stock = c.Stock,
                      Free = d.FreeEligible.Value ? "Yes" : "No"
                  };
        GridView1.DataSource = get;
        GridView1.DataBind();

        if (get.Any())
        {
            decimal pv = get.Sum(x => x.PV.Value * x.Stock);
            decimal dp = get.Sum(x => x.DP.Value * x.Stock);

            Label lblPv = GridView1.FooterRow.FindControl("lblTotalPv") as Label;
            Label lblDp = GridView1.FooterRow.FindControl("lblTotalDp") as Label;

            lblPv.Text = pv.ToString();
            lblDp.Text = dp.ToString();
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "MyStockReport.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //GridView1.AllowPaging = false;
        //Button3_Click(null, null);
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        for (int j = 0; j < GridView1.Columns.Count; j++)
        {
            if (j == 6)
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
}