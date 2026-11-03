<%@ Page Title="Blocked Ids" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="blocked.aspx.cs" Inherits="Admin_blocked" %>

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
                    Blocked IDs
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table>
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Id / Mobile</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" Width="160px" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button5" runat="server" Text="Search" OnClick="Button5_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Label ID="Label1" runat="server" ForeColor="Green" Text="" Visible="False"></asp:Label>
                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                            DataSourceID="SqlDataSource1" PagerStyle-CssClass="PagerStyle" EmptyDataText="No records found"
                            AllowPaging="True" PageSize="50" Width="100%" OnRowCommand="GridView1_RowCommand"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" ItemStyle-HorizontalAlign="Left"
                                    ReadOnly="True" SortExpression="username">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="name" HeaderText="Name" ItemStyle-HorizontalAlign="Left"
                                    SortExpression="name">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="mobile" />
                                <asp:BoundField DataField="plan" HeaderText="Package" SortExpression="plan" />
                                <%-- <asp:BoundField DataField="amount" HeaderText="Amount ($)" 
                SortExpression="amount" />--%>
                                <asp:BoundField DataField="joindate" HeaderText="Join Date" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="128px" DataFormatString="{0:dd/MMM/yyyy }" SortExpression="joindate">
                                </asp:BoundField>
                                <%--  <asp:BoundField DataField="enddate" HeaderText="Expire Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px"  DataFormatString="{0:dd/MMM/yyyy }"
                SortExpression="joindate" />--%>
                                <%--   <asp:BoundField DataField="pan" HeaderText="Pan Number" 
                SortExpression="pan" />--%>
                                <asp:BoundField DataField="kyc" HeaderText="KYC" ItemStyle-Font-Bold="true" SortExpression="panname" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Profile</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" CommandName="profile" CommandArgument='<%# Eval("username") %>'
                                            runat="server">View</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Genealogy</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" CommandName="genealogy" CommandArgument='<%# Eval("username") %>'
                                            runat="server">View</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Downline</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" CommandName="downline" CommandArgument='<%# Eval("username") %>'
                                            runat="server">View</asp:LinkButton>
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
                        <asp:Button ID="Button2" runat="server" Style="float: right;" Text="Activate" OnClick="Button2_Click" />
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LotusBizConnectionString %>"
                        SelectCommand="SELECT [username], [name], [pan],  [mobile], [amount], [plan], [joindate], Case when isPanVerifed=1 then 'Yes' else 'No' end as [kyc] FROM [tblMember] WHERE ([isActive] = @flag)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="flag" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
