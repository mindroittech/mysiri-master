using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RecoveryHistory : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);

                var loanData = db.tblLoans.SingleOrDefault(x => x.loanId == id);
                lblID.Text = loanData.username;

                var mem = db.tblMembers.SingleOrDefault(x => x.username == loanData.username);
                if (mem != null)
                {
                    lblName.Text = mem.name;
                }
                else
                {
                    var franch = db.tblVendors.SingleOrDefault(x => x.Username == loanData.username);
                    lblName.Text = franch.VendorName;
                }

                bind(id);

            }
        }

        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    public void bind(int loanId)
    {

        var loans = from c in db.PaymentRequests
                    where c.loanId == loanId
                    select new
                    {
                        c.loanId,
                        c.username,
                        c.deduction,
                        c.date,
                        c.status
                    };
        GridView1.DataSource = loans;
        GridView1.DataBind();

    }

}