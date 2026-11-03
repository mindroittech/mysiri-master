using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class pinhistory : System.Web.UI.Page
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

                var plan = from c in db.tblPlans
                           select c;
                drpPlan.DataSource = plan;
                drpPlan.DataTextField = "PlanName";
                drpPlan.DataValueField = "PlanAmount";
                drpPlan.DataBind();

                drpPlan.Items.Insert(0, new ListItem("All", "0"));

                bind();
                
            }

        }

        catch
        {
            Response.Redirect("../login.aspx");
        }
    }

    protected void bind()
    {
        string uname = Session["vendor"] != null ? Session["vendor"].ToString() : Session["username"].ToString();
        var bind = from c in db.PinManagaers
                   where c.issued == uname  
                   orderby c.date descending
                   select c;

        GridView1.DataSource = bind;
        GridView1.DataBind();

        Label8.Text = bind.Count().ToString();
        Label9.Text = bind.Where(x => x.status == "Used").Count().ToString();
        Label1.Text = (Convert.ToInt32(Label8.Text) - Convert.ToInt32(Label9.Text)).ToString();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpPlan.SelectedIndex == 0)
        {
            bind();
        }
        else
        {
            string uname = Session["vendor"] != null ? Session["vendor"].ToString() : Session["username"].ToString();
            var bind2 = from c in db.PinManagaers
                        where c.issued == uname && c.package == drpPlan.SelectedItem.Text
                        select c;

            GridView1.DataSource = bind2;
            GridView1.DataBind();

            Label8.Text = bind2.Count().ToString();
            Label9.Text = bind2.Where(x => x.status == "Used").Count().ToString();
            Label1.Text = (Convert.ToInt32(Label8.Text) - Convert.ToInt32(Label9.Text)).ToString();
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "Used")
            {
                e.Row.Cells[4].ForeColor =Color.Red;
            }

        }
    }
}