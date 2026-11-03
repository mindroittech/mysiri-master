using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Member_pin_purchase : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            //if (Request.QueryString["val"] == "sub")
            //{
            //    Label1.ForeColor = Color.Green;
            //    Label1.Text = "Pin Request Sent Successfully";
            //    Label1.Visible = true;
            //}

            if (!IsPostBack)
            {
                var plan = from c in db.tblPlans where c.status==true
                           select c;
                drpPlan.DataSource = plan;
                drpPlan.DataTextField = "PlanName";
                drpPlan.DataValueField = "PlanAmount";
                drpPlan.DataBind();

                drpPlan.Items.Insert(0, new ListItem("Select Plan", "0"));

                var get = db.tblMembers.FirstOrDefault(x => x.username == uname);

                lbbal.InnerHtml = get.credit.Value.ToString();

                if (get.credit.Value < 400)
                {
                    Button1.Enabled = false;
                }
            }
        }
        catch
        {
            Response.Redirect("../login.aspx");
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();
            var get = db.tblMembers.FirstOrDefault(x => x.username == uname);

            decimal credit = get.credit.Value;
            int n = Convert.ToInt32(txtPins.Text);
            decimal amountRequired = n * 100;
            if (n < 4)
            {
                lbErrormsg.Text = "Minimum 4 pin needs to be purchased";
                lbErrormsg.Visible = true;
                return;
            }

            if (credit < amountRequired)
            {
                lbErrormsg.Text = "You don't have sufficient balance in Wallet.";
                lbErrormsg.Visible = true;
                return;
            }

            string pin = GeneratePin();
            for (int i = 0; i < n; i++)
            {

                var f = from c in db.PinManagaers
                        where c.pinnumbers == pin
                        select c;
                if (f.Count() == 1)
                {
                    pin = (Convert.ToDecimal(pin) + 1).ToString();
                }

                var ins = new PinManagaer();
                ins.date = DateTime.UtcNow.Date;
                ins.issued = get.username;
                ins.status = "Not used";
                ins.pinnumbers = pin;
                ins.package = drpPlan.SelectedItem.Text;
                ins.transferby = "Wallet Purchase"; // System.Configuration.ConfigurationManager.AppSettings["companyname"];

                db.PinManagaers.InsertOnSubmit(ins);
                db.SubmitChanges();

                pin = (Convert.ToDecimal(pin) + 1).ToString();
            }

            bvDebit(amountRequired, get.username, "Debit", DateTime.UtcNow.Date, "Pin Purchase");

            Response.Redirect("pinbank.aspx?val=sub");
        }
        catch (Exception ex)
        {
            lbErrormsg.Visible = true;
            lbErrormsg.Text = "Invalid information";
        }

    }

    protected void bvDebit(Decimal amt, string uname, string desc, DateTime date, string remarks)
    {
        var getCredit = db.tblMembers.SingleOrDefault(x => x.username == uname);
        var insert = new Payout();
        insert.username = uname;
        insert.date = date.Date;
        insert.desc = desc;
        insert.tax = 0;
        insert.credit = 0;
        insert.debit = amt;
        insert.status = "Send";
        insert.confirmdate = date;
        insert.incomefrom = uname;
        insert.reward = getCredit.credit;
        insert.remarks = remarks;
        db.Payouts.InsertOnSubmit(insert);

        getCredit.credit -= amt;

        db.SubmitChanges();
    }

    public string GeneratePin()
    {
        string strPwdchar = "0123456789";
        string strPwd = DateTime.Today.ToString("Mddyyyy");
        Random rnd = new Random();
        for (int i = 0; i <= 8; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }

        return strPwd;
    }

}