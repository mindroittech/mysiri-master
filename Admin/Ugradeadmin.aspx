<%@ Page Title="manual Ugreadation" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="Ugradeadmin.aspx.cs" Inherits="Admin_Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Manual Activate ID Binary / PV 
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" Text="" Font-Bold="true"
                        Visible="False"></asp:Label>
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            User ID</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtMemberId" AutoPostBack="true" OnTextChanged="txtusername_Changed"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMemberId"
                                                ErrorMessage="Enter Member Id" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Name</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtName" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Plan</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="drpPlan" AutoPostBack="true"
                                                runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpPlan"
                                                ErrorMessage="Select Plan" ForeColor="Red" InitialValue="0" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-7">
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">
                                </label>
                                <div class="col-sm-5">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary mr-2" Text="Activate"
                                        OnClick="Button1_Click" ValidationGroup="sub" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
              
            </div>
        </div>
    </div>
</asp:Content>
