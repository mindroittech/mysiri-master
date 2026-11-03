<%@ Page Title="My Business" Language="C#" MasterPageFile="admin.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="downline.aspx.cs" Inherits="downline" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>My Business</title>
    <style type="text/css">
        .style14
        {
            width: 70%;
        }
        
        .headerStyle
        {
            background: #009688;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Downline Members
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <table style="width: 50%">
                        <thead>
                            <tr>
                                <td>
                                    <a href='downline.aspx?uname=<%= Convert.ToString(Request.QueryString["uname"]) %>'>
                                        Total Members </a>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="0"></asp:Label>
                                </td>
                                <td>
                                    <a href='downline.aspx?uname=<%= Convert.ToString(Request.QueryString["uname"]) %>&leg=Left'>
                                        Left </a>
                                </td>
                                <td>
                                    <a href='downline.aspx?uname=<%= Convert.ToString(Request.QueryString["uname"]) %>&leg=Right'>
                                        Right </a>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="View in Excel" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </thead>
                    </table>
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                            Width="100%" EmptyDataText="No records found">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" SortExpression="username" />
                                <%-- <asp:TemplateField HeaderText="Level">
                        <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# "Level-"+Eval("level") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="plan" />
                                <asp:BoundField DataField="leg" HeaderText="Position" SortExpression="plan" />
                                <asp:BoundField DataField="joindate" HeaderText="Join Date" DataFormatString="{0:dd/MMM/yyyy hh:mm:ss tt}"
                                    SortExpression="joindate" />
                                 <asp:BoundField DataField="renewal" HeaderText="Activation Date" DataFormatString="{0:dd/MMM/yyyy hh:mm tt}"
                                    SortExpression="joindate" />
                                <asp:BoundField DataField="plan" HeaderText="Package" SortExpression="plan" />
                                <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Font-Bold="true"
                                    SortExpression="plan" />
                                <asp:BoundField DataField="pin" HeaderText="PIN Type" ItemStyle-Font-Bold="true"
                                    SortExpression="plan" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
