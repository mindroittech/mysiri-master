using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Admin_franchiseeWithdrawHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (Session["role"].ToString() != "Admin")
            {
                Response.Redirect("access-denied.aspx", false);
            }

        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Button1_Click(null, null);
    }

    protected void bind()
    {
        var get = from c in db.PaymentRequests
                  orderby c.date descending
                  join d in db.tblVendors
                  on c.username equals d.Username
                  where c.status != "Pending"
                  orderby c.paiddate descending
                  select new
                  {
                      c.paymentid,
                      pan=d.PAN,
                      d.Phone,
                      c.username,
                      c.amount,
                      c.transaction,
                      c.cashavailable,
                      c.comments,
                      c.date,
                      c.paiddate,
                      c.tax,
                      c.sc,
                      c.netpay,
                      c.status,
                      c.deduction,
                      name=d.VendorName,

                  };

        if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
        {
            DateTime d1 = Convert.ToDateTime(TextBox1.Text);
            DateTime d2 = Convert.ToDateTime(TextBox2.Text);

            get = get.Where(x => x.paiddate >= d1 && x.paiddate <= d2);
        }
        if (TextBox3.Text != string.Empty)
        {
            get = get.Where(x => x.username == TextBox3.Text || x.pan == TextBox3.Text);
        }

        GridView1.DataSource = get;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if ((TextBox1.Text == string.Empty || TextBox2.Text == string.Empty) && TextBox3.Text == string.Empty)
        {
            Label1.Text = "Enter atleast one search criteria";
            Label1.Visible = true;
        }
        else
        {
            Label1.Visible = false;
            bind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[6].Text == "Paid")
                e.Row.Cells[6].ForeColor = Color.Green;
            else
                e.Row.Cells[6].ForeColor = Color.Red;
        }
    }
}