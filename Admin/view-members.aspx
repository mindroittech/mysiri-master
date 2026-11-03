<%@ Page Title="View Members" Language="C#" MasterPageFile="admin.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="view-members.aspx.cs" Inherits="admin_view_members" %>

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
                    View Members
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table>
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="160px" CssClass="form-control" TextMode="Date"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" Width="160px" CssClass="form-control" TextMode="Date"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button3" runat="server" Text="Search" OnClick="Button3_Click" />
                                </td>
                                <td>
                                    <b style="font-weight: bold;">Id/Mobile/PAN</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" Width="160px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button5" runat="server" Text="Search" OnClick="Button5_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div class="row">
                            <div class="col-md-8">
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Button4" runat="server" Text="View All" Style="float: right;" CssClass="btn btn-info"
                                    OnClick="Button4_Click" />
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Button1" runat="server" Style="float: right;" Text="View in Excel"
                                    CssClass="btn btn-info" OnClick="Button1_Click" />
                            </div>
                        </div>
                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username"
                            DataSourceID="SqlDataSource1" PagerStyle-CssClass="PagerStyle" EmptyDataText="No records found"
                            AllowPaging="True" PageSize="50" Width="100%" OnRowCommand="GridView1_RowCommand"
                            GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN</HeaderTemplate>
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
                                <asp:BoundField DataField="renewal" HeaderText="Activation Date" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="128px" DataFormatString="{0:dd/MMM/yyyy }" SortExpression="joindate">
                                </asp:BoundField>
                                <%--  <asp:BoundField DataField="enddate" HeaderText="Expire Date" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px"  DataFormatString="{0:dd/MMM/yyyy }"
                SortExpression="joindate" />--%>
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="pan" />
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
                                        Select</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:Button ID="Button2" runat="server" Style="float: right;" Text="Deactivate" OnClick="Button2_Click" />
                        <asp:Button ID="btnSms" runat="server" Style="float: right; margin-right: 10px;"
                            Text="Send Login Sms" OnClick="btnSms_Click" />
                    </div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LotusBizConnectionString %>"
                        SelectCommand="SELECT [username], [name], [pan],  [mobile], [amount], [plan], [joindate],[renewal], [status], 
                        Case when isPanVerifed=1 then 'Yes' else 'No' end as [kyc] FROM [tblMember] WHERE ([isActive] = @flag) ORDER by [id] desc">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="1" Name="flag" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
