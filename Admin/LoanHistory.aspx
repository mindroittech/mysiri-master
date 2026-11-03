<%@ Page Title="Loan History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="LoanHistory.aspx.cs" Inherits="Admin_LoanHistory" %>

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
                    Individual Loan History
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
                                <td>
                                    <a id="A1" runat="server" href="addLoan.aspx" class="btn btn-primary">ADD NEW </a>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                            Width="100%" EmptyDataText="No records found" PageSize="50" AllowPaging="true"
                            OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging">
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
                                <asp:BoundField DataField="loanAmount" HeaderText="Loan Amount" SortExpression="plan" />
                                <asp:BoundField DataField="recoveredAmount" HeaderText="Recovered Amount" SortExpression="plan" />
                                <asp:BoundField DataField="balance" HeaderText="Balance" SortExpression="plan" />
                           <%--     <asp:TemplateField>
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("username") %>'
                                            CommandName="moreloan" Text="More Loans" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
