<%@ Page Title="Daily Business" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="DailyBusiness.aspx.cs" Inherits="Admin_DailyBusiness" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Daily Business
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                  <div class="table-responsive">
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
                      </div>
                <br />
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                            EmptyDataText="No records found" PageSize="50" AllowPaging="false" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="date" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Date"
                                    SortExpression="paiddate"></asp:BoundField>
                                <asp:BoundField DataField="pinBusiness" HeaderText="Payment from PINs" SortExpression="plan" />
                                <asp:BoundField DataField="fundWallet" HeaderText="Payment from Franchisees" SortExpression="plan" />
                                <asp:BoundField DataField="manualBilling" HeaderText="Manual Billing" SortExpression="name" />
                                <asp:BoundField DataField="total" HeaderText="Total(Rs)" SortExpression="name" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
