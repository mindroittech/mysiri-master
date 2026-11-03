<%@ Page Title="Team BV/PV Report" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="memberTeamPVreports.aspx.cs" Inherits="Admin_memberWiseRepurchaseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Team BV/PV Report
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    Username
                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Start Date
                                    <asp:TextBox ID="TextBox2" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    End Date
                                    <asp:TextBox ID="TextBox3" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"
                                        OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            EmptyDataText="No Records Found" CellSpacing="1" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Member ID" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="name" HeaderText="Member Name" SortExpression="initialTransfer" />
                                  <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="self" HeaderText="Self BV" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="binary" HeaderText="Binary PV" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="repurchase" HeaderText="Repurchase PV" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="sponsor" HeaderText="Sponsor Self PV" SortExpression="initialTransfer" />
                              
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
