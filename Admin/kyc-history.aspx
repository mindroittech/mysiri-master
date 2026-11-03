<%@ Page Title="KYC History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="kyc-history.aspx.cs" Inherits="Admin_kyc_history" %>

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
                    KYC History
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table align="center" border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    Username
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="200px" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            CellSpacing="1" CssClass="table table-bordered" Width="100%" PageSize="50" AllowPaging="true"
                            HeaderStyle-BackColor="#1f5fc0" HeaderStyle-ForeColor="White" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" SortExpression="username" />
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="pan" HeaderText="PAN Number" />
                                <asp:BoundField DataField="account" HeaderText="Acc No" />
                                <asp:BoundField DataField="ifsc" HeaderText="IFSC" />
                                <asp:BoundField DataField="UsedDate" HeaderText="Submitted On" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                <asp:BoundField DataField="EligibleDate" HeaderText="Action On" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                <asp:HyperLinkField Target="_blank" Text="View" DataNavigateUrlFields="UploadPan"
                                    DataNavigateUrlFormatString="{0}" HeaderText="PAN" ItemStyle-Width="150" />
                                <asp:HyperLinkField Target="_blank" Text="View" DataNavigateUrlFields="UploadBank"
                                    DataNavigateUrlFormatString="{0}" HeaderText="Bank Passbook" ItemStyle-Width="150" />
                                <asp:BoundField DataField="IsApproved" HeaderText="Status" ReadOnly="True" HtmlEncode="false"
                                    ItemStyle-Font-Bold="true" SortExpression="pinnumbers" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Action</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red" CommandArgument='<%# Eval("username") %>'
                                            CommandName="reject" Text="Reject">
                                        </asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure want to Reject?" Enabled="True" TargetControlID="LinkButton1">
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
