using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_add_supplier_master : System.Web.UI.Page
{
    DataClassesDataContext db = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string admin = Session["admin"].ToString();



        }
        catch
        {
            Response.Redirect("../vendorlogin.aspx");
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        var supplier = new tblSupplier
        {
            SupplierName = txtName.Value,
            Address1 = txtAddress1.Value,
            Address2 = txtAddress2.Value,
            City = txtCity.Value,
            State = inputState.Value,
            IsActive = true,
            PinCode = txtPincode.Value,
            ContactNo = txtContact.Value,
            Email = txtEmail.Value,
            GSTNumber = txtGst.Value,
             ContactNo2=txtContact2.Value

        };

        db.tblSuppliers.InsertOnSubmit(supplier);
        db.SubmitChanges();

        Response.Redirect("supplier-master.aspx");
    }
}