using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_stock : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();
            if (role != "GoDown" && role != "Admin")
            {
                throw new Exception("Access denied");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                    bind(Request.QueryString["Id"]);
                //else
                //    bind();
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string uname = TextBox1.Text;
            bind(uname);
        }
        catch (Exception ex)
        {

        }
    }
    private void bind(string uname = null)
    {
        var get = from c in db.tblVendorStocks
                  join d in db.tblProducts
                  on c.ProductId equals d.ProductId
                  join f in db.tblVendors
                  on c.VendorId equals f.Id
                  select new { d.ProductName, c.ProductId, c.Stock, f.Username, f.CompanyName, f.VendorName };
        if (uname != null)
        {
            get = get.Where(x => x.Username == uname);
        }

        GridView1.DataSource = get;
        GridView1.DataBind();
    }
}