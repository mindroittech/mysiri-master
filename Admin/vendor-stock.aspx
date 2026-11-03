<%@ Page Title="Stock" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="vendor-stock.aspx.cs" Inherits="Admin_stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Franchisee Stock
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
                                    Search By Franchisee ID &nbsp;
                                    <asp:TextBox ID="TextBox1" Width="200px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn btn-primary"
                                        OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductId"
                            Width="80%" EmptyDataText="No records found..">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Username" HeaderText="Franchisee ID" SortExpression="name" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" ReadOnly="True"
                                    SortExpression="username" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock Available(No)" SortExpression="plan" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
