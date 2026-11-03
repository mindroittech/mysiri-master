<%@ Page Title="Generate Pin" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="generate-pin.aspx.cs" Inherits="admin_generate_pin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Generate PIN
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="ID not found" Visible="False"></asp:Label>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Username</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TextBox1" required runat="server" AutoPostBack="true" OnTextChanged="txtUser_Changed"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Name</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtName" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Enter Pin</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="TextBox2" AutoPostBack="true" OnTextChanged="CalculateAmount" runat="server"
                                                required CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                TargetControlID="TextBox2" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Package</label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="drpPlan" AutoPostBack="true" OnTextChanged="CalculateAmount"
                                                runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-7">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Total Amount</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtAmount" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-7">
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
                                                ErrorMessage="Select Payment Mode" ForeColor="Red" ValidationGroup="pin"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divUTR" runat="server" visible="false">
                                <div class="col-md-7">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            UTR No</label>
                                        <div class="col-sm-5" style="padding-top: 10px">
                                            <asp:TextBox ID="txtUTR" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUTR"
                                                ErrorMessage="Enter UTR" ForeColor="Red" ValidationGroup="pin"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-7">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Free PIN?</label>
                                <div class="col-sm-5" style="padding-top: 10px">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    &nbsp;&nbsp; <span style="color: blue; font-size: 12px">(Select only for Free PINs)</span>
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>
                    </div>
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
                        <div class="col-md-7">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                </label>
                                <div class="col-sm-5">
                                    <asp:Button ID="Button1" runat="server" Text="Generate Pin" CssClass="btn btn-info"
                                        OnClick="Button1_Click" ValidationGroup="pin" />
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
