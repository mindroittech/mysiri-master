using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_closedLoans : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (!IsPostBack)
                bind();

        }

        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }


    public void bind(string uname = null)
    {

        var loans = from c in db.tblLoans
                    join d in db.tblMembers
                    on c.username equals d.username
                    where c.status == "Closed"
                    select new
                    {
                        c.loanId,
                        c.username,
                        d.name,
                        c.loanAmount,
                        c.recoveredAmount,
                        balance = c.loanAmount - c.recoveredAmount,
                        c.recoveryPercent,
                        c.date,
                        c.status
                    };

        var loans2 = from c in db.tblLoans
                     join d in db.tblVendors
                     on c.username equals d.Username
                     where c.status == "Closed"
                     select new
                     {
                         c.loanId,
                         c.username,
                         name = d.VendorName,
                         c.loanAmount,
                         c.recoveredAmount,
                         balance = c.loanAmount - c.recoveredAmount,
                         c.recoveryPercent,
                         c.date,
                         c.status
                     };

        var loans3 = from c in db.tblLoanHistories
                    join d in db.tblMembers
                    on c.username equals d.username
                    where c.status == "Closed"
                    select new
                    {
                        c.loanId,
                        c.username,
                        d.name,
                        c.loanAmount,
                        c.recoveredAmount,
                        balance = c.loanAmount - c.recoveredAmount,
                        c.recoveryPercent,
                        c.date,
                        c.status
                    };

        var loans4 = from c in db.tblLoanHistories
                     join d in db.tblVendors
                     on c.username equals d.Username
                     where c.status == "Closed"
                     select new
                     {
                         c.loanId,
                         c.username,
                         name = d.VendorName,
                         c.loanAmount,
                         c.recoveredAmount,
                         balance = c.loanAmount - c.recoveredAmount,
                         c.recoveryPercent,
                         c.date,
                         c.status
                     };

        GridView1.DataSource = loans.Union(loans2);
       // GridView1.DataSource = loans.Union(loans2).Union(loans3).Union(loans4);
        GridView1.DataBind();

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "recovery")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            Response.Redirect("RecoveryHistory.aspx?id=" + id);
        }
        if (e.CommandName == "add")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("add-more-loan.aspx?uname=" + id);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string uname = TextBox1.Text;

            bind(uname);
        }
        catch
        {

        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}