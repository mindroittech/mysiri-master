using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pintransfer : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
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

            if (Request.QueryString["st"] == "sub")
            {
                Label8.Text = "PIN Transferred to " + Request.QueryString["n"] + " (" + Request.QueryString["u"] + ") Successfully";
                Label8.Visible = true;
            }


            if (!IsPostBack)
            {
                var plan = from c in db.tblPlans
                           select c;
                drpPlan.DataSource = plan;
                drpPlan.DataTextField = "PlanName";
                drpPlan.DataValueField = "PlanAmount";
                drpPlan.DataBind();

                drpPlan.Items.Insert(0, new ListItem("Select Plan", "0"));
            }
        }

        catch
        {
            Response.Redirect("../login.aspx");
        }
    }
    protected void txtUser_Changed(object sender, EventArgs e)
    {
        try
        {
            var get = db.tblMembers.Where(x => x.username == TextBox2.Text).SingleOrDefault();
            if (get != null)
                lblName.Text = get.name;
            else
                lblName.Text = "";
        }
        catch
        {
            lblName.Text = "";
        }

    }

    protected void ddlPinType_Changed(object sender, EventArgs e)
    {
        if (drpPlan.SelectedIndex != 0)
        {
            string myusername = Session["vendor"] != null ? Session["vendor"].ToString() : Session["username"].ToString();
            var getPins = (from c in db.PinManagaers
                           where c.package == drpPlan.SelectedItem.Text && c.issued == myusername && c.status == "Not used"
                           select c).Count();
            txtBalance.Text = getPins.ToString();
        }
        else
            txtBalance.Text = "0";
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        var findUser = db.tblMembers.Where(x => x.username == TextBox2.Text).SingleOrDefault();
        string myusername = Session["vendor"] != null ? Session["vendor"].ToString() : Session["username"].ToString();
        if (findUser == null)
        {

            Label1.Visible = true;
            return;
        }

        int count = Convert.ToInt32(txtPins.Text);

        int leftPins = Convert.ToInt32(txtBalance.Text);

        if (count <= leftPins)
        {
            var pins = (from c in db.PinManagaers
                        where c.package == drpPlan.SelectedItem.Text && c.issued == myusername && c.status == "Not used"
                        select c).Take(count);
            foreach (var p in pins)
            {
                var get = (from c in db.PinManagaers
                           where c.pinnumbers == p.pinnumbers
                           select c).SingleOrDefault();

                get.issued = findUser.username;
                get.transferby = myusername;
                db.SubmitChanges();
            }

            var insert = new PinTransferHistory
            {
                transferDate = DateTime.UtcNow.AddHours(5.5),
                transferedFrom = myusername,
                transferedTo = findUser.username,
                pinnumbers = count.ToString(),
                package = drpPlan.SelectedItem.Text
            };

            db.PinTransferHistories.InsertOnSubmit(insert);
            db.SubmitChanges();

            Response.Redirect("pintransfer.aspx?st=sub&n=" + findUser.name + "&u=" + findUser.username);
        }
        else
        {
            Label1.Text = "Insufficient pins.";
            Label1.Visible = true;
            return;
        }
    }

    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        var find = from c in db.tblMembers
    //                   where c.username == TextBox2.Text
    //                   select c;
    //        if (find.Count() == 1)
    //            Response.Redirect("transfer-pin.aspx?uname=" + TextBox2.Text + "&package=" + drpPlan.SelectedItem.Text + "&price=" + drpPlan.SelectedValue);
    //        else
    //            Label1.Visible = true;
    //    }
    //    catch
    //    { }
    //}
}