<%@ Page Title="Finance Report" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="finance-report.aspx.cs" Inherits="Admin_finance_report" %>

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
                    Financial Report
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <h3>
                        Summary</h3>
                    <div class="table-responsive">
                        <table cellspacing="4" cellpadding="3" style="width: 30%">
                            <tr>
                                <td>
                                    Total Registration
                                </td>
                                <td>
                                    &nbsp; :
                                    <asp:Label ID="lblRegistration" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total Collection (Rs)
                                </td>
                                <td>
                                    &nbsp; :
                                    <asp:Label ID="lblCollection" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total Payout (Rs)
                                </td>
                                <td>
                                    &nbsp; :
                                    <asp:Label ID="lblPayout" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Deduction(20%) (Rs)
                                </td>
                                <td>
                                    &nbsp; :
                                    <asp:Label ID="lbltotalTax" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Company Profit (Rs)
                                </td>
                                <td>
                                    &nbsp; :
                                    <asp:Label ID="lblProfit" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table class="boxgborder">
                            <tr class="danger">
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td style="width: 220px">
                                    <asp:TextBox ID="TextBox1" TextMode="Date" AutoCompleteType="None" runat="server"></asp:TextBox>
                                   
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" TextMode="Date" AutoCompleteType="None" runat="server"></asp:TextBox>
                                  
                                </td>
                                <td>
                                    &nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                    &nbsp;
                                    <asp:Button ID="Button2" runat="server" Text="Show All" OnClick="Button2_Click" />
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="true" PageSize="50" GridLines="None" OnPageIndexChanging="GridView_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}" />
                                <asp:BoundField DataField="registration" HeaderText="Total Registration" />
                                <asp:BoundField DataField="collection" HeaderText="Total Collection" />
                                <asp:BoundField DataField="payout" HeaderText="Total Payout" />
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
