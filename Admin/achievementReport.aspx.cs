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
                bind("");
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("../index.html");
        }
    }
    protected void bind(string usernameFilter)
    {
        var payout = from c in db.Payouts
                     where c.credit != 0
                           && c.match != null
                           && c.status == "Send"
                           && c.desc == "Achievement Bonus"
                           && (string.IsNullOrEmpty(usernameFilter) || c.username.Contains(usernameFilter))
                     orderby c.confirmdate descending
                     select new
                     {
                         c.username,
                         confirmdate = c.confirmdate.Value.AddMinutes(1),
                         c.Left_Previous,
                         c.Right_Previous,
                         c.Left_New,
                         c.Right_New,
                         c.match,
                         c.paid
                     };

        GridView1.DataSource = payout.ToList();
        GridView1.DataBind();
    }

    //protected void bind()
    //{
    //    var payout = from c in db.Payouts
    //                 where c.credit != 0 && c.match != null && c.status == "Send" && c.desc== "Achievement Bonus"
    //                 orderby c.confirmdate descending
    //                 select new
    //                 {
    //                     c.username,
    //                     confirmdate = c.confirmdate.Value.AddMinutes(1),
    //                     c.Left_Previous,
    //                     c.Right_Previous,
    //                     c.Left_New,
    //                     c.Right_New,
    //                     c.match,
    //                     c.paid,

    //                 };

    //    GridView1.DataSource = payout;
    //    GridView1.DataBind();
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (TextBox1.Text != string.Empty)
        {
            bind(TextBox1.Text.Trim());
        }
    }
}