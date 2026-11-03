<%@ Page Title="Activation Report" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="ActivationReport.aspx.cs" Inherits="Admin_ActivationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Activation Report
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <div class="table-responsive">
                        <table>
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="160px" CssClass="form-control" TextMode="Date"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" Width="160px" CssClass="form-control" TextMode="Date"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" Text="Search" OnClick="Button3_Click" />
                                </td>
                                <td>
                                    <b style="font-weight: bold;">ID / Mobile / Name</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" Width="160px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button5" runat="server" Text="Search" OnClick="Button5_Click" />
                                </td>
                                <td>
                                      <asp:Button ID="Button1" runat="server" Style="float: right;" Text="View in Excel"
                                    CssClass="btn btn-info" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                          
                        <asp:GridView ID="GridView1" CssClass="two-axis" runat="server" EmptyDataText="No members found"
                            AutoGenerateColumns="False" DataKeyNames="username" Width="100%" HeaderStyle-ForeColor="White"
                            AllowPaging="true" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
                            CellPadding="3" CellSpacing="1" GridLines="None">
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
                                <asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="name" />
                                <asp:BoundField DataField="activatedDate" HeaderText="Activation Date" DataFormatString="{0:dd/MMM/yyyy hh:mm tt}"
                                    SortExpression="joindate" />
                                <asp:BoundField DataField="newPlan" HeaderText="Plan" SortExpression="name" />
                                <asp:BoundField DataField="pintype" HeaderText="PIN Type" SortExpression="name" />
                                <asp:BoundField DataField="state" HeaderText="State" SortExpression="name" />
                                <asp:BoundField DataField="district" HeaderText="District" SortExpression="name" />
                                <asp:BoundField DataField="activatedBy" HeaderText="Activator" SortExpression="name" />
                                <asp:BoundField DataField="sid" HeaderText="Sponsor" SortExpression="name" />
                            </Columns>
                            <HeaderStyle BackColor="#1f5fc0" />
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
