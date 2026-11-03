using iTextSharp.text.pdf.parser;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_run : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
       if(!IsPostBack)
       {
            //updateBV();

            addFranchiseeReferralIncome();
       }
    }

    private void addFranchiseeReferralIncome()
    {
       
        string[] users = { "DLB3204", "DLB3205" };

        var vendors = db.tblVendors
                        .Where(x => x.IsActive == true && users.Contains(x.Username))
                        .ToList();
      
        foreach (var d in vendors)
        {
            decimal amt = d.Amount.Value;

            var refer = db.tblMembers.SingleOrDefault(x => x.username == d.SponsoredBy);

            if (refer != null)
            {
                decimal commision = amt * 5 / 100;

                refer.credit += commision;
                payout(commision, refer.username, d.Username , d.CreatedDate.Value);
            }


        }
        db.SubmitChanges();
    }

    protected void payout(Decimal amt, string uname, string incomefrom , DateTime dat)
    {

        var insert = new Payout();
        insert.username = uname;
        insert.date = dat.Date;
        insert.confirmdate = dat;
        insert.desc = "Franchisee Referral Income";
        insert.tax = 0;
        insert.netpayout = amt;
        insert.credit = amt;
        insert.debit = 0;
        insert.status = "Send";
        insert.incomefrom = incomefrom;
        insert.remarks = "Franchisee Referral Income";
        db.Payouts.InsertOnSubmit(insert);
        db.SubmitChanges();
    }

    private void updateBV()
    {
        DateTime date = DateTime.UtcNow.AddHours(5.5);

        string[] users = { "DLB3199", "DLB3200", "DLB3201" };

        var vendors = db.tblVendors
                        .Where(x => x.IsActive == true && users.Contains(x.Username))
                        .ToList();

        foreach (var d in vendors)
        {
            decimal amount = d.Amount ?? 0;
            decimal salary = amount * 0.01m;

            if (salary <= 0)
                continue;

            // Credit salary
            d.salaryWallet = (d.salaryWallet ?? 0) + salary;

            // Salary payout entry
            db.Payouts.InsertOnSubmit(new Payout
            {
                username = d.Username,
                credit = salary,
                netpayout = salary,
                desc = "Salary Income",
                remarks = "Franchisee Salary",
                status = "Send",
                date = date.Date,
                confirmdate = date
            });


            if ((d.fcount ?? 0) > 0)
                d.fcount--;

            // Generate withdrawal request only if wallet has balance
            decimal wallet = d.salaryWallet ?? 0;

            if (wallet > 0)
            {
                decimal tax = Math.Round(wallet * 0.02m, 2);
                decimal sc = Math.Round(wallet * 0.08m, 2);

                db.PaymentRequests.InsertOnSubmit(new PaymentRequest
                {
                    username = d.Username,
                    amount = wallet,
                    cashavailable = wallet,
                    tax = tax,
                    sc = sc,
                    netpay = wallet - tax - sc,
                    status = "Pending",
                    comments = "Salary Pay",
                    date = date
                });

                // Reset wallet after creating withdrawal request
                d.salaryWallet = 0;
            }
        }

        db.SubmitChanges();
    }


}