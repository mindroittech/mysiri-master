using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_BvReport : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["admin"].ToString();
            if (!IsPostBack)
            {
                 bind();
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

            string type= RadioButtonList1.SelectedValue;

            if (type == "Repurchase")
                bind(d1, d2, RadioButtonList1.SelectedValue, txtMax.Text);
            else
                bindBinary(d1, d2, RadioButtonList1.SelectedValue, txtMax.Text);
        }
    }

    protected void bind(DateTime? d1 = null, DateTime? d2 = null, string type = null, string max = "")
    {

        var bvReport = (from c in db.tblBVs
                        where c.date >= d1 && c.date <= d2 && c.bvType == type
                        group c by c.username into grp
                        join d in db.tblMembers
                        on grp.Key equals d.username
                        select new
                        {
                            d.username,
                            d.name,
                            bvType = grp.Min(x => x.bvType),
                            left = type == "Binary" ? grp.Sum(x => x.lcarry) : grp.Sum(x => x.leftBv),
                            right = type == "Binary" ? grp.Sum(x => x.rcarry) : grp.Sum(x => x.rightBv),
                            lp = type == "Binary" ? grp.Sum(x => x.leftPoints) : null,
                            rp = type == "Binary" ? grp.Sum(x => x.rightPoints) : null,
                            d.mobile,
                            d.state
                        });

        var report = from c in bvReport
                     where (c.left > 0 && c.right > 0)
                     select new
                     {
                         c.username,
                         c.name,
                         c.bvType,
                         c.left,
                         c.right,
                         c.lp,
                         c.rp,
                         bv = c.left > c.right ? c.right : c.left,
                         match = c.lp > c.rp ? c.rp : c.lp,
                         c.mobile,
                         c.state
                     };
        if (max != string.Empty)
        {
            decimal maxPair = Convert.ToDecimal(txtMax.Text);
            report = report.Where(x => x.bv >= maxPair);
        }
        if (report.Any())
            Button2.Visible = true;

        GridView1.DataSource = report;
        GridView1.DataBind();
    }

    protected void bindBinary(DateTime? d1 = null, DateTime? d2 = null, string type = null, string max = "")
    {

        var bvReport =
                 from c in db.Payouts
                 where c.date >= d1 && c.date <= d2 && c.desc == "Matching Bonus"
                 group c by c.username into grp
                 let latest = grp.OrderByDescending(x => x.date).FirstOrDefault()
                 join d in db.tblMembers on grp.Key equals d.username
                 select new
                 {
                     d.username,
                     d.name,
                     bvType = "Binary",
                     left = latest.Left_New+latest.credit+latest.Left_Previous,
                     right = latest.Right_New+latest.credit+latest.Right_Previous,
                     bv = grp.Sum(x => x.credit),  // or latest.credit if you only want one record’s credit
                     match = grp.Sum(x => x.credit),
                     d.mobile,
                     d.state
                 };


        if (max != string.Empty)
        {
            decimal maxPair = Convert.ToDecimal(txtMax.Text);
            bvReport = bvReport.Where(x => x.bv >= maxPair);
        }
        if (bvReport.Any())
            Button2.Visible = true;

        GridView1.DataSource = bvReport;
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BvReport.xls"));
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