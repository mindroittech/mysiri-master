using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Admin_FranchiseeReferrals : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["admin"].ToString();

            if (!IsPostBack)
            {

                var roles = from c in db.tblRoles
                            where c.IsActive == true
                            select c;
                drpRole.DataSource = roles;
                drpRole.DataTextField = "RoleName";
                drpRole.DataValueField = "RoleId";
                drpRole.DataBind();
                drpRole.Items.Insert(0, new ListItem("All", ""));
                bind();

                if (Request.QueryString["v"] == "sub")
                {
                    Label1.Visible = true;
                }
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    private void bind(string user = "", string vendor = "")
    {
        var get = from c in db.tblReferrals
                  join d in db.tblRoles
                  on c.RoleId equals d.RoleId into grp
                  from f in grp.DefaultIfEmpty()
                  where c.status == "Pending"
                  orderby c.CreatedDate descending
                  select new
                  {
                      c.CompanyName,
                      c.CompanyCode,
                      c.VendorName,
                      c.Username,
                      c.Pincode,
                      c.Country,
                      c.State,
                      c.Email,
                      c.Phone,
                      c.City,
                      c.CreatedDate,
                      f.RoleName,
                      f.RoleId,
                      c.Id,
                      c.status,
                      c.Amount,
                      c.slip,
                      c.UTR,
                      c.bank,
                      c.depositeDate
                  };

        if (user != "")
        {
            get = get.Where(x => x.Username == user);
        }
        if (vendor != "")
        {
            get = get.Where(x => x.RoleId.ToString() == vendor);
        }

        GridView1.DataSource = get;
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "delete")
        {
            Label1.Visible = false;
            int id = Convert.ToInt32(e.CommandArgument);

            var get = db.tblReferrals.Where(x => x.Id == id).SingleOrDefault();

            get.status = "Rejected";
            db.SubmitChanges();

            Response.Redirect("FranchiseeReferrals.aspx");
        }
        else if (e.CommandName == "edit")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            var referrer = db.tblReferrals.Where(x => x.Id == id).SingleOrDefault();
            decimal percent = 2;
            var role = db.tblRoles.SingleOrDefault(x => x.RoleId == referrer.RoleId);
            if (role.RoleName == "C and F")
            {
                percent = 1.5m;
            }
            referrer.status = "Approved";
            decimal income = referrer.Amount.Value * percent / 100;
            //// removed on 12/14/2021
            //var franch = db.tblVendors.SingleOrDefault(x => x.Username == referrer.Username);
            //if (franch != null)
            //{
            //    franch.KitWallet += income;
            //    payout(income, franch.Username, franch.Username, "Referral Bonus");
            //}
            //else
            //{
            //    var member = db.tblMembers.SingleOrDefault(x => x.username == referrer.Username);
            //    if (member != null)
            //    {
            //        member.credit += income;
            //        payout(income, member.username, member.username, "Referral Bonus");
            //    }
            //}
            db.SubmitChanges();
            AddVendor(referrer);

            Response.Redirect("FranchiseeReferrals.aspx?v=sub");
        }
    }

    private string GeneratePassword()
    {
        string strPwdchar = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 5; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }
        return strPwd;
    }

    protected void payout(Decimal amt, string uname, string incomefrom, string desc)
    {
        var insert = new Payout();
        insert.username = uname;
        insert.date = DateTime.UtcNow.Date;
        insert.desc = desc;
        insert.tax = 0;
        insert.credit = amt;
        insert.debit = 0;
        insert.status = "Send";
        insert.confirmdate = DateTime.UtcNow.AddHours(5.5);
        insert.incomefrom = incomefrom;
        insert.remarks = "Franchisee Referral Commission";
        db.Payouts.InsertOnSubmit(insert);
        db.SubmitChanges();
    }

    private void AddVendor(tblReferral refer)
    {
        int userid = 1000;
        var find = db.tblVendors.OrderByDescending(x => x.Id).FirstOrDefault();
        if (find != null)
            userid = userid + find.Id;
        string uname = "DLB" + userid;
        var vendor = new tblVendor
        {
            Username = uname,
            Password = GeneratePassword(),
            CreatedDate = DateTime.UtcNow.AddHours(5.5).Date,
            VendorName = refer.VendorName,
            CompanyName = refer.VendorName,
            IsActive = true,
            Phone = refer.Phone,
            RoleId = refer.RoleId,
            City = refer.City,
            Pincode = refer.Pincode,
            Address = refer.Address,
            GSTIN = refer.GSTIN,
            Amount = refer.Amount,
            KitWallet = 0,
            KitWalletDebit = 0,
            RepurchaseWallet = 0,
            CashWallet = refer.Amount.Value,
            RepurchaseWalletDebit = 0,
            State = refer.State,
            SponsoredBy = refer.Username,
            isStatusActive=true,
            isFirst=false,
            isSecond=false,
            salaryWallet = 0,
            isEligiable = false
        };

        if (refer.RoleId > 2)
            vendor.fcount = 24;

        db.tblVendors.InsertOnSubmit(vendor);
        db.SubmitChanges();
        Payout(uname, refer.Amount.Value, "Credit", "From Franchisee Amount", uname);
        string msg = "Dear " + vendor.VendorName + ", Congratulations! Welcome to DLS Biotech. Your Franchisee Login ID:" + vendor.Username + " Password:" + vendor.Password + " Login at: https://www.dlsbiotech.com";
        sm.sms(vendor.Phone, msg, "1407172845282824396");

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        bind(txtUser.Text, drpRole.SelectedValue);
    }
}