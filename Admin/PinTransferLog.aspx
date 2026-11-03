<%@ Page Title="Pin Transfer Log" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="PinTransferLog.aspx.cs" Inherits="Admin_PinTransferLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    PIN Transfer Log
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table align="center" border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    Transfered From:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="150px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    Transfered To:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" Width="150px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"
                                        OnClick="Button1_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            CssClass="table table-bordered" CellSpacing="1" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="50" HeaderStyle-BackColor="#1f5fc0" HeaderStyle-ForeColor="White">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="transferDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                                <asp:BoundField DataField="package" HeaderText="Plan" SortExpression="package" />
                                <asp:BoundField DataField="TransferedFrom" HeaderText="Transfered From" ReadOnly="True"
                                    SortExpression="pinnumbers" />
                                <asp:BoundField DataField="TransferedTo" HeaderText="Transfered To" ReadOnly="True"
                                    SortExpression="pinnumbers" />
                                <asp:BoundField DataField="pins" HeaderText="Pins" SortExpression="status" />
                                <asp:BoundField DataField="UTR" HeaderText="UTR" SortExpression="status" />
                            </Columns>
                            <HeaderStyle CssClass="bg-primary" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
