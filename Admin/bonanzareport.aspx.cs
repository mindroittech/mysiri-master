using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_kyc_history : System.Web.UI.Page
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string uname = TextBox1.Text;
        bind(uname);
    }

    private void bind(string uname = null)
    {

        var members = from c in db.tblMembers
                      where c.isActive == 1 && c.RepurchaseWallet > 0
                      select new
                      {
                          c.username,
                          c.name,
                          c.RepurchaseWallet
                         
                      };

        var get = members;
        if (!string.IsNullOrEmpty(uname))
            get = get.Where(x => x.username == uname);
        GridView1.DataSource = get.OrderByDescending(x => x.RepurchaseWallet);
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}