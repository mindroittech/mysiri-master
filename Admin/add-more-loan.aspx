<%@ Page Title="Add-more-Loans" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="add-more-loan.aspx.cs" Inherits="Admin_Default2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true"></asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    <asp:Label ID="Label1" runat="server" Text="Add Loan"></asp:Label>
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15); border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                    <ContentTemplate>
                        <div class="col-md-12">
                            <div class="container1">
                                <div class="row">
                                    <div class="col-lg-5">
                                        <div class="form-row">
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Member ID</label>
                                                <asp:TextBox ID="txtID" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtUser_Changed"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Member Name</label>
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                            <asp:Panel ID="diverr" runat="server" Visible="false" CssClass="form-group col-md-12" style="color:red">
                                                <label for="remarks">User already has a loan. You can't create a new loan entry, instead you can add more loan to existing entry.</label>
                                            </asp:Panel>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Current Loan Amount (Rs)</label>
                                                <asp:TextBox ID="txtLoanAmt" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtLoanAmt" />
                                            </div>
                                            <div class="form-group col-md-12" runat="server" id="newAmt">
                                                <label for="remarks" style="color:green">New Loan Amount (Rs)</label>
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="TextBox1" />
                                            </div>
                                            <div class="form-group col-md-12" runat="server" id="divtotal">
                                                <label for="remarks">Total Loan Amount (Rs)</label>
                                                <asp:TextBox ID="txttotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txttotal" />
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label for="remarks">Remarks</label>
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="Submit" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
        <div class="table-responsive">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username" 
                Width="100%" EmptyDataText="No records found" PageSize="50" AllowPaging="true" 
                OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="table">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>SNo.</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSRNO" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" SortExpression="username" />
                    <asp:BoundField DataField="date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" HeaderText="Loan Date" SortExpression="paiddate"></asp:BoundField>
                    <asp:BoundField DataField="loanAmount" HeaderText="Loan Amount" SortExpression="plan" />
                    <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="plan" />
                </Columns>
            </asp:GridView>
        </div>
            </div>
        </div>
    </div>
</asp:Content>
