using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vCashWallet : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["vendor"].ToString();
            if (!IsPostBack)
                bind();
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
            string uname = Session["vendor"].ToString();

            var me = db.tblVendors.SingleOrDefault(x => x.Username == uname);

            var get = from c in db.FranchiseeWallets
                      where c.username == uname && c.status == "Send"
                      join d in db.tblVendors
                      on c.username equals d.Username
                      orderby c.confirmdate descending
                      select new
                      {
                          credit = c.credit,
                          c.date,
                          c.confirmdate,
                          c.desc,
                          d.VendorName,
                          c.debit,
                          d.Username,
                          c.remarks
                      };

            GridView1.DataSource = get;
            GridView1.DataBind();

            Decimal? credits = get.Sum(x => x.credit);
            Decimal? debits = get.Sum(x => x.debit);

            Label7.Text = credits.HasValue ? credits.Value.ToString() : "0";
            Label8.Text = debits.HasValue ? debits.Value.ToString() : "0";
            lblBalance.Text = me.CashWallet.ToString();
        }
        catch
        {

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}