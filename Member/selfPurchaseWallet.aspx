<%@ Page Title="Self Purchase Wallet" Language="C#" MasterPageFile="~/Member/member.master"
    AutoEventWireup="true" CodeFile="selfPurchaseWallet.aspx.cs" Inherits="Member_selfPurchaseWallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Self Purchase Wallet
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
                <br />
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" AllowPaging="True" CssClass="table table-bordered" 
                        Font-Size="15px" Width="100%" EmptyDataText="No records found" PageSize="50"
                        OnPageIndexChanging="GridView1_PageIndexChanging" >
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    No.</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="confirmdate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                            <asp:BoundField DataField="remarks" HeaderText="Desc" />
                            <asp:BoundField DataField="credit" HeaderText="Credit" />
                            <asp:BoundField DataField="debit" HeaderText="Debit" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
