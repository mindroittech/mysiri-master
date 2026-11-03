using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class pinrequest : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                if (Request.QueryString["val"] == "sub")
                {
                    Label1.ForeColor = Color.Green;
                    Label1.Text = "Pin Request Sent Successfully";
                    Label1.Visible = true;
                }

                var plan = from c in db.tblPlans where c.status==true
                           select c;
                GridView1.DataSource = plan;
                GridView1.DataBind();

                if (Session["vendor"] != null)
                    Label2.Visible = true;

            }
        }
        catch
        {
            Response.Redirect("../login.aspx");
        }

    }

    protected void txtQuanity_Changed(object sender, EventArgs e)
    {

        Label lblQty = GridView1.FooterRow.FindControl("lblTotalQty") as Label;
        Label lbltotal = GridView1.FooterRow.FindControl("lblTotalAmount") as Label;
        try
        {
            GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;

            TextBox txtNos = (TextBox)row.FindControl("txtNos");
            Label lblAmount = (Label)row.FindControl("lblAmount");

            if (txtNos.Text.Trim() != string.Empty)
            {
                int quantity = Convert.ToInt32(txtNos.Text);
                Decimal cost = Convert.ToDecimal(row.Cells[2].Text.Trim());
                Decimal amt = quantity * cost;
                lblAmount.Text = amt.ToString();
            }
            else
            {
                lblAmount.Text = "0";
            }

            Decimal totalamt = 0;
            int totalQuantity = 0;
            foreach (GridViewRow r in GridView1.Rows)
            {
                Label lblAMt = (Label)r.FindControl("lblAmount");
                TextBox txtQuant = (TextBox)r.FindControl("txtNos");
                if (lblAMt.Text != "" && lblAMt.Text != "0")
                {
                    totalamt += Convert.ToDecimal(lblAMt.Text);
                    totalQuantity += Convert.ToInt32(txtQuant.Text);
                }
            }
            lblCost.Text = totalamt.ToString();
            lblQty.Text = totalQuantity.ToString();
            lbltotal.Text = totalamt.ToString();
        }
        catch
        {
            lblCost.Text = "0";
            lblQty.Text = "0";
            lbltotal.Text = "0";
        }
    }
    protected void DropDownList2_Changed(object sender, EventArgs e)
    {
        if (DropDownList2.SelectedValue == "Online")
            divUTR.Visible = true;
        else
            divUTR.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["vendor"] != null ? Session["vendor"].ToString() : Session["username"].ToString();


            decimal totalAmt = Convert.ToDecimal(lblCost.Text);
            if (totalAmt == 0)
            {
                Label1.Text = "Please select atleast one plan.";
                Label1.Visible = true;
                return;
            }
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

            var req = new PinRequest();
            req.username = uname;
            req.totalAmount = Decimal.Parse(lblCost.Text);
            req.amount = Decimal.Parse(txtAmount.Text);
            req.paymode = DropDownList2.SelectedItem.Text;
            if (FileUpload1.HasFile)
            {
                string fileName = req.username + "_" + FileUpload1.FileName;
                req.counterfoil = "../Files/" + fileName;
                FileUpload1.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            req.remarks = TextBox3.Text;
            req.transactionno = txtUTR.Text;
            req.status = "Pending";
            req.createdDate = DateTime.UtcNow.AddHours(5.5);
            db.PinRequests.InsertOnSubmit(req);
            db.SubmitChanges();

            List<PinRequestByPlan> lstPurchase = new List<PinRequestByPlan>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                int planId = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                TextBox txtNos = (TextBox)row.FindControl("txtNos");
                Label lblAmount = (Label)row.FindControl("lblAmount");
                if (txtNos.Text != "" && txtNos.Text != "0")
                {
                    int quant = Convert.ToInt32(txtNos.Text);
                    var repurchase = new PinRequestByPlan
                    {
                        planId = planId,
                        QtyReq = quant,
                        pId = req.pid
                    };
                    lstPurchase.Add(repurchase);
                }
            }

            db.PinRequestByPlans.InsertAllOnSubmit(lstPurchase);
            db.SubmitChanges();

            Response.Redirect("pinrequest.aspx?val=sub");
        }
        catch
        {
            Label1.Visible = true;
            Label1.Text = "Invalid information";
        }

    }
}