using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ActivationReport : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

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
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
        {
            DateTime? d1 = Convert.ToDateTime(TextBox1.Text);
            DateTime? d2 = Convert.ToDateTime(TextBox2.Text);
            bind(d1, d2, null);
        }
        else
            bind();

    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text != string.Empty)
            bind(null, null, TextBox3.Text);
        else
            bind();
    }
    private void bind(DateTime? d1 = null, DateTime? d2 = null, string uname = null)
    {
        var get = from c in db.tblActivations
                  join d in db.tblMembers
                  on c.username equals d.username
                  join f in db.PinManagaers
                  on c.status equals f.pinnumbers
                  //where f.IsFreePin == false
                  select new
                  {
                      c.username,
                      d.name,
                      c.activatedDate,
                      d.mobile,
                      c.newPlan,
                      d.district,
                      d.state,
                      pintype = f.IsFreePin == true ? "Free" : "Paid",
                      c.activatedBy,
                      d.sid
                  };


        GridView1.AllowPaging = true;
        if (d1 != null && d2 != null)
        {
            get = get.Where(x => x.activatedDate.Value.Date >= d1 && x.activatedDate.Value.Date <= d2);
            GridView1.AllowPaging = false;
        }
        if (uname != null)
        {
            get = get.Where(x => x.username == uname || x.mobile == uname || x.name.Contains(uname));
            GridView1.AllowPaging = false;
        }
        GridView1.DataSource = get.OrderByDescending(x => x.activatedDate);
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void Button1_Click(object sender, EventArgs e)
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
        if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
        {
            DateTime? d1 = Convert.ToDateTime(TextBox1.Text);
            DateTime? d2 = Convert.ToDateTime(TextBox2.Text);
            bind(d1, d2, null);
        }
        else if (TextBox3.Text != string.Empty)
            bind(null, null, TextBox3.Text);
        else
            bind();

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Activation_Report.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        }
        GridView1.RenderControl(htw);

        GridView1.AllowPaging = true;
        GridView1.DataBind();
        Response.Write(sw.ToString());
        Response.End();
    }
}