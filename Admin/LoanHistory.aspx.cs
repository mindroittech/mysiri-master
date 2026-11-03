using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LoanHistory : System.Web.UI.Page
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

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "moreloan")
        {
            string uname = e.CommandArgument.ToString();

            Response.Redirect("addLoan.aspx?uname=" + uname);
        }
    }
    public void bind(string uname = null)
    {

        var loans = from c in db.tblLoans
                    join d in db.tblMembers
                    on c.username equals d.username
                    group new { c, d } by c.username into grp
                    select new
                    {
                        username = grp.Key,
                        name = grp.Min(x => x.d.name),
                        loanAmount = grp.Sum(x => x.c.loanAmount),
                        recoveredAmount = grp.Sum(x => x.c.recoveredAmount),
                        balance = grp.Sum(x => x.c.loanAmount) - grp.Sum(x => x.c.recoveredAmount),
                    };
        var loans2 = from c in db.tblLoans
                     join d in db.tblVendors
                     on c.username equals d.Username
                     group new { c, d } by c.username into grp
                     select new
                     {
                         username = grp.Key,
                         name = grp.Min(x => x.d.VendorName),
                         loanAmount = grp.Sum(x => x.c.loanAmount),
                         recoveredAmount = grp.Sum(x => x.c.recoveredAmount),
                         balance = grp.Sum(x => x.c.loanAmount) - grp.Sum(x => x.c.recoveredAmount),
                     };
        GridView1.DataSource = loans.Union(loans2);
        GridView1.DataBind();

    }
    public void bind11(string uname = null)
    {
        var loans = from c in db.tblLoans
                    join d in db.tblMembers
                    on c.username equals d.username
                    group new { c, d } by c.username into grp
                    select new
                    {
                        username = grp.Key,
                        name = grp.Min(x => x.d.name),
                        loanAmount = grp.Sum(x => x.c.loanAmount),
                        recoveredAmount = grp.Sum(x => x.c.recoveredAmount),
                        balance = grp.Sum(x => x.c.loanAmount) - grp.Sum(x => x.c.recoveredAmount),
                    };

        var loans2 = from c in db.tblLoans
                     join d in db.tblVendors
                     on c.username equals d.Username
                     group new { c, d } by c.username into grp
                     select new
                     {
                         username = grp.Key,
                         name = grp.Min(x => x.d.VendorName),
                         loanAmount = grp.Sum(x => x.c.loanAmount),
                         recoveredAmount = grp.Sum(x => x.c.recoveredAmount),
                         balance = grp.Sum(x => x.c.loanAmount) - grp.Sum(x => x.c.recoveredAmount),
                     };

        var loans3 = from c in db.tblLoanHistories
                     join d in db.tblMembers
                     on c.username equals d.username
                     group new { c, d } by c.username into grp
                     select new
                     {
                         username = grp.Key,
                         name = grp.Min(x => x.d.name),
                         loanAmount = grp.Sum(x => x.c.loanAmount),
                         recoveredAmount = grp.Sum(x => x.c.recoveredAmount),
                         balance = grp.Sum(x => x.c.loanAmount) - grp.Sum(x => x.c.recoveredAmount),
                     };

        var loans4 = from c in db.tblLoanHistories
                     join d in db.tblVendors
                     on c.username equals d.Username
                     group new { c, d } by c.username into grp
                     select new
                     {
                         username = grp.Key,
                         name = grp.Min(x => x.d.VendorName),
                         loanAmount = grp.Sum(x => x.c.loanAmount),
                         recoveredAmount = grp.Sum(x => x.c.recoveredAmount),
                         balance = grp.Sum(x => x.c.loanAmount) - grp.Sum(x => x.c.recoveredAmount),
                     };

        // Union all queries
        var allLoans = loans.Union(loans2).Union(loans3).Union(loans4);

        // Bind the combined result to GridView
        GridView1.DataSource = allLoans;
        GridView1.DataBind();
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


