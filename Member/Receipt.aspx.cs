using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_Receipt : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();


            var member = db.tblMembers.SingleOrDefault(x => x.username == uname);

            var pin = db.PinManagaers.Single(x => x.pinnumbers == member.pinnumber);

            if (pin.IsFreePin == true)
            {
                inv.Visible = false;
                lberror.Text = "Sorry Invoice is Not available...";
                lberror.Visible = true;
                print.Visible = false;
                return;

            }

            var invoice = db.RequestProducts.FirstOrDefault(x => (x.username == uname|| x.pin_number==member.pinnumber) /*&& x.status == "Send"*/);
            if (invoice == null)
            {
                //Response.Redirect("../login.aspx", false);

                inv.Visible = false;
                lberror.Text = "Sorry Invoice is Not available. contact admin";
                lberror.Visible = true;
                print.Visible = false;
                return;
            }
            lblName.Text = member.name;
            lblAddress.Text = member.address + " " + member.city + " " + member.state;
            string state = "";
            if (member != null)
            {
                lblCity.Text = member.city;
                lblState.Text = member.state;
                lblPincode.Text = member.pincode;
                state = member.state;
            }
            lblUser.Text = member.username;
            lblInv.Text = invoice.pid.ToString("0000");
            lblInvDate.Text = invoice.date.Value.ToString("dd/MM/yyyy");

            var plan = db.tblPlans.FirstOrDefault(x => x.PlanName == invoice.name || x.Description == invoice.name);

            lblDesc.Text = "Organic Heath Kit";
            decimal cost = decimal.Parse(invoice.name.Split('-')[1]);
            double price = Math.Floor((double)cost * 100 / 112);
            double gst = ((double)cost - price) / 2;
            lblPrice.Text = price.ToString("0");
            lblCGST.Text = lblSGST.Text = gst.ToString();
            lblTotal.Text = cost.ToString();

            //lblTotalCGST.Text = (invoice.cost.Value / 6).ToString();
            //lblTotalSGST.Text = (invoice.cost.Value / 6).ToString();

        }
        catch (Exception ex)
        {
            Response.Redirect("../login.aspx");
           //throw ex;
        }

    }
}