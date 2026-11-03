using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class member : System.Web.UI.MasterPage
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();
            if (!IsPostBack)
            {
                var q = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
                lblName.Text = lblName2.Text = q.name;
                lblUsername.Text = uname;
                lblDate.Text = Convert.ToDateTime(q.joindate.Value).ToString("dd/MMM/yyyy");

                if (q.isPanVerifed == true)
                {
                    lblKYC.Text = "Verified";
                    lblKYC.ForeColor = Color.Green;
                }
                else
                {
                    lblKYC.ForeColor = Color.Red;
                    lblKYC.Text = "Not Verified";
                }
                if (q.UploadId != null)
                    profilePic.Src = q.UploadId;

                var inv = db.tblMembers.Single(x => x.username == uname);
                var pin = db.PinManagaers.Single(x => x.pinnumbers == inv.pinnumber);

                if (pin.IsFreePin == false)
                {
                    receipt.Visible = true;
                    
                }
                  

            }

        }
        catch
        {

        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("../index.html");
    }
}
