using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default3 : System.Web.UI.Page
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
                bind();
            }

            if (Request.QueryString["n"] == "v")
            {
                Label1.Visible = true;
                Label1.Text = "Changed Successfully";
                Label1.ForeColor=System.Drawing.Color.Green;
            }
          
        }

        catch
        {

            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void txtUser_Changed(object sender, EventArgs e)
    {
        findName(TextBox1.Text.Trim());
    }
    private void bind()
    {

        var get = from c in db.tblIDChangeHistories
                  orderby c.ChangeDate descending
                  select new { 
                      c.username, 
                      c.oldname, 
                      c.newname,
                      c.oldmobile,
                      c.newmobile,
                      c.ChangeDate,
                      c.oldpan,
                      c.newpan,
                      kyc = c.isAchievement == true ? "Yes" : "No" 
                  };
       
        GridView1.DataSource = get;
        GridView1.DataBind();
    }
    private void findName(string uname)
    {
        try
        {
            var get = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
            if (get != null)
            {
                lblname.Text = get.name;
                lblmobile.Text = get.mobile;
                lbljoindate.Text = get.dob.ToString();
                Label2.Text = get.alcount.ToString();


                Label3.Text = get.arcount.ToString();
                Label4.Text = get.alcarry.ToString();
                Label5.Text = get.arcarry.ToString();

                lblpan.Text = get.pan;

            }
            else
            {
                lblname.Text = "";
                lblmobile.Text = "";
                lbljoindate.Text = "";
                Label2.Text = "";


                Label3.Text = "";
                Label4.Text = "";
                Label5.Text = "";
                lblpan.Text = "";
            }
        }
        catch
        {
           
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
       
        var find = (from c in db.tblMembers
                    where c.username == TextBox1.Text.Trim()
                    select c).SingleOrDefault();

      
        string mobile = TextBox4.Text.Trim();
        string name = TextBox3.Text;
        string pan = Txtpan.Text;

        if (find == null)
        {
            Label1.Visible = true;
            Label1.Text = "ID not found";
            return;
        }
        if (string.IsNullOrWhiteSpace(TextBox4.Text) && string.IsNullOrWhiteSpace(TextBox3.Text))
        {
            Label1.Visible = true;
            Label1.Text = "Invalid Details";
            return;
        }

        var history = new tblIDChangeHistory
        {
            username = find.username,
            oldname = find.name,
            oldmobile = find.mobile,
            oldpan = find.pan,
            newname = name,
            newmobile = mobile,
            ChangeDate = DateTime.Now,
            newpan = pan
        };
        db.tblIDChangeHistories.InsertOnSubmit(history);
        history.isAchievement = false;
        find.name= name;
        find.mobile= mobile;
        find.pan= pan;

        if (CheckBox1.Checked)
        {
            find.alcount = find.arcount = find.alcarry = find.arcarry = find.algoldcarry = find.argoldcarry = 0;

            history.isAchievement = true;
        }

        db.SubmitChanges();

        Response.Redirect("idchange.aspx?n=v", false);
    }
}