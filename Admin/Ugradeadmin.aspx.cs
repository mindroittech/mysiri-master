using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["v"] == "sub")
            {
                Label1.ForeColor = Color.Green;
                Label1.Text = "ID has been successfully activated, Please collect your product from the person who sent PINs to you";
                Label1.Visible = true;

            }

            var plan = from c in db.tblPlans
                       where c.status == true
                       select c;
            drpPlan.DataSource = plan;
            drpPlan.DataTextField = "PlanName";
            drpPlan.DataValueField = "PlanId";
            drpPlan.DataBind();

            drpPlan.Items.Insert(0, new ListItem("Select Plan", "0"));

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        string uname = txtMemberId.Text;
        var insert = db.tblMembers.FirstOrDefault(x => x.username == uname);

        string plan = drpPlan.SelectedItem.Text;
        var findPlan = db.tblPlans.Where(x => x.PlanName == plan).SingleOrDefault();

        try
        {

            var sid = db.tblMembers.SingleOrDefault(x => x.username == insert.sid);
            if (sid != null && sid.renewal != null)
            {
               
                DateTime joinDate = (DateTime)sid.renewal;
                DateTime currentDate = DateTime.UtcNow.AddHours(5.5);
                TimeSpan timeDifference = currentDate - joinDate;
              
                bool within48Hours = timeDifference.TotalHours <= 48;

                if (within48Hours)
                {
                    decimal performanceBonus = findPlan.bonus.Value;
                    sid.credit += performanceBonus;
                    payout(performanceBonus, sid.username, insert.username, "Performance Bonus", "Referral Income");

                }
            }

            int refid = (int)insert.refid;
            string leg = insert.leg;
            int i = 0;
            Decimal bvAmount = (decimal)findPlan.BVAmount;
            Decimal pvAmount = (decimal)findPlan.PvAmount;
            Decimal points = (decimal)findPlan.points;
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
                        bv.alcarry = bv.alcarry + pvAmount;
                        bv.alcount = bv.alcount + pvAmount;
                    }
                    if (leg == "Right")
                    {
                        bv.rcarry = bv.rcarry + bvAmount;
                        bv.trcount = bv.trcount + bvAmount;
                        bv.silverRight += 1;
                        bv.diamondrcarry += findPlan.PlanAmount;

                        // added on 20 aug 2024
                        bv.arcarry = bv.arcarry + pvAmount;
                        bv.arcount = bv.arcount + pvAmount;
                    }
                    if (bv.TourPackageEligible == false)   // 2:1 or 1:2 update  for Enable flag
                    {
                        if (bv.silverLeft >= 1 && bv.silverRight >= 2)
                        {
                            bv.TourPackageEligible = true;
                        }
                        else if (bv.silverLeft >= 2 && bv.silverRight >= 1)
                        {
                            bv.TourPackageEligible = true;
                        }
                    }
                    db.SubmitChanges();
                    AddBv(bv.username, 1, points, insert.renewal.Value.Date, leg, insert.username, "Binary");
                }

                refid = (int)bv.refid;
                leg = bv.leg;
            }
            Response.Redirect("Ugradeadmin.aspx?v=sub",false); 
        }
        catch(Exception ex)
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
