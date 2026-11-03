using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PrintStockTransfer : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();

    private static string role = "";
    private static string fcountry = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string adminUSer = Session["admin"].ToString();
            if (!IsPostBack)
            {
                var vendors = from c in db.tblVendors
                              join d in db.tblRoles
                              on c.RoleId equals d.RoleId
                              where c.IsActive == true && d.RoleName != "Admin"
                              select new { c.Username, c.CompanyName };
                drpVendor.DataSource = vendors;
                drpVendor.DataTextField = "CompanyName";
                drpVendor.DataValueField = "Username";
                drpVendor.DataBind();
                drpVendor.Items.Insert(0, new ListItem("--Select--", ""));
                if (Request.QueryString["Id"] != null)
                {
                    int SrId = Convert.ToInt32(Request.QueryString["Id"]);

                    var get = (from c in db.StockRequests
                               join d in db.tblVendors
                               on c.VendorId equals d.Id
                               where c.SrId == SrId
                               select new { d.Username, d.RoleId, d.CompanyName, c.AmountPaid }).SingleOrDefault();
                    if (get != null)
                    {
                        txtAmount.Text = get.AmountPaid.ToString();
                        drpVendor.SelectedValue = get.Username;
                        txtVendor_Changed(null, null);
                        drpVendor.Enabled = false;
                        //lblName.Text = get.CompanyName;
                    }
                    bind(SrId);
                }

            }
        }
        catch (Exception)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void txtVendor_Changed(object sender, EventArgs e)
    {
        try
        {
            string username = drpVendor.SelectedValue;
            var user = db.tblVendors.SingleOrDefault(x => x.Username == username);
            if (user != null)
            {
                lblName.Text = user.Username;
                txtBilling.Text = user.Address + " " + user.Pincode;
                txtType.Text = role = db.tblRoles.Single(x => x.RoleId == user.RoleId).RoleName;
                fcountry = user.Country;

            }
            else
            {
                lblName.Text = "";
                txtBilling.Text = "";

                txtType.Text = role = "";
                fcountry = "";
            }
        }
        catch
        {

        }
    }

    private void bind(int SrId)
    {
        string vendor = Session["admin"].ToString();
        var stockRequest = from c in db.StockRequestProducts
                           join f in db.StockRequests
                           on c.SrId equals f.SrId
                           join d in db.tblProducts
                           on c.ProductId equals d.ProductId
                           where c.SrId == SrId
                           select new
                           {
                               c.ProductId,
                               d.ProductName,
                               d.ImageUrl,
                               Cost = d.DP,
                               Quantity = d.Quantity.HasValue ? d.Quantity.Value : 0,
                               c.QtyReq,
                               TotalCost = d.DP.Value * c.QtyReq.Value,
                               c.QtySent
                           };
        GridView1.DataSource = stockRequest.ToList();
        GridView1.DataBind();

        Decimal totalamt = 0;
        int totalQuantity = 0;
        foreach (var r in stockRequest)
        {
            int quant = Convert.ToInt32(r.QtyReq);
            Decimal cost = Convert.ToDecimal(r.Cost);

            totalamt += quant * cost;
            totalQuantity += quant;
        }
        lblCost.Text = totalamt.ToString();
        lblTotalQty.Text = totalQuantity.ToString();
    }
}