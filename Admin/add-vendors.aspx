<%@ Page Title="Add Franchisee" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="add-vendors.aspx.cs" Inherits="Admin_add_vendors" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Add/Edit Franchisee
                </p>
            </div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Text=""></asp:Label>
                    <div class="container1">
                        <div class="row">
                            <div class="col-lg-5">
                                <div class="form-row">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Franchisee Type
                                                </label>
                                                <asp:DropDownList ID="drpRole" CssClass="form-control" runat="server" OnSelectedIndexChanged="drpRole_SelectedIndexChanged" AutoPostBack="true" required>
                                                </asp:DropDownList>
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
                                                   State
                                                </label>
                                                <asp:DropDownList ID="drpstates" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="drpstates_SelectedIndexChanged" runat="server" required>
                                                </asp:DropDownList>
                                                    
                                               
                                                
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                   District
                                                </label>
                                                <asp:DropDownList ID="drpdist" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpdist_SelectedIndexChanged" required>
                                                </asp:DropDownList>
                                            </div>
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
                                                    Sponsored By
                                                </label>
                                                <asp:TextBox ID="txtReferedBy" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Reporting To
                                                </label>
                                                <asp:DropDownList ID="drpReporter" runat="server" CssClass="form-control" required>
                                                </asp:DropDownList>
                                            </div>
                                            <div id="divUpdate" runat="server" visible="false">
                                                <div class="form-group col-md-12">
                                                    <label for="remarks">
                                                        Username
                                                    </label>
                                                    <asp:TextBox ID="txtUser" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label for="remarks">
                                                        Password
                                                    </label>
                                                    <asp:TextBox ID="txtPass" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="col-lg-5">
                                <div class="form-row">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Email
                                                </label>
                                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="inputState">
                                                    Nominee Name</label>
                                                <asp:TextBox ID="txtNominee" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Nominee Age</label>
                                                <asp:TextBox ID="txtAge" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtAge" />
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Relationship with Nominee</label>
                                                <asp:TextBox ID="txtRelation" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    IFSC
                                                </label>
                                                <asp:TextBox ID="txtIfsc" CssClass="form-control" OnTextChanged="txtIfsc_changed"
                                                    AutoPostBack="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Acc Number
                                                </label>
                                                <asp:TextBox ID="txtAccount" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Bank Name
                                                </label>
                                                <asp:TextBox ID="txtBank" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Branch
                                                </label>
                                                <asp:TextBox ID="txtBranch" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Pan Number
                                                </label>
                                                <asp:TextBox ID="txtPAN" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Upload PAN
                                                </label>
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                <asp:HyperLink ID="HyperLink1" Visible="false" runat="server" Target="_blank">View</asp:HyperLink>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Upload Bank Passbook
                                                </label>
                                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                                <asp:HyperLink ID="HyperLink2" Visible="false" runat="server" Target="_blank">View</asp:HyperLink>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">
                                                    Active
                                                </label>
                                                <asp:CheckBox ID="chkActive" runat="server" />
                                            </div>

                                             <%-- <div class="form-group col-md-12">
                                                  <label for="remarks">
                                                      Allow Salary
                                                  </label>
                                                  <asp:CheckBox ID="chsalary" runat="server" />
                                              </div>--%>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="form-group col-md-12">
                                        <br />
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                            Text="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
