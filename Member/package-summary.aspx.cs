using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_package_summary : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            string uname = Session["username"].ToString();

            if (!IsPostBack)
            {

                var get = from c in db.tblMembers
                          where c.username == uname
                          select new { c.username, date=c.joindate, Package = c.plan, activestatus = "Active" };

                //var get1 = from c in db.tblMembers
                //           where c.username == uname && c.goldAchieveDate!=null
                //           select new { c.username, date = c.goldAchieveDate, Package = "Gold", activestatus = "Active" };

                //var get2 = from c in db.tblMembers
                //           where c.username == uname && c.diamondAchieveDate != null
                //           select new { c.username, date = c.diamondAchieveDate, Package = "Diamond", activestatus = "Active" };


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