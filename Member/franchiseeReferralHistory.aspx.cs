using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Franchisee_ReferralHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string user = Session["username"].ToString();

            if (!IsPostBack)
            {


                bind(user);
                
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../login.aspx");
        }
    }
    private void bind(string user = "")
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
        if (user != "")
        {
            get = get.Where(x => x.Username == user);
        }
        GridView1.DataSource = get;
        GridView1.DataBind();
    }


}