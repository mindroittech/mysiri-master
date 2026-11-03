using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RejectedPayout : System.Web.UI.Page
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

                if (GridView1.Rows.Count == 0)
                {
                    Button1.Visible = false;

                }


            }
        }

        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        DateTime? d1 = null, d2 = null;
        string user = null;
        if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
        {
            d1 = Convert.ToDateTime(TextBox1.Text);
            d2 = Convert.ToDateTime(TextBox2.Text);
        }
        if (txtUser.Text != string.Empty)
        {
            user = txtUser.Text;
        }

        bind(d1, d2, user);
        if (GridView1.Rows.Count == 0)
        {
            Button1.Visible = false;
        }
    }


    protected void bind(DateTime? d1 = null, DateTime? d2 = null, string user = null)
    {

        var getpayout = from c in db.PaymentRequests
                        join d in db.tblMembers
                        on c.username equals d.username
                        where c.status == "Rejected"
                        orderby c.date descending
                        select new
                        {
                            c.cashavailable,
                            c.paymentid,
                            c.username,
                            amount = c.amount,
                            name = d.name,
                            tax = c.tax,
                            sc = c.sc,
                            netpay = c.netpay,
                            date = c.date,
                            d.bankname,
                            d.branch,
                            d.account,
                            d.ifsc,
                            c.comments,
                            d.isPanVerifed
                        };
        if (d1 != null && d2 != null)
        {
            getpayout = getpayout.Where(x => x.date >= d1 && x.date <= d2);
        }
        if (user != null)
            getpayout = getpayout.Where(x => x.username == user);

        GridView1.DataSource = getpayout;
        GridView1.DataBind();
    }


    protected void bind()
    {

        var getpayout = from c in db.PaymentRequests
                        join d in db.tblMembers
                        on c.username equals d.username
                        where c.status == "Rejected"
                        orderby c.date descending
                        select new
                        {
                            c.cashavailable,
                            c.paymentid,
                            c.username,
                            amount = c.amount,
                            name = d.name,
                            tax = c.tax,
                            sc = c.sc,
                            netpay = c.netpay,
                            date = c.date,
                            d.bankname,
                            d.branch,
                            d.account,
                            d.ifsc,
                            c.comments,
                            d.isPanVerifed
                        };

        GridView1.DataSource = getpayout;
        GridView1.DataBind();
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
            add.paiddate = DateTime.UtcNow.Date;

            string uname = add.username;
            var debit = (from c in db.tblMembers
                         where c.username == uname
                         select c).SingleOrDefault();

            var insert = new Payout();
            insert.username = uname;
            insert.tax = 0;
            insert.debit = add.amount;
            insert.desc = "Debit";
            insert.credit = 0;
            insert.reward = 0;
            insert.netpayout = 0;
            insert.status = "Send";
            insert.date = DateTime.UtcNow.Date;
            insert.confirmdate = DateTime.UtcNow;
            insert.remarks = "Withdrawn";
            db.Payouts.InsertOnSubmit(insert);

            debit.debit = debit.debit + add.amount;

            db.SubmitChanges();

            //string msg1 = "Dear " + debit.name + ", We have transferred Rs " + add.netpay + " to your bank account on " + DateTime.UtcNow.Date.ToString("dd/MMM/yyyy") + " as per your withdraw request";
            //sm.sms(debit.mobile, msg1);

            Response.Redirect("RejectedPayout.aspx");
        }
        if (e.CommandName == "view")
        {
            string uname = e.CommandArgument.ToString();
            string url = "detail.aspx?uname=" + uname;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=480,left=150,top=150,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        if (e.CommandName == "del")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            PaymentRequest add = db.PaymentRequests.Where(x => x.paymentid == id).SingleOrDefault();
            add.status = "Rejected";
            db.SubmitChanges();
            Response.Redirect("withdraw-request.aspx");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("export.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("RejectedPayout.aspx");
    }
}