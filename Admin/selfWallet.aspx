<%@ Page Title="Self Purchase Wallet" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="selfWallet.aspx.cs" Inherits="Admin_selfWallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Self Purchase Wallet
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" CssClass="two-axis" runat="server" EmptyDataText="No members found"
                            AutoGenerateColumns="False" DataKeyNames="username" Width="100%" HeaderStyle-ForeColor="White"
                            AllowPaging="true" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
                            CellPadding="3" CellSpacing="1" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" SortExpression="username" />
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="joindate" HeaderText="Join Date" DataFormatString="{0:dd/MMM/yyyy}"
                                    SortExpression="joindate" />
                                <asp:BoundField DataField="selfPurchaseWallet" HeaderText="Wallet Amount (Rs)" SortExpression="name" />
                            </Columns>
                            <HeaderStyle BackColor="#1f5fc0" />
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
