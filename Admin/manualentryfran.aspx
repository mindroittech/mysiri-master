<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="manualentryfran.aspx.cs" Inherits="Admin_Default3" %>

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
                    Manual Entry Franchisee Salary Income
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
               
                        <div class="col-md-12">
                            <div class="container1">
                                <div class="row">
                                    <div class="col-lg-5">
                                        <div class="form-row">
                                              <div class="form-group col-md-12" id="diverr" runat="server">
                                    <asp:Label ID="lblerror" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Franchisee ID</label>
                                                <asp:TextBox ID="txtID" required runat="server" AutoPostBack="true" OnTextChanged="txtUser_Changed"
                                                    CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Franchisee Name</label>
                                                <asp:TextBox ID="txtName" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                          
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Income Amount (Rs)</label>
                                                <asp:TextBox ID="txtLoanAmt" required runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtLoanAmt" />
                                            </div>
                                           
                                           <div class="form-group col-md-12">
                                             <label for="remarks">Date</label>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
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
                   
            </div>
        </div>
    </div>
</asp:Content>
