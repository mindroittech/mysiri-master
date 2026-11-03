using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class pin_history : System.Web.UI.Page
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

                var pinTransfered = from c in db.PinTransferHistories

                                    where c.transferedFrom == uname
                                    select new { c.pinnumbers, date = c.transferDate, issued = c.transferedTo, c.package };
                GridView1.DataSource = pinTransfered;
                GridView1.DataBind();

                var pinReceived = from c in db.PinTransferHistories
                                  where c.transferedTo == uname
                                  select new { c.pinnumbers, date = c.transferDate, transferby = c.transferedFrom, c.package };
                GridView2.DataSource = pinReceived;
                GridView2.DataBind();
            }

        }

        catch
        {
            Response.Redirect("../login.aspx");
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "Used")
            {
                e.Row.Cells[4].ForeColor = Color.Red;
            }

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[4].Text == "Used")
            {
                e.Row.Cells[4].ForeColor = Color.Red;
            }

        }
    }
}