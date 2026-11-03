<%@ Page Title="Vendor Request Stock" Language="C#" MasterPageFile="admin.master"
    AutoEventWireup="true" CodeFile="vRequestStock.aspx.cs" Inherits="Admin_vRequestStock" %>

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
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $get('scrollDiv').scrollLeft;
            yPos = $get('scrollDiv').scrollTop;
        }
        function EndRequestHandler(sender, args) {
            $get('scrollDiv').scrollLeft = xPos;
            $get('scrollDiv').scrollTop = yPos;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Stock Request
                </p>
            </div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <div class="container1">
                        <div class="col-md-6">
                            Search By Product :
                            <input type="text" id="filter" tabindex="0">
                        </div>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text="" Visible="False"></asp:Label>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="max-height: 600px; overflow: auto">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                            ShowFooter="true" PageSize="50" Width="100%" DataKeyNames="ProductId" EmptyDataText="No stocks available">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
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
                                                <asp:BoundField DataField="Stock" HeaderText="Current Stock" SortExpression="description" />
                                                <asp:BoundField DataField="Cost" HeaderText="Cost (Rs)" SortExpression="description" />
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" ImageUrl='<%# Eval("ImageUrl") %>' Width="35px" runat="server" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Total
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity (No)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNos" OnTextChanged="txtQuanity_Changed" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtNos" />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalQty" runat="server" Text=""></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Total Cost
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <br />
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Total Cost
                                            </label>
                                            <div class="col-sm-5 pt-2">
                                                Rs
                                                <asp:Label ID="lblCost" runat="server" Text="0"></asp:Label>
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
                                                Request To</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="drpReporter" runat="server" CssClass="form-control" required>
                                                </asp:DropDownList>
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
                                                Pay Mode</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="drpPayMode" runat="server" Enabled="false" CssClass="form-control"
                                                    required>
                                                    <asp:ListItem>Cash Wallet</asp:ListItem>
                                                </asp:DropDownList>
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
                                                Cash Wallet Balance
                                            </label>
                                            <div class="col-sm-5 pt-2">
                                                Rs
                                                <asp:Label ID="lblWallet" runat="server" Text="0"></asp:Label>
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
                                                Remarks
                                            </label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="2" CssClass="form-control"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                    </label>
                                    <div class="col-sm-5">
                                        <asp:Button ID="Button1" runat="server" Text="Submit" UseSubmitBehavior="false" OnClientClick="this.disabled='true';"
                                            OnClick="btnSubmit_Click" />
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
