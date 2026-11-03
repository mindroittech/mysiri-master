using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

public partial class admin_withdraw_history : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (Session["role"].ToString() != "Admin")
            {
                Response.Redirect("access-denied.aspx");
            }

            if (!IsPostBack)
            {

                bind(DateTime.Today.Date, DateTime.Today.Date, Request.QueryString["p"]);
            }

        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void bind(DateTime? d1 = null, DateTime? d2 = null, string type = null, string user = null)
    {

        var get = from c in db.PaymentRequests
                  where c.status == "Paid" && c.comments == type
                  orderby c.paiddate descending
                  join d in db.tblMembers
                  on c.username equals d.username
                  join f in db.tblRepurchaseRanks
                       on d.rankid equals f.RankId into grp
                  from g in grp.DefaultIfEmpty()
                  select new
                  {
                      c.paymentid,
                      c.username,
                      c.amount,
                      d.mobile,
                      c.transaction,
                      c.cashavailable,
                      c.comments,
                      c.date,
                      c.paiddate,
                      c.deduction,
                      c.tax,
                      c.sc,
                      c.netpay,
                      c.status,
                      d.name,
                      g.RankName
                  };
        if (d1 != null && d2 != null)
            get = get.Where(x => x.paiddate.Value.Date >= d1 && x.paiddate.Value.Date <= d2);
        if (user != null)
            get = get.Where(x => x.username == user);
        GridView1.DataSource = get;
        GridView1.DataBind();

        if (GridView1.Rows.Count > 0)
            Button3.Visible = true;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime? d1 = null;
        DateTime? d2 = null;
        string user = null;
        if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
        {
            d1 = Convert.ToDateTime(TextBox1.Text);
            d2 = Convert.ToDateTime(TextBox2.Text);
        }
        if (txtUser.Text != string.Empty)
        {

            user = txtUser.Text;

        }
        bind(d1, d2, Request.QueryString["p"], user);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("withdraw-history.aspx?p=" + Request.QueryString["p"]);
    }

    protected void Button3_Click(object sender, EventArgs e)
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
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "WithdrawHistory.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        //GridView1.AllowPaging = false;
        //Button3_Click(null, null);
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");

        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        }
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

}