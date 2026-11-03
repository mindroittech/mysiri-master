using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Admin_add_product_master : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string role = Session["role"].ToString();

            if (!IsPostBack)
            {
                CheckBox3.Checked = true;

                if (Request.QueryString["Id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["Id"]);
                    var get = db.tblProducts.Where(x => x.ProductId == id).SingleOrDefault();
                    txtProduct.Value = get.ProductName;
                    txtDescription.Value = get.Description;
                    txthsncode.Text = get.HSNCode;

                    txtMRP.Text = get.MRP.ToString();
                    txtDP.Text = get.DP.ToString();
                    txtPV.Text = get.PV.ToString();

                    txtGST.Text = get.GST.Value.ToString();


                    CheckBox1.Checked = get.FreeEligible.Value;
                    CheckBox2.Checked = get.AwardEligible.Value;
                    CheckBox3.Checked = get.ActiveToFanchisee.Value;

                    if (get.ImageUrl != null)
                    {
                        ImageUrl.ImageUrl = get.ImageUrl;
                    }

                    Button1.Text = "Update";
                }
            }
        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Submit")
        {
            var product = new tblProduct
            {
                ProductName = txtProduct.Value,
                Description = txtDescription.Value,
                GST = Convert.ToDecimal(txtGST.Text),
                IsActive = true,

                MRP = Convert.ToDecimal(txtMRP.Text),
                DP = Convert.ToDecimal(txtDP.Text),
                PV = Convert.ToDecimal(txtPV.Text),

                FreeEligible = CheckBox1.Checked,
                AwardEligible = CheckBox2.Checked,
                ActiveToFanchisee=CheckBox3.Checked,
                HSNCode=txthsncode.Text
                
            };
           
            if (FileUpload1.HasFile)
            {
                string fileName = DateTime.UtcNow.ToString("ddMMyyyyHHmmss") + "_" + FileUpload1.FileName;
                string path = Server.MapPath("~/Files/Product/" + fileName);
                FileUpload1.SaveAs(path);
                product.ImageUrl = "~/Files/Product/" + fileName;
            }
            db.tblProducts.InsertOnSubmit(product);
            db.SubmitChanges();
        }
        else if (Button1.Text == "Update")
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);
            var get = db.tblProducts.Where(x => x.ProductId == id).SingleOrDefault();

            get.ProductName = txtProduct.Value;
            get.Description = txtDescription.Value;
            get.GST = Convert.ToDecimal(txtGST.Text);
           
            get.MRP = Convert.ToDecimal(txtMRP.Text);
            get.DP = Convert.ToDecimal(txtDP.Text);
            get.PV = Convert.ToDecimal(txtPV.Text);
            get.HSNCode = txthsncode.Text;
            get.FreeEligible = CheckBox1.Checked;
            get.AwardEligible = CheckBox2.Checked;
            get.ActiveToFanchisee = CheckBox3.Checked;

            if (FileUpload1.HasFile)
            {
                string fileName = DateTime.UtcNow.ToString("ddMMyyyyHHmm") + "_" + FileUpload1.FileName;
                string path = Server.MapPath("~/Files/Product/" + fileName);
                FileUpload1.SaveAs(path);
                get.ImageUrl = "~/Files/Product/" + fileName;
            }
         
            db.SubmitChanges();
        }
        Response.Redirect("product-master.aspx");
    }
}