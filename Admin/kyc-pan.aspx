<%@ Page Title="KYC Details" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="kyc-pan.aspx.cs" Inherits="Admin_kyc_pan" %>

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
                    Verify KYC
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Green" Text="KYC Approved"
                        Visible="False"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                            Width="100%" EmptyDataText="No new records for approval" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" SortExpression="username" />
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="pan" HeaderText="PAN" SortExpression="name" />
                                <asp:BoundField DataField="account" HeaderText="Acc No" />
                                <asp:BoundField DataField="ifsc" HeaderText="IFSC" />
                                <asp:BoundField DataField="UsedDate" HeaderText="Submitted On" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                <asp:HyperLinkField Text="View" DataNavigateUrlFields="UploadPan" Target="_blank"
                                    DataNavigateUrlFormatString="{0}" HeaderText="View PAN" />
                                <asp:HyperLinkField Text="View" DataNavigateUrlFields="UploadBank" Target="_blank"
                                    DataNavigateUrlFormatString="{0}" HeaderText="View Passbook" />
                                <asp:TemplateField ItemStyle-Width="180px">
                                    <HeaderTemplate>
                                        Action</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("username") %>'
                                            CommandName="edit" Text="Approve"></asp:LinkButton>
                                        &emsp;
                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red" CommandArgument='<%# Eval("username") %>'
                                            CommandName="reject" Text="Reject"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
