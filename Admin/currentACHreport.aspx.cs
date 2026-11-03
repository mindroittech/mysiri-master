using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string admin = Session["admin"].ToString();
            if (!IsPostBack)
            {
               
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("../index.html");
        }
    }
    protected void bind(string usernameFilter)
    {
        var payout = from c in db.tblMembers
                     where c.username == usernameFilter
                     select new
                     {
                         c.username,   
                         c.name,
                         c.alcarry,
                         c.arcarry,
                         c.alcount,
                         c.arcount
                     };

        GridView1.DataSource = payout;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != string.Empty)
        {
            bind(TextBox1.Text.Trim());
        }
    }
}