using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (!IsPostBack)
                bind();
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            tblBV bv = db.tblBVs.SingleOrDefault(x => x.BvId == id);
            //pending
            var mem = db.tblMembers.SingleOrDefault(x => x.username == bv.username);
            if (bv.desc == "Left")
            {
                mem.alcarry -= bv.leftBv;
                mem.alcount -= bv.leftBv;
            }
            else if (bv.desc == "Right")
            {
                mem.arcarry -= bv.rightBv;
                mem.arcount -= bv.rightBv;
            }


            db.tblBVs.DeleteOnSubmit(bv);
            db.SubmitChanges();

            Response.Redirect("achievementPv.aspx");
        }

    }
    public void bind(string uname = null)
    {

        var loans = from c in db.tblBVs
                    join d in db.tblMembers
                    on c.username equals d.username
                    where c.bvFrom == "Admin"
                    && c.bvType == "Achievement"
                    orderby c.BvId descending
                    select new
                    {
                        c.BvId,
                        c.desc,
                        c.username,
                        d.name,
                        c.leftBv,
                        c.rightBv,
                        bv = c.desc == "Left" ? c.leftBv : c.rightBv,
                        c.bvType,
                        c.date,
                        c.status
                    };
        GridView1.DataSource = loans;
        GridView1.DataBind();

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }
}