<%@ Page Title="Stock Request History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="vStockRequestHistory.aspx.cs" Inherits="Admin_vStockHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Stock Request/Transfer History
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-12">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Text="Stock request submitted successfully"
                        Visible="False"></asp:Label>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
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
                                        <asp:BoundField DataField="Requestdate" HeaderText="Request Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Reporter" HeaderText="Requested To" SortExpression="initialTransfer" />
                                        <asp:HyperLinkField DataTextField="InvoiceNo" DataNavigateUrlFields="url,InvoiceNo"
                                           DataNavigateUrlFormatString="{0}?inv={1}"   HeaderText="Inv No" Target="_blank"
                                            ItemStyle-ForeColor="Yellow" />
                                        <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid" SortExpression="name" />
                                        <asp:BoundField DataField="PayMode" HeaderText="Pay Mode" SortExpression="name" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Font-Bold="true"
                                            SortExpression="name" />
                                        <asp:BoundField DataField="SentDate" HeaderText="Sent Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Courier" HeaderText="Delivery" SortExpression="name" />
                                        <asp:BoundField DataField="AwbNo" HeaderText="AWB" SortExpression="name" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="name" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Products</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton5" CommandName="products" CommandArgument='<%# Eval("SrId") %>'
                                                    runat="server" ToolTip="View Products">View</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="80px">
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("SrId") %>'
                                                    CommandName="accept" Height="26px" Visible='<%# Eval("Status").ToString()=="Processed" %>'
                                                    ImageUrl="img/accept.jpg" Width="26px" ToolTip="Accept" />
                                                &nbsp;
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmOnFormSubmit="true"
                                                    runat="server" ConfirmText="Are you sure your order has been delivered?" Enabled="True"
                                                    TargetControlID="ImageButton1">
                                                </asp:ConfirmButtonExtender>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("SrId") %>'
                                                    CommandName="delete" Height="20px" ImageUrl="img/delete-icon.png" Width="20px"
                                                    ToolTip="Delete" Visible='<%# Eval("Status").ToString()=="Requested" %>' />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" ConfirmOnFormSubmit="true"
                                                    runat="server" ConfirmText="Are you sure want to delete?" Enabled="True" TargetControlID="ImageButton2">
                                                </asp:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="button3" runat="server" Text="Ok" Style="display: none" />
                                <asp:ModalPopupExtender ID="ImageButton2_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="CancelButton" Enabled="True" PopupControlID="Panel2" TargetControlID="button3">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="x_content table-wrapper" Style="display: none">
                                    <div class="modal-content" style="height: 600px; overflow: auto">
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
                                                <asp:BoundField DataField="QtyReq" HeaderText="Qty Requested" />
                                                <asp:BoundField DataField="QtySent" HeaderText="Qty Received" />
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
