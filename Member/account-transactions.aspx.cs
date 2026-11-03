using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class account_transactions : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();
            if (!IsPostBack)
                bind();

        }
        catch (Exception ex)
        {
            //throw ex;
            Response.Redirect("../login.aspx");
        }
    }

   

   

    protected void bind()
    {
        string uname = Session["username"].ToString();
        var get = from c in db.Payouts
                  where c.username == uname && c.status == "Send" && c.desc != "Self Purchase Wallet"
                  orderby c.confirmdate descending
                  select new
                  {
                      credit = c.credit,
                      c.username,
                      c.desc,
                      c.tax,
                      c.netpayout,
                      c.debit,
                      c.date,
                      c.confirmdate,
                      c.Left_New,
                      c.Right_New,
                      c.Left_Previous,
                      c.Right_Previous,
                      c.remarks,
                      lapse = c.desc == "Lapse" ? c.reward : 0
                  };


        GridView1.DataSource = get;
        GridView1.DataBind();

        Decimal am = 0;
        foreach (var g in get)
        {
            am = am + (Decimal)g.credit;
        }

        Label7.Text = Math.Round(am).ToString();
        Label8.Text = Math.Round(db.tblMembers.Where(x => x.username == uname).SingleOrDefault().debit.Value).ToString();
        lblCreditLeft.Text = Math.Round(db.tblMembers.Where(x => x.username == uname).SingleOrDefault().credit.Value).ToString();

    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            //string uname = Session["uname"].ToString();
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DateTime date = Convert.ToDateTime(e.Row.Cells[4].Text);
            //    var get = from c in db.Payouts
            //              where c.username == uname && c.date <= date && c.status == "Send"
            //              select c;
            //    Decimal am = 0;
            //    foreach (var g in get)
            //    {
            //        am = am + Convert.ToDecimal(g.credit + g.reward - g.debit);
            //    }
            //    e.Row.Cells[5].Text = am.ToString();

            //}
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
}