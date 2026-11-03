using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;

public partial class admin_product_requests : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    sendsms sm = new sendsms();
    bool isclicked = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (!IsPostBack)
            {
                bind();
            }

            if (Request.QueryString["p"] == "sub")
            {
                Label3.Visible = true;
            }

        }
        catch (Exception ex)
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        isclicked = true;
        bind(TextBox3.Text);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("product-requests.aspx");
    }
    private string getUsedByMember(string uname)
    {
        var m = db.tblMembers.AsEnumerable().SingleOrDefault(x => x.username == uname);
        return m.username + "- " + m.name;
    }
    private void bind(string uname = null)
    {
        var member = from c in db.RequestProducts
                     join d in db.tblMembers
                     on c.username equals d.username
                     join f in db.PinManagaers
                     on c.pin_number equals f.pinnumbers into grp
                     from g in grp.DefaultIfEmpty().AsEnumerable()
                     where c.status == "Pending"
                     let k = db.tblMembers.SingleOrDefault(x => x.username == g.usedby)
                     select new
                     {
                         c.username,
                         d.name,
                         plan = c.name,
                         c.pin_number,
                         c.pid,
                         c.pinReqId,
                         g.usedby,
                         usedbyname = k != null ? g.usedby + " " + k.name : "",
                         joindate = g.date
                     };
        var franchisee = from c in db.RequestProducts
                         join d in db.tblVendors
                         on c.username equals d.Username
                         join f in db.PinManagaers
                         on c.pin_number equals f.pinnumbers into grp
                         from g in grp.DefaultIfEmpty()
                         where c.status == "Pending"
                         let k = db.tblMembers.SingleOrDefault(x => x.username == g.usedby)
                         select new
                         {
                             c.username,
                             name = d.VendorName,
                             plan = c.name,
                             c.pin_number,
                             c.pid,
                             c.pinReqId,
                             g.usedby,
                             usedbyname = k != null ? g.usedby + " " + k.name : "",
                             joindate = g.date
                         };
        var products = member.Union(franchisee);
        if (uname != null)
            products = products.Where(x => x.username == uname || x.usedby == uname);
        if (isclicked)
            GridView1.AllowPaging = false;
        GridView1.DataSource = products.OrderByDescending(x => x.joindate);
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "send")
        {
            int pid = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            TextBox txtProd = row.FindControl("txtProduct") as TextBox;
            TextBox txtDate = row.FindControl("txtDate") as TextBox;
            TextBox txtShip = row.FindControl("txtReceiver") as TextBox;

            RequestProduct up = db.RequestProducts.SingleOrDefault(x => x.pid == pid);

            up.product = txtProd.Text;
            up.status = "Send";
            if (txtDate.Text.Trim() != string.Empty)
            {
                up.senddate = Convert.ToDateTime(txtDate.Text.Trim());
            }
            else
            {
                up.senddate = DateTime.UtcNow.Date;
            }
            up.shipment = txtShip.Text;
            db.SubmitChanges();

            Response.Redirect("product-requests.aspx?p=sub");
        }

        if (e.CommandName == "dispatch")
        {
            int pid = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            TextBox txtProd = row.FindControl("txtProduct") as TextBox;
            TextBox txtDate = row.FindControl("txtDate") as TextBox;
            TextBox txtShip = row.FindControl("txtReceiver") as TextBox;

            RequestProduct up = db.RequestProducts.SingleOrDefault(x => x.pid == pid);

            up.product = txtProd.Text;
            up.status = "Send";
            if (txtDate.Text.Trim() != string.Empty)
            {
                up.senddate = Convert.ToDateTime(txtDate.Text.Trim());
            }
            else
            {
                up.senddate = DateTime.UtcNow.Date;
            }
            up.shipment = txtShip.Text;
            db.SubmitChanges();

            var get = (from c in db.tblMembers
                       where c.username == up.username
                       select c).SingleOrDefault();

            string sms = "Dear " + row.Cells[2].Text + ", Your product has been dispatched on " + up.senddate.Value.ToString("dd/MM/yyyy") + ". Dispatch details: " + up.shipment + ". " + ConfigurationManager.AppSettings["companyname"];
            sm.sms(get.mobile, sms, "1407169591244200896");

            Response.Redirect("product-requests.aspx?p=sub");
        }

        if (e.CommandName == "rem")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            var up = (from c in db.RequestProducts
                      where c.pid == id
                      select c).SingleOrDefault();
            db.RequestProducts.DeleteOnSubmit(up);
            db.SubmitChanges();
            Response.Redirect("product-requests.aspx");
        }

        if (e.CommandName == "pins")
        {
            int id = Convert.ToInt32(e.CommandArgument);

            var pins = from c in db.PinRequestByPlans
                       join d in db.tblPlans
                       on c.planId equals d.PlanId
                       where c.pId == id
                       && c.QtySent != null
                       select new { c.planId, c.QtyReq, c.QtySent, d.PlanName };

            GridView2.DataSource = pins;
            GridView2.DataBind();
            ImageButton2_ModalPopupExtender.Show();
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bind();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        bind(); // Ensure GridView1 is bound with data

        var products = GridView1.DataSource as IEnumerable<object>;
        if (products != null && products.Any())
        {
            var filteredData = products.Select(p => new
            {
                Username = p.GetType().GetProperty("username").GetValue(p),
                Name = p.GetType().GetProperty("name").GetValue(p),
                Plan = p.GetType().GetProperty("plan").GetValue(p),
                PinNumber = p.GetType().GetProperty("pin_number").GetValue(p),
                Date = p.GetType().GetProperty("joindate").GetValue(p),
                UsedBy = p.GetType().GetProperty("usedby").GetValue(p),             
               
            });

            using (OfficeOpenXml.ExcelPackage excelPackage = new OfficeOpenXml.ExcelPackage())
            {
                OfficeOpenXml.ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Products");

                string[] columnsToExport = { "Username","Name", "Plan", "PinNumber","Date", "UsedBy" };

                // Add header row
                int rowIndex = 1;
                foreach (var columnName in columnsToExport)
                {
                    // Add header cell with text
                    worksheet.Cells[1, rowIndex].Value = columnName;

                    // Apply style to header cell (change color, font, etc. as needed)
                    worksheet.Cells[1, rowIndex].Style.Font.Bold = true;
                    worksheet.Cells[1, rowIndex].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[1, rowIndex].Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#0000FF"));
                    worksheet.Cells[1, rowIndex].Style.Font.Color.SetColor(System.Drawing.Color.White);

                    rowIndex++;
                }

                // Add data rows
                int row = 2;
                foreach (var item in filteredData)
                {
                    int col = 1;
                    foreach (var property in item.GetType().GetProperties())
                    {
                        worksheet.Cells[row, col].Value = property.GetValue(item);
                        col++;
                    }
                    row++;
                }

                // Save the Excel package to response stream
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=Dispatch_Products.xlsx");
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.End();
            }
        }
        else
        {
            Response.Write("No data available for export.");
        }
    }


}