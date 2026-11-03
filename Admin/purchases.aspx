<%@ Page Title="Purchases" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="purchases.aspx.cs" Inherits="Admin_purchases" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">
            Purchases
        </h1>
    </div>
    <div class="row" style="margin-top: 20px;">
        <div class="col-md-12">
            <span id="lbErrormsg" runat="server" visible="false" class="text-danger"></span>
            <div class="table-responsive">
                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td>
                            Start Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtStart" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            End Date
                        </td>
                        <td>
                            <asp:TextBox ID="txtEnd" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            Product
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" Width="150px" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"
                                OnClick="Button1_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="Button3_Click" />
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Export" CssClass="btn btn-primary"
                                OnClick="Button2_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="row1" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
        border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
        <div class="col-md-12">
            <div class="row">
                <div class="col-4">
                    <span style="font-weight: bold">Total Purchases(Rs): </span>
                    <asp:Label ID="lblTotalAmt" runat="server" Text="0"></asp:Label>
                </div>
                <div class="col-3">
                    <span style="font-weight: bold">Total Quantity: </span>
                    <asp:Label ID="lblTotalQty" runat="server" Text="0"></asp:Label>
                </div>
                <div class="col-2">
                </div>
                <div class="col-3">
                    <p style="text-align: right;">
                        <a href="add-purchases.aspx" class="btn btn-primary">Add New </a>
                    </p>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                    EmptyDataText="No purchase found" OnRowCommand="GridView1_RowCommand" AllowPaging="true"
                    PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                SNo.
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PurchaseDate" HeaderText="Date" ReadOnly="True" SortExpression="username"
                            DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="InviceNo" HeaderText="Invoice No" SortExpression="name" />
                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier" SortExpression="plan" />
                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="joindate" />
                        <%--         <asp:BoundField DataField="ProductCode" HeaderText="Product Code" SortExpression="joindate" />
                        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="joindate" />
                        <asp:BoundField DataField="HSNCode" HeaderText="HSN Code" SortExpression="joindate" />--%>
                        <asp:BoundField DataField="GST" HeaderText="GST%" SortExpression="joindate" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty(No)" SortExpression="joindate" />
                        <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="joindate" DataFormatString="{0:0}" />
                        <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="joindate" DataFormatString="{0:0}" />
                        <asp:BoundField DataField="GSTAmt" HeaderText="GST Amt" SortExpression="joindate"
                            DataFormatString="{0:0}" />
                        <asp:BoundField DataField="NetTotal" HeaderText="Net Total" SortExpression="joindate"
                            DataFormatString="{0:0}" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Delete
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("PurchaseId") %>'
                                    CommandName="delete" Height="20px" ImageUrl="img/delete-icon.png" Width="20px" />
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" ConfirmOnFormSubmit="true"
                                    runat="server" ConfirmText="Are you sure want to delete?" Enabled="True" TargetControlID="ImageButton2">
                                </asp:ConfirmButtonExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
