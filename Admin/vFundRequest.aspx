<%@ Page Title="Fund Request" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="vFundRequest.aspx.cs" Inherits="Admin_vFundRequest" %>

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
                    Fund Request
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" ForeColor="#FF3300" Text="" Visible="False"></asp:Label>
                    <br />
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Fund Required (Rs)</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                        TargetControlID="txtAmount" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAmount"
                                        ErrorMessage="Enter fund required" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Mode of Payment</label>
                                <div class="col-sm-5">
                                    <asp:RadioButtonList ID="DropDownList2" runat="server" RepeatDirection="Horizontal">
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
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    UTR Number</label>
                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtUTR" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUTR"
                                        ErrorMessage="Enter UTR number" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
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
                        <div class="col-md-8">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Attach Proof</label>
                                <div class="col-sm-5">
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                </div>
                                <div class="col-sm-4">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
                                        ErrorMessage="Please attach proof" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-7">
                            <div class="form-group row">
                                <label class="col-sm-4 col-form-label">
                                </label>
                                <div class="col-sm-5">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary mr-2" Text="Submit"
                                        OnClick="Button1_Click" ValidationGroup="sub" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
