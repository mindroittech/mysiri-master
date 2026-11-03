using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Threading.Tasks;

public partial class Member_activation : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["role"] != null)
            this.MasterPageFile = "../Admin/admin.master";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["vendor"] != null ? Session["vendor"].ToString() : Session["username"].ToString();

            if (!IsPostBack)
            {
                if (Request.QueryString["v"] == "sub")
                {
                    Label1.ForeColor = Color.Green;
                    Label1.Text = "ID has been successfully activated, Please collect your product from the person who sent PINs to you";
                    Label1.Visible = true;

                }

                var plan = from c in db.tblPlans where c.status==true
                           select c;
                drpPlan.DataSource = plan;
                drpPlan.DataTextField = "PlanName";
                drpPlan.DataValueField = "PlanId";
                drpPlan.DataBind();

                drpPlan.Items.Insert(0, new ListItem("Select Plan", "0"));


                var records = from c in db.tblActivations
                              join d in db.tblMembers
                              on c.username equals d.username
                              where c.activatedBy == uname
                              orderby c.activatedDate descending
                              select new { c.username, d.name, d.plan, c.status, c.activatedBy, c.activatedDate };
                GridView1.DataSource = records;
                GridView1.DataBind();
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
            string myuser = Session["vendor"] != null ? Session["vendor"].ToString() : Session["username"].ToString();
            string uname = txtMemberId.Text;
            var insert = db.tblMembers.FirstOrDefault(x => x.username == uname);
            Label1.ForeColor = Color.Red;
            Label1.Visible = false;
            if (insert == null)
            {
                Label1.Text = "User ID not valid";
                Label1.Visible = true;
                return;
            }

            string plan = drpPlan.SelectedItem.Text;
            var findPlan = db.tblPlans.Where(x => x.PlanName == plan).SingleOrDefault();
            if (insert.status == "Paid")
            {
                Label1.Text = "ID already activated";
                Label1.Visible = true;
                return;
            }
            var findPIN = db.PinManagaers.Where(x => x.id == Convert.ToInt32(drpPIN.SelectedValue)).SingleOrDefault();
            if (findPIN.status == "Used")
            {
                Label1.Text = "Invalid PIN";
                Label1.Visible = true;
                return;
            }
            findPIN.status = "Used";
            findPIN.usedby = uname;
            findPIN.useDate = DateTime.UtcNow.AddHours(5.5);
            insert.status = "Paid";
            insert.plan = plan;
            insert.amount = findPlan.PlanAmount;
            insert.pinnumber = findPIN.pinnumbers;
            insert.renewal = DateTime.UtcNow.AddHours(5.5);

            var topup = new tblActivation
            {
                username = insert.username,
                activatedDate = insert.renewal,
                amount = insert.amount,
                activatedBy = myuser,
                isactive = 1,
                status = insert.pinnumber,
                newPlan = plan
            };
            db.tblActivations.InsertOnSubmit(topup);
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

            Response.Redirect("activation.aspx?v=sub",false);
        }
        catch (Exception ex)
        {
            throw ex;
        }
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

    protected void drpPlan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string myuser = Session["vendor"] != null ? Session["vendor"].ToString() : Session["username"].ToString();
        var pins = from c in db.PinManagaers
                   where c.issued == myuser && c.status == "Not used" && c.package == drpPlan.SelectedItem.Text
                   select c;
        drpPIN.DataSource = pins;
        drpPIN.DataTextField = "pinnumbers";
        drpPIN.DataValueField = "id";
        drpPIN.DataBind();
        drpPIN.Items.Insert(0, new ListItem("Select PIN", "0"));
    }

    protected void txtusername_Changed(object sender, EventArgs e)
    {
        try
        {
            var get = (from c in db.tblMembers
                       where c.username == txtMemberId.Text.Trim()
                       select c).SingleOrDefault();
            if (get != null)
            {
                txtName.Text = get.name;
            }
        }
        catch
        {
            txtName.Text = "";
        }
    }

}