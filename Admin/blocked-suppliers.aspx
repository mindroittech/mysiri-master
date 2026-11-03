<%@ Page Title="Blocked Suppliers" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="blocked-suppliers.aspx.cs" Inherits="Admin_blocked_suppliers" %>

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
                    Blocked Supplier List
                </p>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SupplierId"
                            Width="100%" EmptyDataText="No records found" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True"
                                    SortExpression="username" />
                                <asp:BoundField DataField="Address1" HeaderText="Address1" SortExpression="name" />
                                <asp:BoundField DataField="Address2" HeaderText="Address2" SortExpression="plan" />
                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="joindate" />
                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="joindate" />
                                <asp:BoundField DataField="PinCode" HeaderText="PIN Code" SortExpression="joindate" />
                                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" SortExpression="joindate" />
                                <asp:BoundField DataField="ContactNo2" HeaderText="Contact No2" SortExpression="joindate" />
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="joindate" />
                                <asp:BoundField DataField="GSTNumber" HeaderText="GST" SortExpression="joindate" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("SupplierId") %>'
                                            CommandName="delete" Text="Unblock" ToolTip="UnBlock" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure want to unblock?" Enabled="True" TargetControlID="ImageButton2">
                                        </asp:ConfirmButtonExtender>
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
