<%@ Page Title="Product Dispatch" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="dispatch-history.aspx.cs" Inherits="Member_dispatch_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Product Dispatch
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table>
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Username</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" Width="200px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="View All" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                            Width="100%" AllowPaging="True" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
                            EmptyDataText="No records found" CellSpacing="1" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="activatedDate" HeaderText="Activation Date" SortExpression="senddate"
                                    DataFormatString="{0:dd/MMM/yyyy hh:mm tt}" />
                                <asp:BoundField DataField="plan" HeaderText="Plan" SortExpression="username" />
                                <asp:BoundField DataField="product" HeaderText="Product Name" SortExpression="product" />
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="username" />
                                <asp:BoundField DataField="senddate" HeaderText="Dispatch Date" SortExpression="senddate"
                                    DataFormatString="{0:dd/MMM/yyyy}" />
                                <asp:BoundField DataField="shipment" HeaderText="Shipment" SortExpression="name" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
