<%@ Page Title="Account Transactions" Language="C#" MasterPageFile="member.master"
    AutoEventWireup="true" CodeFile="account-transactions.aspx.cs" Inherits="account_transactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Account Transactions
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-4">
                    <p style="font-size: 20px; color: #000;">
                        Wallet Amount<br />
                        <span style="color: #FFAA6F; font-size: 20px;">
                            <asp:Label ID="lblCreditLeft" runat="server" Text=""></asp:Label></span></p>
                </div>
                <div class="col-md-4">
                    <p style="font-size: 20px; color: #000;">
                        Total Credits<br />
                        <span style="color: #FFAA6F; font-size: 20px;">
                            <asp:Label ID="Label7" runat="server" Text=""></asp:Label></span></p>
                </div>
                <div class="col-md-4">
                    <p style="font-size: 20px; color: #000;">
                        Total Debits<br />
                        <span style="color: #FFAA6F; font-size: 20px;">
                            <asp:Label ID="Label8" runat="server" Text=""></asp:Label></span></p>
                </div>
                <br />
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" AllowPaging="True" CssClass="table table-bordered" 
                        Font-Size="15px" Width="100%" EmptyDataText="No records found" PageSize="50"
                        OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    No.</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="confirmdate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy hh:59 tt}" />
                            <asp:BoundField DataField="desc" HeaderText="Desc" />
                            <asp:BoundField DataField="credit" HeaderText="Credit" />
                            <asp:BoundField DataField="debit" HeaderText="Debit" />
                            <%-- <asp:BoundField DataField="tax" HeaderText="Deduction(15%)" />--%>
                            <asp:BoundField DataField="netpayout" HeaderText="NetPay" />
                            <asp:BoundField DataField="lapse" HeaderText="Lapsed" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
