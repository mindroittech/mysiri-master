<%@ Page Title="Add New Loan" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="addLoan.aspx.cs" Inherits="Admin_addLoan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    <asp:Label ID="Label1" runat="server" Text="Add Loan"></asp:Label>
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                    <ContentTemplate>
                        <div class="col-md-12">
                            <div class="container1">
                                <div class="row">
                                    <div class="col-lg-5">
                                        <div class="form-row">
                                              <div class="form-group col-md-12" id="diverr" runat="server" visible="false" style="color:red">
                                                <label for="remarks">User already has a loan. You can't create a new loan entry, instead you can add more loan to existing entry.</label>         
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Member ID</label>
                                                <asp:TextBox ID="txtID" required runat="server" AutoPostBack="true" OnTextChanged="txtUser_Changed"
                                                    CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Member Name</label>
                                                <asp:TextBox ID="txtName" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                          
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Loan Amount (Rs)</label>
                                                <asp:TextBox ID="txtLoanAmt" required runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtLoanAmt" />
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Confirm Loan Amount (Rs)</label>
                                                <asp:TextBox ID="txtConfirmLoanAmt" required runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtConfirmLoanAmt" />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmLoanAmt"
                                                    ControlToCompare="txtLoanAmt" ForeColor="Red" Display="Dynamic" Font-Size="Small"
                                                    ErrorMessage="Loan amount does not match"></asp:CompareValidator>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Remarks</label>
                                                <input type="text" class="form-control" runat="server" id="txtRemarks" />
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks" style="font-weight: bold">Recovery Details</label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Recovery Percentage</label>
                                                <asp:TextBox ID="txtPercent" required MaxLength="3" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtPercent" />
                                            </div>
                                            <div class="form-group col-md-12">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                                    Text="Submit" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
