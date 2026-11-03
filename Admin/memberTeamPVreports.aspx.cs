using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_memberWiseRepurchaseReport : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    private static bool isclicked = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();
            if (role != "GoDown" && role != "Admin")
            {
                throw new Exception("Access denied");
            }
            if (!IsPostBack)
            {
                //bind();
            }
        }
        catch (Exception)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            DateTime? d1 = null, d2 = null;
            if (TextBox2.Text != string.Empty)
            {
                d1 = Convert.ToDateTime(TextBox2.Text);
            }
            if (TextBox3.Text != string.Empty)
            {
                d2 = Convert.ToDateTime(TextBox3.Text);
            }
            bind(TextBox1.Text, d1, d2);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    private void bind(string uname, DateTime? startDate = null, DateTime? endDate = null)
    {
        db.CommandTimeout = 7200;

        var query = from c in db.tblBVs
                    join d in db.tblMembers
                        on c.username equals d.username
                    join s in db.tblMembers
                        on d.sid equals s.username
                    join sbv in db.tblBVs
                        on s.username equals sbv.username
                    where d.status == "Paid"
                    select new
                    {
                        c.username,
                        d.name,
                        c.date,

                        self = c.Bv ?? 0,

                        binary = c.bvType == "Binary"
                                 ? ((c.lcarry ?? 0) + (c.rcarry ?? 0))
                                 : 0,

                        repurchase = c.bvType == "repurchase"
                                     ? ((c.leftBv ?? 0) + (c.rightBv ?? 0))
                                     : 0,

                        sponsor = sbv.Bv ?? 0
                    };

        // 🔹 filters
        if (!string.IsNullOrEmpty(uname))
            query = query.Where(x => x.username == uname);

        if (startDate.HasValue)
            query = query.Where(x => x.date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(x => x.date < endDate.Value.AddDays(1));

        // 🔥 GROUP BY → SINGLE RECORD
        var result = from x in query
                     group x by new { x.username, x.name } into g
                     select new
                     {
                         username = g.Key.username,
                         name = g.Key.name,

                         self = g.Sum(a => a.self),
                         binary = g.Sum(a => a.binary),
                         repurchase = g.Sum(a => a.repurchase),
                         sponsor = g.Sum(a => a.sponsor)
                     };

        GridView1.DataSource = result.ToList();
        GridView1.DataBind();
    }


}