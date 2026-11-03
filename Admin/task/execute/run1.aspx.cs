using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_task_execute_run1 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        db.CommandTimeout = 7200;

        int ved;
        if (Request.QueryString["hours"] != null)
        {
            ved = Convert.ToInt32(Request.QueryString["hours"]);
        }
        else
        {
            ved = 24;
        }

        var allMembers = from c in db.tblMembers
                         orderby c.run_date ascending
                         where (c.isActive == 1 && c.run_date <= DateTime.Now.AddHours(-ved))
                         select c;

        foreach (var m in allMembers)
        {
            try
            {
                DataTable leftDt = MyDownline(m.id, "Left", "Silver");
                DataTable rightDt = MyDownline(m.id, "Right", "Silver");

                int leftCount = leftDt.AsEnumerable().Count();
                int rightCount = rightDt.AsEnumerable().Count();

                decimal lbv = db.tblBVs
                                .Where(x => x.username == m.username && x.desc == "Left" && x.bvType == "Repurchase" && x.bvFrom != "Admin")
                                .Sum(x => (decimal?)x.leftBv) ?? 0;

                decimal rbv = db.tblBVs
                                .Where(x => x.username == m.username && x.desc == "Right" && x.bvType == "Repurchase" && x.bvFrom != "Admin")
                                .Sum(x => (decimal?)x.rightBv) ?? 0;

                decimal sbv = db.tblBVs
                                .Where(x => x.username == m.username && x.desc == "Self" && x.bvType == "Repurchase")
                                .Sum(x => (decimal?)x.Bv) ?? 0;

           
                m.total_left = leftCount;
                m.total_right = rightCount;
                m.cumbv_left = lbv;
                m.cumbv_right = rbv;
                m.cumbv_self = sbv;
                m.run_date = DateTime.UtcNow.AddHours(5.5);

                db.SubmitChanges();
            }
            catch (Exception ex)
            {              
                throw;
            }
          
        }
    }
    public DataTable MyDownline(int id, string leg, string plan)
    {
        try
        {
            string query1 = string.Format(@"WITH t AS(SELECT id, refid, name,username,joindate,[status],isActive FROM tblMember WHERE refid={0} AND leg='{1}'
          UNION ALL
          SELECT c.id, c.refid, c.name,c.username,c.joindate,c.[status],c.isActive FROM t p JOIN tblMember c ON c.refid = p.id )
            SELECT * FROM t OPTION (MAXRECURSION 0)", id, leg, plan);   
            
            //  where t.isActive=1 removed

            //   string query1 = string.Format(@"WITH t AS(SELECT id, refid, name,username,joindate,[status],isActive FROM tblMember WHERE refid={0} AND leg='{1}'
            //UNION ALL
            //SELECT c.id, c.refid, c.name,c.username,c.joindate,c.[status],c.isActive FROM t p JOIN tblMember c ON c.refid = p.id )
            //  SELECT * FROM t where t.isActive=1 OPTION (MAXRECURSION 0)", id, leg, plan);

            string conn = ConfigurationManager.ConnectionStrings["LotusBizConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = conn;
                SqlCommand cmd = new SqlCommand(query1, con);
                cmd.CommandTimeout = 7200;
                //create the DataAdapter & DataSet
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        catch (Exception ex)
        {
            //cls.SendMail("rajesha.billawa@gmail.com", "Admin", "bkrajeshkumar1992@gmail.com", "MGL", ex.Message);
            throw ex;
        }

    }
}
