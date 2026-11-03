<%@ Page Title="My Stock" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="vStock.aspx.cs" Inherits="Admin_vStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    My Stocks
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-12">
                    <asp:Button ID="Button2" runat="server" Text="Export" CssClass="btn btn-primary"
                        Style="float: right" OnClick="Button2_Click" />
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                            DataKeyNames="ProductId" Width="100%" EmptyDataText="No records found..">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Product Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank"><%# Eval("ProductName")%></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BV">
                                    <ItemTemplate>
                                        <%#Eval("PV")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalPv" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Free" HeaderText="Free Eligible" SortExpression="plan" />
                                <asp:TemplateField HeaderText="DP (INR)">
                                    <ItemTemplate>
                                        <%#Eval("DP")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalDp" runat="server" Text="0"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Image ID="Image1" ImageUrl='<%# Eval("ImageUrl") %>' Width="35px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Stock" HeaderText="Stock Available(No)" SortExpression="plan" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
