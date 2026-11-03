<%@ Page Title="KYC Details" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="kyc-detail.aspx.cs" Inherits="Admin_kyc_detail" %>

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
                    KYC Details
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-5">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">
                                    Username</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="TextBox1" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">
                                    Name</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtName" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">
                                    PAN Number</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtPAN" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">
                                </label>
                                <div class="col-sm-8">
                                    <asp:Button ID="Button1" runat="server" Text="Approve" CssClass="btn btn-info" OnClick="Button1_Click" />
                                    &emsp;
                                    <asp:Button ID="Button2" runat="server" Text="Reject" CssClass="btn btn-info" OnClick="Button2_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-7">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Image ID="Image1" Height="320px" Width="250px" runat="server" />
                        </div>
                        <div class="col-md-6">
                            <asp:Image ID="Image2" Height="320px" Width="250px" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
