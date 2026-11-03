<%@ Page Title="" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="news.aspx.cs" Inherits="admin_news" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Manage News</title>
    <style type="text/css">
        .stylean1
        {
            color: Black;
            margin-left: 10px;
        }
        
        *
        {
            -webkit-text-size-adjust: none;
        }
        
        
        .stylean3
        {
            width: 190px;
        }
        .stylean2
        {
            height: 20px;
        }
        .stylean4
        {
            height: 20px;
            width: 190px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    News Update
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
                border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
                <div class="col-md-12">
                    <span id="lbErrormsg" runat="server" visible="false" class="text-danger"></span>
                    <div style="width: 38%">
                        <table __designer:mapid="68" align="center" cellpadding="6" cellspacing="0" class="stylean1">
                            <tr __designer:mapid="69">
                                <td __designer:mapid="6a" colspan="2" style="text-align: center; font-weight: 800;
                                    color: Black">
                                    &nbsp;&nbsp;&nbsp;&nbsp;Add News
                                </td>
                            </tr>
                            <tr __designer:mapid="71">
                                <td __designer:mapid="72" valign="top">
                                    News Description:
                                </td>
                                <td __designer:mapid="73" class="stylean3">
                                    <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr __designer:mapid="75">
                                <td __designer:mapid="76">
                                    Status&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; :
                                </td>
                                <td __designer:mapid="77" class="stylean3">
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>Active</asp:ListItem>
                                        <asp:ListItem>Inactive</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr __designer:mapid="7b">
                                <td __designer:mapid="7c" class="stylean2">
                                </td>
                                <td __designer:mapid="7d" class="stylean4">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                        Style="margin-left: 3px" Text="Add" />
                                    &nbsp;<asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" OnClick="Button2_Click"
                                        Style="margin-left: 1px" Text="Update" Visible="False" />
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False"
                            DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" Style="margin-left: 0px;
                            text-align: left; width: 100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />--%>
                                <asp:BoundField DataField="desc" HeaderText="Description" SortExpression="description" />
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                <asp:BoundField DataField="createDate" HeaderText="From Date" SortExpression="title"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="toDate" HeaderText="To Date" SortExpression="title" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        Edit
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Eval("nid") %>'
                                            CommandName="ed" ImageUrl="img/edit.png" Width="25px" Height="24px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("nid") %>'
                                            CommandName="delete" Text='<%# Eval("status").ToString()=="Active"?"Inactive":"Active" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LotusBizConnectionString %>"
                            SelectCommand="SELECT [title],[nid], [desc],[createDate],[toDate], [status] FROM [News]">
                        </asp:SqlDataSource>
                    </div>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
