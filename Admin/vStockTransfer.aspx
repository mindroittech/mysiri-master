<%@ Page Title="Vendor Stock Transfer" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="vStockTransfer.aspx.cs" Inherits="Admin_vStockTransfer" %>

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

        function DisableButton() {
            document.getElementById("<%=Button1.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Stock Transfer
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <div class="container1">
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Green" Text="" Visible="False"></asp:Label><br />
                                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text="" Visible="False"></asp:Label>
                                <br />
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Franchisee ID
                                            </label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtVendor" OnTextChanged="txtVendor_Changed" AutoPostBack="true"
                                                    CssClass="form-control" runat="server"></asp:TextBox>
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
                                                Cash Wallet Balance
                                            </label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtAmount" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtAmount" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Delivery Method</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="drpDelivery" AutoPostBack="true" OnSelectedIndexChanged="drpDelivery_Changed"
                                                    runat="server" CssClass="form-control" required>
                                                    <asp:ListItem Value="">Select Delivery</asp:ListItem>
                                                    <asp:ListItem>Courier</asp:ListItem>
                                                    <asp:ListItem>Handover</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
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
                                    <div class="col-md-4">
                                        <input type="text" placeholder="Search By Product" id="filter" tabindex="0">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="max-height: 600px; overflow: auto">
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
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" ImageUrl='<%# Eval("ImageUrl") %>' Width="35px" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Cost" HeaderText="Cost" SortExpression="description" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Available Quantity" SortExpression="description" />
                                                <asp:BoundField DataField="QtyReq" HeaderText="Requested Quantity" SortExpression="description" />
                                                <asp:TemplateField HeaderText="Approved Quantity (No)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNos" Text='<%# Eval("QtyReq") %>' OnTextChanged="txtQuanity_Changed"
                                                            AutoPostBack="true" runat="server"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtNos" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Total Cost
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
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
                        <div id="Div1" class="row" runat="server">
                            <div class="col-md-7">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        Enter OTP
                                    </label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:LinkButton ID="LinkButton1" ForeColor="Blue" runat="server" OnClick="GenerateOTP_Click">Click Here</asp:LinkButton>
                                        to generate OTP
                                        <br />
                                        <asp:Label ID="lblOtpMessage" ForeColor="DarkGreen" Font-Size="12px" runat="server"
                                            Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                    </label>
                                    <div class="col-sm-5">
                                        <br />
                                        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
