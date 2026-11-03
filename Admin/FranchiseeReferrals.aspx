<%@ Page Title="Franchisee Referrals" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="FranchiseeReferrals.aspx.cs" Inherits="Admin_FranchiseeReferrals" %>

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
                    Franchisee Referrals
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-3">
                    <label for="lname">
                        Franchisee Type<i class="fa fa-question-circle" style="color: #FCC878; margin-left: 5px;"></i>
                    </label>
                    <asp:DropDownList ID="drpRole" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label for="lname">
                        User ID
                    </label>
                    <asp:TextBox ID="txtUser" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                </div>
                <div class="col-md-3">
                </div>
                <div class="col-md-12">
                    <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Green" Text="Franchisee referral has been approved"
                        Visible="False"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                            Width="130%" EmptyDataText="No vendors found" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Username" HeaderText="Referred By" SortExpression="name" />
                                <asp:BoundField DataField="VendorName" HeaderText="Franchisee Name" ReadOnly="True"
                                    SortExpression="username" />
                                <asp:BoundField DataField="RoleName" HeaderText="Franchisee Type" SortExpression="name" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="name"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="name" />
                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="plan" />
                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="plan" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount Deposited" SortExpression="plan" />
                                <asp:BoundField DataField="depositeDate" HeaderText="Deposite Date" SortExpression="name"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="bank" HeaderText="Bank Deposited" SortExpression="name" />
                                <asp:BoundField DataField="UTR" HeaderText="UTR" SortExpression="plan" />
                                <asp:HyperLinkField Text="View" DataNavigateUrlFields="slip" DataNavigateUrlFormatString="{0}"
                                    HeaderText="Deposite Slip" Target="_blank" />
                                <asp:TemplateField ItemStyle-Width="100px">
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("Id") %>'
                                            CommandName="edit" Height="25px" ImageUrl="img/accept.jpg" Width="25px" ToolTip="Accept" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure want to Approve?" Enabled="True" TargetControlID="ImageButton1">
                                        </asp:ConfirmButtonExtender>
                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("Id") %>'
                                            CommandName="delete" Height="25px" ImageUrl="img/reject.png" Width="25px" ToolTip="Reject" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure want to reject?" Enabled="True" TargetControlID="ImageButton2">
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
