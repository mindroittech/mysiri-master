using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_login : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void login_click(object sender, EventArgs e)
    {
        try
        {
            var login = (from c in db.tblMembers
                         where (c.username == userid.Value || c.mobile == userid.Value) && c.password == psswd.Value
                         select c).SingleOrDefault();
            if (login != null)
            {

                if (login.isActive == 0)
                {
                    Label1.Text = "Your account has been deactivated";
                    Label1.Visible = true;
                    return;
                }
                if (login.status == "Inactive")
                {
                    Label1.Text = "Your account has been blocked";
                    Label1.Visible = true;
                    return;
                }

                Session["username"] = login.username;
                Session["name"] = login.name;
                Response.Redirect("member/dashboard.aspx");
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Invalid Username or Password";

            }
        }
        catch
        {
            Label1.Visible = true;
            Label1.Text = "Invalid Username or Password";

        }
    }
}