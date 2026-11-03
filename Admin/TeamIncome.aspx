<%@ Page Title="Team Income" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="TeamIncome.aspx.cs" Inherits="Admin_TeamIncome" %>

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
                    Team Income
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table align="center" border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    Username
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="200px" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <span style="font-weight: bold">Total Pending :
                            <asp:Label ID="Label1" ForeColor="Red" runat="server" Text="0"></asp:Label></span>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                            Width="100%" EmptyDataText="No records found">
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
                                <asp:BoundField DataField="leg" HeaderText="Position" SortExpression="plan" />
                                <asp:BoundField DataField="level" HeaderText="Level" ItemStyle-Font-Bold="true" SortExpression="plan" />
                                <asp:BoundField DataField="earnedBinary" HeaderText="Earned Binary Income" SortExpression="plan" />
                                <asp:BoundField DataField="earnedRepurchase" HeaderText="Earned Repurchase Income"
                                    SortExpression="plan" />
                                <asp:BoundField DataField="binary" HeaderText="Pending Binary Income" SortExpression="plan" />
                                <asp:BoundField DataField="repurchase" HeaderText="Pending Repurchase Income" SortExpression="plan" />
                                <asp:BoundField DataField="income" HeaderText="Pending Income" SortExpression="plan" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
