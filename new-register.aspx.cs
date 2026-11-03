using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["pro"] == "sub")
        {
            Label3.Text = "Congratulations! You have successfully registed to DLS Biotech. Please login to activate your account.<br/> UserID: " + Request.QueryString["u"] + " Password: " + Request.QueryString["p"];
            Label3.Visible = true;
        }
        if (Request.QueryString["user"] != null)
        {
            TextBox20.Text = Request.QueryString["user"];
            TextBox20_TextChanged(null, null);
        }
        if (Request.QueryString["leg"] != null)
        {
            RadioButtonList1.SelectedValue = Request.QueryString["leg"];
        }
    }
    public string GeneratePassword()
    {
        string strPwdchar = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 5; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }
        return strPwd;
    }

    private string GenerateUserID()
    {
        string strPwdchar = "1234567890";
        string username = "DLS";
        Random rnd = new Random();
        for (int i = 0; i <= 5; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            username += strPwdchar.Substring(iRandom, 1);
        }

        var get = from c in db.tblMembers
                  where c.username == username
                  select c;
        while (get.Any())
        {
            GenerateUserID();
        }

        return username;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        SubmitDetails();
    }

    protected void SubmitDetails()
    {
        try
        {
            Label3.Visible = false;
            var mobile = (from c in db.tblMembers
                          where c.mobile == TextBox4.Text.Trim()
                          select c);
            if (mobile.Any())
            {
                Label2.Text = "Mobile number already exists. Please use diff number";
                Label2.Visible = true;
                return;
            }
            var pan = (from c in db.tblMembers
                       where c.pan == txtPan.Text.Trim()
                       select c);
            if (pan.Any())
            {
                Label2.Text = "PAN number already exists. Please use diff PAN";
                Label2.Visible = true;
                return;
            }

            var sponsor = (from c in db.tblMembers
                           where c.username == TextBox20.Text
                           select c).SingleOrDefault();
            if (sponsor == null)
            {
                Label2.Text = "Sponsor ID not found";
                Label2.Visible = true;
                return;
            }
            if (sponsor.isActive == 0)
            {
                Label2.Text = "Sponsor ID is inactive";
                Label2.Visible = true;
                return;
            }
            string uname = GenerateUserID();

            try
            {
                tblMember insert = new tblMember();

                var newSponsor = (from c in db.tblMembers
                                  where c.refid == sponsor.id && c.leg == RadioButtonList1.SelectedValue
                                  select c).FirstOrDefault();
                int referid = sponsor.id;
                int lev = sponsor.level.Value;

                while (newSponsor != null)
                {
                    referid = newSponsor.id;
                    lev = newSponsor.level.Value;

                    newSponsor = (from c in db.tblMembers
                                  where c.refid == referid && c.leg == RadioButtonList1.SelectedValue
                                  select c).FirstOrDefault();
                }

                insert.username = uname;
                insert.password = GeneratePassword();
                insert.leg = RadioButtonList1.SelectedValue;
                insert.joindate = DateTime.UtcNow.AddHours(5.5);
                insert.level = lev + 1;
                insert.refid = referid;
                insert.name = TextBox2.Text;

                insert.state = drpState.SelectedItem.Text;
                insert.district = txtDistrict.Text;
                insert.mobile = TextBox4.Text;
                insert.status = "Free";

                insert.sid = sponsor.username;
                insert.sname = sponsor.name;
                insert.pan = txtPan.Text.Trim();
                insert.credit = 0;
                insert.debit = 0;
                insert.lcarry = 0;
                insert.rcarry = 0;
                insert.tlcount = 0;
                insert.trcount = 0;
                insert.goldlcarry = 0;
                insert.goldrcarry = 0;
                insert.diamondlcarry = 0;
                insert.diamondrcarry = 0;

                insert.silverLeft = 0;
                insert.silverRight = 0;
                insert.selfBv = 0;
                insert.leftBv = insert.rightBv = 0;
                insert.leftTotalBv = insert.rightTotalBv = 0;
                insert.maintenanceBv = 0;
                insert.RepurchaseWallet = 0;
                insert.flushOut = 0;

                insert.isActive = 1;
                insert.TourPackageEligible = false;

                // new added
                insert.total_left = 0;
                insert.total_right = 0;
                insert.alcarry = 0;
                insert.arcarry = 0;
                insert.arcount = 0;
                insert.alcount = 0;
                insert.algoldcarry = 0;
                insert.argoldcarry = 0;
                insert.achievementwallet = 0;
                insert.cumbv_left = 0;
                insert.cumbv_right = 0;
                insert.cumbv_self = 0;
                insert.run_date = DateTime.UtcNow.AddHours(5.5);

                // new added 04-09-2025

                insert.salaryWallet = 0;
                insert.MFA = 0;           // new added 19-12-2025

                db.tblMembers.InsertOnSubmit(insert);
                db.SubmitChanges();


                string msg = "Dear " + insert.name + ", Congratulations! Welcome to " + ConfigurationManager.AppSettings["companyname"] + ". Your Login ID:" + insert.username + " Password:" + insert.password + " Login at: " + ConfigurationManager.AppSettings["companywebsite"] + " All the Best!";
                sm.sms(insert.mobile, msg, "1407169591229002928");

                Response.Redirect("register.aspx?pro=sub&u=" + insert.username + "&p=" + insert.password);
            }
            catch (Exception ex)
            {
                Label2.Text = ex.Message;
                Label2.Visible = true;
                return;
            }
        }
        catch
        {
            Label2.Text = "No Sponsor ID found";
            Label2.Visible = true;
        }
    }

    protected void TextBox20_TextChanged(object sender, EventArgs e)
    {
        var m = db.tblMembers.Where(x => x.username == TextBox20.Text).SingleOrDefault();
        if (m != null)
            lblName.Text = m.name;
        else
            lblName.Text = "";
    }
}