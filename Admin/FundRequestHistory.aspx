<%@ Page Title="Fund Request History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="FundRequestHistory.aspx.cs" Inherits="Admin_FundRequestHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Fund Requests History
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
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
                                <a href="FundRequestHistory.aspx">Show All</a>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="true" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
                            EmptyDataText="No history found">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Username" HeaderText="Franchisee ID" SortExpression="username" />
                                <asp:BoundField DataField="VendorName" HeaderText="Franchisee Name" SortExpression="username" />
                                <asp:BoundField DataField="amountRequired" HeaderText="Req.Amount" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="paymode" HeaderText="Payment Mode" SortExpression="paymode" />
                                <asp:TemplateField HeaderText="Counterfoil">
                                    <ItemTemplate>
                                        <a id="A1" href='<%# Eval("counterfoil") %>' runat="server" visible='<%# Eval("counterfoil")!=null %>'
                                            target="_blank">View</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
                                <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Font-Bold="true"
                                    SortExpression="remarks" />
                                <asp:BoundField DataField="ActionOn" HeaderText="Action On" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <center>
                            <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="#993300" Text="No New Requests"
                                Visible="False"></asp:Label></center>
                    </div>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
