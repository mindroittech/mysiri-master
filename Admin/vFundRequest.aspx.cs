using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Admin_vFundRequest : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["vendor"].ToString();

            if (!IsPostBack)
            {
                if (Request.QueryString["val"] == "sub")
                {
                    Label1.ForeColor = Color.Green;
                    Label1.Text = "Fund Request Submitted Successfully";
                    Label1.Visible = true;
                }

            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["vendor"].ToString();

            if (DropDownList2.SelectedValue == "Online")
            {
                var checkUTR = from c in db.PinRequests
                               where c.transactionno == txtUTR.Text
                               select c;
                if (checkUTR.Any())
                {
                    Label1.Text = "UTR Number already exists";
                    Label1.Visible = true;
                    return;
                }

                var check = from c in db.FranchiseeWalletRequests
                            where c.transactionno == txtUTR.Text && c.status!= "Rejected"
                            select c;
                if (check.Any())
                {
                    Label1.Text = "UTR Number already exists";
                    Label1.Visible = true;
                    return;
                }

            }
            var req = new FranchiseeWalletRequest();
            req.username = uname;
            req.amountRequired = Convert.ToDecimal(txtAmount.Text);
            req.transactionno = txtUTR.Text;


            req.paymode = DropDownList2.SelectedItem.Text;
            if (FileUpload1.HasFile)
            {
                string fileName = req.username + "_" + FileUpload1.FileName;
                req.counterfoil = "../Files/" + fileName;
                FileUpload1.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            req.remarks = TextBox3.Text;
            req.status = "Pending";
            req.createdDate = DateTime.UtcNow.AddHours(5.5);
            db.FranchiseeWalletRequests.InsertOnSubmit(req);
            db.SubmitChanges();
            Response.Redirect("vFundRequest.aspx?val=sub");
        }
        catch
        {
            Label1.Visible = true;
            Label1.Text = "Invalid information";
        }

    }
}