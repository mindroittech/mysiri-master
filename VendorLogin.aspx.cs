using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_FranchiseeLogin : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void login_click(object sender, EventArgs e)
    {
        try
        {

            var login = (from c in db.tblVendors
                         join d in db.tblRoles
                         on c.RoleId equals d.RoleId
                         where c.Username == userid.Value && c.Password == psswd.Value
                         select new { c.Username, c.IsActive, d.role }).SingleOrDefault();
            if (login != null)
            {
                if (login.IsActive == false)
                {
                    Label1.Text = "Your membership has been deactivated,contact admin for more info";
                    Label1.Visible = true;
                    return;
                }

                Session["role"] = login.role;
                if (login.role == "Admin")
                {
                    Session["admin"] = login.Username;
                    Session["vendor"] = null;
                    Response.Redirect("admin/dashboard.aspx");
                }
                else
                {
                    Session["vendor"] = login.Username;
                    Session["admin"] = null;
                    Response.Redirect("admin/vdashboard.aspx");
                }
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