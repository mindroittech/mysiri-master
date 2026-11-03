using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Admin_BinaryBvAdjust : System.Web.UI.Page
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
                    Label2.Text = "Binary Bv has been added successfully";
                    Label2.Visible = true;
                }
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

                lblLeft.Text = get.lcarry.ToString();
                lblRight.Text = get.rcarry.ToString();

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
                if (leg == "Left")
                {
                    find.lcarry = find.lcarry + bv;
                    find.tlcount = find.tlcount + bv;
                }
                if (leg == "Right")
                {
                    find.rcarry = find.rcarry + bv;
                    find.trcount = find.trcount + bv;
                }

                AddBv(find.username, bv, leg, "", "Admin", 0, "Binary");
                Response.Redirect("BinaryBV.aspx?pl=sub");
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