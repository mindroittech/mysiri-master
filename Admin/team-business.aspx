<%@ Page Title="Team Business" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="team-business.aspx.cs" Inherits="Admin_Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                   Team Business
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                         <table style="width: 100%">
                            <tr>
                                 <td>
                                    Username
                                    <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="140px" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" Width="140px" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem>Binary</asp:ListItem>
                                        <asp:ListItem>Repurchase</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:label ID="lbl1" runat="server" forecolor="Red" Visible="False"></asp:label>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-lg-12">
                        <br />
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 30%">
                            <tr class="danger">
                                <td>
                                    Total Business
                                </td>
                                <td>
                                    <asp:Label ID="lbltotal" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr class="danger">
                                <td>
                                   Charge 10%
                                </td>
                                <td>
                                    <asp:Label ID="lblcharge" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr class="danger">
                                <td>
                                    Net Amount
                                </td>
                                <td>
                                    <asp:Label ID="lblnet" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                         
                        </table>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            EmptyDataText="Repurchase rank not achieved" CellSpacing="1" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RankName" HeaderText="Rank Achieved" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="dateAchieved" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="MatchingIncome" HeaderText="Income" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="termLeft" HeaderText="Income Left" SortExpression="name" />
                                <asp:BoundField DataField="MaintenanceBv" HeaderText="Maintainance Bv Required" SortExpression="name" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
