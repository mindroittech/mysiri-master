<%@ Page Title="Transfer Pin" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="pintransfer.aspx.cs" Inherits="pintransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectSingleRadiobutton(rdbtnid) {
            var rdBtn = document.getElementById(rdbtnid);
            var rdBtnList = document.getElementsByTagName("input");
            for (i = 0; i < rdBtnList.length; i++) {
                if (rdBtnList[i].type == "radio" && rdBtnList[i].id != rdBtn.id) {
                    rdBtnList[i].checked = false;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    PIN Transfer
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label8" runat="server" ForeColor="#009933" Text="" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="Label1" runat="server" ForeColor="#FF6600" Text="Not a valid username"
                        Visible="False" Font-Size="Medium"></asp:Label>
                    <div class="col-lg-7">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="form-group row">
                                    <label class="col-sm-4 col-form-label">
                                        Select Plan*</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="drpPlan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPinType_Changed"
                                            CssClass="form-control">
                                            <asp:ListItem>Select Plan</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpPlan"
                                            ErrorMessage="Select Plan" InitialValue="0" ValidationGroup="gt" Font-Size="Small"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-4 col-form-label">
                                        Pin Balance*</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtBalance" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPins"
                                            ErrorMessage="Select Plan" InitialValue="0" ValidationGroup="gt" Font-Size="Small"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-4 col-form-label">
                                       No of Pins To Transfer*</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtPins" runat="server"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                            TargetControlID="txtPins" />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPins"
                                            ErrorMessage="Select Plan" InitialValue="0" ValidationGroup="gt" Font-Size="Small"
                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-4 col-form-label">
                                        Transfer To User Id*</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBox2" runat="server" AutoPostBack="true" OnTextChanged="txtUser_Changed"
                                            CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox2"
                                            ErrorMessage="Enter username" ValidationGroup="gt" Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="col-md-4">
                            </div>
                            <div class="col-md-3">
                                <div class="form-group row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="Button2" runat="server" Text="Continue" OnClick="Button2_Click" CssClass="btn btn-primary mr-2"
                                            ValidationGroup="gt" />
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
