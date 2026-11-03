using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Admin_BvAdjust : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();


            if (!IsPostBack)
            {
                if (Request.QueryString["val"] == "sub")
                {
                    Label2.ForeColor = Color.Green;
                    Label2.Text = "Bv has been adjusted Successfully";
                    Label2.Visible = true;
                }


                //drpPlan.Items.Insert(0, new ListItem("Select Plan", "0"));
            }

        }

        catch
        {

            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void txtUser_Changed(object sender, EventArgs e)
    {
        findName(TextBox1.Text);
    }

    private bool findName(string uname)
    {
        try
        {
            var get = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
            if (get != null)
            {
                txtName.Text = get.name;
                var findBv = from c in db.tblBVs
                             where c.status == "Pending" && c.bvType == "Repurchase"
                             && c.username == uname
                             select new
                             {
                                 username = c.username,
                                 leftBv = c.leftBv,
                                 rightBv = c.rightBv,
                                 selfBv = c.Bv
                             };
                if (findBv.Any())
                {
                    lblLeft.Text = findBv.Sum(x => x.leftBv).ToString();
                    lblRight.Text = findBv.Sum(x => x.rightBv).ToString();
                }
                return true;
            }
            else
            {
                Label1.Visible = true;
                return false;
            }
        }
        catch
        {
            txtName.Text = "";
            return false;
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Visible = false;
        if (Panel1.Visible == false)
        {
            bool isValid = findName(TextBox1.Text);
            if (!isValid)
                return;
            Panel1.Visible = true;
        }
        else
        {
            string leg = RadioButtonList1.SelectedValue;
            decimal bv = Convert.ToDecimal(txtBV.Text);
            var find = (from c in db.tblMembers
                        where c.username == TextBox1.Text.Trim()
                        select c).SingleOrDefault();
            if (find != null)
            {
                AddBv(find.username, bv, leg, "", "Admin", 0, "Repurchase");
                Response.Redirect("RepurchaseBV.aspx?pl=sub");
            }
        }
    }

    private void AddBv(string uname, decimal pv, string desc, string remarks, string bvfrom, int invId, string type)
    {
        DateTime date = DateTime.UtcNow.AddHours(5.5);
        var history = new tblBV
        {
            username = uname,
            Bv = 0,
            leftBv = 0,
            rightBv = 0,
            bvFrom = bvfrom,
            date = date.Date,
            desc = desc,
            remarks = remarks,
            status = "Pending",
            invId = invId,
            lcarry = 0,
            rcarry = 0,
            bvType = type
        };
        if (desc == "Self")
            history.Bv = pv;
        else if (desc == "Left")
            history.leftBv = pv;
        else if (desc == "Right")
            history.rightBv = pv;

        db.tblBVs.InsertOnSubmit(history);
        db.SubmitChanges();
    }

}