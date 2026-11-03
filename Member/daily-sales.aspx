<%@ Page Title="Daily Sales Report" Language="C#" MasterPageFile="member.master"
    AutoEventWireup="true" CodeFile="daily-sales.aspx.cs" Inherits="portal_Admin_daily_sales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Binary Sales Report
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" 
                            PagerStyle-CssClass="PagerStyle" AllowPaging="true" EmptyDataText="No records found"
                            OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="confirmdate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy hh:00 tt}" />
                                <asp:BoundField DataField="Left_Previous" HeaderText="Left Carry" />
                                <asp:BoundField DataField="Right_Previous" HeaderText="Right Carry" />
                                <asp:BoundField DataField="Left_New" HeaderText="New Left" />
                                <asp:BoundField DataField="Right_New" HeaderText="New Right" />
                                <asp:BoundField DataField="match" HeaderText="Match" SortExpression="amount" />
                                <asp:BoundField DataField="paid" HeaderText="Paid" SortExpression="amount" />
                                <asp:BoundField DataField="flush" HeaderText="Flush" SortExpression="amount" />
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
