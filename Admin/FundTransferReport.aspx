<%@ Page Title="Fund Transfer Report" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="FundTransferReport.aspx.cs" Inherits="Admin_FundTransferReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Fund Transfer Report
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label9" runat="server" Text="Username doesnot exist" ForeColor="Red"
                        Visible="False"></asp:Label>
                    <div class="table-responsive">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                            <tr class="danger">
                                <td>
                                    Franchisee ID:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="200px" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-info" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" AutoGenerateColumns="False" PagerStyle-CssClass="PagerStyle"
                            runat="server" EmptyDataText="No records found" AllowPaging="True" Width="100%"
                            PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Username" HeaderText="ID" />
                                <asp:BoundField DataField="VendorName" HeaderText="Name" />
                                <asp:BoundField DataField="confirmdate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                                <asp:BoundField DataField="credit" HeaderText="Credit" />
                                <asp:BoundField DataField="debit" HeaderText="Debit" />
                                <asp:BoundField DataField="desc" HeaderText="DESC" />
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
