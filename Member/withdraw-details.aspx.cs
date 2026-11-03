using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class withdraw_request : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            if (!IsPostBack)
            {
                bind();
            }
        }

        catch
        {
            Response.Redirect("../login.aspx");
        }

    }

    public bool IsVisible(string status)
    {

        if (status == "Paid")
            return true;
        else
            return false;
    }

    protected void bind(DateTime? d1 = null, DateTime? d2 = null, string type = null)
    {
        string uname = Session["username"].ToString();
        var get = from c in db.PaymentRequests
                  where c.username == uname
                  orderby c.date descending
                  select new
                  {
                      c.paymentid,
                      c.paiddate,
                      c.netpay,
                      c.username,
                      c.transaction,
                      c.status,
                      c.cashavailable,
                      c.deduction,
                      c.amount,
                      c.date,
                      c.tax,
                      c.sc,
                      c.comments
                  };
        if (d1 != null && d2 != null)
            get = get.Where(x => x.date.Value.Date >= d1 && x.date.Value.Date <= d2);
        if (type != null)
            get = get.Where(x => x.comments == type);

        GridView1.DataSource = get;
        GridView1.DataBind();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime? d1 = null, d2 = null;
        string type = null;
        if (fromDate.Value != string.Empty && toDate.Value != string.Empty)
        {
            d1 = Convert.ToDateTime(fromDate.Value);
            d2 = Convert.ToDateTime(toDate.Value);
        }
        if (RadioButtonList1.SelectedValue != string.Empty)
            type = RadioButtonList1.SelectedValue;

        bind(d1, d2, type);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[9].Text == "Paid")
                e.Row.Cells[9].ForeColor = Color.Red;
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Button1_Click(null, null);
    }
}