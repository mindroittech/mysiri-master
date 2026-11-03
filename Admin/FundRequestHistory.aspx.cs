using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FundRequestHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            string admin = Session["admin"].ToString();

            if (!IsPostBack)
                bind();
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string user = null;
        DateTime? d1 = null;
        DateTime? d2 = null;
        string plan = null;
        bool paging = true;
        if (TextBox1.Text != string.Empty)
        {
            user = TextBox1.Text;
            paging = false;
        }

        GridView1.AllowPaging = paging;
        bind(user);
    }

    protected void bind(string user = null)
    {
        var funds = from c in db.FranchiseeWalletRequests
                    join d in db.tblVendors
                     on c.username equals d.Username
                    orderby c.pid descending
                    select new
                    {
                        d.Username,
                        d.VendorName,
                        c.pid,
                        c.amountRequired,
                        c.status,
                        c.counterfoil,
                        c.createdDate,
                        c.remarks,
                        c.paymode,
                        c.ActionOn
                    };
        if (user != null)
            funds = funds.Where(x => x.Username == user);
        GridView1.DataSource = funds;
        GridView1.DataBind();
    }
}