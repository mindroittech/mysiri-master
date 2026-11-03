<%@ Page Title="Withdraw Details" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="withdraw-details.aspx.cs" Inherits="withdraw_request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Withdrawals
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="form-horizontal">
                        <div class="row form-group">
                            <div class="col-md-3">
                                <label>
                                    From Date
                                </label>
                                <input name="" type="date" id="fromDate" runat="server" class="form-control" placeholder="From Date">
                            </div>
                            <div class="col-md-3">
                                <label>
                                    To Date
                                </label>
                                <input name="" type="date" id="toDate" runat="server" class="form-control" placeholder="To Date">
                            </div>
                            <div class="col-md-3">
                                <label>
                                    Withdrawal Type
                                </label>
                                <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Value="Auto Pay">Binary</asp:ListItem>
                                    <asp:ListItem Value="Repurchase Pay">Repurchase</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-3">
                                <br>
                                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found" CssClass="table table-bordered" 
                            Style="width: 100%" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
                            OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="date" HeaderText="Req Date" SortExpression="date" DataFormatString="{0:dd-MMM-yyyy}"
                                    ItemStyle-Width="125px"></asp:BoundField>
                                <asp:BoundField DataField="paiddate" HeaderText="Paid Date" DataFormatString="{0:dd-MMM-yyyy}"
                                    ItemStyle-Width="125px" SortExpression="paiddate"></asp:BoundField>
                                <%-- <asp:BoundField DataField="paymentid" HeaderText="Payment Ref No" />--%>
                                <asp:BoundField DataField="amount" HeaderText="Amount (Rs)" SortExpression="amount" />
                                <asp:BoundField DataField="tax" HeaderText="TDS(5%)" />
                                <asp:BoundField DataField="sc" HeaderText="Admin Charges(5%)" />
                                <asp:BoundField DataField="deduction" HeaderText="Deduction" />
                                <asp:BoundField DataField="netpay" HeaderText="NetPay(Rs)" />
                                <%--<asp:HyperLinkField Text="View" HeaderText="Details" DataNavigateUrlFields="paymentid"
                                    DataNavigateUrlFormatString="payment-summary.aspx?id={0}" Target="_blank" />--%>
                                <asp:BoundField DataField="transaction" HeaderText="Transaction No" />
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                <%--   <asp:HyperLinkField Text="View" Visible='<%# IsVisible(Eval("status").ToString()) %>'
                                    HeaderText="Receipt" DataNavigateUrlFields="paymentid" DataNavigateUrlFormatString="receipt.aspx?id={0}"
                                    Target="_blank" />--%>
                                <%--<asp:TemplateField HeaderText="Receipt">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="ConditionHyperLink" Target="_blank" NavigateUrl='<%# string.Format("receipt.aspx?id={0}",Eval("paymentid")) %>'
                                            runat="server" Visible='<%# IsVisible(Eval("status").ToString())  %>' Text="View" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
