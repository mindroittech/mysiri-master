using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_addLoan : System.Web.UI.Page
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
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    var loan = db.tblLoans.SingleOrDefault(x => x.loanId == id);

                    txtID.Enabled = txtLoanAmt.Enabled = txtConfirmLoanAmt.Enabled = false;
                    txtID.Text = loan.username;
                    findName(txtID.Text);
                    txtLoanAmt.Text = txtConfirmLoanAmt.Text = loan.loanAmount.ToString();
                    txtRemarks.Value = loan.remarks;
                    txtPercent.Text = loan.recoveryPercent.ToString();
                    Button1.Text = "Update";
                }
                if (Request.QueryString["uname"] != null)
                {
                    txtID.Text = Request.QueryString["uname"].ToString();
                    findName(txtID.Text);
                   
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
        string uname = "";

        // Find the user in tblMembers
        var find = (from c in db.tblMembers
                    where c.username == txtID.Text.Trim()
                    select c).SingleOrDefault();

        // If user not found in tblMembers, check in tblVendors
        if (find == null)
        {
            var franch = db.tblVendors.Where(x => x.Username == txtID.Text.Trim()).SingleOrDefault();
            if (franch == null)
            {
                Label1.Visible = true;
                return;
            }
            else
            {
                uname = franch.Username;
            }
        }
        else
        {
            uname = find.username;
        }

        // Handling update or insert
        if (Request.QueryString["id"] != null)
        {
            // Update loan if id is present in the query string
            int id = Convert.ToInt32(Request.QueryString["id"]);
            var loan = db.tblLoans.SingleOrDefault(x => x.loanId == id);

            if (loan != null)
            {
                loan.recoveryPercent = Convert.ToInt32(txtPercent.Text);
                loan.remarks = txtRemarks.Value;
                db.SubmitChanges();
                Response.Redirect("activeLoans.aspx?pl=sub");
                return;
            }
        }
        var userLoans = (from c in db.tblLoans
                         where c.username == txtID.Text.Trim()
                         select c).ToList(); // Changed to ToList() to get all matching records
        var activeLoan = userLoans.FirstOrDefault(l => l.status == "Active" || l.status == "Closed");
        if (activeLoan != null)
        {
            diverr.Visible = true;
            return;
        }

        var activeLoan1 = userLoans.FirstOrDefault(l => l.status == "Active");
        if (activeLoan1 != null)
        {

            diverr.Visible = true;
            return;
        }
        // Insert a new loan record
        var ins = new tblLoan();
        ins.date = DateTime.UtcNow.AddHours(5.5);
        ins.username = txtID.Text;
        ins.status = "Active";
        ins.loanAmount = Convert.ToDecimal(txtLoanAmt.Text);
        ins.recoveredAmount = 0;
        ins.recoveryPercent = Convert.ToInt32(txtPercent.Text);
        ins.remarks = txtRemarks.Value;

        db.tblLoans.InsertOnSubmit(ins);
        db.SubmitChanges();

        var ins1 = new tblLoanHistory();
        ins1.date = DateTime.UtcNow.AddHours(5.5);
        ins1.username = txtID.Text;
        ins1.status = "Active";
        ins1.loanAmount = Convert.ToDecimal(txtLoanAmt.Text);
        ins1.recoveredAmount = 0;
        ins1.recoveryPercent = Convert.ToInt32(txtPercent.Text);
        ins1.remarks = txtRemarks.Value;

        db.tblLoanHistories.InsertOnSubmit(ins1);
        db.SubmitChanges();


        Response.Redirect("activeLoans.aspx?pl=sub");

    }

}
//}  protected void Button1_Click(object sender, EventArgs e)
//    {
//        string uname = "";
//        var find = (from c in db.tblMembers
//                    where c.username == txtID.Text.Trim()
//                    select c).SingleOrDefault();

//        if (find == null)
//        {
//            var franch = db.tblVendors.Where(x => x.Username == txtID.Text.Trim()).SingleOrDefault();
//            if (franch == null)
//            {
//                Label1.Visible = true;
//                return;
//            }
//            else
//                uname = franch.Username;
//        }
//        else
//        {
//            uname = find.username;
//        }

//        if (Request.QueryString["id"] != null)
//        {
//            int id = Convert.ToInt32(Request.QueryString["id"]);
//            var loan = db.tblLoans.SingleOrDefault(x => x.loanId == id);
//            //if (loan.status == "Closed")
//            //{

//            //    loan.loanAmount = Convert.ToDecimal(txtLoanAmt.Text);
//            //    loan.date = DateTime.UtcNow.AddHours(5.5);
//            //    loan.status = "Active";
//            //    loan.recoveredAmount = 0;
//            //    loan.recoveryPercent= Convert.ToInt32(txtPercent.Text);

