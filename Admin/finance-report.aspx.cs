using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_finance_report : System.Web.UI.Page
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
                    Response.Redirect("access-denied.aspx");

                }
                bind();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void bind(DateTime? date1 = null, DateTime? date2 = null)
    {
        DateTime startDate = new DateTime(2021, 05, 14);
        DateTime endDate = DateTime.UtcNow.AddDays(-1).Date;

        List<DateTime> allDates = new List<DateTime>();

        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            allDates.Add(date);

        if (date1 != null && date2 != null)
        {
            allDates = allDates.Where(x => x.Date >= date1 && x.Date <= date2).ToList();
        }

        var getData = from c in allDates
                      orderby c descending
                      select new
                      {
                          date = c.Date,
                          registration = Helper.totalRegistration(c.Date),
                          collection = Helper.totalCollection(c.Date),
                          payout = Helper.totalPayout(c.Date)
                      };

        lblRegistration.Text = getData.Sum(c => c.registration).ToString();
        lblCollection.Text = getData.Sum(c => c.collection).ToString();
        lblPayout.Text = ((int)getData.Sum(c => c.payout)).ToString();
        lbltotalTax.Text = ((int)(getData.Sum(c => c.payout) * 10 / 100)).ToString();
        lblProfit.Text = ((int)(getData.Sum(c => c.collection) - getData.Sum(c => c.payout))).ToString();
        GridView1.DataSource = getData.ToList();
        GridView1.DataBind();
    }
    protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
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