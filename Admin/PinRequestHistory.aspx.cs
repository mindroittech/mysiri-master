using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PinRequestHistory : System.Web.UI.Page
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string user = null;
        DateTime? d1 = null;
        DateTime? d2 = null;
        string plan = null;
        bool paging = true;
        if (TextBox1.Text != string.Empty)
        {
            user = TextBox1.Text;
            paging = false;
        }

        GridView1.AllowPaging = paging;
        bind(user);
    }

    private void bind(string user = null, DateTime? d1 = null, DateTime? d2 = null, string plan = null)
    {
        var history1 = from c in db.PinRequests
                       join d in db.tblMembers
                        on c.username equals d.username into grp
                       from f in grp.DefaultIfEmpty()
                       join k in db.tblVendors
                       on c.username equals k.Username into grp1
                       from g in grp1.DefaultIfEmpty()

                       where c.status != "Pending"
                       orderby c.pid descending
                       select new
                       {
                           c.username,
                           c.PlanName,
                           c.amount,
                           c.counterfoil,
                           c.bank,
                           c.status,
                           c.noofPins,
                           c.paymode,
                           c.remarks,
                           c.transactionno,
                           c.createdDate,
                           c.pid,
                           Name = g != null ? g.VendorName : f.name
                       };

        if (user != null)
        {
            history1 = history1.Where(x => x.username == user);
        }


        if (plan != null)
        {
            history1 = history1.Where(x => x.PlanName == plan);
        }


        GridView1.DataSource = history1.ToList();
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "pins")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            var pins = from c in db.PinRequestByPlans
                       join d in db.tblPlans
                       on c.planId equals d.PlanId
                       where c.pId == id
                       select new { c.planId, c.QtyReq, c.QtySent, d.PlanName };

            GridView2.DataSource = pins;
            GridView2.DataBind();
            ImageButton2_ModalPopupExtender.Show();
        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        bind();
    }
}