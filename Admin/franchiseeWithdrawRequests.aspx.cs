using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_franchiseeWithdrawRequests : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            if (Session["role"].ToString() != "Admin")
            {
                Response.Redirect("access-denied.aspx", false);
            }

            if (!IsPostBack)
            {

                bind();
            }
        }

        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }


    //protected void Button4_Click(object sender, EventArgs e)
    //{

    //    Response.Redirect("franchiseeWithdrawRequests.aspx");
    //}

    protected void Button2_Click(object sender, EventArgs e)
    {
        DateTime? d1 = null, d2 = null;
        string user = null;

        if (txtUser.Text != string.Empty)
        {
            user = txtUser.Text;
        }

        bind(null, user);
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("franchiseeWithdrawRequests.aspx");
    }

    protected void bind(DateTime? date = null, string user = null)
    {

        var getpayout = from c in db.PaymentRequests
                        join d in db.tblVendors
                        on c.username equals d.Username
                        where c.status == "Pending"
                        && d.IsActive == true
                        orderby c.date descending
                        select new
                        {
                            c.cashavailable,
                            c.paymentid,
                            c.username,
                            d.Phone,
                            amount = c.amount,
                            name = d.VendorName,
                            tax = c.tax,
                            sc = c.sc,
                            netpay = c.netpay,
                            c.date,
                            d.Country,
                            fromDate = c.date,
                            d.bankname,
                            c.deduction,
                            d.branch,
                            d.account,
                            d.ifsc,
                            c.comments,
                            pan = d.PAN
                        };


        if (date != null)
        {
            getpayout = getpayout.Where(x => x.date == date);
        }
        if (user != null)
        {
            getpayout = getpayout.Where(x => x.username == user);
        }

        GridView1.DataSource = getpayout.OrderByDescending(x => x.date).ThenBy(x => x.pan);
        GridView1.DataBind();

        if (GridView1.Rows.Count == 0)
        {
            Button1.Visible = false;
            Button9.Visible = false;
        }
        else
        {
            Button1.Visible = true;
            Button9.Visible = true;
        }
    }

    public decimal getTotalInome(string uname)
    {

        var getTotalDebit = (from d in db.tblMembers
                             where d.username == uname
                             group d by d.account into grp
                             select new { totalInome = grp.Sum(x => x.debit) }).SingleOrDefault();

        return getTotalDebit.totalInome.Value;
    }



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "send")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            TextBox txt = row.FindControl("txtbox") as TextBox;


            PaymentRequest add = db.PaymentRequests.Where(x => x.paymentid == id).SingleOrDefault();

            add.status = "Paid";
            add.transaction = txt.Text;
            add.paiddate = DateTime.UtcNow.AddHours(5.5).Date;

            db.SubmitChanges();

            //string msg1 = "Dear " + debit.name + ", Your weekly payment of Rs " + add.netpay + " has been paid.";
            //sm.sms(debit.mobile, msg1, cmn.GetCountryCode(debit.country));

            Response.Redirect("franchiseeWithdrawRequests.aspx");
        }
        if (e.CommandName == "view")
        {
            string uname = e.CommandArgument.ToString();

            string url = "fdetails.aspx?uname=" + uname;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=480,left=150,top=150,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }
        if (e.CommandName == "del")
        {

            int id = Convert.ToInt32(e.CommandArgument);

            PaymentRequest add = db.PaymentRequests.Where(x => x.paymentid == id).SingleOrDefault();

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            TextBox txt = row.FindControl("txtbox") as TextBox;

            add.status = "Rejected";
            add.transaction = txt.Text;
            add.paiddate = DateTime.UtcNow.Date;
            db.SubmitChanges();
            Response.Redirect("franchiseeWithdrawRequests.aspx");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("export.aspx?t=Franchisee");
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        Response.Redirect("bankExport.aspx?t=Franchisee");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chk = row.FindControl("CheckBox1") as CheckBox;

            if (chk.Checked)
            {
                int id = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[2]);  // paymentid

                PaymentRequest add = db.PaymentRequests.Where(x => x.paymentid == id).SingleOrDefault();

                add.status = "Paid";
                add.transaction = txtTrans.Text;
                add.paiddate = DateTime.UtcNow.Date;
            
                db.SubmitChanges();
            }
        }

        Response.Redirect("franchiseeWithdrawRequests.aspx");
    }
}