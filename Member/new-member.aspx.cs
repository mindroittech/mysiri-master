using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class new_member : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();
            Label2.Visible = false;
            if (!IsPostBack)
            {
                var g = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
                if (Request.QueryString["user"] != null)
                {
                    txtSponsor.Text = Request.QueryString["user"];
                    ddlPosition.Value = Request.QueryString["leg"];
                }

                var plan = from c in db.tblPlans where c.status==true
                           select c;
                drpPlan.DataSource = plan;
                drpPlan.DataTextField = "PlanName";
                drpPlan.DataValueField = "PlanId";
                drpPlan.DataBind();

                drpPlan.Items.Insert(0, new ListItem("Select Plan", ""));

                drpPIN.Items.Insert(0, new ListItem("Select PIN", ""));

            }

        }

        catch
        {
            Response.Redirect("../login.aspx");
        }
    }

    [System.Web.Services.WebMethod]

    public static string txtSponsorChange(string refId)
    {
        try
        {
            DataClassesDataContext db1 = new DataClassesDataContext();
            var d = (from c in db1.tblMembers
                     where c.username == refId
                     select c).SingleOrDefault();

            if (d != null)
            {
                return d.name;
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }


    protected void btnContinueMember_Click(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();
            var search = (from c in db.tblMembers
                          where c.username == txtSponsor.Text.Trim()
                          select c).SingleOrDefault();

            if (search == null)
            {
                Label2.Text = "Sponsor ID not found";
                Label2.Visible = true;
                return;
            }
            if (search.isActive == 0)
            {
                Label2.Text = "Sponsor ID is inactive";
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
            var mobile = (from c in db.tblMembers
                          where c.mobile == TextBox6.Text.Trim()
                          select c);
            if (mobile.Any())
            {
                Label2.Text = "Mobile number already exists. Please use diff number";
                Label2.Visible = true;
                return;
            }
            var checkPin = (from c in db.PinManagaers
                            where c.pinnumbers == drpPIN.SelectedItem.Text && c.status == "Not used"
                            select c).SingleOrDefault();
            if (checkPin == null)
            {
                Label2.Text = "Invalid PIN";
                Label2.Visible = true;
                return;
            }

            Label2.Visible = false;
            Submit();
        }
        catch (Exception ex)
        {
            Label2.Text = "No Sponsor ID found";
            Label2.Visible = true;
            return;
        }

    }

    protected void txtRef_Changed(object sender, EventArgs e)
    {
        try
        {
            var get = (from c in db.tblMembers
                       where c.username == txtSponsor.Text.Trim()
                       select c).SingleOrDefault();

            lblName.Text = get.name;
        }
        catch
        {
            lblName.Text = "";
        }
    }

    protected void TextBox15_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var get = (from c in db.Ifscs
                       where c.ifsc1 == (txtIfsc.Text)
                       select c).SingleOrDefault();

            txtBank.Text = get.bankname;
            txtBranch.Text = get.branchname;
        }
        catch
        { }
    }

    protected void Submit()
    {
        try
        {
            Label2.Visible = false;
            string username = Session["username"].ToString();

            var selectedPlan = Convert.ToInt32(drpPlan.SelectedValue);
            var findPlan = db.tblPlans.SingleOrDefault(x => x.PlanId == selectedPlan);

            string uname = GenerateUserID();

            var addedBy = (from c in db.tblMembers
                           where c.username == username
                           select c).SingleOrDefault();
            var insert = new tblMember();

            var sponsor = (from c in db.tblMembers
                           where c.username == txtSponsor.Text.Trim()
                           select c).SingleOrDefault();
            int referid = sponsor.id;
            int lev = sponsor.level.Value;

            var sponosr = (from c in db.tblMembers
                           where c.refid == sponsor.id && c.leg == ddlPosition.Value
                           select c).FirstOrDefault();

            while (sponosr != null)
            {
                referid = sponosr.id;
                lev = sponosr.level.Value;

                sponosr = (from c in db.tblMembers
                           where c.refid == referid && c.leg == ddlPosition.Value
                           select c).FirstOrDefault();
            }

            insert.sid = addedBy.username;
            insert.sname = addedBy.name;
            insert.username = uname;
            insert.name = TextBox3.Text;
            insert.email = TextBox5.Text;
            insert.refid = referid;
            insert.level = lev + 1;
            insert.leg = ddlPosition.Value;
            insert.isActive = 1;
            insert.gender = RadioButtonList2.SelectedValue;
            insert.mobile = TextBox6.Text;
            insert.address = TextBox7.Text;
            insert.joindate= insert.renewal = DateTime.UtcNow.AddHours(5.5);
            insert.pincode = TextBox9.Text;

            // insert.city = TextBox8.Text;
            insert.bankname = txtBank.Text;
            insert.branch = txtBranch.Text;
            insert.account = txtAccountNumber.Text;
            insert.ifsc = txtIfsc.Text;
            insert.pan = txtPan.Text.Trim();
            // insert.aadhar = txtAdhar.Text.Trim();
            insert.accHolderName = txtAccountHolderName.Value;
            insert.accType = ddlAccounttype.Value;
            insert.plan = drpPlan.SelectedItem.Text;
            insert.amount = findPlan.PlanAmount;
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
            insert.status = "Paid";
            insert.flushOut = 0;
            insert.state = drpState.SelectedItem.Text;
            insert.district = txtDistrict.Text;
            insert.TourPackageEligible = false;
            insert.pinnumber = drpPIN.SelectedItem.Text;

            // new added
            insert.alcarry = 0;
            insert.arcarry = 0;
            insert.arcount = 0;
            insert.alcount = 0;
            insert.total_left = 0;
            insert.total_right = 0;
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

            insert.password = GeneratePassword();

            db.tblMembers.InsertOnSubmit(insert);

            var findPIN = db.PinManagaers.Where(x => x.id == Convert.ToInt32(drpPIN.SelectedValue)).SingleOrDefault();
            if (findPIN.status == "Used")
            {
                Label2.Text = "Invalid PIN";
                Label2.Visible = true;
                return;
            }
            findPIN.status = "Used";
            findPIN.usedby = insert.username;
            findPIN.useDate = DateTime.UtcNow.AddHours(5.5);
            db.SubmitChanges();

            if (findPIN.IsFreePin == false)
            {
                // ProductRequest(insert.username, plan);
                var sid = db.tblMembers.SingleOrDefault(x => x.username == insert.sid);

                if (sid != null && sid.status == "Paid" && findPlan != null)
                {
                    // 1. Sponsor Income: 5% of package amount
                    decimal performanceBonus = (findPlan.PlanAmount ?? 0m) * 5m / 100m;

                    if (performanceBonus > 0)
                    {
                        sid.credit += performanceBonus;

                        payout(
                            performanceBonus,
                            sid.username,
                            insert.username,
                            "Sponsor Income",
                            "Referral Income"
                        );
                    }

                    // 2. Booster Income: 2 direct sponsors within 24 hours
                    if (sid.renewal != null)
                    {
                        DateTime activationDate = sid.renewal.Value;
                        DateTime currentDate = DateTime.UtcNow.AddHours(5.5);

                        TimeSpan timeDifference = currentDate - activationDate;

                        if (timeDifference.TotalHours >= 0 &&
                            timeDifference.TotalHours <= 24)
                        {

                            int boosterAlreadyPaid = db.Payouts.Count(x =>
                                                      x.username == sid.username &&
                                                      x.desc == "Booster Income");

                            if (boosterAlreadyPaid < 2)
                            {
                                // Additional 5% + 5% Booster Income
                                decimal booster = (findPlan.PlanAmount ?? 0m) * 5m / 100m;

                                if (booster > 0)
                                {
                                    sid.credit += booster;

                                    payout(
                                        booster,
                                        sid.username,
                                        insert.username,
                                        "Booster Income",
                                        "Referral Income"
                                    );
                                }
                            }
                        }
                    }
                }


                int refid = (int)insert.refid;
                string leg = insert.leg;
                int i = 0;
                Decimal bvAmount = (decimal)findPlan.BVAmount;
                // Decimal pvAmount = (decimal)findPlan.PvAmount;
                Decimal points = 0;
                while (refid != 0)
                {
                    i++;
                    var bv = (from c in db.tblMembers
                              where c.id == refid
                              select c).SingleOrDefault();

                    if (bv.status == "Paid")
                    {
                        if (leg == "Left")
                        {
                            bv.lcarry = bv.lcarry + bvAmount;
                            bv.tlcount = bv.tlcount + bvAmount;
                            bv.silverLeft += 1;
                            bv.diamondlcarry += findPlan.PlanAmount;

                            // added on 20 aug 2024
                            //bv.alcarry = bv.alcarry + pvAmount;
                            //bv.alcount = bv.alcount + pvAmount;
                        }
                        if (leg == "Right")
                        {
                            bv.rcarry = bv.rcarry + bvAmount;
                            bv.trcount = bv.trcount + bvAmount;
                            bv.silverRight += 1;
                            bv.diamondrcarry += findPlan.PlanAmount;

                            // added on 20 aug 2024
                            //bv.arcarry = bv.arcarry + pvAmount;
                            //bv.arcount = bv.arcount + pvAmount;
                        }

                        db.SubmitChanges();
                        AddBv(bv.username, 1, points, insert.renewal.Value.Date, leg, insert.username, "Binary");
                    }

                    refid = (int)bv.refid;
                    leg = bv.leg;
                }
            }


            string msg = "Dear " + insert.name + ", Congratulations! Welcome to  DLS Biotech. Your Login ID:" + insert.username + " Password:" + insert.password + " Login at: https://www.dlsbiotech.com All the Best!";
            sm.sms(insert.mobile, msg, "1407172845276587925");

            Response.Redirect("confirm.aspx?pro=sub&n=" + insert.name + "&u=" + insert.username);
        }
        catch (Exception ex)
        {
            Label2.Text = "Something went wrong, please contact admin";
            Label2.Visible = true;
            throw ex;
        }
    }

    private void AddBv(string uname, decimal pv, decimal points, DateTime date, string desc, string bvfrom, string type)
    {
        var history = new tblBV
        {
            username = uname,
            Bv = 0,
            leftBv = 0,
            rightBv = 0,
            bvFrom = bvfrom,
            date = date.Date,
            desc = desc,
            status = "Pending",
            lcarry = 0,
            rcarry = 0,
            bvType = type
        };
        if (desc == "Left")
        {
            history.lcarry = pv;
            history.leftPoints = points;
        }
        else if (desc == "Right")
        {
            history.rcarry = pv;
            history.rightPoints = points;
        }

        db.tblBVs.InsertOnSubmit(history);
        db.SubmitChanges();
    }


    private void ProductRequest(string uname, string plan)
    {
        RequestProduct up = new RequestProduct();

        up.username = uname;
        up.status = "Pending";
        up.date = DateTime.UtcNow.AddHours(5.5);
        up.name = plan;

        db.RequestProducts.InsertOnSubmit(up);
        db.SubmitChanges();

    }

    protected void payout(Decimal amt, string uname, string incomefrom, string desc, string remarks)
    {

        var insert = new Payout();
        insert.username = uname;
        insert.date = DateTime.UtcNow.Date;
        insert.confirmdate = DateTime.UtcNow.AddHours(5.5);
        insert.desc = desc;
        insert.tax = 0;
        insert.netpayout = amt;
        insert.credit = amt;
        insert.debit = 0;
        insert.status = "Send";
        insert.incomefrom = incomefrom;
        insert.remarks = remarks;
        db.Payouts.InsertOnSubmit(insert);
        db.SubmitChanges();
    }

    private string GenerateUserID()
    {
        string strPwdchar = "1234567890";
        string username = "MS";
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

    private string GeneratePassword()
    {
        string strPwdchar = "123456789abcdefghijklmnopqrstuvwxyz";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 5; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }
        return strPwd;
    }
    protected void drpPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string uname = Session["username"].ToString();

        var pins = from c in db.PinManagaers
                   where c.issued == uname && c.status == "Not used" && c.package == drpPlan.SelectedItem.Text
                   select c;
        drpPIN.DataSource = pins;
        drpPIN.DataTextField = "pinnumbers";
        drpPIN.DataValueField = "id";
        drpPIN.DataBind();
        drpPIN.Items.Insert(0, new ListItem("Select PIN", ""));
    }
}