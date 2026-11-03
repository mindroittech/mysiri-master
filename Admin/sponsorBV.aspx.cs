using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default3 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            db.CommandTimeout = 7200;

            string uname = Session["admin"].ToString();
            if (!IsPostBack)
            {
                //bind();
               

            }
           

        }

        catch (Exception ex)
        {
            Response.Redirect("../index.html");

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text))
            {
                return;
            }

            DateTime d1, d2;
            if (!DateTime.TryParse(TextBox1.Text, out d1) || !DateTime.TryParse(TextBox2.Text, out d2))
            {
                return;
            }

            d2 = d2.Date.AddDays(1).AddSeconds(-1);

            decimal pvFilter = 0;
            if (!string.IsNullOrEmpty(TextBox3.Text))
            {
                decimal.TryParse(TextBox3.Text.Trim(), out pvFilter);
            }
            if (pvFilter <= 0)
            {
                pvFilter = 1;  // ✅ Always filter pv >= 1 or whatever is in the textbox (but > 0)
            }

            bind(d1, d2, pvFilter);
        }
        catch (Exception ex)
        {
            Response.Redirect("../index.html");
        }
    }
    protected void bind(DateTime dateFrom, DateTime dateTo, decimal pvFilter = 1)
    {
        var membersQuery = db.tblMembers.AsQueryable();

        var result = membersQuery.Select(user => new
        {
            id = user.username,
            name = user.name,
            mobile = user.mobile,
            pv = (
                from m in db.tblMembers
                join act in db.tblActivations on m.username equals act.username
                join p in db.tblPlans on act.newPlan equals p.PlanName
                where m.sid == user.username
                   && act.activatedDate >= dateFrom
                   && act.activatedDate <= dateTo
                select (decimal?)p.PvAmount
            ).Sum() ?? 0
        })
        .Where(x => x.pv >= pvFilter )   // ✅ Now it filters pv >= entered digit (or 0 if not entered)
        .ToList();

        GridView1.DataSource = result;
        GridView1.DataBind();

        if (GridView1.Rows.Count > 0)
        {
            Button2.Visible = true;
        } 
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
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=SponsorBV_Report.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            // Remove paging to export all rows
            GridView1.AllowPaging = false;

            // Rebind data to ensure all rows are included
            DateTime d1, d2;
            decimal pvFilter = 1;
            if (DateTime.TryParse(TextBox1.Text, out d1) && DateTime.TryParse(TextBox2.Text, out d2))
            {
                d2 = d2.Date.AddDays(1).AddSeconds(-1);
                if (!string.IsNullOrEmpty(TextBox3.Text))
                {
                    decimal.TryParse(TextBox3.Text.Trim(), out pvFilter);
                    if (pvFilter <= 0) pvFilter = 1;
                }

                bind(d1, d2, pvFilter);
            }

            GridView1.HeaderRow.Style.Add("background-color", "#ffffff");
            for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
            {
                GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                GridView1.HeaderRow.Cells[i].Style.Add("color", "white");
                GridView1.HeaderRow.Cells[i].Style.Add("font-weight", "bold");
            }

            GridView1.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            // Handle export error
        }
    }

}