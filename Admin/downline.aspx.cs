using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class downline : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();

            string uname = "";
            if (Request.QueryString["uname"] != null)
            {
                uname = Request.QueryString["uname"];
            }

            if (!IsPostBack)
            {

                var get = (from c in db.tblMembers
                           where c.username == uname
                           select c).SingleOrDefault();

                if (Request.QueryString["leg"] != null)
                {
                    leafnode(get.id, (int)get.level, Request.QueryString["leg"]);
                }
                else
                {

                    leafnode(get.id, (int)get.level);
                }

                //int LefCount = count(get.id, "Left");
                //int rightCount = count(get.id, "Right");

                Label8.Text = (GridView1.Rows.Count).ToString();
                if (GridView1.Rows.Count == 0)
                    Button1.Visible = false;

                //Label10.Text = LefCount.ToString();
                //Label11.Text = rightCount.ToString();
            }

        }

        catch (Exception ex)
        {
             Response.Redirect("../vendorlogin.aspx");
        }

    }

    protected int count(int id, string leg)
    {
        string query1 = string.Format(@"WITH t AS(SELECT id, refid, name,username,joindate FROM tblMember WHERE refid={0} AND leg='{1}'
          UNION ALL
          SELECT c.id, c.refid, c.name,c.username,c.joindate FROM t p JOIN tblMember c ON c.refid = p.id)
SELECT * FROM t OPTION (MAXRECURSION 0)", id, leg);

        string conn = ConfigurationManager.ConnectionStrings["LotusBizConnectionString"].ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = conn;
        SqlCommand cmd = new SqlCommand(query1, con);
        cmd.CommandTimeout = 120;
        //create the DataAdapter & DataSet
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        return ds.Tables[0].Rows.Count;
    }

    public void leafnode(int id, int level)
    {
        string query1 = string.Format(@"WITH t AS(SELECT id, refid,(level-{1}) as level,[status], name,username,leg,joindate,renewal,[plan],pinnumber,mobile FROM tblMember WHERE refid={0}
          UNION ALL
          SELECT c.id, c.refid,(c.level-{1}) as level,c.[status], c.name,c.username,p.leg,c.joindate,c.renewal,c.[plan],c.pinnumber,c.mobile FROM t p JOIN tblMember c ON c.refid = p.id)
        SELECT t.* , Case when p.IsFreePin=1 then 'Free' else 'Paid' end as pin FROM t 
        Inner Join PinManagaer p on t.pinnumber=p.pinnumbers order by joindate desc OPTION (MAXRECURSION 0)", id, level);

        string conn = ConfigurationManager.ConnectionStrings["LotusBizConnectionString"].ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = conn;
        SqlCommand cmd = new SqlCommand(query1, con);
         cmd.CommandTimeout = 120;
        //create the DataAdapter & DataSet
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        //con.Close();
        Session["dtDownline"] = ds.Tables[0];

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

    }

    protected void leafnode(int id, int level, string leg)
    {
        string query1 = string.Format(@"WITH t AS(SELECT id, refid,(level-{1}) as level,[status], name,username,leg,joindate,renewal,[plan],pinnumber,mobile FROM tblMember WHERE refid={0} AND leg='{2}'
          UNION ALL
          SELECT c.id, c.refid,(c.level-{1}) as level,c.[status], c.name,c.username,p.leg,c.joindate,c.renewal,c.[plan],c.pinnumber,c.mobile FROM t p JOIN tblMember c ON c.refid = p.id)
            SELECT t.*, Case when p.IsFreePin=1 then 'Free' else 'Paid' end as pin FROM t 
            Inner Join PinManagaer p on t.pinnumber=p.pinnumbers order by joindate desc OPTION (MAXRECURSION 0)", id, level, leg);

        string conn = ConfigurationManager.ConnectionStrings["LotusBizConnectionString"].ToString();
        SqlConnection con = new SqlConnection();
        con.ConnectionString = conn;
        SqlCommand cmd = new SqlCommand(query1, con);
        //create the DataAdapter & DataSet
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);

        Session["dtDownline"] = ds.Tables[0];

        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Export();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void Export()
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Downline.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        GridView1.Width = 300;
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        }
        GridView1.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Downline.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    //protected void Button3_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = (DataTable)Session["dtDownline"];


    //    if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
    //    {
    //        DateTime from = Convert.ToDateTime(TextBox1.Text);
    //        DateTime to = Convert.ToDateTime(TextBox2.Text);

    //        dt.DefaultView.RowFilter = "joindate>='"+from+"' AND joindate<='"+to+"'";

    //        Session["dtDownline"] = dt.DefaultView.ToTable();

    //        GridView1.DataSource = dt.DefaultView.ToTable();
    //        GridView1.DataBind();

    //    }

    //}
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("downline.aspx");
    }
}