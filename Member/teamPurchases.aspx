<%@ Page Title="Team Purchases" Language="C#" MasterPageFile="~/Member/member.master"
    AutoEventWireup="true" CodeFile="teamPurchases.aspx.cs" Inherits="Member_teamPurchases" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Team Repurchase
                </p>
            </div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-12">
                    <span id="lbErrormsg" runat="server" visible="false" class="text-danger"></span>
                    <div class="table-responsive">
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    Start Date
                                    <asp:TextBox ID="TextBox3" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    End Date
                                    <asp:TextBox ID="TextBox4" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"
                                        OnClick="Button1_Click" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton4_Click">Show All</asp:LinkButton>
                                </td>
                                <td>
                                    Total Left BV:
                                    <asp:Label ID="lblLeft" runat="server" Text="0"></asp:Label>
                                </td>
                                <td>
                                    Total Right BV:
                                    <asp:Label ID="lblRight" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-md-12">
                    <br />
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="table table-bordered" 
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
                                        <asp:BoundField DataField="username" HeaderText="Member Id" SortExpression="initialTransfer" />
                                        <asp:BoundField DataField="name" HeaderText="Member Name" SortExpression="name" />
                                        <asp:BoundField DataField="PurchaseDate" HeaderText="Purchased Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="Amount" HeaderText="Product Worth (Rs)" />
                                        <asp:BoundField DataField="leftBV" HeaderText="Left BV" />
                                        <asp:BoundField DataField="rightBV" HeaderText="Right BV" />
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
                                                <asp:BoundField HeaderText="Cost(Rs)" DataField="DP" />
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
