<%@ Page Title="TDS Report" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="tds-report.aspx.cs" Inherits="Admin_tds_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    TDS Report
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table style="width: 110%">
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True">Individual</asp:ListItem>
                                        <asp:ListItem>Summary</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMinAmount" Style="max-width: 90px" placeholder="Min" TextMode="Number"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMaxAmount" Style="max-width: 90px" placeholder="Max" TextMode="Number"
                                        runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtID" placeholder="User ID" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                        &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="" Visible="False"></asp:Label>
                        <br />
                        <asp:Button ID="Button2" Style="float: right;" Visible="false" runat="server" Text="Export to Excel"
                            CssClass="btn btn-primary mr-2" OnClick="Button2_Click" /><br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PagerStyle-CssClass="PagerStyle"
                            EmptyDataText="No records found" PageSize="50" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="paiddate" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Date"
                                    SortExpression="paiddate"></asp:BoundField>
                                <asp:BoundField DataField="pan" HeaderText="PAN Number" />
                                <asp:BoundField DataField="tax" HeaderText="TDS Deducted" />
                                <asp:BoundField DataField="username" HeaderText="Username" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                <asp:BoundField DataField="amount" HeaderText="Total Income" />
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
