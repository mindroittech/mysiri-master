<%@ Page Title="Unused Pins" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="pinunused.aspx.cs" Inherits="Admin_pinunused" %>

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
                    Unused PINs
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
                                    &nbsp;<asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                    &nbsp;<asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Show All</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="pinnumbers"
                            OnRowCommand="GridView1_RowCommand" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="50" AllowPaging="True" GridLines="None">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="80">
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="pinnumbers" HeaderText="Pin Number" ReadOnly="True" SortExpression="pinnumbers" />
                                <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="issued" HeaderText="Issued To" SortExpression="issued" />
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="package" HeaderText="Package" SortExpression="package" />
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                <asp:BoundField DataField="isfree" HeaderText="Free PIN" SortExpression="usedby" />
                                <asp:TemplateField ItemStyle-Width="85px">
                                    <HeaderTemplate>
                                        Action</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="del" CommandArgument='<%# Eval("id") %>'>Delete</asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure you want to Delete?" Enabled="True"
                                            TargetControlID="LinkButton2">
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="85px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
