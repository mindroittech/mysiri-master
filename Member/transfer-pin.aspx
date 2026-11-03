<%@ Page Title="Transfer Pin" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="transfer-pin.aspx.cs" Inherits="transfer_pin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Transfer PIN
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <table border="1" cellpadding="3" cellspacing="3" width="86%">
                        <tr>
                            <td style="width: 120px">
                                <strong>Plan&nbsp;</strong>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <strong>Plan Amount </strong>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                                </td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>Transfer To Username </strong>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <strong>NAME</strong>
                            </td>
                            <td>
                                <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="Label14" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <br />
                    Pin Details<font color="#ff0000" size="1" style="font-weight: bold"><sup> +</sup></font>
                    <br />
                    <table border="0" cellpadding="0" cellspacing="0" class="tabline insideTDBgcolor"
                        width="86%">
                        <tr>
                            <td align="left" valign="top">
                                &nbsp;<asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" DataTextField="pinnumbers"
                                    DataValueField="pinnumbers" Height="184px" Width="190px" SelectionMode="Multiple">
                                </asp:ListBox>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Add &gt;&gt;" />
                                &nbsp;
                                <asp:Button ID="Button2" runat="server" Text="&lt;&lt; Remove" OnClick="Button2_Click" />
                                &nbsp;<asp:ListBox ID="ListBox2" runat="server" Height="184px" Width="190px" SelectionMode="Multiple">
                                </asp:ListBox>
                                &nbsp; <strong style="font-weight: bold;">Total Pins:</strong>
                                <asp:Label ID="Label13" runat="server" Text="0"></asp:Label>
                                <asp:Button ID="Button3" runat="server" Text="Assign Pin(s)" OnClick="Button3_Click"
                                    CssClass="btn btn-primary mr-2" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LotusBizConnectionString %>"
                        SelectCommand="SELECT [pinnumbers] FROM [PinManagaer] WHERE (([issued] = @issued) AND ([status] = @status) AND ([package] = @package))">
                        <SelectParameters>
                            <asp:SessionParameter Name="issued" SessionField="username" Type="String" />
                            <asp:Parameter DefaultValue="Not used" Name="status" Type="String" />
                            <asp:ControlParameter ControlID="Label8" Name="package" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
