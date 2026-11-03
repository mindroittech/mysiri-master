using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DailyBusiness : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (!IsPostBack)
                bind();
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    public void bind(DateTime? date1 = null, DateTime? date2 = null)
    {

        DateTime endDate = DateTime.UtcNow.AddDays(-1).Date;
        DateTime startDate = endDate.AddMonths(-3);

        List<DateTime> allDates = new List<DateTime>();

        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            allDates.Add(date);

        if (date1 != null && date2 != null)
        {
            allDates = allDates.Where(x => x.Date >= date1 && x.Date <= date2).ToList();
        }

        var getData = from c in allDates
                      orderby c descending
                      let pin = Helper.totalPinCollection(c.Date)
                      let fund = Helper.totalFundWallet(c.Date)
                      let manual = Helper.totalManualBilling(c.Date)
                      select new
                      {
                          date = c.Date,
                          pinBusiness = pin,
                          fundWallet = fund,
                          manualBilling = manual,
                          total = pin + fund + manual
                      };


        GridView1.DataSource = getData;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        int pageIndex = e.NewPageIndex;
        bind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text == string.Empty || TextBox2.Text == string.Empty)
        {
            return;
        }
        else
        {

            bind(Convert.ToDateTime(TextBox1.Text), Convert.ToDateTime(TextBox2.Text));
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TextBox1.Text = TextBox2.Text = string.Empty;
        bind();
    }
}
