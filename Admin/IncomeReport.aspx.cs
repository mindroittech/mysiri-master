using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_IncomeReport : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["admin"].ToString();
            if (!IsPostBack)
            {
                // bind();
            }

        }

        catch (Exception ex)
        {
            Response.Redirect("../index.html");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text == string.Empty || TextBox2.Text == string.Empty)
        {

        }
        else
        {
            DateTime d1 = Convert.ToDateTime(TextBox1.Text);
            DateTime d2 = Convert.ToDateTime(TextBox2.Text);

            bind(d1, d2, RadioButtonList1.SelectedValue, txtMax.Text);
        }
    }

    protected void bind(DateTime? d1 = null, DateTime? d2 = null, string type = null, string max = "")
    {
        string comments = "Auto Pay";
        if (type == "Repurchase")
            comments = "Repurchase Pay";

        var report = (from c in db.PaymentRequests
                      where c.date >= d1 && c.date <= d2
                      group c by c.username into grp
                      join d in db.tblMembers
                      on grp.Key equals d.username
                      join f in db.tblRepurchaseRanks
                      on d.rankid equals f.RankId into grp1
                      from k in grp1.DefaultIfEmpty()
                      select new
                      {
                          d.username,
                          d.name,
                          d.mobile,
                          type = RadioButtonList1.SelectedItem.Text,
                          income = type == "Total" ? grp.Sum(x => x.amount) : grp.Where(x => x.comments == comments).Sum(x => x.amount),
                          rank = k != null ? k.RankName : null
                      });

        if (max != string.Empty)
        {
            decimal maxPair = Convert.ToDecimal(txtMax.Text);
            report = report.Where(x => x.income >= maxPair);
        }
        if (report.Any())
            Button2.Visible = true;

        GridView1.DataSource = report;
        GridView1.DataBind();
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "IncomeReport.xls"));
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}