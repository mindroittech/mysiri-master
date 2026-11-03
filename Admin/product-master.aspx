<%@ Page Title="Product Master" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="product-master.aspx.cs" Inherits="Admin_product_master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script>
        $(function () {
            $('#filter').keyup(function () {
                var val = $(this).val().toUpperCase();

                $('#<%= GridView1.ClientID%>> tbody > tr').not(':first').each(function (index, element) {
                    if ($(this).text().toUpperCase().indexOf(val) < 0)
                        $(this).hide();
                    else
                        $(this).show();
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Product Master
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <span id="lbErrormsg" runat="server" visible="false" class="text-danger"></span>
                    <div class="table-responsive">
                        <table border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    Search By Product
                                </td>
                                <td>
                                    <input type="text" style="width: 200px" id="filter" tabindex="0" />
                                </td>
                                <%--  <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"
                                OnClick="Button1_Click" />--%>
                                <td>
                                    <asp:Button ID="Button3" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="Button3_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Export" CssClass="btn btn-primary"
                                        OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="row" style="padding-top: 10px">
                        <div class="col-md-3">
                            <span style="font-weight: bold">Total Quantity: </span>
                            <asp:Label ID="lblTotalQty" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <span style="font-weight: bold">Total Amount: </span>
                            <asp:Label ID="lblTotalAmt" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <span style="font-weight: bold">Total BV: </span>
                            <asp:Label ID="lblTotalBV" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <p style="text-align: right;">
                                <a href="product-master.aspx?ptype=free" class="btn btn-primary">Free Products </a>&emsp;
                                <a href="add-product-master.aspx" class="btn btn-primary">Add New </a>
                            </p>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductId"
                            Width="100%" EmptyDataText="No products found" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" SortExpression="username" />
                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" ImageUrl='<%# Eval("ImageUrl") %>' Width="40px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Quantity" HeaderText="My Stock" SortExpression="plan" />
                                <asp:BoundField DataField="GST" HeaderText="GST%" SortExpression="joindate" />
                                <asp:BoundField DataField="MRP" HeaderText="MRP" SortExpression="name" />
                                <asp:BoundField DataField="DP" HeaderText="DP" SortExpression="plan" />
                                <asp:BoundField DataField="PV" HeaderText="BV" SortExpression="plan" />
                                <asp:BoundField DataField="Free" HeaderText="Free" SortExpression="name" />
                                <asp:BoundField DataField="AW" HeaderText="A&R" SortExpression="name" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("ProductId") %>'
                                            CommandName="edit" Height="25px" ImageUrl="img/edit.png" Width="22px" ToolTip="Edit" />
                                        &nbsp;
                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("ProductId") %>'
                                            CommandName="delete" Height="20px" ImageUrl="img/delete-icon.png" Width="20px"
                                            ToolTip="Delete" Visible='<%# IsVisible(Convert.ToInt32(Eval("ProductId"))) %>' />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender22" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure want to delete?" Enabled="True" TargetControlID="ImageButton2">
                                        </asp:ConfirmButtonExtender>
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
