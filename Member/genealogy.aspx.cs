using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class genealogy : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();

    protected void bind(string pl, System.Web.UI.HtmlControls.HtmlGenericControl img, string status)
    {

        if (status == "Paid")
        {
            img.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:#21ef21;     '></i>";
        }
        else
        {
            img.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:red;     '></i>";
        }
        //else if (status == "Gold")
        //{
        //    img.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:#FFA500;     '></i>";
        //}
        //else if (status == "Diamond")
        //{
        //    img.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:#BC8F8F;     '></i>";
        //}


    }

    private string GetUserName(int? refid)
    {
        string username = "";
        var fst = (from c in db.tblMembers
                   where c.id == refid
                   select c).SingleOrDefault();

        if (fst != null)
            username = fst.username;

        return username;

    }

    private decimal LeftPv(string uname)
    {
        decimal bv = 0;
        var get = db.tblBVs.Where(x => x.username == uname && x.desc == "Left" && x.bvType == "Binary" && x.bvFrom == "Admin");

        if (get.Any())
            bv = get.Sum(x => x.leftBv.Value);

        return bv;
    }
    private decimal RightPv(string uname)
    {
        decimal bv = 0;
        var get = db.tblBVs.Where(x => x.username == uname && x.desc == "Right" && x.bvType == "Binary" && x.bvFrom == "Admin");

        if (get.Any())
            bv = get.Sum(x => x.rightBv.Value);

        return bv;
    }

    private decimal cumulativeLeftBv(string uname)
    {
        decimal bv = 0;
        var get = db.tblBVs.Where(x => x.username == uname && x.desc == "Left" && x.bvType == "Repurchase" && x.bvFrom != "Admin");

        if (get.Any())
            bv = get.Sum(x => x.leftBv.Value);

        return bv;
    }
    private decimal cumulativeRightBv(string uname)
    {
        decimal bv = 0;
        var get = db.tblBVs.Where(x => x.username == uname && x.desc == "Right" && x.bvType == "Repurchase" && x.bvFrom != "Admin");

        if (get.Any())
            bv = get.Sum(x => x.rightBv.Value);

        return bv;
    }
    private decimal selfBv(string uname)
    {
        decimal bv = 0;
        var get = db.tblBVs.Where(x => x.username == uname && x.desc == "Self" && x.bvType == "Repurchase");

        if (get.Any())
            bv = get.Sum(x => x.Bv.Value);

        return bv;
    }
    private string getRank(int? id)
    {
        string rank = null;
        if (id != null)
        {
            var get = db.tblRepurchaseRanks.SingleOrDefault(x => x.RankId == id);
            rank = get.RankName;
        }
        return rank;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            if (Request.QueryString["uname"] != null)
            {
                uname = Helper.Decrypt(Request.QueryString["uname"]);
            }
            var fst = (from c in db.tblMembers
                       where c.username == uname
                       select c).SingleOrDefault();

            string businessvalue = "<table class=style1 border=1 ><tr><td colspan=3 style=color:Maroon >Business</td></tr>  <tr><td></td><td>Left </td> <td>  Right </td> </tr><tr><td>Total Counts</td><td>" + count(fst.id, "Left", "Silver") + "</td><td>" + count(fst.id, "Right", "Silver") + "</td></tr>"
                + "<tr><td>Confirm Counts</td><td>" + fst.silverLeft + "</td><td>" + fst.silverRight + "</td></tr><tr><td>Binary Points</td><td>" + (fst.tlcount - LeftPv(fst.username)) + "</td><td>" + (fst.trcount - RightPv(fst.username)) + "</td></tr><tr><td>Cumulative Bv</td><td>" + cumulativeLeftBv(fst.username)
                + "</td><td>" + cumulativeRightBv(fst.username) + "</td></tr><tr><td>Self BV</td><td colspan=2>" + selfBv(fst.username) + "</td></tr>  </table>";
            string tit = "<table class=style2> <tr><td> Sponsored By</td><td>: " + fst.sid + "</td></tr><tr><td>Join Date</td> <td>: " + fst.joindate.Value.ToString("dd/MMM/yyyy") + "</td></tr><tr><td>Activation Date</td> <td>: " + (fst.renewal.HasValue ? fst.renewal.Value.ToString("dd/MMM/yyyy") : "") + "</td></tr>"
                + "<tr><td>Package</td> <td>: " + fst.plan + "</td></tr><tr><td>Club Qualification</td> <td>: " + getRank(fst.rankid) + "</td></tr></table>" + businessvalue;
            chain.InnerHtml = "<a  href='genealogy.aspx?uname=" + Helper.Encrypt(fst.username) + "'>" + fst.username + "</a><br/>" + fst.name + " <span class='tooltiptext'>" + tit + "</span>";


            bind(fst.plan, Image1, fst.status);


            var fst1 = from c in db.tblMembers
                       where c.refid == fst.id
                       select c;
            if (fst1.Count() == 0)
            {
                LinkButton3.Text = "New <br/> Member";
                LinkButton3.Enabled = true;
                LinkButton3.PostBackUrl = "../register.aspx?user=" + fst.username + "&n=" + fst.name + "&leg=Right&g=" + fst.gender + "";

                LinkButton2.Text = "New <br/> Member";
                LinkButton2.Enabled = true;
                LinkButton2.PostBackUrl = "../register.aspx?user=" + fst.username + "&n=" + fst.name + "&leg=Left&g=" + fst.gender + "";
            }

            foreach (var f in fst1)
            {
                //count(f.id, "Left")
                string businessvalue1 = "<table class=style1 border=1 ><tr><td colspan=3 style=color:Maroon >Business</td></tr>  <tr><td></td><td>Left </td> <td>  Right </td> </tr><tr><td>Count</td><td>" + count(f.id, "Left", "Silver") + "</td><td>" + count(f.id, "Right", "Silver") + "</td></tr>"
                    + "<tr><td>Confirm Counts</td><td>" + f.silverLeft + "</td><td>" + f.silverRight + "</td></tr><tr><td>Binary Points</td><td>" + (f.tlcount - LeftPv(f.username)) + "</td><td>" + (f.trcount - RightPv(f.username)) + "</td></tr> <tr><td>Cumulative Bv</td><td>" + cumulativeLeftBv(f.username) + "</td><td>"
                    + cumulativeRightBv(f.username) + "</td></tr><tr><td>Self BV</td><td colspan=2>" + selfBv(f.username) + "</td></tr>   </table>";
                string title = "<table class=style2> <tr><td> Sponsored By</td><td>: " + fst.sid + "</td></tr><tr><td>Join Date</td> <td>: " + f.joindate.Value.ToString("dd/MMM/yyyy") + "</td></tr><tr><td>Activation Date</td> <td>: " + (f.renewal.HasValue ? f.renewal.Value.ToString("dd/MMM/yyyy") : "") + "</td></tr>"
                    + "<tr><td>Package</td> <td>: " + f.plan + "</td></tr><tr><td>Club Qualification</td> <td>: " + getRank(f.rankid) + "</td></tr></table>" + businessvalue1;
                if (f.leg == "Left")
                {
                    LinkButton3.Text = "New <br/> Member";
                    LinkButton3.Enabled = true;
                    LinkButton3.PostBackUrl = "../register.aspx?user=" + fst.username + "&n=" + fst.name + "&leg=Right&g=" + fst.gender + "";

                    LinkButton4.Text = "New <br/> Member";
                    LinkButton4.Enabled = true;
                    LinkButton4.PostBackUrl = "../register.aspx?user=" + f.username + "&n=" + f.name + "&leg=Left&g=" + f.gender + "";

                    LinkButton5.Text = "New <br/> Member";
                    LinkButton5.Enabled = true;
                    LinkButton5.PostBackUrl = "../register.aspx?user=" + f.username + "&n=" + f.name + "&leg=Right&g=" + f.gender + "";

                    chain1.InnerHtml = "<a href='genealogy.aspx?uname=" + Helper.Encrypt(f.username) + "'>" + f.username + "</a><br/>" + f.name + " <div class='tooltiptext'>" + title + "</div>";

                    bind(f.plan, Image2, f.status);

                    //Image2.InnerHtml = "<i class='fa fa-user' aria-hidden='true'  style='font-size:58px; color:green;     '></i>";

                }
                if (f.leg == "Right")
                {
                    LinkButton2.Text = "New <br/> Member";
                    LinkButton2.Enabled = true;
                    LinkButton2.PostBackUrl = "../register.aspx?user=" + fst.username + "&n=" + fst.name + "&leg=Left&g=" + fst.gender + "";

                    LinkButton6.Text = "New <br/> Member";
                    LinkButton6.Enabled = true;
                    LinkButton6.PostBackUrl = "../register.aspx?user=" + f.username + "&n=" + f.name + "&leg=Left&g=" + f.gender + "";

                    LinkButton7.Text = "New <br/> Member";
                    LinkButton7.Enabled = true;
                    LinkButton7.PostBackUrl = "../register.aspx?user=" + f.username + "&n=" + f.name + "&leg=Right&g=" + f.gender + "";

                    chain2.InnerHtml = "<a href='genealogy.aspx?uname=" + Helper.Encrypt(f.username) + "' >" + f.username + "</a><br/>" + f.name + " <div class='tooltiptext'>" + title + "</div>";

                    bind(f.plan, Image3, f.status);

                    // Image3.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;    '></i>";

                }
                var fst2 = from c in db.tblMembers
                           where c.refid == f.id
                           select c;

                foreach (var f2 in fst2)
                {
                    string businessvalue2 = "<table class=style1 border=1 ><tr><td colspan=3 style=color:Maroon >Business </td> </tr> <tr><td></td><td>Left </td> <td>  Right </td> </tr><tr><td>Count</td><td>" + count(f2.id, "Left", "Silver") + "</td><td>" + count(f2.id, "Right", "Silver") + "</td></tr>"
                        + "<tr><td>Confirm Counts</td><td>" + f2.silverLeft + "</td><td>" + f2.silverRight + "</td></tr><tr><td>Binary Points</td><td>" + (f2.tlcount - LeftPv(f2.username)) + "</td><td>" + (f2.trcount - RightPv(f2.username)) + "</td></tr><tr><td>Cumulative Bv</td><td>" + cumulativeLeftBv(f2.username)
                        + "</td><td>" + cumulativeRightBv(f2.username) + "</td></tr> <tr><td>Self BV</td><td colspan=2>" + selfBv(f2.username) + "</td></tr> </table>";
                    string title2 = "<table class=style2> <tr><td> Sponsored By</td><td>: " + f.sid + "</td></tr><tr><td>Join Date</td> <td>: " + f2.joindate.Value.ToString("dd/MMM/yyyy") + "</td></tr><tr><td>Activation Date</td> <td>: " + (f2.renewal.HasValue ? f2.renewal.Value.ToString("dd/MMM/yyyy") : "") + "</td></tr>"
                       + "<tr><td>Package</td> <td>: " + f2.plan + "</td></tr><tr><td>Club Qualification</td> <td>: " + getRank(f2.rankid) + "</td></tr></table>" + businessvalue2;
                    if (f.leg == "Left")
                    {
                        if (f2.leg == "Left")
                        {
                            LinkButton5.Text = "New <br/> Member";
                            LinkButton5.Enabled = true;
                            LinkButton5.PostBackUrl = "../register.aspx?user=" + f.username + "&n=" + f.name + "&leg=Right&g=" + f.gender + "";

                            //LinkButton8.Text = "New <br/> Member";
                            //LinkButton8.Enabled = true;
                            //LinkButton8.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Left&g=" + f2.gender + "";

                            //LinkButton9.Text = "New <br/> Member";
                            //LinkButton9.Enabled = true;
                            //LinkButton9.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Right&g=" + f2.gender + "";

                            chain3.InnerHtml = "<a href='genealogy.aspx?uname=" + Helper.Encrypt(f2.username) + "'>" + f2.username + "</a><br/>" + f2.name + " <div class='tooltiptext'>" + title2 + "</div>";

                            bind(f2.plan, Image4, f2.status);

                            // Image4.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;    '></i>";

                        }
                        if (f2.leg == "Right")
                        {
                            LinkButton4.Text = "New <br/> Member";
                            LinkButton4.Enabled = true;
                            LinkButton4.PostBackUrl = "../register.aspx?user=" + f.username + "&n=" + f.name + "&leg=Left&g=" + f.gender + "";

                            //LinkButton10.Text = "New <br/> Member";
                            //LinkButton10.Enabled = true;
                            //LinkButton10.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Left&g=" + f2.gender + "";

                            //LinkButton11.Text = "New <br/> Member";
                            //LinkButton11.Enabled = true;
                            //LinkButton11.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Right&g=" + f2.gender + "";

                            chain4.InnerHtml = "<a href='genealogy.aspx?uname=" + Helper.Encrypt(f2.username) + "'>" + f2.username + "</a><br/>" + f2.name + " <div class='tooltiptext'>" + title2 + "</div>";


                            bind(f2.plan, Image5, f2.status);

                            // Image5.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;     '></i>";

                        }

                    }

                    if (f.leg == "Right")
                    {
                        if (f2.leg == "Left")
                        {
                            LinkButton7.Text = "New <br/> Member";
                            LinkButton7.Enabled = true;
                            LinkButton7.PostBackUrl = "../register.aspx?user=" + f.username + "&n=" + f.name + "&leg=Right&g=" + f.gender + "";

                            //LinkButton12.Text = "New <br/> Member";
                            //LinkButton12.Enabled = true;
                            //LinkButton12.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Left&g=" + f2.gender + "";

                            //LinkButton13.Text = "New <br/> Member";
                            //LinkButton13.Enabled = true;
                            //LinkButton13.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Right&g=" + f2.gender + "";

                            chain5.InnerHtml = "<a href='genealogy.aspx?uname=" + Helper.Encrypt(f2.username) + "' >" + f2.username + "</a><br/>" + f2.name + " <div class='tooltiptext'>" + title2 + "</div>";

                            bind(f2.plan, Image6, f2.status);

                            //Image6.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;     '></i>";

                        }
                        if (f2.leg == "Right")
                        {
                            LinkButton6.Text = "New <br/> Member";
                            LinkButton6.Enabled = true;
                            LinkButton6.PostBackUrl = "../register.aspx?user=" + f.username + "&n=" + f.name + "&leg=Left&g=" + f.gender + "";

                            chain6.InnerHtml = "<a href='genealogy.aspx?uname=" + Helper.Encrypt(f2.username) + "'>" + f2.username + "</a><br/>" + f2.name + " <div class='tooltiptext'>" + title2 + "</div>";

                            bind(f2.plan, Image7, f2.status);

                            // Image7.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;   '></i>";

                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("../login.aspx");
        }
    }

    protected int count(int id, string leg, string plan)
    {
        string query1 = string.Format(@"WITH t AS(SELECT id, refid, name,username,joindate,[status],isActive FROM tblMember WHERE refid={0} AND leg='{1}'
          UNION ALL
          SELECT c.id, c.refid, c.name,c.username,c.joindate,c.[status],c.isActive FROM t p JOIN tblMember c ON c.refid = p.id )
           SELECT * FROM t where t.isActive=1 OPTION (MAXRECURSION 0)", id, leg, plan);

        string conn = ConfigurationManager.ConnectionStrings["LotusBizConnectionString"].ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = conn;
        SqlCommand cmd = new SqlCommand(query1, con);
        //create the DataAdapter & DataSet
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds.Tables[0].Rows.Count;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {

            string uname = Session["username"].ToString();
            var get = (from c in db.tblMembers
                       where c.username == uname
                       select c).SingleOrDefault();


            string query1 = string.Format(@"WITH t AS(SELECT id, refid, name,username,leg,joindate,[plan] FROM tblMember WHERE refid={0}
          UNION ALL
          SELECT c.id, c.refid, c.name,c.username,c.leg,c.joindate,c.[plan] FROM t p JOIN tblMember c ON c.refid = p.id)
SELECT * FROM t OPTION (MAXRECURSION 0)", get.id);

            string conn = ConfigurationManager.ConnectionStrings["LotusBizConnectionString"].ToString();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = conn;
            SqlCommand cmd = new SqlCommand(query1, con);
            //create the DataAdapter & DataSet
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow ro = dt.NewRow();
            ro["username"] = uname;
            dt.Rows.Add(ro);

            bool exists = dt.Select().ToList().Exists(row => row["username"].ToString() == search.Value);

            if (exists)
            {
                Label10.Visible = false;
                Response.Redirect("genealogy.aspx?uname=" + search.Value);

            }
            else
            {
                Label10.Text = "Username not found";
                Label10.Visible = true;
            }
        }

        catch
        {

        }


    }
}