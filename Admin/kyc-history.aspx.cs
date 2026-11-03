using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_kyc_history : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            if (!IsPostBack)
            {
                bind();
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string uname = TextBox1.Text;
        bind(uname);
    }

    private void bind(string uname = null)
    {

        var members = from c in db.tblMembers
                      where c.isActive == 1 && c.isPanVerifed != null && c.UploadPan != null
                      select new
                      {
                          c.username,
                          c.name,
                          c.pan,
                          c.account,
                          c.ifsc,
                          c.UploadPan,
                          c.UploadBank,
                          c.isPanVerifed,
                          c.UsedDate,
                          c.EligibleDate,
                          IsApproved = c.isPanVerifed.HasValue ? c.isPanVerifed.Value ? "<span style='color:Green'>Approved<span>" : "<span style='color:Red'>Rejected</span>" : "<span style='color:Black'>Pending</span>"
                      };

        var vendors = from c in db.tblVendors
                      where c.IsActive == true && c.isKyc != null && c.UploadPAN != null
                      select new
                      {
                          username = c.Username,
                          name = c.VendorName,
                          pan = c.PAN,
                          c.account,
                          c.ifsc,
                          UploadPan = c.UploadPAN,
                          c.UploadBank,
                          isPanVerifed = c.isKyc,
                          c.UsedDate,
                          c.EligibleDate,
                          IsApproved = c.isKyc.HasValue ? c.isKyc.Value ? "<span style='color:Green'>Approved<span>" : "<span style='color:Red'>Rejected</span>" : "<span style='color:Black'>Pending</span>"
                      };

        var get = members.Union(vendors);
        if (!string.IsNullOrEmpty(uname))
            get = get.Where(x => x.username == uname);
        GridView1.DataSource = get.OrderByDescending(x => x.EligibleDate);
        GridView1.DataBind();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "reject")
        {
            string username = e.CommandArgument.ToString();

            if (username.Length == 7)
            {
                var member = db.tblVendors.Where(c => c.Username == username).SingleOrDefault();
                member.isKyc = false;
                db.SubmitChanges();
            }
            else
            {
                var member = db.tblMembers.Where(c => c.username == username).SingleOrDefault();
                member.isPanVerifed = false;
                db.SubmitChanges();
            }
            Response.Redirect("kyc-history.aspx?s=rejected");
        }

    }
}