using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_tds_report : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            if (!IsPostBack)
            {
                //var findWeek = (from c in db.PaymentRequests
                //                orderby c.date descending
                //                select c.date).Distinct().OrderByDescending(x => x.Value);

                //int i = findWeek.Count();
                //foreach (var d in findWeek)
                //{
                //    drpWeek.Items.Add(new ListItem { Text = "Week-" + i + " " + d.Value.ToString("dd/MMM"), Value = d.Value.ToString() });
                //    i--;
                //}
            }
        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }


    protected void bind(string type, DateTime? d1 = null, DateTime? d2 = null, Decimal? minAmount = 0, Decimal? maxAmount = 0, string uname = null)
    {
        if (type == "Individual")
        {
            var mem = from c in db.PaymentRequests
                      where c.date >= d1 && c.date <= d2 && c.status == "Paid"
                      orderby c.date descending
                      join d in db.tblMembers
                      on c.username equals d.username
                      select new { c.username, c.paiddate, c.tax, d.pan, d.name, c.amount };

            var franch = from c in db.PaymentRequests
                         where c.date >= d1 && c.date <= d2 && c.status == "Paid"
                         orderby c.date descending
                         join d in db.tblVendors
                         on c.username equals d.Username
                         select new { c.username, c.paiddate, c.tax, pan = d.PAN, name = d.VendorName, c.amount };
            var get = mem.Union(franch);

            if (minAmount > 0)
                get = get.Where(x => x.tax >= minAmount);
            if (maxAmount > 0)
                get = get.Where(x => x.tax <= maxAmount);
            if (uname != null)
                get = get.Where(x => x.username == uname);
            GridView1.DataSource = get;
            GridView1.DataBind();
        }
        else
        {
            var mem = from c in db.PaymentRequests
                      where c.date >= d1 && c.date <= d2 && c.status == "Paid"
                      join d in db.tblMembers
                      on c.username equals d.username
                      group new { d, c } by d.pan into grp
                      select new
                      {
                          pan = grp.Key,
                          tax = grp.Sum(x => x.c.tax),
                          username = grp.Min(x => x.d.username),
                          paiddate = grp.Max(x => x.c.paiddate),
                          name = grp.Min(x => x.d.name),
                          amount = grp.Sum(x => x.c.amount)
                      };

            var franch = from c in db.PaymentRequests
                      where c.date >= d1 && c.date <= d2 && c.status == "Paid"
                      join d in db.tblVendors
                      on c.username equals d.Username
                      group new { d, c } by d.PAN into grp
                      select new
                      {
                          pan = grp.Key,
                          tax = grp.Sum(x => x.c.tax),
                          username = grp.Min(x => x.d.Username),
                          paiddate = grp.Max(x => x.c.paiddate),
                          name = grp.Min(x => x.d.VendorName),
                          amount = grp.Sum(x => x.c.amount)
                      };
            var get = mem.Union(franch);
            if (minAmount > 0)
                get = get.Where(x => x.tax >= minAmount);
            if (maxAmount > 0)
                get = get.Where(x => x.tax <= maxAmount);
            if (uname != null)
                get = get.Where(x => x.username == uname);
            GridView1.DataSource = get.OrderByDescending(x => x.paiddate);
            GridView1.DataBind();
        }


        if (GridView1.Rows.Count > 0)
            Button2.Visible = true;
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "TDS.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        GridView1.Width = 300;
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        }
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text == string.Empty || TextBox2.Text == string.Empty)
        {
            Label1.Text = "Date cannot be empty";
            Label1.Visible = true;
        }
        else
        {
            Label1.Visible = false;
            Decimal minAmt = 0;
            Decimal maxAmt = 0;
            DateTime d1 = Convert.ToDateTime(TextBox1.Text);
            DateTime d2 = Convert.ToDateTime(TextBox2.Text);
            string uname = null;

            if (txtMinAmount.Text != string.Empty)
            {
                minAmt = Convert.ToDecimal(txtMinAmount.Text);
            }
            if (txtMaxAmount.Text != string.Empty)
            {
                maxAmt = Convert.ToDecimal(txtMaxAmount.Text);
            }
            if (txtID.Text != string.Empty)
                uname = txtID.Text;
            bind(RadioButtonList1.SelectedValue, d1, d2, minAmt, maxAmt, uname);
        }
    }
}