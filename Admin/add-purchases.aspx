<%@ Page Title="Add Purchases" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="add-purchases.aspx.cs" Inherits="Admin_add_purchases" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script>
        $(function () {
            $('#filter').keyup(function () {
                var val = $(this).val().toUpperCase();

                $('#<%= GridView1.ClientID%>> tbody > tr').not(':first').each(function (index, element) {
                    if ($(this).text().toUpperCase().indexOf(val) < 0)
                        $(this).hide();
                    else
                        $(this).show();
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">
            Add Purchases</h1>
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
        border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
        <div class="col-md-12">
            <div class="container1">
                <div class="row">
                    <div class="col-lg-12">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="table-responsive" style="max-height: 500px; overflow: auto">
                                    <table border="0" cellpadding="2" cellspacing="2">
                                        <tr>
                                            <td>
                                                Search By Product
                                            </td>
                                            <td>
                                                <input type="text" style="width: 200px" id="filter" tabindex="0" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductId"
                                        Width="100%" EmptyDataText="No products found">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    SN</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductName" HeaderText="Product" ReadOnly="True" SortExpression="username" />
                                            <asp:BoundField DataField="GST" HeaderText="GST%" SortExpression="joindate" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Base Price
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRate" Style="max-width: 100px" Text='<%# Eval("BasePrice") %>'
                                                        OnTextChanged="txtQuanity_Changed" AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                                        TargetControlID="txtRate" ValidChars="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Quantity" HeaderText="Stock Left" SortExpression="joindate" />
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNos" Style="max-width: 60px" OnTextChanged="txtQuanity_Changed"
                                                        AutoPostBack="true" runat="server"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtNos" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    Total Price
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    Gst Amt
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGstAmt" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    Purchase Price
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetTotal" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="row" style="text-align: center">
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-1">
                                        Total
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblTotalQty" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblTotalPrice" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-1">
                                        &emsp;
                                        <asp:Label ID="lblTotalGST" runat="server" Text="0"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblFinalPrice" runat="server" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-6">
                        <br />
                        <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                        <div class="form-row">
                            <div class="form-group col-md-12">
                                <label for="remarks">
                                    Purchase Date</label>
                                <asp:TextBox ID="txtDate" required TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-12">
                                <label for="remarks">
                                    Invoice No</label>
                                <input type="text" class="form-control" runat="server" id="invoiceNo" required>
                            </div>
                            <div class="form-group col-md-12">
                                <label for="remarks">
                                    Supplier</label>
                                <asp:DropDownList ID="drpSupplier" required runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-12">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                    Text="Submit" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
