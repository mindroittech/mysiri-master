using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();


            if (!IsPostBack)
            {
                if (Request.QueryString["uname"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["uname"]);
                    var loan = db.tblLoans.SingleOrDefault(x => x.loanId == id);

                    txtID.Enabled = txtLoanAmt.Enabled = false;
                    txtID.Text = loan.username;
                    findName(txtID.Text);
                    string searchName = loan.username.Trim();
                    bind(searchName);
                    txtLoanAmt.Text  = loan.loanAmount.ToString();
                   
                }
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void txtUser_Changed(object sender, EventArgs e)
    {
        findName(txtID.Text);
    }
    private void bind(string name)
    {  
        var loans1 = from c in db.tblLoanHistories
                     where c.username == name
                     select new
                     {
                         c.loanId,
                         c.username,
                         c.loanAmount,
                         c.recoveredAmount,                   
                         c.date,
                         c.remarks                     
                     };

        GridView1.DataSource = loans1.ToList();
        GridView1.DataBind();
    }


    private void findName(string uname)
    {
        try
        {
            var get = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
            if (get != null)
                txtName.Text = get.name;

            else
            {
                var franch = db.tblVendors.Where(x => x.Username == uname).SingleOrDefault();
                if (franch != null)
                {
                    txtName.Text = franch.VendorName;
                }
                else
                    txtName.Text = "";
            }

        }
        catch
        {
            txtName.Text = "";
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request.QueryString["uname"]);
        var loan = db.tblLoans.SingleOrDefault(x => x.loanId == id);

        if (loan != null)
        {
            // adding new amount record
            var history1 = new tblLoanHistory
            {
                loanId = loan.loanId,
                username = loan.username,
                date = DateTime.UtcNow.AddHours(5.5),
                loanAmount = Convert.ToInt32(TextBox1.Text),
                remarks = txtRemarks.Text,
                status = "Active",
                recoveredAmount = loan.recoveredAmount
            };
            db.tblLoanHistories.InsertOnSubmit(history1);
            db.SubmitChanges();

            if (loan.status == "Closed")
                loan.status = "Active";

            loan.loanAmount = Convert.ToInt32(txttotal.Text);      
            loan.remarks = txtRemarks.Text;
            db.SubmitChanges();
            Response.Redirect("activeLoans.aspx?pl=sub");
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        var total = Convert.ToInt32(txtLoanAmt.Text) + Convert.ToInt32(TextBox1.Text);

        txttotal.Text = total.ToString();
       
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}