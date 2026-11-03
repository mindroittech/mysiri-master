<%@ Page Title="Introduction Packages" Language="C#" MasterPageFile="admin.master"
    AutoEventWireup="true" CodeFile="member-tours.aspx.cs" Inherits="portal_Admin_member_tours" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row">
        <br />
        <div class="col-md-12">
            <div class="card" style="padding: 21px;">
                <div class="card-body">
                    <h4 class="card-title" style="color: #0072bc; font-weight: 600;">
                        Introduction Package Eligible Members</h4>
                    <hr />
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" CssClass="two-axis" runat="server" EmptyDataText="No eligible members"
                            AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" DataKeyNames="username"
                            Width="100%" HeaderStyle-ForeColor="White" CellPadding="3" CellSpacing="1" GridLines="None">
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
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Tour Package</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="drpTour" CssClass="form-control" Width="160px" runat="server"
                                            DataSourceID="SqlDataSource2" DataTextField="tourPackage" DataValueField="tourId">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Start Date</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtStart" runat="server" Width="120px"></asp:TextBox>
                                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtStart">
                                        </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        End Date</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEnd" runat="server" Width="120px"></asp:TextBox>
                                        <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="txtEnd">
                                        </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="85px">
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
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#1f5fc0" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LotusBizConnectionString %>"
                            SelectCommand="SELECT tourId,tourPackage FROM [tblTourPackage]"></asp:SqlDataSource>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </div>
    </div>
</asp:Content>
