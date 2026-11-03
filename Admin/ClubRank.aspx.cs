using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ClubRank : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["admin"].ToString();
            if (!IsPostBack)
                bindcurrent();
        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClubRank.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime? d1 = null, d2 = null;
        string search = null;
        if (TextBox1.Text != string.Empty && TextBox2.Text != string.Empty)
        {
            d1 = Convert.ToDateTime(TextBox1.Text);
            d2 = Convert.ToDateTime(TextBox2.Text);
        }
        if (TextBox3.Text != string.Empty)
        {
            search = TextBox3.Text;

        }
        bind(d1, d2, search);
    }

    private void bind(DateTime? d1 = null, DateTime? d2 = null, string search = null)
    {
        MakeFieldVisible("dateAchieved");
        MakeFieldVisible("RankName");

        var data = from c in db.tblRankAchieveds
                   join d in db.tblMembers
                   on c.username equals d.username
                   join f in db.tblRepurchaseRanks
                   on c.rankId equals f.RankId
                   join k in db.tblRepurchaseRanks
                   on d.rankid equals k.RankId
                   where c.dateAchieved.Value.Date >= new DateTime(2023, 02, 01)
                   && c.rankType == "New"
                   select new
                   {
                       c.rid,
                       c.username,
                       c.rankId,
                       d.name,
                       f.RankName,
                       c.dateAchieved,
                       f.Reward,
                       c.rewardStatus,
                       c.rewardDate,
                       d.mobile,
                       d.state,
                       CurrentRank = k.RankName
                   };

        
        if (search != null)
        {
            data = data.Where(x => x.username == search || x.RankName == search);
        }
        if (d1 != null && d2 != null)
        {
            data = data.Where(x => x.dateAchieved.Value.Date >= d1 && x.dateAchieved.Value.Date <= d2);
        }
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
    private void MakeFieldVisible(string dataField)
    {
        BoundField field = GridView1.Columns.OfType<BoundField>().FirstOrDefault(f => f.DataField == dataField);
        if (field != null)
        {
            field.Visible = true;
        }
    }
    private void bindcurrent()
    {
        var data = from c in db.tblRankAchieveds
                   join d in db.tblMembers
                   on c.username equals d.username
                   join f in db.tblRepurchaseRanks
                   on c.rankId equals f.RankId
                   join k in db.tblRepurchaseRanks
                   on d.rankid equals k.RankId
                   where c.dateAchieved.Value.Date >= new DateTime(2023, 02, 01)
                   && c.rankType == "New"
                   select new
                   {
                       c.rid,
                       c.username,
                       c.rankId,
                       d.name,
                       f.RankName,
                       c.dateAchieved,
                       f.Reward,
                       c.rewardStatus,
                       c.rewardDate,
                       d.mobile,
                       d.state,
                       CurrentRank = k.RankName
                   };

        var latestRecords = data.GroupBy(x => x.username)
                              .Select(group => group.OrderByDescending(x => x.dateAchieved).First());

        GridView1.DataSource = latestRecords.ToList();
        GridView1.DataBind();
    }

    protected void btnEXPORT_Click(object sender, EventArgs e)
    {
        bindcurrent();
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Rank_Achievers.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        }
        GridView1.RenderControl(htw);

        GridView1.AllowPaging = true;
        GridView1.DataBind();
        Response.Write(sw.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.
    }
}