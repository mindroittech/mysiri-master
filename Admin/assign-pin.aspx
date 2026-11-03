<%@ Page Title="Assign PIN" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="assign-pin.aspx.cs" Inherits="Admin_assign_pin" %>

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
                    Assign PIN
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="ID not found" Visible="False"></asp:Label>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Username</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TextBox1" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Name</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtName" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Amount Deposited</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TextBox2" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Pay Mode</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TextBox3" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            UTR</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TextBox4" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Attachment</label>
                                        <div class="col-sm-5">
                                            <asp:Image ID="Image1" runat="server" Width="120px" />
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="max-height: 600px; overflow: auto">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                        ShowFooter="true" PageSize="50" Width="60%" DataKeyNames="PlanId" EmptyDataText="No Plans">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    SN
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PlanName" HeaderText="Plan" SortExpression="description" />
                                            <asp:BoundField DataField="PlanAmount" HeaderText="Cost" SortExpression="description" />
                                            <asp:TemplateField HeaderText="No Of Pins">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNos" runat="server" AutoPostBack="true" OnTextChanged="txtQuanity_Changed"
                                                        Text='<%# Eval("QtyReq") %>'></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtNos" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalQty" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Total Cost
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:0}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row" runat="server">
                        <div class="col-md-6">
                        <br />
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Enter OTP
                                </label>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-5">
                                    <asp:LinkButton ID="LinkButton1" ForeColor="Blue" runat="server" OnClick="GenerateOTP_Click">Click Here</asp:LinkButton>
                                    to generate OTP
                                    <br />
                                    <asp:Label ID="lblOtpMessage" ForeColor="DarkGreen" Font-Size="12px" runat="server"
                                        Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                </label>
                                <div class="col-sm-5">
                                    <asp:Button ID="Button1" runat="server" Text="Generate PIN" CssClass="btn btn-info"
                                        OnClick="Button1_Click" ValidationGroup="otp" />
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
