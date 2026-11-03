<%@ Page Title="Withdraw Request" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="withdraw-request.aspx.cs" Inherits="admin_withdraw_request" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            var headerChk = $(".chkHeader input");
            var itemChk = $(".chkItem input");

            headerChk.click(function () {
                itemChk.each(function () {
                    this.checked = headerChk[0].checked;
                })
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Withdraw Request
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive" style="overflow: auto">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">ID</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUser" Width="140px" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Search" OnClick="Button2_Click" />
                                    <asp:Button ID="Button3" runat="server" Text="Show All" OnClick="Button3_Click" />
                                </td>
                            </tr>
                        </table>
                        <p style="padding:8px">
                        Total NetPay(Rs): 
                            <asp:Label ID="lblNetPay" runat="server" Text="0"></asp:Label>

                            &emsp;   Total TDS(Rs): 
                            <asp:Label ID="lblTDS" runat="server" Text="0"></asp:Label>

                            <asp:Button ID="Button8" runat="server" Style="float: right;" Text="Pending Withdrawals"
                                OnClick="Button8_Click" />
                            &emsp;
                            <asp:Button ID="Button1" Style="float: right;" runat="server" Text="Export to Excel"
                                CssClass="btn btn-info" OnClick="Button1_Click" />
                            &emsp;
                            <asp:Button ID="Button9" Style="float: right;" runat="server" Text="Bank Export"
                                CssClass="btn btn-info" OnClick="Button9_Click" />
                        </p>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="username,sc,paymentid"
                            Width="100%" Font-Size="15px" EmptyDataText="No records found." OnRowCommand="GridView1_RowCommand"
                            PageSize="50" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" CssClass="chkHeader" runat="server" /></HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" CssClass="chkItem" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        No</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
                                <asp:BoundField DataField="name" HeaderText="Name" HtmlEncode="false" SortExpression="name" />
                                <asp:BoundField DataField="account" HeaderText="Account No" HtmlEncode="false" SortExpression="name" />
                                <asp:BoundField DataField="amount" HeaderText="Wallet Amount" SortExpression="cashavailable">
                                </asp:BoundField>
                                <asp:BoundField DataField="RankName" HeaderText="Club Rank" SortExpression="cashavailable">
                                </asp:BoundField>
                                <asp:BoundField DataField="tax" HeaderText="TDS(2%)" />
                                <asp:BoundField DataField="sc" HeaderText="A.C(8%)" />
                                <asp:BoundField DataField="deduction" HeaderText="Deduction" />
                                <asp:BoundField DataField="netpay" HeaderText="Net Pay(Rs)" DataFormatString="{0:0}" />
                                <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd-MMM-yyyy}">
                                    <ItemStyle Width="115px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Transaction No
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbox" runat="server" Width="100px">  </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Bank Details</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" CommandName="view" CommandArgument='<%# Eval("username") %>'
                                            runat="server">View</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="85px">
                                    <HeaderTemplate>
                                        Action</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" CommandName="send" CommandArgument='<%# Eval("paymentid") %>'
                                            runat="server">Send</asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure you want to send?" Enabled="True" TargetControlID="LinkButton1">
                                        </asp:ConfirmButtonExtender>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="del" CommandArgument='<%# Eval("paymentid") %>'>Remove</asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" ConfirmOnFormSubmit="true"
                                            runat="server" ConfirmText="Are you sure you want to remove?" Enabled="True"
                                            TargetControlID="LinkButton2">
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="85px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        &nbsp;
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtTrans" placeholder="Transaction No" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary mr-2"
                        OnClick="Button4_Click" /></div>
                <div class="col-md-6">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
