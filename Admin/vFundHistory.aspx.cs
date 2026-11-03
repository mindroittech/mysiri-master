using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vFundHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string vendor = Session["vendor"].ToString();

            if (!IsPostBack)
            {
                bind(vendor);
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }


    private void bind(string user = null, DateTime? d1 = null, DateTime? d2 = null, string plan = null)
    {
        var history1 = from c in db.FranchiseeWalletRequests
                       where c.username == user
                       orderby c.pid descending
                       select new
                       {
                           c.username,
                           c.PlanName,
                           c.amount,
                           c.counterfoil,
                           c.bank,
                           c.status,
                           c.amountRequired,
                           c.paymode,
                           c.remarks,
                           c.transactionno,
                           c.ActionOn,
                           c.createdDate
                       };

        if (user != null)
        {
            history1 = history1.Where(x => x.username == user);
        }
        GridView1.DataSource = history1.ToList();
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        bind();
    }
}