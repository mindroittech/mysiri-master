using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class transfer_pin : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            Label8.Text = Request.QueryString["package"];
            Label10.Text = Request.QueryString["uname"];

            var get = (from c in db.tblMembers
                       where c.username == Label10.Text
                       select c).SingleOrDefault();

            Label9.Text = Request.QueryString["price"];
            Label11.Text = get.name;

        }

        catch
        {
            Response.Redirect("pintransfer.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in ListBox1.Items)
        {
            if (item.Selected)
            {
                ListBox2.Items.Add(item);
            }
        }

        Label13.Text = ListBox2.Items.Count.ToString();

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        ListBox2.Items.Remove(ListBox2.SelectedItem);
        Label13.Text = ListBox2.Items.Count.ToString();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ListBox2.Items.Count == 0)
        {
            Label14.Text = "Please add PINs to transfer";
            Label14.Visible = true;
            return;
        }

        foreach (ListItem item in ListBox2.Items)
        {
            var get = (from c in db.PinManagaers
                       where c.pinnumbers == item.Text
                       select c).SingleOrDefault();

            get.issued = Label10.Text;
            get.transferby = Session["username"].ToString();

            var insert = new PinTransferHistory
            {
                pinnumbers = get.pinnumbers,
                transferDate = DateTime.UtcNow.Date,
                transferedFrom = Session["username"].ToString(),
                transferedTo = get.issued
            };
            db.PinTransferHistories.InsertOnSubmit(insert);
            db.SubmitChanges();
        }

        //var get1 = (from c in db.tblMembers
        //            where c.username == Session["username"].ToString()
        //            select c).SingleOrDefault();

        //////string msg = "Dear "+get1.name+", You have successfully sent "+ListBox2.Items.Count+" PINS to "+Label11.Text;
        //////sm.sms(get1.mobile, msg);

        //var get2 = (from c in db.tblMembers
        //            where c.username == Label10.Text
        //            select c).SingleOrDefault();

        //string msg = "Dear " + get2.name + ", You have received " + ListBox2.Items.Count + " PINs from " + get1.name;
        //sm.sms(get2.mobile, msg);

        Response.Redirect("pintransfer.aspx?st=sub&n=" + Label11.Text + "&u=" + Label10.Text + "");

    }
}