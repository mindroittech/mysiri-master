using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_bvReport : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();
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


    protected void bind()
    {
        string uname = Session["username"].ToString();
        var payout = from c in db.BVReports
                     where c.username == uname
                     orderby c.date descending
                     select new
                     {
                         c.date,
                         c.oldLeft,
                         c.oldRight,
                         c.newLeft,
                         c.newRight,
                         totalLeft = c.oldLeft + c.newLeft,
                         totalRight = c.oldRight + c.newRight,
                         c.selfBv,
                         c.maintenanceBv,
                         c.income
                     };

        GridView1.DataSource = payout;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}