using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FundTransferReport : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (!IsPostBack)
                bind();
        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }

    protected void bind(string uname = null)
    {
        try
        {
            string admin = Session["admin"].ToString();
            var me = db.tblVendors.SingleOrDefault(x => x.Username == uname);

            var get = from c in db.FranchiseeWallets
                      where c.username == uname && c.status == "Send"
                      && c.IncomeFrom == admin
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


        }
        catch
        {
            Label9.Visible = true;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bind(TextBox1.Text);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}