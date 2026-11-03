<%@ Page Title="Closed Loans" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="closedLoans.aspx.cs" Inherits="Admin_closedLoans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Closed Loans
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
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                            Width="100%" EmptyDataText="No records found" PageSize="50" AllowPaging="true"
                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand">
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
                                <asp:BoundField DataField="date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" HeaderText="Loan Date"
                                    SortExpression="paiddate"></asp:BoundField>
                                <asp:BoundField DataField="loanAmount" HeaderText="Loan Amount" SortExpression="plan" />
                                <asp:BoundField DataField="recoveredAmount" HeaderText="Recovered Amount" SortExpression="plan" />
                                <asp:BoundField DataField="balance" HeaderText="Balance (Rs)" SortExpression="plan" />
                                <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-ForeColor="Red"
                                    SortExpression="plan" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("loanId") %>'
                                            CommandName="recovery" Text="Recovery History" />

                                            |
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("loanId") %>'
                                            CommandName="add" Text="Add More" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
