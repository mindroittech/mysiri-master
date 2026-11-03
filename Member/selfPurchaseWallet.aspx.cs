using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_selfPurchaseWallet : System.Web.UI.Page
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
                  where c.username == uname && c.status == "Send" && c.desc == "Self Purchase Wallet"
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
                      c.remarks,
                  };


        GridView1.DataSource = get;
        GridView1.DataBind();

        lblCreditLeft.Text = db.tblMembers.Where(x => x.username == uname).SingleOrDefault().selfPurchaseWallet.ToString();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

}