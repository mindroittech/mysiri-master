<%@ Page Title="Payout History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="vPayoutHistory.aspx.cs" Inherits="Admin_vPayoutHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Payout History
                </p>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <table>
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td style="width: 220px">
                                    <asp:TextBox ID="TextBox1" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row1" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                            Width="100%" EmptyDataText="No Withdrawals found" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
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
                                <asp:BoundField DataField="date" HeaderText="Date Req" SortExpression="date" DataFormatString="{0:dd-MMM-yyyy}"
                                    ItemStyle-Width="140px">
                                    <ItemStyle Width="140px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="paiddate" HeaderText="Paid Date" DataFormatString="{0:dd-MMM-yyyy}"
                                    ItemStyle-Width="120px" SortExpression="paiddate">
                                    <ItemStyle Width="120px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                <asp:BoundField DataField="tax" HeaderText="TDS(5%)" />
                                <asp:BoundField DataField="sc" HeaderText="A.C(5%)" />
                                <asp:BoundField DataField="netpay" HeaderText="NetPay" />
                                <asp:BoundField DataField="transaction" HeaderText="Transaction No" />
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
