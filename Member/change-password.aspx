<%@ Page Title="Change Password" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="change-password.aspx.cs" Inherits="change_password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Change Password
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
                <div class="col-md-10 form-horizontal">
                    <div class="form-group">
                        <div class="col-sm-3">
                            <span id="" style="font-weight: bold;">Enter Old Password</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ValidationGroup="val"
                                TextMode="Password" placeholder="Old Password*"></asp:TextBox>
                        </div>
                        <div class="col-sm-3" style="color: red;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="Field can not be empty" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3">
                            <span id="" textmode="Password" style="font-weight: bold;">Enter New Password</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" placeholder="New Password*"
                                CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-sm-3" style="color: red;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2"
                                ErrorMessage="Field can not be empty" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3">
                            <span id="" style="font-weight: bold;">Re-enter New Password</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" CssClass="form-control"
                                placeholder="Retype Password*"></asp:TextBox>
                        </div>
                        <div class="col-sm-3" style="color: red;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox3"
                                ErrorMessage="Field can not be empty" ForeColor="Red" ValidationGroup="val"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox2"
                                ControlToValidate="TextBox3" ErrorMessage="Password does not match" ForeColor="Red"
                                ValidationGroup="val"></asp:CompareValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-3">
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                Text="Submit" ValidationGroup="val" />
                        </div>
                        <div class="col-sm-3" style="color: red;">
                        </div>
                    </div>
                    <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                </div>
            </div>
            <!-- card-body -->
        </div>
        <!-- card -->
    </div>
</asp:Content>
