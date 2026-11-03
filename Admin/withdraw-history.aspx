<%@ Page Title="Withdraw History" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="withdraw-history.aspx.cs" Inherits="admin_withdraw_history" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Withdraw History
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    &nbsp;<br />
                    &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="" Visible="False"></asp:Label>
                    <div class="table-responsive">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">ID</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUser" Width="140px" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Clear" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="Button3" Style="float: right;" runat="server" Text="Export to Excel"
                            CssClass="btn btn-info" OnClick="Button3_Click" Visible="false" /><br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PagerStyle-CssClass="PagerStyle"
                            EmptyDataText="No records found.." PageSize="50" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="comments" HeaderText="Type" SortExpression="status" />
                                <asp:BoundField DataField="transaction" HeaderText="Transaction No" />
                                <asp:BoundField DataField="username" HeaderText="Username" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                <asp:BoundField DataField="mobile" HeaderText="Mobile" />
                                <asp:BoundField DataField="date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Req Date"
                                    SortExpression="date">
                                    <ItemStyle Width="115px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="paiddate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Paid Date"
                                    SortExpression="paiddate">
                                    <ItemStyle Width="115px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="RankName" HeaderText="Club Rank" SortExpression="cashavailable" />
                                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                <asp:BoundField DataField="tax" HeaderText="TDS(2%)" />
                                <asp:BoundField DataField="sc" HeaderText="A.C(8%)" />
                                <asp:BoundField DataField="deduction" HeaderText="Deduction" />
                                <asp:BoundField DataField="netpay" HeaderText="Net Pay" />
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
