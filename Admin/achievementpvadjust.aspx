<%@ Page Title="Achievement Points Adjustment" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="achievementpvadjust.aspx.cs" Inherits="Admin_Default2" %>

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
                    Achievement PV Adjustment
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Username not found" Visible="False"></asp:Label>
                    <asp:Label ID="Label2" runat="server" ForeColor="#FF3300" Text="" Visible="False"></asp:Label>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Member ID</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TextBox1" required runat="server" AutoPostBack="false" OnTextChanged="txtUser_Changed"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="Panel1" Visible="false" runat="server">
                                <div class="row">
                                    <div class="col-md-7">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Member Name:
                                            </label>
                                            <div class="col-sm-4">
                                                <asp:Label ID="txtName" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">
                                                 Left Achievement PV:</label>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblLeft" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-4 col-form-label">
                                                Right Achievement PV:</label>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblRight" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Additional PV:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtBV" required CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Placement
                                            </label>
                                            <div class="col-sm-4" style="padding-top: 10px">
                                                <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                                                    <asp:ListItem>Left</asp:ListItem>
                                                    <asp:ListItem>Right</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadioButtonList1"
                                                    ForeColor="Red" Display="Dynamic" Font-Size="Small" ErrorMessage="Select Placement"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-7">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                </label>
                                <div class="col-sm-5">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-info" OnClick="Button1_Click" />
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
