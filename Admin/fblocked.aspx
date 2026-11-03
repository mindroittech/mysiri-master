<%@ Page Title="Blocked Franchisees" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="fblocked.aspx.cs" Inherits="Admin_Default3" %>


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
                    Blocked Franchisees
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
                        ID/Name
                    </label>
                    <asp:TextBox ID="txtID" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />&nbsp;
                    <asp:Button ID="Button2" runat="server" Text="Clear" OnClick="Button2_Click" />
                </div>
              
                <div class="col-md-12">
                    <div class="table-responsive">
                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                            Width="100%" EmptyDataText="No vendors found" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="VendorName" HeaderText="Franchisee Name" ReadOnly="True"
                                    SortExpression="username" />
                                <asp:BoundField DataField="Username" HeaderText="ID" SortExpression="name" />
                                <asp:BoundField DataField="RoleName" HeaderText="Franchisee Type" SortExpression="name" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" SortExpression="name"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="name" />
                                <asp:BoundField DataField="Reporting" HeaderText="Reporting To" SortExpression="joindate" />
                                <asp:BoundField DataField="CashWallet" HeaderText="Cash Wallet" SortExpression="joindate" />
                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="plan" />
                                <%--   <asp:BoundField DataField="City" HeaderText="City" SortExpression="plan" />--%>
                                <asp:HyperLinkField Text="Stock" DataNavigateUrlFields="Username" DataNavigateUrlFormatString="vendor-stock.aspx?Id={0}"
                                    HeaderText="View Stock" Target="_blank" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("Id") %>'
                                            CommandName="edit" Height="25px" ImageUrl="img/edit.png" Width="22px" ToolTip="Edit" />
                                        <%--<asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("Id") %>'
                                            CommandName="delete" Height="22px" ImageUrl="img/reject.png" Width="22px" ToolTip="Block" />
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure want to block?" Enabled="True" TargetControlID="ImageButton2">
                                        </asp:ConfirmButtonExtender>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                     <asp:Button ID="Button3" runat="server" Style="float: right;" Text="Activate" OnClick="Button3_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
