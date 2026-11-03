<%@ Page Title="Bv Report" Language="C#" MasterPageFile="~/Member/member.master"
    AutoEventWireup="true" CodeFile="bvReport.aspx.cs" Inherits="Member_bvReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Repurchase BV Report
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
                                <asp:BoundField DataField="date" HeaderText="Month" DataFormatString="{0:dd/MMM/yyyy}" />
                                <asp:BoundField DataField="selfBv" HeaderText="New Self Bv" />
                                <asp:BoundField DataField="oldLeft" HeaderText="Left Carry" />
                                <asp:BoundField DataField="oldRight" HeaderText="Right Carry" />
                                <asp:BoundField DataField="maintenanceBv" HeaderText="Maintenance BV" SortExpression="amount" />
                                <asp:BoundField DataField="newLeft" HeaderText="New Left" />
                                <asp:BoundField DataField="newRight" HeaderText="New Right" />
                                <asp:BoundField DataField="totalLeft" HeaderText="Total Left" />
                                <asp:BoundField DataField="totalRight" HeaderText="Total Right" />
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
