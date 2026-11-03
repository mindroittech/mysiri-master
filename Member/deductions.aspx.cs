using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_deductions : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();
            if (!IsPostBack)
                Bind(uname);

        }
        catch (Exception ex)
        {
            //throw ex;
            Response.Redirect("../login.aspx");
        }
    }

    private void Bind(string uname)
    {
        var bind = from c in db.Payouts
                   where c.username == uname
                   && c.status == "Send" && c.desc.Contains("Pair Commission Deduction")
                   orderby c.confirmdate descending
                   select new
                   {
                       desc = c.desc.Replace("Pair Commission Deduction", ""),
                       c.debit,
                       c.confirmdate,
                       c.remarks
                   };

        GridView1.DataSource = bind;
        GridView1.DataBind();
    }
}