using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_change_pwd : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();

            if (Request.QueryString["g"] == "t")
            {
                Label5.Visible = true;
                Label5.Text = "Your password has been updated";
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string adminUser = "";
        string role = Session["role"].ToString();
        if (role == "Admin")
            adminUser = Session["admin"].ToString();
        else
            adminUser = Session["vendor"].ToString();

        var res = db.tblVendors.Where(x => x.Username == adminUser).SingleOrDefault();
        if (res.Password == TextBox1.Text)
        {
            try
            {
                res.Password = TextBox2.Text;
                db.SubmitChanges();
                Response.Redirect("change-pwd.aspx?g=t");

            }
            catch (Exception ex)
            {
                Label4.Visible = true;
                Label4.Text = ex.Message;
            }
        }
        else
        {
            Label4.Visible = true;
            Label4.Text = "You old password does not match";
        }
    }
}