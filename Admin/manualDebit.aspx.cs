using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_Admin_manualDebit : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["admin"].ToString();
            if (Request.QueryString["v"] == "sub")
            {
                lbSuccess.InnerHtml = "Amount " + Request.QueryString["s"] + " franchisee account successfully";
                lbSuccess.Visible = true;
            }

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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    protected void GenerateOTP_Click(object sender, EventArgs e)
    {
        try
        {
            sendsms sm = new sendsms();
            decimal amt = 0;

            amt = Convert.ToDecimal(TextBox2.Text);
            string otp = Helper.GenerateOTP();
            Cache.Insert("FundOTP", otp, null, DateTime.UtcNow.AddHours(5.5).AddMinutes(20), TimeSpan.Zero);
            lblOtpMessage.Text = "Otp has been sent to your phone number";
            string msg = otp + " is the OTP to approve the Fund Request for the ID : " + txtUser.Text + " Name : " + lblName.InnerText +
                " for the amount Rs. " + amt + ". DLS Biotech";
            sm.sms("9380393866,9448415584", msg, "1407169591333295787");

        }
        catch
        {

        }
    }

    private void bind()
    {
        var bind = from c in db.FranchiseeWallets
                   join d in db.tblVendors
                   on c.username equals d.Username
                   where c.remarks.Contains("Manual")
                   orderby c.confirmdate descending
                   select new
                   {
                       c.username,
                       name = d.VendorName,
                       c.confirmdate,
                       c.remarks,
                       c.credit,
                       c.desc,
                       c.debit
                   };
        GridView1.DataSource = bind;
        GridView1.DataBind();
    }

    protected void txtUser_Changed(object sender, EventArgs e)
    {
        var get = db.tblVendors.Where(x => x.Username == txtUser.Text.Trim()).SingleOrDefault();
        if (get != null)
        {
            lblName.InnerText = get.VendorName;
            lbbal.InnerText = get.CashWallet.ToString();
        }
        else
        {
            lblName.InnerText = "";
            lbbal.InnerText = "0";
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            string uname = txtUser.Text.Trim();

            decimal Amount = Convert.ToDecimal(TextBox2.Text);
            var franch = db.tblVendors.Where(x => x.Username == uname).SingleOrDefault();
            string status = rdOption.SelectedValue == "1" ? "Credited to" : "Debited from";
            if (franch == null)
            {
                lbErrormsg.InnerText = "Invalid Franchisee ID.";
                lbErrormsg.Visible = true;
                return;
            }
            if (Cache["FundOTP"] == null || Cache["FundOTP"].ToString() != txtOTP.Text)
            {
                lbErrormsg.InnerText = "Invalid OTP";
                lbErrormsg.Visible = true;
                return;
            }

            if (rdOption.SelectedValue == "1")  // for credit
            {
                string desc = "Credit";
                string remarks = txtRemarks.Text;
                franch.CashWallet += Amount;
                var history = new FranchiseeWallet
                {
                    username = uname,
                    credit = Amount,
                    debit = 0,
                    date = DateTime.UtcNow.AddHours(5.5).Date,
                    confirmdate = DateTime.UtcNow.AddHours(5.5),
                    desc = desc,
                    remarks = "Manual Credit-" + remarks,
                    status = "Send",
                    IncomeFrom = admin
                };
                db.FranchiseeWallets.InsertOnSubmit(history);
                db.SubmitChanges();

                string msg = string.Format("Dear {0}, Your Cash Wallet is credited with Rs {1}. Current Balance is Rs {2}. DLS Biotech", franch.VendorName, Amount, franch.CashWallet);

                sm.sms(franch.Phone, msg, "1407169591301077821");

            }

            else if (rdOption.SelectedValue == "2")  // for debit
            {
                string desc = "Debit";
                string remarks = txtRemarks.Text;
                franch.CashWallet -= Amount;

                var history = new FranchiseeWallet
                {
                    username = uname,
                    credit = 0,
                    debit = Amount,
                    date = DateTime.UtcNow.AddHours(5.5).Date,
                    confirmdate = DateTime.UtcNow.AddHours(5.5),
                    desc = desc,
                    remarks = "Manual Debit-" + txtRemarks.Text,
                    status = "Send",
                };

                db.FranchiseeWallets.InsertOnSubmit(history);
                db.SubmitChanges();

                string receivermsg = string.Format("Dear {0}, Your Cash Wallet is debited with Rs {1}. Current Balance is Rs {2}. DLS Biotech", franch.VendorName, Amount, franch.CashWallet);

                sm.sms(franch.Phone, receivermsg, "1407169591304792948");

            }


            Response.Redirect("manualDebit.aspx?v=sub&s=" + status);
        }
        catch (Exception ex)
        {
            lbErrormsg.InnerText = ex.Message;
        }
    }

}