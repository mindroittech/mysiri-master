using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class id : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string uname = Session["username"].ToString();

            if (!IsPostBack)
            {
                var mem = db.tblMembers.SingleOrDefault(x => x.username == uname);
                lblName.Text = mem.name;
                lblID.Text = mem.username;
                lblJoining.Text = mem.joindate.Value.ToString("dd/MM/yyyy");
                lblMobile.Text = mem.mobile;
                if (mem.UploadId != null)
                    profilePic.Src = mem.UploadId;
                if (mem.rankid != null)
                {
                    var rank = db.tblRepurchaseRanks.SingleOrDefault(x => x.RankId == mem.rankid);
                    lblDesignation.Text = rank.RankName;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("../login.aspx");
        }
    }
}