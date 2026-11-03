using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_dispatch_history : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string vendor = Session["vendor"].ToString();
            if (!IsPostBack)
            {
                bindProducts(vendor);
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string vendor = Session["vendor"].ToString();
        bindProducts(vendor, TextBox3.Text);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("dispatch-history.aspx");
    }

    protected void bindProducts(string vendor, string uname = null)
    {

        var franch = from c in db.RequestProducts
                     join d in db.tblVendors
                     on c.username equals d.Username
                     join f in db.PinManagaers
                        on c.pin_number equals f.pinnumbers into grp
                     from g in grp.DefaultIfEmpty()
                     join m in db.tblActivations
                     on g.pinnumbers equals m.status
                     join n in db.tblMembers
                     on m.username equals n.username
                     where d.Username == vendor
                     select new
                     {
                         n.username,
                         n.name,
                         plan = c.name,
                         c.product,
                         c.shipment,
                         c.cost,
                         c.senddate,
                         c.pin_number,
                         g.usedby,
                         c.status,
                         m.activatedDate
                     };
        var res = franch;
        if (uname != null)
            res = res.Where(x => x.username == uname || x.usedby == uname);
        GridView1.DataSource = res.OrderByDescending(x => x.activatedDate);
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string vendor = Session["vendor"].ToString();
        GridView1.PageIndex = e.NewPageIndex;
        bindProducts(vendor);
    }


}