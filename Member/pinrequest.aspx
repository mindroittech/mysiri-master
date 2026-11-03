<%@ Page Title="Pin Request" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="pinrequest.aspx.cs" Inherits="pinrequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    PIN Request
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-7">
                    <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" Text="" Visible="False"></asp:Label>
                    <br />
                    Note: <asp:Label ID="Label2" runat="server" ForeColor="#FF3300" Text="PIN Requests after 6 PM will not be eligible for PIN Commission."
                        Visible="False"></asp:Label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12" style="max-height: 600px; overflow: auto">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="false" CssClass="table table-bordered"
                                        ShowFooter="true" PageSize="50" Width="100%" DataKeyNames="PlanId" EmptyDataText="No Plans">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    SN
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PlanName" HeaderText="Plan" SortExpression="description" />
                                            <asp:BoundField DataField="PlanAmount" HeaderText="Cost" SortExpression="description" />
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
                            <div class="clear-fix">
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Total Cost</label>
                                        <div class="col-sm-5">
                                            <asp:Label ID="lblCost" runat="server" Enabled="false"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Amount Deposited</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAmount"
                                                ErrorMessage="Enter amount paid" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                             <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Mode of Payment</label>
                                        <div class="col-sm-5">
                                            <asp:RadioButtonList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_Changed"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem>Cash Deposite</asp:ListItem>
                                                <asp:ListItem>Online</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DropDownList2"
                                                ErrorMessage="Select Payment Mode" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divUTR" runat="server" visible="false">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            UTR Number</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtUTR" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUTR"
                                                ErrorMessage="Enter UTR number" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Remarks</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                Rows="3"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Attach Proof</label>
                                        <div class="col-sm-5">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </div>
                                        <div class="col-md-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
                                                ErrorMessage="Upload Proof" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                             <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">
                                </label>
                                <div class="col-sm-5">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary mr-2" Text="Submit"
                                        OnClick="Button1_Click" ValidationGroup="sub" OnClientClick="return validateAmount();"/>                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   <script type="text/javascript">
    function validateAmount() {
        // Get the values of total cost and amount deposited
        var lblCostValue = parseFloat(document.getElementById('<%= lblCost.ClientID %>').innerText);
        var txtAmountValue = parseFloat(document.getElementById('<%= txtAmount.ClientID %>').value);

        // Check if amount deposited matches the total cost
        if (lblCostValue !== txtAmountValue) {
            document.getElementById('<%= lblError.ClientID %>').innerText = "Amount Deposit does not match the total cost.";
            return false; // Prevent form submission
        }

        // Check if a file is uploaded
        var fileUpload = document.getElementById('<%= FileUpload1.ClientID %>');
       var label3 = document.getElementById('<%= Label3.ClientID %>');
        if (fileUpload && fileUpload.files.length === 0) {
            label3.innerText = "Please upload a file.";
            return false; // Prevent form submission
        }

        return true; // Proceed with submission
    }
</script>
</asp:Content>
