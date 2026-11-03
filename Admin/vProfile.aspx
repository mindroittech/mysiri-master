<%@ Page Title=" Profile" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="vProfile.aspx.cs" Inherits="Admin_vProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">
            Edit Profile
        </h1>
    </div>
    <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
        border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
        <div class="col-md-12">
            <div class="container1">
                <div class="x_content">
                    <asp:Panel ID="Panel1" runat="server" ForeColor="#009933" Visible="False">
                        Your profile has been successfully updated.</asp:Panel>
                    <p class="card-description" style="font-size: 15px; color: #ff9c00; font-weight: 600;">
                        Personal info
                    </p>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Franchisee Name</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtCompany" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Phone No*</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtPhone" runat="server" required CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    State*</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList ID="drpState" runat="server" CssClass="form-control">
                                        <asp:ListItem>Select State</asp:ListItem>
                                        <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                                        <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                        <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                        <asp:ListItem>Assam</asp:ListItem>
                                        <asp:ListItem>Bihar</asp:ListItem>
                                        <asp:ListItem>Chandigadh</asp:ListItem>
                                        <asp:ListItem>Chattisgadh</asp:ListItem>
                                        <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                                        <asp:ListItem>Daman and Diu</asp:ListItem>
                                        <asp:ListItem>Delhi</asp:ListItem>
                                        <asp:ListItem>Goa</asp:ListItem>
                                        <asp:ListItem>Gujarat</asp:ListItem>
                                        <asp:ListItem>Haryana</asp:ListItem>
                                        <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                        <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                                        <asp:ListItem>Jharkhand</asp:ListItem>
                                        <asp:ListItem>Karnataka</asp:ListItem>
                                        <asp:ListItem>Kerala</asp:ListItem>
                                        <asp:ListItem>Lakshadweep</asp:ListItem>
                                        <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                        <asp:ListItem>Maharashtra</asp:ListItem>
                                        <asp:ListItem>Manipur</asp:ListItem>
                                        <asp:ListItem>Meghalaya</asp:ListItem>
                                        <asp:ListItem>Mizoram</asp:ListItem>
                                        <asp:ListItem>Nagaland</asp:ListItem>
                                        <asp:ListItem>Puducherry</asp:ListItem>
                                        <asp:ListItem>Punjab</asp:ListItem>
                                        <asp:ListItem>Rajasthan</asp:ListItem>
                                        <asp:ListItem>Sikkim</asp:ListItem>
                                        <asp:ListItem>Tamil Nadu</asp:ListItem>
                                        <asp:ListItem>Tripura</asp:ListItem>
                                        <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                        <asp:ListItem>Uttarakhand</asp:ListItem>
                                        <asp:ListItem>West Bengal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Email</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    City*</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Address*</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Pincode</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtPincode" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    <asp:Label ID="lblUnique" runat="server" Text="PAN"></asp:Label>
                                    Number</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtPAN" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <p class="card-description" style="font-size: 15px; color: #ff9c00; font-weight: 600;">
                        Nominee Details
                    </p>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Nominee Name</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtNominee" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Nominee Age</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtAge" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Relationship with Nominee</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtRelation" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <p class="card-description" style="font-size: 15px; color: #ff9c00; font-weight: 600;">
                        Bank Details
                    </p>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    IFSC</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtIfsc" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Acc Number</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtAccount" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Bank Name</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtBank" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Branch</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtBranch" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <center>
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary mr-2" OnClick="Button1_Click"
                            Text="Update" />
                    </center>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
