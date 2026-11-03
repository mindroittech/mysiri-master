<%@ Page Title="Change Password" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="change-pwd.aspx.cs" Inherits="admin_change_pwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Change Password
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 15px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ValidationGroup="val"
                                        TextMode="Password" placeholder="Old Password*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                        ErrorMessage="Field can not be empty" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" placeholder="New Password*"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2"
                                        ErrorMessage="Field can not be empty" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" CssClass="form-control"
                                        placeholder="Retype Password*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox3"
                                        ErrorMessage="Field can not be empty" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox2"
                                        ControlToValidate="TextBox3" ErrorMessage="Password does not match" ForeColor="Red"
                                        ValidationGroup="val"></asp:CompareValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group row">
                                <div class="col-sm-12">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" OnClick="Button1_Click"
                                        Text="Submit" ValidationGroup="val" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
