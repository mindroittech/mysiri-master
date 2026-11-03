using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;

public partial class admin_product_history : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();
            if (!IsPostBack)
            {
                bindProducts();
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bindProducts(TextBox3.Text);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("product-history.aspx");
    }

    protected void bindProducts(string uname = null)
    {
        var members = from c in db.RequestProducts
                      join d in db.tblMembers
                      on c.username equals d.username
                      join f in db.PinManagaers
                         on c.pin_number equals f.pinnumbers into grp
                      from g in grp.DefaultIfEmpty()
                      where c.status == "Send"
                      let k = db.tblMembers.SingleOrDefault(x => x.username == g.usedby)
                      select new
                      {
                          c.username,
                          d.name,
                          plan = c.name,
                          c.product,
                          c.shipment,
                          c.cost,
                          c.senddate,
                          c.pin_number,
                          g.usedby,
                          usedbyname = k != null ? g.usedby + " " + k.name : "",
                      };

        var franch = from c in db.RequestProducts
                     join d in db.tblVendors
                     on c.username equals d.Username
                     join f in db.PinManagaers
                        on c.pin_number equals f.pinnumbers into grp
                     from g in grp.DefaultIfEmpty()
                     where c.status == "Send"
                     let k = db.tblMembers.SingleOrDefault(x => x.username == g.usedby)
                     select new
                     {
                         c.username,
                         name = d.VendorName,
                         plan = c.name,
                         c.product,
                         c.shipment,
                         c.cost,
                         c.senddate,
                         c.pin_number,
                         g.usedby,
                         usedbyname = k != null ? g.usedby + " " + k.name : "",
                     };
        var res = members.Concat(franch);
        if (uname != null)
            res = res.Where(x => x.username == uname || x.usedby == uname);
        GridView1.DataSource = res.OrderByDescending(x => x.senddate);
        GridView1.DataBind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        bindProducts();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        Export();
    }

    protected void Export()
    {
        bindProducts(); // Ensure GridView1 is bound with data

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Dispatch_History.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        //Change the Header Row back to white color
        GridView1.HeaderRow.Style.Remove("background-color");
        //Applying stlye to gridview header cells
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#0000FF");
        }
        foreach (GridViewRow row in GridView1.Rows)
        {
           // row.BackColor = Color.White;
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
        GridView1.RenderControl(htw);

        GridView1.AllowPaging = true;
        GridView1.DataBind();
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        //Allows for printing
    }

    public override bool EnableEventValidation
    {
        get { return false; }
        set { /*Do nothing*/ }
    }
   
}