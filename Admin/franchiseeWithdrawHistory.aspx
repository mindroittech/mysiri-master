<%@ Page Title="Franchisee Payout History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="franchiseeWithdrawHistory.aspx.cs" Inherits="Admin_franchiseeWithdrawHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Franchisee Payout History
                </p>
            </div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="" Visible="False"></asp:Label>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table>
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td style="width: 220px">
                                    <asp:TextBox ID="TextBox1" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">ID </b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" Width="160px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            Width="100%" EmptyDataText="No Payout history found" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        No
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="transaction" HeaderText="Transaction No" />
                                <asp:BoundField DataField="username" HeaderText="ID" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                <asp:BoundField DataField="Phone" HeaderText="Mobile" SortExpression="name" />
                                <asp:BoundField DataField="pan" HeaderText="PAN" />
                                <asp:BoundField DataField="date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Req Date"
                                    SortExpression="date">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                <asp:BoundField DataField="paiddate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Action On"
                                    SortExpression="paiddate">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                <asp:BoundField DataField="tax" HeaderText="TDS(2%)" />
                                <asp:BoundField DataField="sc" HeaderText="A.C(8%)" />
                                <asp:BoundField DataField="deduction" HeaderText="Deduction" />
                                <asp:BoundField DataField="netpay" HeaderText="Net Pay" />
                                <asp:BoundField DataField="comments" HeaderText="Remarks" />
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
