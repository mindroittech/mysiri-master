using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_Admin_member_tours : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();


            if (!IsPostBack)
            {

                var bindGrid = from c in db.tblMembers
                               join d in db.tblMemberTours on c.username equals d.username into ps
                               from f in ps.DefaultIfEmpty()
                               where f.id == null
                               select new { c.username, c.joindate, c.name };

                GridView1.DataSource = bindGrid;
                GridView1.DataBind();

            }

        }

        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "up")
        {

            string uname = e.CommandArgument.ToString();

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            DropDownList drp = row.FindControl("drpTour") as DropDownList;
            TextBox txtStart = row.FindControl("txtStart") as TextBox;
            TextBox txtEnd = row.FindControl("txtEnd") as TextBox;


            if (txtStart.Text != "" && txtEnd.Text != "")
            {

                DateTime start = Convert.ToDateTime(txtStart.Text);
                DateTime end = Convert.ToDateTime(txtEnd.Text);

                var add = new tblMemberTour();


                add.username = uname;
                add.tourId = Convert.ToInt32(drp.SelectedValue);

                add.tourType = "Introduction";
                add.startDate = start;
                add.endDate = end;
                add.updatedDate = DateTime.UtcNow.AddHours(5.5);

                db.tblMemberTours.InsertOnSubmit(add);
                db.SubmitChanges();

                Response.Redirect("member-tours.aspx");

            }
            else
            {
                Label1.Text = "Please fill out all the fields";
                Label1.Visible = true;
            }


        }
    }
}