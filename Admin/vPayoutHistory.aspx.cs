using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Admin_vPayoutHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["vendor"].ToString();
            if (!IsPostBack)
            {
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

        DateTime? d1 = null;
        DateTime? d2 = null;

        string uname = Session["username"].ToString();

        if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
        {
            d1 = Convert.ToDateTime(TextBox1.Text);
            d2 = Convert.ToDateTime(TextBox2.Text);
        }

        bind(d1, d2);

    }

    protected void bind(DateTime? d1 = null, DateTime? d2 = null)
    {
        try
        {
            string uname = Session["vendor"].ToString();

            //var mobileNo = db.tblMembers.Single(x => x.username == uname);
            var get = from c in db.PaymentRequests
                      join d in db.tblVendors
                      on c.username equals d.Username
                      where c.username == uname && c.comments.Contains("Franchisee")
                      orderby c.date descending
                      select new { c.paiddate, c.netpay, c.username, c.transaction, c.status, c.cashavailable, c.amount, c.date, c.tax, c.sc };

            if (d1 != null && d2 != null)
            {
                get = get.Where(x => x.date >= d1 && x.date <= d2);
            }

            var bind = get.ToList();
            GridView1.DataSource = bind;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[8].Text == "Paid")
                e.Row.Cells[8].ForeColor = Color.Red;

            //Decimal am = Convert.ToDecimal(e.Row.Cells[3].Text);
            //e.Row.Cells[6].Text = (am * 20 / 100).ToString();
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}