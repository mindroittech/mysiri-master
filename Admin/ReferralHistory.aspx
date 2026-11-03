<%@ Page Title="Referrals History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="ReferralHistory.aspx.cs" Inherits="Admin_ReferralHistory" %>

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
                    Referrals History
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="130%"
                            EmptyDataText="No referrals found" AllowPaging="false" PageSize="50">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Username" HeaderText="Referred By" SortExpression="name" />
                                <asp:BoundField DataField="VendorName" HeaderText="Franchisee Name" ReadOnly="True"
                                    SortExpression="username" />
                                <asp:BoundField DataField="RoleName" HeaderText="Franchisee Type" SortExpression="name" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="name"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="name" />
                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="plan" />
                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="plan" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount Deposited" SortExpression="plan" />
                                <asp:BoundField DataField="depositeDate" HeaderText="Deposite Date" SortExpression="name"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="bank" HeaderText="Bank Deposited" SortExpression="name" />
                                <asp:BoundField DataField="UTR" HeaderText="UTR" SortExpression="plan" />
                                <asp:HyperLinkField Text="View" DataNavigateUrlFields="slip" DataNavigateUrlFormatString="{0}"
                                    HeaderText="Deposite Slip" Target="_blank" />
                                <asp:BoundField DataField="status" ItemStyle-Font-Bold="true" HeaderText="Status"
                                    SortExpression="plan" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
