using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ReferralHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();

            if (!IsPostBack)
            {

                if (role == "Admin")
                    bind();
                else
                {
                    bind(Session["vendor"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    private void bind(string vendor = "")
    {
        var get = from c in db.tblReferrals
                  join d in db.tblRoles
                  on c.RoleId equals d.RoleId
                  orderby c.CreatedDate descending
                  select new
                  {
                      c.CompanyName,
                      c.CompanyCode,
                      c.VendorName,
                      c.Username,
                      c.Pincode,
                      c.Country,
                      c.State,
                      c.Email,
                      c.Phone,
                      c.City,
                      c.CreatedDate,
                      c.status,
                      c.remarks,
                      c.Amount,
                      c.slip,
                      d.RoleName,
                      c.UTR,
                      c.bank,
                      c.depositeDate
                  };
        if (vendor != "")
        {
            get = get.Where(x => x.Username == vendor);
        }
        GridView1.DataSource = get;
        GridView1.DataBind();
    }


}