<%@ Page Title="Order History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="vOrderHistory.aspx.cs" Inherits="Admin_vOrderHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Order History
                </p>
            </div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-12">
                    <span id="lbErrormsg" runat="server" visible="false" class="text-danger"></span>
                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td>
                                Member ID
                                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Invoice No.
                                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                Order Type
                                <asp:RadioButtonList CssClass="form-control" RepeatDirection="Horizontal" ID="RadioButtonList1"
                                    runat="server">
                                    <asp:ListItem>Paid</asp:ListItem>
                                    <asp:ListItem>Free</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn btn-primary"
                                    OnClick="Button1_Click" /><br />
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton4_Click">Show All</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row1" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-12">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Text="" Visible="False"></asp:Label>
                    <br />
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                    OnRowCommand="GridView1_RowCommand" EmptyDataText="No records found" CellSpacing="1"
                                    Width="100%" PageSize="50" AllowPaging="true" OnPageIndexChanging="GridView_PageIndex">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:HyperLinkField DataTextField="InvoiceNo" DataNavigateUrlFields="url,InvoiceNo"
                                            HeaderText="Inv No" Target="_blank" ItemStyle-ForeColor="Yellow" />
                                        <asp:BoundField DataField="username" HeaderText="Username" SortExpression="initialTransfer" />
                                        <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                        <asp:BoundField DataField="PurchaseDate" HeaderText="Request Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="DispachedOn" HeaderText="Sent Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount Paid" />
                                        <asp:BoundField DataField="PayMode" HeaderText="Pay Mode" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Products
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton5" CommandName="products" CommandArgument='<%# Eval("InvoiceId") %>'
                                                    runat="server" ToolTip="View Products">View</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="button3" runat="server" Text="Ok" Style="display: none" />
                                <asp:ModalPopupExtender ID="ImageButton2_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="CancelButton" Enabled="True" PopupControlID="Panel2" TargetControlID="button3">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="x_content table-wrapper" Style="display: none">
                                    <div class="modal-content" style="max-height: 600px; overflow: auto">
                                        <span>
                                            <asp:LinkButton ID="CancelButton" runat="server" CssClass="close1">&times;</asp:LinkButton></span>
                                        <asp:GridView ID="GridView2" runat="server" EmptyDataText="No products found" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" ImageUrl='<%# Eval("ImageUrl") %>' Width="35px" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
