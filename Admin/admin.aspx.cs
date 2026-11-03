using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_admin : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();


            if (Request.QueryString["pl"] == "sub")
            {
                Label3.Text = Request.QueryString["n"] + " " + Request.QueryString["pt"] + " PINs successfully assigned to the member";
                Label3.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }

    protected void bind()
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "assign")
        {
            int pid = Convert.ToInt32(e.CommandArgument);
            var get = (from c in db.PinRequests
                       where c.pid == pid
                       select c).SingleOrDefault();
            if (get.status != "Sent")
            {
                Response.Redirect("assign-pin.aspx?id=" + get.pid);
            }
            else
            {
                Response.Redirect("admin.aspx");
            }
        }
        if (e.CommandName == "rem")
        {
            int pid = Convert.ToInt32(e.CommandArgument);
            var del = db.PinRequests.Where(x => x.pid == pid).SingleOrDefault();
            db.PinRequests.DeleteOnSubmit(del);
            db.SubmitChanges();
            Response.Redirect("admin.aspx");
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}