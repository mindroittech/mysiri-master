using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class confirm : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["pro"] == "sub")
        {
            Panel1.Visible = true;
            Label8.Text = Request.QueryString["n"];

            string uname = Request.QueryString["u"];

            var q = (from c in db.tblMembers
                     where c.username == uname
                     select c).SingleOrDefault();

            lblUserName.Text = q.username;
            lblPassword.Text = q.password;
        }


    }
}