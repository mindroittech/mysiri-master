using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StockRequest : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bind();
                if (Request.QueryString["p"] == "sub")
                {
                    lblMessage.Text = "Stock transferred successfully";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void GridView_PageIndex(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        bind(txtID.Text);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockRequests.aspx");
    }

    private void bind(string username = "")
    {
        string currentUser = Session["role"].ToString() == "Admin" ? Session["admin"].ToString() : Session["vendor"].ToString();
        var history1 = (from c in db.StockRequests
                        join f in db.tblVendors
                            on c.VendorId equals f.Id
                        join d in db.tblVendors
                            on c.RequestTo equals d.Id
                        where d.Username == currentUser && c.Status == "Requested" && c.RequestType == "Product"
                        && c.IsActive == true
                        && f.IsActive == true
                        orderby c.SrId descending
                        select new
                        {
                            c.VendorId,
                            f.Username,
                            f.CompanyName,
                            c.Remarks,
                            c.Requestdate,
                            c.RequestTo,
                            c.PayMode,
                            c.AmountPaid,
                            c.SrId,
                            Reporter = d.CompanyName,
                            c.Status
                        });
        if (username != "")
        {
            history1 = history1.Where(x => x.Username == username);
        }
        GridView1.DataSource = history1.ToList();
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "products")
        {
            int srid = Convert.ToInt32(e.CommandArgument);

            if (Session["role"].ToString() == "Admin")
                Response.Redirect("StockTransfer.aspx?Id=" + srid);
            else
                Response.Redirect("vStockTransfer.aspx?Id=" + srid);
        }

        if (e.CommandName == "print")
        {
            int srid = Convert.ToInt32(e.CommandArgument);

            if (Session["role"].ToString() == "Admin")
                Response.Redirect("PrintStockTransfer.aspx?Id=" + srid);
            else
                Response.Redirect("vStockTransfer.aspx?Id=" + srid);
        }
    }
}