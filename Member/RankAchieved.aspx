<%@ Page Title="RankAchieved" Language="C#" MasterPageFile="~/Member/member.master"
    AutoEventWireup="true" CodeFile="RankAchieved.aspx.cs" Inherits="Member_RankAchieved" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Repurchase Rank Achieved
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <br />
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 30%">
                        <tr class="danger">
                            <td>
                                Self Bv
                            </td>
                            <td>
                                <asp:Label ID="lblSelfBv" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr class="danger">
                            <td>
                                Left Bv Carry
                            </td>
                            <td>
                                <asp:Label ID="lblTotalLeftBv" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                        <tr class="danger">
                            <td>
                                Right Bv Carry
                            </td>
                            <td>
                                <asp:Label ID="lblTotalRightBv" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="table table-bordered" 
                            EmptyDataText="Repurchase rank not achieved" CellSpacing="1" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RankName" HeaderText="Rank Achieved" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="dateAchieved" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="MatchingIncome" HeaderText="Income" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="termLeft" HeaderText="Income Left" SortExpression="name" />
                                <asp:BoundField DataField="MaintenanceBv" HeaderText="Maintainance Bv Required" SortExpression="name" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
