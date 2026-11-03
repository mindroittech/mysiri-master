using iTextSharp.text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class Admin_Default3 : System.Web.UI.Page
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
                if (Request.QueryString["pl"]=="sub")
                {
                    lblerror.Text = "Salary income has been added successfully and is now available in Franchisee Payout page";
                    lblerror.ForeColor = Color.Green;
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
            var franch = db.tblVendors.Where(x => x.Username == uname).SingleOrDefault();
            if (franch != null)
            {
                txtName.Text = franch.VendorName;
            }
            else
                txtName.Text = "";
        }
        catch
        {
            txtName.Text = "";
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        var franch = db.tblVendors.Where(x => x.Username == txtID.Text.Trim()).SingleOrDefault();
        if (franch == null)
        {
            lblerror.Text = "Franchisee ID not found";
            lblerror.ForeColor = Color.Red;
            return;
        }
        DateTime date;

        if (!DateTime.TryParseExact(txtdate.Text, "yyyy-MM-dd",
         CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            lblerror.Text = "⚠️ Please select a valid date.";
            diverr.Visible = true;
            return;
        }
        decimal salary = Convert.ToDecimal(txtLoanAmt.Text);

         int count = db.Payouts
                          .Count(x => x.username == franch.Username && x.desc == "Salary Income");

         // ✅ Only for 10 salaries
        if (count < 11)
        {
            db.Payouts.InsertOnSubmit(new Payout
            {
                username = franch.Username,
                credit = salary,
                netpayout = salary,
                debit = 0,
                tax = 0,
                desc = "Salary Income",
                remarks = "Monthly Franchisee Salary",
                status = "Send",
                date = date.Date,
                confirmdate = date
            });

            var insert = new PaymentRequest
            {
                date = date,
                username = franch.Username,
                cashavailable = salary,
                amount =salary,
                tax = salary * 2 / 100,
                sc = salary * 8 / 100,
                netpay =salary * 90 / 100,
                status = "Pending",
                comments = "Salary Pay"
            };
            db.PaymentRequests.InsertOnSubmit(insert);

            db.SubmitChanges();
            Response.Redirect("manualentryfran.aspx?pl=sub");
        }
        else
        {
            lblerror.Text = "Salary bonus credit limit has been exceeded. Please review the limit before proceeding.";
            lblerror.ForeColor = Color.Red;
            return;

        }
       

    }
}