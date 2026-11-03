<%@ Page Title="Self BV Report" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="SelfBvReport.aspx.cs" Inherits="Admin_SelfBvReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Self BV Report
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table style="width: 100%">
                            <tr>
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
                                    <b style="font-weight: bold;">Min Pairs/BV</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMax" Width="70px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="Button2" Style="float: right;" Visible="false" runat="server" Text="Export to Excel"
                            CssClass="btn btn-primary mr-2" OnClick="Button2_Click" /><br />
                        <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False"
                            PagerStyle-CssClass="PagerStyle" AllowPaging="false" EmptyDataText="No records found"
                            OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                 <asp:BoundField DataField="mobile" HeaderText="Mobile No" SortExpression="name" />
                                <asp:BoundField DataField="state" HeaderText="State" SortExpression="name" />
                                <asp:BoundField DataField="Bv" HeaderText="Self BV" SortExpression="amount" />
                                <asp:BoundField DataField="bvType" HeaderText="Bv Type" />
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
