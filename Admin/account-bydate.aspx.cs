using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class admin_account_bydate : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            
        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }


    protected void bind()
    {
        try
        {


            DateTime d1 = Convert.ToDateTime(TextBox1.Text);
            DateTime d2 = Convert.ToDateTime(TextBox2.Text);

            var get = from c in db.Payouts
                      where c.confirmdate.Value.Date >= d1 && c.confirmdate.Value.Date <= d2 && c.status == "Send"
                      join d in db.tblMembers
                      on c.username equals d.username
                      orderby c.date descending
                      select new { credit = c.credit, c.date, c.confirmdate,c.remarks, c.netpayout, c.tax, c.desc, d.name, c.debit, c.username };

            GridView1.DataSource = get;
            GridView1.DataBind();

            Decimal am = 0;
            Decimal deb = 0;
            foreach (var g in get)
            {
                am = am + (Decimal)g.credit;
                deb = deb + (Decimal)g.debit;
            }

            Label7.Text = am.ToString();
            Label8.Text = deb.ToString();

            if (get.Any())
                Button2.Visible = true;

        }
        catch
        { }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        bind();

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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Transactions.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        GridView1.Width = 300;
        GridView1.AllowPaging = false;
        //this.bind();
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}