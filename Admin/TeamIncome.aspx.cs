using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_TeamIncome : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();


        }

        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    public void leafnode(int id, int level)
    {
        string query1 = string.Format(@"WITH t AS(SELECT id, refid,(level-{1}) as level,[status], name,username,leg,joindate,[plan] FROM tblMember WHERE refid={0}
          UNION ALL
          SELECT c.id, c.refid,(c.level-{1}) as level,c.[status], c.name,c.username,p.leg,c.joindate,c.[plan] FROM t p JOIN tblMember c ON c.refid = p.id)
            SELECT t.*,binary=(Select sum(netpay) from PaymentRequest where username=t.username and status='Pending' and comments='Auto Pay'),
            repurchase=(Select sum(netpay) from PaymentRequest where username=t.username and status='Pending' and comments='Repurchase Pay'),
            earnedBinary=(Select sum(netpay) from PaymentRequest where username=t.username and status='Paid' and comments='Auto Pay'),
            earnedRepurchase=(Select sum(netpay) from PaymentRequest where username=t.username and status='Paid' and comments='Repurchase Pay'),
            income=(Select sum(netpay) from PaymentRequest where username=t.username and status='Pending') FROM t inner join PinManagaer p on t.username=p.usedby where p.IsFreePin=0
            order by level OPTION (MAXRECURSION 0)", id, level);

        string conn = ConfigurationManager.ConnectionStrings["LotusBizConnectionString"].ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = conn;
        SqlCommand cmd = new SqlCommand(query1, con);
        //create the DataAdapter & DataSet
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);


        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        var d = from c in ds.Tables[0].AsEnumerable()
                where c.Field<decimal?>("income") != null
                select c.Field<decimal?>("income");
        Label1.Text = d.Sum().HasValue ? d.Sum().ToString() : "0";


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string uname = TextBox1.Text;

            var get = (from c in db.tblMembers
                       where c.username == uname
                       select c).SingleOrDefault();
            leafnode(get.id, (int)get.level);
        }
        catch
        {

        }

    }
}