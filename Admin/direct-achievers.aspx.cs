using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_flight_trips : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

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
    private void bind()
    {
        var get = from d in db.tblMembers
                  group d by d.sid into grp
                  where grp.Count() >= 10
                  join c in db.tblMembers
                  on grp.Key equals c.username
                  orderby grp.Count() descending
                  select new { c.username, c.name, c.joindate, count = grp.Count() };
        GridView1.DataSource = get;
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "up")
        {

            string uname = e.CommandArgument.ToString();

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            TextBox txtStart = row.FindControl("txtStart") as TextBox;
            TextBox txtEnd = row.FindControl("txtEnd") as TextBox;


            if (txtStart.Text != "" && txtEnd.Text != "")
            {

                DateTime start = Convert.ToDateTime(txtStart.Text);
                DateTime end = Convert.ToDateTime(txtEnd.Text);

                var update = db.tblMembers.Where(x => x.username == uname).SingleOrDefault();
                update.TourPackageEligible = false;
                update.UsedDate = DateTime.UtcNow.AddHours(5.5).Date;

                var add = new tblMemberTour();

                add.username = uname;
                add.tourType = "Flight Trip";
                add.startDate = start;
                add.endDate = end;
                add.updatedDate = DateTime.UtcNow.AddHours(5.5);

                db.tblMemberTours.InsertOnSubmit(add);
                db.SubmitChanges();

                Response.Redirect("flight-trips.aspx");

            }
            else
            {
                Label1.Text = "Please fill out all the fields";
                Label1.Visible = true;
            }


        }
    }
}