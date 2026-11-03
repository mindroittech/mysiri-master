<%@ Page Title="Stock Requests" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="StockRequests.aspx.cs" Inherits="Admin_StockRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Stock Requests
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-3">
                    <label for="lname">
                        Franchisee ID
                    </label>
                    <asp:TextBox ID="txtID" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="Clear" OnClick="Button2_Click" />
                </div>
                <div class="col-md-12">
                    <asp:Label ID="lblMessage" ForeColor="Green" runat="server" Text=""></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            OnRowCommand="GridView1_RowCommand" EmptyDataText="No records found" CellSpacing="1"
                            Width="100%" PageSize="50" AllowPaging="true" OnPageIndexChanging="GridView_PageIndex">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Username" HeaderText="Vendor ID" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Vendor Name" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="Requestdate" HeaderText="Request Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                                <asp:BoundField DataField="AmountPaid" HeaderText="Amount Payable" SortExpression="name" />
                                <asp:BoundField DataField="PayMode" HeaderText="Pay Mode" SortExpression="name" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="name" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Products</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton5" CommandName="products" CommandArgument='<%# Eval("SrId") %>'
                                            runat="server" ToolTip="Assign Products">Assign</asp:LinkButton>
                                        &nbsp;|&nbsp;
                                        <asp:LinkButton ID="LinkButton1" CommandName="print" CommandArgument='<%# Eval("SrId") %>'
                                            runat="server" ToolTip="Print">Print</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
