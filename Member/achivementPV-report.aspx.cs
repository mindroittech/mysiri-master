using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_Default : System.Web.UI.Page
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
        var payout = from c in db.Payouts
                     where c.credit != 0 && c.match != null && c.status == "Send" && c.desc == "Achievement Bonus" && c.username==uname
                     orderby c.confirmdate descending
                     select new
                     {
                        
                         confirmdate = c.confirmdate.Value.AddMinutes(1),
                         c.Left_Previous,
                         c.Right_Previous,
                         c.Left_New,
                         c.Right_New,
                         c.match,
                         c.paid,

                     };

        GridView1.DataSource = payout;
        GridView1.DataBind();

        GridView1.DataSource = payout;
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}