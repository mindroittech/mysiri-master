<%@ Page Title="Stock Transfer History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="StockTransferHistory.aspx.cs" Inherits="Admin_StockTransferHistory" %>

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
                    Stock Transfer History
                </p>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-12">
                    <span id="lbErrormsg" runat="server" visible="false" class="text-danger"></span>
                    <div class="table-responsive">
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    Start Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStart" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    End Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEnd" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Vendor ID
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="140px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"
                                        OnClick="Button1_Click" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="Button2_Click">Transferred By Me</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
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
                                        <asp:BoundField DataField="Username" HeaderText="Vendor ID" SortExpression="initialTransfer" />
                                        <asp:BoundField DataField="CompanyName" HeaderText="Vendor Name" SortExpression="initialTransfer" />
                                        <asp:HyperLinkField DataTextField="InvoiceNo" DataNavigateUrlFields="url,InvoiceNo"
                                            DataNavigateUrlFormatString="{0}?inv={1}" HeaderText="Inv No" Target="_blank"
                                            ItemStyle-ForeColor="Yellow" />
                                        <asp:BoundField DataField="Requestdate" HeaderText="Request Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                                        <asp:BoundField DataField="SentDate" HeaderText="Sent Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                                        <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid" SortExpression="name" />
                                        <asp:BoundField DataField="PayMode" HeaderText="Pay Mode" SortExpression="name" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="name" />
                                        <asp:BoundField DataField="SentBy" HeaderText="Sent By" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Font-Bold="true"
                                            SortExpression="name" />
                                        <asp:BoundField DataField="Courier" HeaderText="Delivery" SortExpression="name" />
                                        <asp:BoundField DataField="Free" HeaderText="Free Product" SortExpression="name" />
                                        <asp:BoundField DataField="AwbNo" HeaderText="AWB" SortExpression="name" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Stock
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton5" CommandName="products" CommandArgument='<%# Eval("SrId") %>'
                                                    runat="server" ToolTip="Send Products">View</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("SrId") %>'
                                                    CommandName="delete" Height="20px" ImageUrl="img/delete-icon.png" Width="20px"
                                                    ToolTip="Delete" Visible='<%# Eval("status").ToString()=="Processed" %>' />
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender22" ConfirmOnFormSubmit="true"
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
                                <asp:Panel ID="Panel2" runat="server" CssClass="x_content table-wrapper" Style="display: none;">
                                    <div class="modal-content" style="height: 600px; overflow: auto">
                                        <span>
                                            <asp:LinkButton ID="CancelButton" runat="server" CssClass="close1">&times;</asp:LinkButton></span>
                                        <asp:GridView ID="GridView2" runat="server" EmptyDataText="No stocks found" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" ImageUrl='<%# Eval("ImageUrl") %>' Width="35px" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="QtyReq" HeaderText="Stock Requested" />
                                                <asp:BoundField DataField="QtySent" HeaderText="Stock Sent" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
