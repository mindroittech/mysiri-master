using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_direct_refferal : System.Web.UI.Page
{
    DataClassesDataContext
         db = new DataClassesDataContext();


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            if (!IsPostBack)
            {
                var get = from c in db.tblMembers
                          where c.sid == uname
                          select c;

                GridView1.DataSource = get;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("../login.aspx");
        }

    }
}