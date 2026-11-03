using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class forgot : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            var mob = (from c in db.tblMembers
                       where c.username == userid.Value
                       select c).SingleOrDefault();

            string msg = "Dear " + mob.name + ", Your DLS Biotech login password is " + mob.password + "";
            sm.sms(mob.mobile, msg, "1407169591232754869");

            Label2.Visible = true;
            Label3.Visible = false;

        }
        catch
        {
            Label3.Visible = true;
        }
    }
}