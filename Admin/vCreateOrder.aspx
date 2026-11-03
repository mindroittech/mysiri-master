<%@ Page Title="Create Order" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="vCreateOrder.aspx.cs" Inherits="Admin_vCreateOrder" %>

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
                    Repurchase Order
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text="" Visible="False"></asp:Label>
                                <br />
                                <div class="row">
                                    <div class="col-md-12" style="max-height: 560px; overflow: auto">
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
                                                <asp:BoundField DataField="Cost" HeaderText="DP (Rs)" SortExpression="description" />
                                                <asp:BoundField DataField="PV" HeaderText="BV" SortExpression="description" />
                                                <asp:BoundField DataField="Stock" HeaderText="Available Quantity" SortExpression="description" />
                                                <asp:BoundField DataField="Free" HeaderText="Wallet Eligible" SortExpression="description" />
                                                <asp:TemplateField HeaderText="Requested Quantity (No)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNos" OnTextChanged="txtQuanity_Changed" AutoPostBack="true" runat="server"></asp:TextBox>
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
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Total BV
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPV" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
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
                                                Total Qty
                                            </label>
                                            <div class="col-sm-5">
                                                <asp:Label ID="lblTotalQty" runat="server" Text="0"></asp:Label>
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
                                                Total Cost
                                            </label>
                                            <div class="col-sm-5">
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
                                                Total BV
                                            </label>
                                            <div class="col-sm-5">
                                                <label id="lblTotalBv" runat="server">
                                                    0</label>
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
                                                Member ID</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtUser" OnTextChanged="txtUser_Changed" AutoPostBack="true" CssClass="form-control"
                                                    runat="server" required></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                                <br />
                                                <asp:Label ID="lblConversion" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="guestName" visible="false">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Guest Name</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtDispatched" CssClass="form-control" runat="server" required></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="guestAddress" visible="false">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Guest Address</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" Rows="3" CssClass="form-control"
                                                    runat="server" required></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" runat="server" id="Div1">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Choose Self BV leg
                                            </label>
                                            <div class="col-sm-9">
                                                <div class="d-flex align-items-center">
                                                    <asp:RadioButton ID="rbLeft" runat="server" GroupName="SelfBVLeg" Text="Left" CssClass="mr-3" />
                                                    <asp:RadioButton ID="rbRight" runat="server" GroupName="SelfBVLeg" Text="Right" CssClass="mr-3" />
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server"
                                                        ErrorMessage="Please select Self BV Left or Right"
                                                        ClientValidationFunction="validateBVLeg"
                                                        Display="Dynamic"
                                                        ForeColor="Red">
                                                    </asp:CustomValidator>
                                                    <span class="ml-4"> 
                                                   
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                 <div class="row" runat="server" id="Div2">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Current BV
                                            </label>
                                            <div class="col-sm-9">
                                                <div class="d-flex align-items-center">
                                                   
                                                        <asp:Label ID="lblleft" runat="server" Text="0" ></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblright" runat="server" Text="0" ></asp:Label>
                                                   
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" runat="server" id="payMode">
                                    <div class="col-md-8">
                                        <div class="form-group row">
                                            <label class="col-sm-3 col-form-label">
                                                Pay Mode</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="drpPayMode" runat="server" OnSelectedIndexChanged="drpPayMode_Changed"
                                                    AutoPostBack="true" CssClass="form-control" required>
                                                    <asp:ListItem Value="">Select Payment</asp:ListItem>
                                                    <asp:ListItem>Cash</asp:ListItem>
                                    
                                                  <%--  <asp:ListItem>Product Wallet</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="tmWallet" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-8">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">
                                                    Product Wallet
                                                </label>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblTMWallet" runat="server" Text="0"></asp:Label>
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
                                                    Member OTP
                                                </label>
                                                <div class="col-sm-5">
                                                    <asp:TextBox ID="txtOTP" CssClass="form-control" runat="server" required></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="LinkButton1" ForeColor="Blue" runat="server" OnClick="GenerateOTP_Click">Click Here</asp:LinkButton>
                                                    to generate OTP.
                                                    <br />
                                                    <asp:Label ID="lblOtpMessage" ForeColor="DarkGreen" Font-Size="12px" runat="server"
                                                        Text=""></asp:Label>
                                                </div>
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
                                        OTP
                                    </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtMyOtp" CssClass="form-control" runat="server" required></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="LinkButton2" ForeColor="Blue" runat="server" OnClick="MyOTP_Click">Click Here</asp:LinkButton>
                                        to generate OTP.
                                        <br />
                                        <asp:Label ID="Label1" ForeColor="DarkGreen" Font-Size="12px" runat="server" Text=""></asp:Label>
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
    <script type="text/javascript">
    function validateBVLeg(sender, args) {
        var left = document.getElementById('<%= rbLeft.ClientID %>');
        var right = document.getElementById('<%= rbRight.ClientID %>');
        args.IsValid = left.checked || right.checked;
    }
</script>
</asp:Content>
