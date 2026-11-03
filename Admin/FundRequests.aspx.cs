using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FundRequests : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            string admin = Session["admin"].ToString();

            if (Request.QueryString["pl"] != null)
            {
                Label3.Text = "Fund Request " + Request.QueryString["pl"].ToString() + " successfully";
                Label3.Visible = true;
            }

            if (!IsPostBack)
                bind();
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }

    protected void bind()
    {
        var funds = from c in db.FranchiseeWalletRequests
                    join d in db.tblVendors
                     on c.username equals d.Username
                    where c.status == "Pending"
                    orderby c.pid descending
                    select new
                    {
                        d.Username,
                        d.VendorName,
                        c.pid,
                        c.transactionno,
                        c.amountRequired,
                        c.status,
                        c.counterfoil,
                        c.createdDate,
                        c.remarks,
                        c.paymode
                    };
        GridView1.DataSource = funds;
        GridView1.DataBind();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "approve")
        {
            hdnReqId.Value = e.CommandArgument.ToString();
            int pid = Convert.ToInt32(hdnReqId.Value);
            var get = (from c in db.FranchiseeWalletRequests
                       where c.pid == pid
                       select c).SingleOrDefault();
            lblutr.Text = get.transactionno.ToString();
            LinkButton3.NavigateUrl = get.counterfoil;
            LinkButton3.Target = "_blank";
            ImageButton2_ModalPopupExtender.Show();

        }
        if (e.CommandName == "reject")
        {
            int pid = Convert.ToInt32(e.CommandArgument);
            var del = db.FranchiseeWalletRequests.Where(x => x.pid == pid).SingleOrDefault();
            del.status = "Rejected";
            del.ActionOn = DateTime.UtcNow.AddHours(5.5);
            db.SubmitChanges();
            Response.Redirect("FundRequests.aspx?pl=Rejected");
        }
    }

    protected void GenerateOTP_Click(object sender, EventArgs e)
    {
        try
        {
            sendsms sm = new sendsms();
            decimal amt = 0;
            int pid = Convert.ToInt32(hdnReqId.Value);
            var get = (from c in db.FranchiseeWalletRequests
                       where c.pid == pid
                       select c).SingleOrDefault();
            var mem = db.tblVendors.SingleOrDefault(x => x.Username == get.username);
            amt = get.amountRequired.Value;
            string otp = Helper.GenerateOTP();
            Session["FundOTP"] = otp;
            lblOtpMessage.Text = "Otp has been sent to Admin" + " "+ otp;
            string msg = otp + " is the OTP to approve the Fund Request for the ID : " + mem.Username + " Name : " + mem.VendorName +
                " for the amount Rs. " + amt + ". DLS Biotech";
            sm.sms("9380393866,9448415584", msg, "1407169591333295787");
            ImageButton2_ModalPopupExtender.Show();

        }
        catch
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string admin = Session["admin"].ToString();
        int pid = Convert.ToInt32(hdnReqId.Value);
        var get = (from c in db.FranchiseeWalletRequests
                   where c.pid == pid
                   select c).SingleOrDefault();

        if (Session["FundOTP"] == null || Session["FundOTP"].ToString() != txtOTP.Text)
        {
            lbErrormsg.Text = "Invalid OTP";
            lbErrormsg.Visible = true;
            return;
        }

        if (get.status != "Approved")
        {
            get.status = "Approved";
            get.amount = get.amountRequired;
            get.ActionOn = DateTime.UtcNow.AddHours(5.5);

            var franch = db.tblVendors.SingleOrDefault(x => x.Username == get.username);
            if (franch != null)
            {
                franch.CashWallet += get.amount.Value;
                Payout(franch.Username, get.amount.Value, "Credit", "From Purchase from Company", admin);
                string msg = string.Format("Dear {0}, Your Cash Wallet is credited with Rs {1}. Current Balance is Rs {2}. DLS Biotech", franch.VendorName, get.amount, franch.CashWallet);
                sm.sms(franch.Phone, msg, "1407169591301077821");
            }

            //// ✅ Find sum of DP × Quantity for this franchisee’s stock
            //decimal totalDP = (from d in db.tblProducts
            //                   join c in db.tblVendorStocks
            //                       on d.ProductId equals c.ProductId
            //                   where c.VendorId == franch.Id
            //                         && d.IsActive == true
            //                         && c.Stock > 0
            //                   select (decimal?)(d.DP * c.Stock)).Sum() ?? 0;


            //if ((totalDP + franch.CashWallet) >= franch.Amount)
            //{
            //    DateTime today = DateTime.UtcNow.AddHours(5.5);

            //    if (today.Day >= 6 && today.Day <= 10)
            //    {
            //        franch.isFirst = true;
            //    }
            //    else if (today.Day >= 21 && today.Day <= 25)
            //    {
            //        franch.isSecond = true;
            //    }
            //}

            db.SubmitChanges();
        }

        Session["FundOTP"] = null;
        Response.Redirect("FundRequests.aspx?pl=Approved");
    }

    private void Payout(string uname, Decimal amt, string desc, string remarks, string incomeFrom)
    {
        DateTime date = DateTime.UtcNow.AddHours(5.5);
        var history = new FranchiseeWallet
        {
            username = uname,
            credit = amt,
            debit = 0,
            date = date.Date,
            confirmdate = date,
            desc = desc,
            remarks = remarks,
            status = "Send",
            IncomeFrom = incomeFrom
        };

        db.FranchiseeWallets.InsertOnSubmit(history);
        db.SubmitChanges();
    }
}