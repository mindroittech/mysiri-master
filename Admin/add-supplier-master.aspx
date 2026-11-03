<%@ Page Title="Add Supplier Master" Language="C#" MasterPageFile="admin.master"
    AutoEventWireup="true" CodeFile="add-supplier-master.aspx.cs" Inherits="Admin_add_supplier_master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Add Suppliers
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <div class="container1">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-row">
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            Supplier Name</label>
                                        <input type="text" class="form-control" id="txtName" runat="server" required />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            Address 1</label>
                                        <input type="text" class="form-control" id="txtAddress1" runat="server" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            Address 2</label>
                                        <input type="text" class="form-control" id="txtAddress2" runat="server" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            City</label>
                                        <input type="text" class="form-control" id="txtCity" runat="server" required />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="inputState">
                                            State</label>
                                        <select id="inputState" class="form-control" runat="server" required>
                                            <option value="">Select State</option>
                                            <option value="Andaman and Nicobar Islands">Andaman and Nicobar Islands</option>
                                            <option value="Andhra Pradesh">Andhra Pradesh</option>
                                            <option value="Arunachal Pradesh">Arunachal Pradesh</option>
                                            <option value="Assam">Assam</option>
                                            <option value="Bihar">Bihar</option>
                                            <option value="Chandigarh">Chandigarh</option>
                                            <option value="Chhattisgarh">Chhattisgarh</option>
                                            <option value="Dadra and Nagar Haveli">Dadra and Nagar Haveli</option>
                                            <option value="Daman and Diu">Daman and Diu</option>
                                            <option value="Delhi">Delhi</option>
                                            <option value="Goa">Goa</option>
                                            <option value="Gujarat">Gujarat</option>
                                            <option value="Haryana">Haryana</option>
                                            <option value="Himachal Pradesh">Himachal Pradesh</option>
                                            <option value="Jammu and Kashmir">Jammu and Kashmir</option>
                                            <option value="Jharkhand">Jharkhand</option>
                                            <option value="Karnataka">Karnataka</option>
                                            <option value="Kerala">Kerala</option>
                                            <option value="Lakshadweep">Lakshadweep</option>
                                            <option value="Madhya Pradesh">Madhya Pradesh</option>
                                            <option value="Maharashtra">Maharashtra</option>
                                            <option value="Manipur">Manipur</option>
                                            <option value="Meghalaya">Meghalaya</option>
                                            <option value="Mizoram">Mizoram</option>
                                            <option value="Nagaland">Nagaland</option>
                                            <option value="Odisha">Odisha</option>
                                            <option value="Puducherry">Puducherry</option>
                                            <option value="Punjab">Punjab</option>
                                            <option value="Rajasthan">Rajasthan</option>
                                            <option value="Sikkim">Sikkim</option>
                                            <option value="Tamil Nadu">Tamil Nadu</option>
                                            <option value="Telengana">Telengana</option>
                                            <option value="Tripura">Tripura</option>
                                            <option value="Uttar Pradesh">Uttar Pradesh</option>
                                            <option value="Uttarakhand">Uttarakhand</option>
                                            <option value="West Bengal">West Bengal</option>
                                        </select>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            PIN Code</label>
                                        <input type="text" class="form-control" runat="server" id="txtPincode" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            Contact No</label>
                                        <input type="text" class="form-control" runat="server" id="txtContact" required />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            Contact No 2</label>
                                        <input type="text" class="form-control" runat="server" id="txtContact2" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            Email</label>
                                        <input type="text" class="form-control" runat="server" id="txtEmail" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            GST No</label>
                                        <input type="text" class="form-control" id="txtGst" runat="server" required />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                            Text="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
