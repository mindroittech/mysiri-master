using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default3 : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms send = new sendsms();
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
                var roles = from c in db.tblRoles
                            where c.IsActive == true
                            select c;
                drpRole.DataSource = roles;
                drpRole.DataTextField = "RoleName";
                drpRole.DataValueField = "RoleId";
                drpRole.DataBind();
                drpRole.Items.Insert(0, new ListItem("All", ""));
                bind();
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    private void bind(string username = "", string vendor = "")
    {
        var get = from c in db.tblVendors
                  join d in db.tblRoles
                  on c.RoleId equals d.RoleId into grp
                  from f in grp.DefaultIfEmpty()
                  where c.IsActive == false && c.Id > 1
                  let rep = db.tblVendors.SingleOrDefault(x => x.Id == c.ReportingId)
                  select new
                  {
                      c.CompanyName,
                      c.CompanyCode,
                      c.VendorName,
                      c.Username,
                      c.Pincode,
                      c.Country,
                      c.State,
                      c.Email,
                      c.Phone,
                      c.City,
                      c.CreatedDate,
                      f.RoleName,
                      f.RoleId,
                      c.Id,
                      c.CashWallet,
                      Reporting = rep != null ? rep.VendorName : null
                  };

        if (username != "")
        {
            get = get.Where(x => x.Username == username || x.VendorName.Contains(username));
        }
        if (vendor != "")
        {
            get = get.Where(x => x.RoleId.ToString() == vendor);
        }

        GridView1.DataSource = get.OrderByDescending(x => x.CreatedDate);
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bind(txtID.Text, drpRole.SelectedValue);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("fblocked.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            CheckBox chk = row.FindControl("CheckBox1") as CheckBox;

            if (chk.Checked)
            {
                string uname = row.Cells[2].Text;

                var up = (from c in db.tblVendors
                          where c.Username == uname
                          select c).SingleOrDefault();
                up.IsActive = true;
                db.SubmitChanges();
            }

        }

        Response.Redirect("fblocked.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "delete")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            var get = db.tblVendors.Where(x => x.Id == id).SingleOrDefault();

            get.IsActive = false;
            db.SubmitChanges();

            Response.Redirect("vendors.aspx");
        }
        else if (e.CommandName == "edit")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            Response.Redirect("add-vendors.aspx?Id=" + id);
        }

    }
}