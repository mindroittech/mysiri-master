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

        if (status == "Silver")
        {
            img.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:#21ef21;     '></i>";
        }
        else if (status == "Gold")
        {
            img.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:#FFA500;     '></i>";
        }
        else if (status == "Diamond")
        {
            img.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:#BC8F8F;     '></i>";
        }


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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            if (Request.QueryString["uname"] != null)
            {
                uname = Request.QueryString["uname"];
            }
            var fst = (from c in db.tblMembers
                       where c.username == uname
                       select c).SingleOrDefault();

            string businessvalue = "<table class=style1 border=1 ><tr><td colspan=3 style=color:Maroon >Business</td>  <tr><td></td><td>Left </td> <td>  Right </td> </tr><tr><td>Silver</td><td>" + count(fst.id, "Left", "Silver") + "</td><td>" + count(fst.id, "Right", "Silver") + "</td></tr><tr><td>Gold</td><td>" + count(fst.id, "Left", "Gold") + "</td><td>" + count(fst.id, "Right", "Gold") + "</td></tr><tr><td>Diamond</td><td>" + count(fst.id, "Left", "Diamond") + "</td><td>" + count(fst.id, "RIght", "Diamond") + "</td></tr>   </table>";
            string tit = "<table class=style2> <tr><td> Sponsored By</td><td>: " + GetUserName(fst.refid) + "</td></tr><tr><td>Join Date</td> <td>: " + fst.joindate.Value.ToString("dd/MMM/yyyy") + "</td></tr><tr><td>Current Plan</td><td>: " + fst.status + "</td></tr></table>" + businessvalue;
            chain.InnerHtml = "<a  href='genealogy.aspx?uname=" + fst.username + "'>" + fst.username + "</a><br/>" + fst.name + " <span class='tooltiptext'>" + tit + "</span>";


            bind(fst.plan, Image1, fst.status);


            var fst1 = from c in db.tblMembers
                       where c.refid == fst.id
                       select c;
            if (fst1.Count() == 0)
            {
                LinkButton3.Text = "New <br/> Member";
                LinkButton3.Enabled = true;
                LinkButton3.PostBackUrl = "new-member.aspx?user=" + fst.username + "&n=" + fst.name + "&leg=Right&g=" + fst.gender + "";

                LinkButton2.Text = "New <br/> Member";
                LinkButton2.Enabled = true;
                LinkButton2.PostBackUrl = "new-member.aspx?user=" + fst.username + "&n=" + fst.name + "&leg=Left&g=" + fst.gender + "";
            }

            foreach (var f in fst1)
            {
                //count(f.id, "Left")
                string businessvalue1 = "<table class=style1 border=1 ><tr><td colspan=3 style=color:Maroon >Business</td>  <tr><td></td><td>Left </td> <td>  Right </td> </tr><tr><td>Silver</td><td>" + count(f.id, "Left", "Silver") + "</td><td>" + count(f.id, "Right", "Silver") + "</td></tr> <tr><td>Gold</td><td>" + count(f.id, "Left", "Gold") + "</td><td>" + count(f.id, "Right", "Gold") + "</td></tr><tr><td>Diamond</td><td>" + count(f.id, "Left", "Diamond") + "</td><td>" + count(f.id, "Right", "Diamond") + "</td></tr>  </table>";
                string title = "<table class=style2> <tr><td> Sponsored By</td><td>: " + fst.username + "</td></tr><tr><td>Join Date</td> <td>: " + f.joindate.Value.ToString("dd/MMM/yyyy") + "</td></tr><tr><td>Current Plan </td><td>: " + f.status + "</td></tr></table>" + businessvalue1;
                if (f.leg == "Left")
                {
                    LinkButton3.Text = "New <br/> Member";
                    LinkButton3.Enabled = true;
                    LinkButton3.PostBackUrl = "new-member.aspx?user=" + fst.username + "&n=" + fst.name + "&leg=Right&g=" + fst.gender + "";

                    LinkButton4.Text = "New <br/> Member";
                    LinkButton4.Enabled = true;
                    LinkButton4.PostBackUrl = "new-member.aspx?user=" + f.username + "&n=" + f.name + "&leg=Left&g=" + f.gender + "";

                    LinkButton5.Text = "New <br/> Member";
                    LinkButton5.Enabled = true;
                    LinkButton5.PostBackUrl = "new-member.aspx?user=" + f.username + "&n=" + f.name + "&leg=Right&g=" + f.gender + "";

                    chain1.InnerHtml = "<a href='genealogy.aspx?uname=" + f.username + "'>" + f.username + "</a><br/>" + f.name + " <div class='tooltiptext'>" + title + "</div>";

                    bind(f.plan, Image2, f.status);

                    //Image2.InnerHtml = "<i class='fa fa-user' aria-hidden='true'  style='font-size:58px; color:green;     '></i>";

                }
                if (f.leg == "Right")
                {
                    LinkButton2.Text = "New <br/> Member";
                    LinkButton2.Enabled = true;
                    LinkButton2.PostBackUrl = "new-member.aspx?user=" + fst.username + "&n=" + fst.name + "&leg=Left&g=" + fst.gender + "";

                    LinkButton6.Text = "New <br/> Member";
                    LinkButton6.Enabled = true;
                    LinkButton6.PostBackUrl = "new-member.aspx?user=" + f.username + "&n=" + f.name + "&leg=Left&g=" + f.gender + "";

                    LinkButton7.Text = "New <br/> Member";
                    LinkButton7.Enabled = true;
                    LinkButton7.PostBackUrl = "new-member.aspx?user=" + f.username + "&n=" + f.name + "&leg=Right&g=" + f.gender + "";

                    chain2.InnerHtml = "<a href='genealogy.aspx?uname=" + f.username + "' >" + f.username + "</a><br/>" + f.name + " <div class='tooltiptext'>" + title + "</div>";

                    bind(f.plan, Image3, f.status);

                    // Image3.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;    '></i>";

                }
                var fst2 = from c in db.tblMembers
                           where c.refid == f.id
                           select c;

                foreach (var f2 in fst2)
                {
                    string businessvalue2 = "<table class=style1 border=1 ><tr><td colspan=3 style=color:Maroon >Business </td>  <tr><td> </td><td>Left </td> <td>  Right </td> </tr><tr><td>Silver</td><td>" + count(f2.id, "Left", "Silver") + "</td><td>" + count(f2.id, "Right", "Silver") + "</td></tr> <tr><td>Gold</td><td>" + count(f2.id, "Left", "Gold") + "</td><td>" + count(f2.id, "Right", "Gold") + "</td></tr><tr><td>Diamond</td><td>" + count(f2.id, "Left", "Diamond") + "</td><td>" + count(f2.id, "Right", "Diamond") + "</td></tr>  </table>";
                    string title2 = "<table class=style2> <tr><td> Sponsored By</td><td>: " + f.username + "</td></tr><tr><td>Join Date</td> <td>: " + f2.joindate.Value.ToString("dd/MMM/yyyy") + "</td></tr><tr><td>Current Plan</td><td>: " + f2.status + "</td></tr></table>" + businessvalue2;
                    if (f.leg == "Left")
                    {
                        if (f2.leg == "Left")
                        {
                            LinkButton5.Text = "New <br/> Member";
                            LinkButton5.Enabled = true;
                            LinkButton5.PostBackUrl = "new-member.aspx?user=" + f.username + "&n=" + f.name + "&leg=Right&g=" + f.gender + "";

                            //LinkButton8.Text = "New <br/> Member";
                            //LinkButton8.Enabled = true;
                            //LinkButton8.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Left&g=" + f2.gender + "";

                            //LinkButton9.Text = "New <br/> Member";
                            //LinkButton9.Enabled = true;
                            //LinkButton9.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Right&g=" + f2.gender + "";

                            chain3.InnerHtml = "<a href='genealogy.aspx?uname=" + f2.username + "'>" + f2.username + "</a><br/>" + f2.name + " <div class='tooltiptext'>" + title2 + "</div>";

                            bind(f2.plan, Image4, f2.status);

                            // Image4.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;    '></i>";

                        }
                        if (f2.leg == "Right")
                        {
                            LinkButton4.Text = "New <br/> Member";
                            LinkButton4.Enabled = true;
                            LinkButton4.PostBackUrl = "new-member.aspx?user=" + f.username + "&n=" + f.name + "&leg=Left&g=" + f.gender + "";

                            //LinkButton10.Text = "New <br/> Member";
                            //LinkButton10.Enabled = true;
                            //LinkButton10.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Left&g=" + f2.gender + "";

                            //LinkButton11.Text = "New <br/> Member";
                            //LinkButton11.Enabled = true;
                            //LinkButton11.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Right&g=" + f2.gender + "";

                            chain4.InnerHtml = "<a href='genealogy.aspx?uname=" + f2.username + "'>" + f2.username + "</a><br/>" + f2.name + " <div class='tooltiptext'>" + title2 + "</div>";


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
                            LinkButton7.PostBackUrl = "new-member.aspx?user=" + f.username + "&n=" + f.name + "&leg=Right&g=" + f.gender + "";

                            //LinkButton12.Text = "New <br/> Member";
                            //LinkButton12.Enabled = true;
                            //LinkButton12.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Left&g=" + f2.gender + "";

                            //LinkButton13.Text = "New <br/> Member";
                            //LinkButton13.Enabled = true;
                            //LinkButton13.PostBackUrl = "new-member.aspx?user=" + f2.username + "&n=" + f2.name + "&leg=Right&g=" + f2.gender + "";

                            chain5.InnerHtml = "<a href='genealogy.aspx?uname=" + f2.username + "' >" + f2.username + "</a><br/>" + f2.name + " <div class='tooltiptext'>" + title2 + "</div>";

                            bind(f2.plan, Image6, f2.status);

                            //Image6.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;     '></i>";

                        }
                        if (f2.leg == "Right")
                        {
                            LinkButton6.Text = "New <br/> Member";
                            LinkButton6.Enabled = true;
                            LinkButton6.PostBackUrl = "new-member.aspx?user=" + f.username + "&n=" + f.name + "&leg=Left&g=" + f.gender + "";



                            chain6.InnerHtml = "<a href='genealogy.aspx?uname=" + f2.username + "'>" + f2.username + "</a><br/>" + f2.name + " <div class='tooltiptext'>" + title2 + "</div>";

                            bind(f2.plan, Image7, f2.status);

                            // Image7.InnerHtml = "<i class='fa fa-user' aria-hidden='true' style='font-size:58px; color:green;   '></i>";

                        }
                    }

                }

            }



        }
        catch
        {
            Response.Redirect("../login.aspx");
        }
    }

    protected int count(int id, string leg, string plan)
    {
        string query1 = string.Format(@"WITH t AS(SELECT id, refid, name,username,joindate,[status] FROM tblMember WHERE refid={0} AND leg='{1}'
          UNION ALL
          SELECT c.id, c.refid, c.name,c.username,c.joindate,c.[status] FROM t p JOIN tblMember c ON c.refid = p.id)
SELECT * FROM t where t.[status]='{2}' OPTION (MAXRECURSION 0)", id, leg, plan);

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