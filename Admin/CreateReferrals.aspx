<%@ Page Title="Refer And Earn" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="CreateReferrals.aspx.cs" Inherits="Admin_ReferAndEarn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Add Referrals
                </p>
            </div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <div class="container1">
                        <div class="row">
                            <div class="col-lg-9">
                                <div class="form-row">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group col-md-12">
                                                <label for="inputState">
                                                    Sponsored By</label>
                                                <input type="text" class="form-control" id="txtReferredBy" runat="server">
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="inputState">
                                                    Franchisee Name</label>
                                                <input type="text" class="form-control" id="txtCompanay" runat="server" required>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Phone Number</label>
                                                <input type="text" class="form-control" id="txtPhone" maxlength="10" runat="server"
                                                    required>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Amount Deposited</label>
                                                <asp:TextBox required ID="txtAmount" runat="server"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtAmount" />
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Date of Deposite</label>
                                                <asp:TextBox ID="txtDate" TextMode="Date" CssClass="form-control" required runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Bank Deposited</label>
                                                <asp:TextBox ID="txtBank" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    UTR Number</label>
                                                <asp:TextBox ID="txtUTR" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Deposite Slip</label>
                                                <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Franchisee Type
                                                </label>
                                                <asp:DropDownList ID="drpRole" CssClass="form-control" runat="server" required>
                                                </asp:DropDownList>
                                            </div>
                                             <div class="form-group col-md-12">
                                                <label for="remarks">
                                                   State
                                                </label>
                                                <asp:DropDownList ID="drpstates" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpstates_SelectedIndexChanged" runat="server" required>
                                                </asp:DropDownList>
                                                 </div>
                                             <div class="form-group col-md-12">
                                                <label for="remarks">
                                                   District
                                                </label>
                                                <asp:DropDownList ID="drpdist" CssClass="form-control" runat="server" OnSelectedIndexChanged="drpdist_SelectedIndexChanged" AutoPostBack="true" required>
                                                </asp:DropDownList>
                                            </div>

                                            <%--<div class="form-group col-md-12" id="divState" runat="server">
                                                <label for="remarks">
                                                    State
                                                </label>
                                                <select id="drpState" class="form-control" runat="server" required>
                                                    <option selected="selected" value="">Select State</option>
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
                                            </div>--%>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    City
                                                </label>
                                                <asp:TextBox ID="txtCity" CssClass="form-control" runat="server" required></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Address
                                                </label>
                                                <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    ZIP/ Pincode
                                                </label>
                                                <asp:TextBox ID="txtPincode" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtPincode" />
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    GST Number
                                                </label>
                                                <asp:TextBox ID="txtGSTNo" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Reporting To
                                                </label>
                                                <asp:DropDownList ID="drpReporter" runat="server" CssClass="form-control" required>
                                                </asp:DropDownList>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="form-group col-md-12">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                            Text="Submit" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
