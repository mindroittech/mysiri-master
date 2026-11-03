using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vTransactions : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
                bind();
        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void bind(DateTime? d1 = null, DateTime? d2 = null)
    {
        try
        {

            string vendor = Session["vendor"].ToString();

            var get = from c in db.Payouts
                      //    where c.confirmdate.Value.Date >= d1 && c.confirmdate.Value.Date <= d2 
                      join d in db.tblVendors
                      on c.username equals d.Username
                      where c.status == "Send" && c.username == vendor
                      orderby c.date descending
                      select new { credit = c.credit.Value, c.date, c.remarks, c.desc };

            var bind = from c in db.StockRequests
                       join d in db.tblVendors
                       on c.SentBy equals d.Id
                       where d.Username == vendor && c.commission > 0
                       select new
                       {
                           credit = c.commission,
                           date = c.SentDate,
                           remarks = "Stock Transfer Commission",
                           desc = "Repurchase Income"
                       };
            var result = get.Union(bind);
            if (d1 != null && d2 != null)
                result = result.Where(x => x.date >= d1 && x.date <= d2);

            GridView1.DataSource = result.OrderByDescending(c => c.date);
            GridView1.DataBind();
        }
        catch (Exception ex)
        { throw ex; }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("vTransactions");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        DateTime d1 = Convert.ToDateTime(TextBox1.Text);
        DateTime d2 = Convert.ToDateTime(TextBox2.Text);

        bind(d1, d2);

    }
}