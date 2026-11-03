using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_franchisees : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();
            if (!IsPostBack)
            {

                var get = from c in db.tblVendors
                          join d in db.tblRoles
                          on c.RoleId equals d.RoleId
                          where c.IsActive == true && d.role == "Franchisee"
                          && c.isStatusActive == true
                          select new
                          {
                              c.CompanyName,
                              c.CompanyCode,
                              c.VendorName,
                              c.Username,
                              c.Pincode,

                              c.State,
                              c.Email,
                              c.Phone,
                              c.City,
                              c.Id,
                              c.Address
                          };
                GridView1.DataSource = get;
                GridView1.DataBind();

            }

        }
        catch
        {
            Response.Redirect("../login.aspx");
        }

    }
}