<%@ Page Title="Account Transactions" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="vTransactions.aspx.cs" Inherits="Admin_vTransactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Account Transactions
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <table border="0" cellpadding="4" cellspacing="3" class="boxgborder" style="width: 80%">
                        <tr>
                            <td>
                                Start Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" TextMode="Date" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                End Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox2" TextMode="Date" runat="server"></asp:TextBox> &nbsp;&nbsp;
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="btn btn-primary mr-2"
                                    Text="Submit" />
                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" CssClass="btn btn-primary mr-2"
                                    Text="Clear" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" HeaderStyle-ForeColor="White" AutoGenerateColumns="False"
                            PagerStyle-CssClass="PagerStyle" runat="server" EmptyDataText="No records found"
                            AllowPaging="True" Width="80%" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="desc" HeaderText="DESC" />
                                <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="credit" HeaderText="Credit" />
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
