<%@ Page Title="Print Stock Transfer" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="PrintStockTransfer.aspx.cs" Inherits="Admin_PrintStockTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function printDiv(printableArea) {
            var printContents = document.getElementById(printableArea).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Stock Transfer
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <br />
                    <center>
                        <input onclick="printDiv('printableArea')" type="button" value="Print" style="cursor: pointer;" />
                    </center>
                    <div id='printableArea' class="container1">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Franchisee ID
                                            </label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="drpVendor" OnTextChanged="txtVendor_Changed" CssClass="form-control"
                                                    required AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblName" Font-Bold="true" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Franchisee Type
                                            </label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtType" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Amount Paid
                                            </label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtAmount" CssClass="form-control" runat="server" required></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtAmount" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divCourier" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">
                                                    Select Courier</label>
                                                <div class="col-sm-5">
                                                    <asp:DropDownList ID="drpCourier" runat="server" CssClass="form-control" required>
                                                        <asp:ListItem Value="">Select Courier</asp:ListItem>
                                                        <asp:ListItem>Professional Couriers</asp:ListItem>
                                                        <asp:ListItem>DTDC</asp:ListItem>
                                                        <asp:ListItem>Blue Dart</asp:ListItem>
                                                        <asp:ListItem>Others</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">
                                                    AWB Number</label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtAWB" CssClass="form-control" required runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Billing Address
                                            </label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtBilling" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                            PageSize="50" Width="100%" DataKeyNames="ProductId" EmptyDataText="No products found">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ProductName" HeaderText="Product" SortExpression="description" />
                                                <asp:BoundField DataField="QtyReq" HeaderText="Req Quantity" SortExpression="description" />
                                                <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" DataFormatString="{0:0}"
                                                    SortExpression="description" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-7">
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group row">
                                            <label class="col-sm-7 col-form-label">
                                                Total Qty
                                            </label>
                                            <div class="col-sm-5 pt-2">
                                                <asp:Label ID="lblTotalQty" runat="server" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group row">
                                            <label class="col-sm-7 col-form-label">
                                                Total Cost
                                            </label>
                                            <div class="col-sm-5 pt-2">
                                                <asp:Label ID="lblCost" runat="server" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
