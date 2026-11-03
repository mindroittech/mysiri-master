using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class change_password : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();
            if (Request.QueryString["s"] == "update")
            {
                Label5.Visible = true;
                Label5.Text = "Your password has been updated";
            }


        }
        catch
        {
            Response.Redirect("../login.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string uname = Session["username"].ToString();

        var res = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
        if (res.password == TextBox1.Text)
        {
            res.password = TextBox2.Text;
            try
            {
                db.SubmitChanges();
                Response.Redirect("change-password.aspx?s=update");

            }
            catch (Exception ex)
            {
                Label4.Visible = true;
                Label4.Text = ex.Message;
            }
        }
        else
        {
            Label4.Text = "Your old password is incorrect!!";
            Label4.Visible = true;
        }
    }
}