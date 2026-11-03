using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_supplier_master : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            var get = from c in db.tblSuppliers
                      where c.IsActive == true
                      select c;
            GridView1.DataSource = get;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "delete")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            var get = db.tblSuppliers.Where(x => x.SupplierId == id).SingleOrDefault();

            get.IsActive = false;
            db.SubmitChanges();

            Response.Redirect("supplier-master.aspx");
        }

    }
}