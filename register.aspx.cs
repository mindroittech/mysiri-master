 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Threading.Tasks;
using System.Configuration;
using System.Drawing;

public partial class register : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Request.QueryString["pro"] == "sub")
        {
            Label3.Text = "Congratulations! You have successfully registed to MYSIRI SOURABHA. Please login to activate your account.<br/> UserID: " + Request.QueryString["u"] + " Password: " + Request.QueryString["p"];
            Label3.Visible = true;
        }
        if (Request.QueryString["user"] != null)
        {
            txtplacement.Text = Request.QueryString["user"];
            txtplacement_TextChanged(null, null);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            var placedID = db.tblMembers.Where(x => x.username == txtplacement.Text).SingleOrDefault();


            var newSponsors = db.tblMembers
                        .Where(c => c.refid == placedID.id)
                        .ToList();

            if (newSponsors.Count < 2)
            {
                // Check if there's at least one sponsor
                var firstSponsor = newSponsors.FirstOrDefault();

                if (firstSponsor == null)
                {
                    SubmitDetails(placedID.id, (int)placedID.level);
                }
                else if (firstSponsor.leg != RadioButtonList1.SelectedValue)
                {
                    SubmitDetails(placedID.id, (int)placedID.level);
                }
                else
                {
                    Label2.Text = RadioButtonList1.SelectedValue + " is not available. Please select another position.";
                    Label2.Visible = true;
                    Label2.ForeColor = Color.Red;
                    return;
                }

            }
            else
            {
                Label2.Text = "No vacant Place..Please provide other Placement ID.";
                Label2.ForeColor = Color.Red;
                Label2.Visible = true;
                newreg.Visible = true;
                return;
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void SubmitDetails( int placed ,int plevel)
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
                Label2.ForeColor = Color.Red;
                return;
            }
            var pan = (from c in db.tblMembers
                       where c.pan == txtPan.Text.Trim()
                       select c);
            if (pan.Any())
            {
                Label2.Text = "PAN number already exists. Please use diff PAN";
                Label2.Visible = true;
                Label2.ForeColor = Color.Red;
                return;
            }

            var sponsor = (from c in db.tblMembers
                           where c.username == TextBox20.Text
                           select c).SingleOrDefault();
            if (sponsor == null)
            {
                Label2.Text = "Sponsor ID not found";
                Label2.Visible = true;
                Label2.ForeColor = Color.Red;
                return;
            }
            if (sponsor.isActive == 0)
            {
                Label2.Text = "Sponsor ID is inactive";
                Label2.Visible = true;
                Label2.ForeColor = Color.Red;
                return;
            }
            string uname = GenerateUserID();

            try
            {
                tblMember insert = new tblMember();

                int referid = placed;
                int lev = plevel;

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
                insert.GoldPairMatch = false;
                insert.DiamondPaidMatch = false;
                insert.diamondFlushout = 0;
                insert.goldFlushout = 0;

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

                insert.isActive = 1;
                insert.TourPackageEligible = false;

                // new added 04-09-2025

                insert.salaryWallet = 0;
                insert.MFA = 0;           // new added 19-12-2025


                db.tblMembers.InsertOnSubmit(insert);
                db.SubmitChanges();

                string msg = "Dear " + insert.name + ", Congratulations! Welcome to  DLS Biotech. Your Login ID:" + insert.username + " Password:" + insert.password + " Login at: https://www.dlsbiotech.com All the Best!";
                sm.sms(insert.mobile, msg, "1407172845276587925");    

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
            Label2.ForeColor = Color.Red;
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

    protected void txtplacement_TextChanged(object sender, EventArgs e)
    {
        var m = db.tblMembers.Where(x => x.username == txtplacement.Text).SingleOrDefault();
        if (m != null)
            lblplacement.Text = m.name;
        else
            lblplacement.Text = "";
        if (RadioButtonList1.SelectedIndex >= 0)
        {
            RadioButtonList1_SelectedIndexChanged(RadioButtonList1, EventArgs.Empty);
        }
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var placedID = db.tblMembers.Where(x => x.username == txtplacement.Text).SingleOrDefault();


        var newSponsors = db.tblMembers
                    .Where(c => c.refid == placedID.id)
                    .ToList();

        if (newSponsors.Count < 2)
        {
            // Check if there's at least one sponsor
            var firstSponsor = newSponsors.FirstOrDefault();

            if (firstSponsor == null)
            {
                Label2.Text = "Available Please Proceed to Register";
                Label2.ForeColor = Color.Green;
                Label2.Visible = true;
                newreg.Visible = false;
                return;
            }
            else if (firstSponsor.leg != RadioButtonList1.SelectedValue)
            {
                Label2.Text = "Available Please Proceed to Register";
                Label2.ForeColor = Color.Green;
                Label2.Visible = true;
                newreg.Visible = false;
                return;
            }
            else
            {
                Label2.Text = RadioButtonList1.SelectedValue + " is not available. Please select another position.";
                Label2.Visible = true;
                Label2.ForeColor = Color.Red;
                Label2.Visible = true;

                lblleg.Text= leg1.Text = RadioButtonList1.SelectedValue;
                lblplace.Text = txtplacement.Text;

                // Show popup
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "showConfirm",
                    "openBottomConfirm();",
                    true
                );
                return;
            }

        }
        else
        {
            Label2.Text = "No vacant Place..Please provide other Placement ID.";
            Label2.ForeColor = Color.Red;
            Label2.Visible = true;
            newreg.Visible = true;

            lblleg.Text = leg1.Text = RadioButtonList1.SelectedValue;
            lblplace.Text = txtplacement.Text;

            // Show popup
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "showConfirm",
                "openBottomConfirm();",
                true
            );
            return;
        }
    }
    protected void btnYesBottom_Click(object sender, EventArgs e)
    {
        try
        {
            string sponsor = TextBox20.Text;
            string placement = txtplacement.Text;
            string position = RadioButtonList1.SelectedValue;
         
            Response.Redirect("placement-register.aspx?sid=" + sponsor + "&pid=" + placement + "&pos=" + position + "&v=s", true);

        }
        catch
        {

        }
    }

}