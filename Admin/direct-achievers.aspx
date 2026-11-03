<%@ Page Title="Direct Referral Achievers" Language="C#" MasterPageFile="admin.master"
    AutoEventWireup="true" CodeFile="direct-achievers.aspx.cs" Inherits="Admin_flight_trips" %>

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
                    Referrals Report
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" CssClass="two-axis" runat="server" EmptyDataText="No members found"
                            AutoGenerateColumns="False" DataKeyNames="username" Width="100%" HeaderStyle-ForeColor="White"
                            AllowPaging="true" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
                            CellPadding="3" CellSpacing="1"  GridLines="None" OnRowCommand="GridView1_RowCommand">
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
                                <asp:BoundField DataField="joindate" HeaderText="Join Date" DataFormatString="{0:dd/MMM/yyyy}"
                                    SortExpression="joindate" />
                                <asp:BoundField DataField="count" HeaderText="Total Referrals" SortExpression="name" />
                                <%--<asp:TemplateField ItemStyle-Width="85px">
                                    <HeaderTemplate>
                                        Action</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" CommandName="up" CommandArgument='<%# Eval("username") %>'
                                            runat="server">Update</asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure you want to update?" Enabled="True"
                                            TargetControlID="LinkButton1">
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <HeaderStyle BackColor="#1f5fc0" />
                        </asp:GridView>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
