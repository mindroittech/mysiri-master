using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class product_history : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var prod = from c in db.RequestProducts
                       where c.username == Session["username"].ToString()
                       select c;
            GridView1.DataSource = prod;
            GridView1.DataBind();

        }
        catch
        {

            Response.Redirect("login.aspx");
        }
    }
}