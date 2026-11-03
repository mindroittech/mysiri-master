using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_vKyc : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string vendor = Session["vendor"].ToString();

            if (!IsPostBack)
            {
                var bind = from c in db.tblVendors
                           where c.IsActive == true && c.Username == vendor
                            && c.UploadPAN != null
                           select new
                           {
                               c.UploadPAN,
                               c.UploadBank,
                               c.PAN,
                               IsApproved = c.isKyc.HasValue ? c.isKyc.Value ? "<span style='color:Green'>Approved<span>" : "<span style='color:Red'>Rejected</span>" : "<span style='color:Black'>Pending</span>"
                           };
                GridView1.DataSource = bind;
                GridView1.DataBind();

                if (Request.QueryString["v"] == "sub")
                {
                    Label1.Visible = true;
                    Label1.Text = "KYC details successfully updated";
                }
            }


        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

            string vendor = Session["vendor"].ToString();


            if (!(FileUpload1.HasFile && FileUpload2.HasFile))
            {
                Label1.Visible = false;
                Label2.Visible = true;
                Label2.Text = "Submit both files for KYC";
                return;
            }

            var get = (from c in db.tblVendors
                       where c.Username == vendor
                       select c).SingleOrDefault();
            if (FileUpload1.HasFile)
            {
                string fileName = vendor + "_PAN_" + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath("~/Files/" + fileName));
                string link = "~/Files/" + fileName;
                get.UploadPAN = link;
            }
            if (FileUpload2.HasFile)
            {
                string bankPass = vendor + "_Pass_" + FileUpload1.FileName;
                FileUpload2.SaveAs(Server.MapPath("~/Files/" + bankPass));
                string passbooklink = "~/Files/" + bankPass;
                get.UploadBank = passbooklink;
            }
            get.UsedDate = DateTime.UtcNow.AddHours(5.5);
            if (get.isKyc.HasValue)
            {
                get.isKyc = null;
            }
            db.SubmitChanges();

            Response.Redirect("vKyc.aspx?v=sub");
        }
        catch
        {

        }

    }
}