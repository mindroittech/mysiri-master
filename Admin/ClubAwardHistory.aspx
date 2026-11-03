<%@ Page Title="Repurchase Club Awards History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="ClubAwardHistory.aspx.cs" Inherits="Admin_ClubAwardHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Repurchase Club Awards History
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <span id="lbErrormsg" runat="server" visible="false" class="text-warning"></span>
                    <div class="table-responsive">
                        <table align="center" border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    Username
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="200px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"
                                        OnClick="Button1_Click" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Show All</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div class="col-md-12" style="max-height: 650px; overflow: auto">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                EmptyDataText="No records found" CellSpacing="1" CssClass="two-axis" Width="100%"
                                PageSize="50" HeaderStyle-ForeColor="White" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="username" HeaderText="User Id" SortExpression="initialTransfer" />
                                    <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                    <asp:BoundField DataField="mobile" HeaderText="Mobile No" SortExpression="name" />
                                    <asp:BoundField DataField="state" HeaderText="State" SortExpression="name" />
                                    <asp:BoundField DataField="dateAchieved" HeaderText="Achieved On" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <asp:BoundField DataField="RankName" HeaderText="Rank Name" SortExpression="package" />
                                    <asp:BoundField DataField="Reward" HeaderText="Rewards" ReadOnly="True" SortExpression="pinnumbers" />
                                    <asp:BoundField DataField="rewardDate" HeaderText="Dispatched On" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}"
                                        SortExpression="pinnumbers" />
                                </Columns>
                                <HeaderStyle BackColor="#1f5fc0" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