//            //}
//            loan.recoveryPercent = Convert.ToInt32(txtPercent.Text);
//            loan.remarks = txtRemarks.Value;
//            db.SubmitChanges();
//            Response.Redirect("activeLoans.aspx?pl=sub");
//        }
//        else
//        {

//            var ins = new tblLoan();
//            ins.date = DateTime.UtcNow.AddHours(5.5);
//            ins.username = txtID.Text;
//            ins.status = "Active";
//            ins.loanAmount = Convert.ToDecimal(txtLoanAmt.Text);
//            ins.recoveredAmount = 0;
//            ins.recoveryPercent = Convert.ToInt32(txtPercent.Text);
//            ins.remarks = txtRemarks.Value;

//            db.tblLoans.InsertOnSubmit(ins);
//            db.SubmitChanges();
//            Response.Redirect("LoanHistory.aspx?pl=sub");
//        }

//    }
//}
//else if (Request.QueryString["uname"] != null)
//{
//    string name = Request.QueryString["uname"];

//    var user = db.tblLoans.FirstOrDefault(x => x.status == "Active" && x.username == name);
//    if(user != null)
//    {

//        var history = new tblLoanHistory
//        {
//            loanId = user.loanId,
//            username = user.username,
//            date = user.date,
//            loanAmount = user.loanAmount,
//            remarks = user.remarks,
//            status = user.status,
//            recoveryPercent = user.recoveryPercent,
//            recoveredAmount = user.recoveredAmount,
//            closedDate = user.closedDate
//        };
//        db.tblLoanHistories.InsertOnSubmit(history);
//        db.SubmitChanges();

//        var history1 = new tblLoanHistory
//        {
//            loanId = user.loanId,
//            username = user.username,
//            date = DateTime.UtcNow.AddHours(5.5),
//            loanAmount = Convert.ToInt32(TextBox1.Text),
//            remarks = txtRemarks.Value,
//            status = "Active",
//            recoveryPercent = Convert.ToInt32(txtPercent.Text),
//            recoveredAmount = 0                  
//        };
//        db.tblLoanHistories.InsertOnSubmit(history1);
//        db.SubmitChanges();


//        user.loanAmount= Convert.ToInt32(txttotal.Text);
//        user.recoveryPercent = Convert.ToInt32(txtPercent.Text);
//        user.remarks = txtRemarks.Value;
//        db.SubmitChanges();
//        Response.Redirect("activeLoans.aspx?pl=sub");
//    }
//}



    // closed loans

//else
//{
//    // Check if there are any closed loans and reopen one of them
//    var closedLoan = userLoans.FirstOrDefault(l => l.status == "Closed");
//    if (closedLoan != null)
//    {
//        var history = new tblLoanHistory
//        {
//            loanId=closedLoan.loanId,
//            username = closedLoan.username,
//            date = closedLoan.date,
//            loanAmount = closedLoan.loanAmount,
//            remarks = closedLoan.remarks,
//            status = closedLoan.status,
//            recoveryPercent = closedLoan.recoveryPercent,
//            recoveredAmount = closedLoan.recoveredAmount,
//            closedDate = closedLoan.closedDate
//        };
//        db.tblLoanHistories.InsertOnSubmit(history);
//        db.SubmitChanges();

//        //new
//        var history1 = new tblLoanHistory
//        {
//            loanId = closedLoan.loanId,
//            username = closedLoan.username,
//            date = DateTime.UtcNow.AddHours(5.5),
//            loanAmount = Convert.ToDecimal(txtLoanAmt.Text),
//            remarks = txtRemarks.Value,
//            status = "Active",
//            recoveryPercent = Convert.ToInt32(txtPercent.Text),
//            recoveredAmount = 0
//        };
//        db.tblLoanHistories.InsertOnSubmit(history1);
//        db.SubmitChanges();

//        // Reopen closed loan
//        closedLoan.loanAmount = Convert.ToDecimal(txtLoanAmt.Text);
//        closedLoan.date = DateTime.UtcNow.AddHours(5.5);
//        closedLoan.status = "Active";
//        closedLoan.recoveredAmount = 0;
//        closedLoan.recoveryPercent = Convert.ToInt32(txtPercent.Text);
//        closedLoan.remarks= txtRemarks.Value;
//        db.SubmitChanges();
//        Response.Redirect("activeLoans.aspx?pl=sub");
//    }
//    else
//    {
//        // Insert a new loan record
//        var ins = new tblLoan();
//        ins.date = DateTime.UtcNow.AddHours(5.5);
//        ins.username = txtID.Text;
//        ins.status = "Active";
//        ins.loanAmount = Convert.ToDecimal(txtLoanAmt.Text);
//        ins.recoveredAmount = 0;
//        ins.recoveryPercent = Convert.ToInt32(txtPercent.Text);
//        ins.remarks = txtRemarks.Value;

//        db.tblLoans.InsertOnSubmit(ins);
//        db.SubmitChanges();
//        Response.Redirect("LoanHistory.aspx?pl=sub");
//    }
//}