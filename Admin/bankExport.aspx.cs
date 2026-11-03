using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

public partial class Admin_bankExport : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["t"] == "Member")
        {
            bind(Request.QueryString["p"]);
        }
        else if (Request.QueryString["t"] == "Franchisee")
        {
            franchiseePayout();
        }
        ExportToExcel(Request.QueryString["t"]);
    }

    protected void bind(string pay)
    {

        var getpayout = from c in db.PaymentRequests
                        join d in db.tblMembers
                        on c.username equals d.username
                        where c.status == "Pending"
                        && c.comments == pay && (d.isPanVerifed == true && d.account != string.Empty)
                        orderby c.date descending
                        select new
                        {
                            c.paymentid,
                            c.username,
                            d.name,
                            idformat = c.username,
                            netpay = c.netpay,
                            date = c.date.Value.Day + "/" + c.date.Value.Month + "/" + c.date.Value.Year,
                            d.account,
                            d.bankname,
                            ifsc = d.ifsc.ToUpper(),
                            paymentMethod = d.bankname.Contains("HDFC") ? "I" : "N",
                            email = (d.email == "" || d.email == null) ? "dlsbiotech01@gmail.com" : d.email
                        };

        GridView1.DataSource = getpayout;
        GridView1.DataBind();
    }


    protected void franchiseePayout()
    {

        var getpayout = from c in db.PaymentRequests
                        join d in db.tblVendors
                        on c.username equals d.Username
                        where c.status == "Pending" && d.IsActive == true
                        orderby c.date descending
                        select new
                        {
                            c.username,
                            name = d.VendorName,
                            netpay = c.netpay,
                            idformat = c.username + ", " + d.VendorName,
                            date = c.date.Value.Day + "/" + c.date.Value.Month + "/" + c.date.Value.Year,
                            d.account,
                            d.bankname,
                            ifsc = d.ifsc.ToUpper(),
                            paymentMethod = d.bankname.Contains("HDFC") ? "I" : "N",
                            email = (d.Email == "" || d.Email == null) ? "dlsbiotech01@gmail.com" : d.Email
                        };


        GridView1.DataSource = getpayout;
        GridView1.DataBind();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void ExportToExcel(string name)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}_Payout_BankFormat.xls", name));
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            GridView1.AllowPaging = false;
            GridView1.DataBind();

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
                    if (row.Cells[2] == cell)
                        cell.CssClass = "text";
                    else
                        cell.CssClass = "textmode";
                }
            }

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } .text{mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}