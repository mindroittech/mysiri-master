using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class portal_upload_pan : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();

            if (!IsPostBack)
            {
                var bind = from c in db.tblMembers
                           where c.isActive == 1 && c.username == uname
                           && c.UploadPan != null
                           select new
                           {
                               c.UploadPan,
                               c.UploadBank,
                               c.pan,
                               IsApproved = c.isPanVerifed.HasValue ? c.isPanVerifed.Value ? "<span style='color:Green'>Approved<span>" : "<span style='color:Red'>Rejected</span>" : "<span style='color:Black'>Pending</span>"
                           };
                GridView1.DataSource = bind;
                GridView1.DataBind();

                if (bind.Any() && bind.First().IsApproved == "Rejected")
                    divKyc.Visible = false;

                if (Request.QueryString["v"] == "sub")
                {
                    Label1.Visible = true;
                    Label1.Text = "KYC details successfully updated";
                }
            }


        }
        catch (Exception ex)
        {
            Response.Redirect("../login.aspx");
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            string uname = Session["username"].ToString();


            if (!(FileUpload1.HasFile && FileUpload2.HasFile))
            {
                Label1.Visible = false;
                Label2.Visible = true;
                Label2.Text = "Submit both files for KYC";
                return;
            }

            var get = (from c in db.tblMembers
                       where c.username == uname
                       select c).SingleOrDefault();
            if (FileUpload1.HasFile)
            {
                string fileName = uname + "_PAN_" + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/Files/" + fileName));
                string link = "~/Files/" + fileName;
                get.UploadPan = link;
            }
            if (FileUpload2.HasFile)
            {
                string bankPass = uname + "_Pass_" + FileUpload1.FileName;
                FileUpload2.SaveAs(Server.MapPath("~/Files/" + bankPass));
                string passbooklink = "~/Files/" + bankPass;
                get.UploadBank = passbooklink;

            }
            get.UsedDate = DateTime.UtcNow.AddHours(5.5);
            if (get.isPanVerifed.HasValue)
            {
                get.isPanVerifed = null;
            }
            db.SubmitChanges();

            Response.Redirect("kyc.aspx?v=sub");
        }
        catch
        {

        }

    }
}