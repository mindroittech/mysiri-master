<%@ Page Title="Product Requests" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="product-requests.aspx.cs" Inherits="admin_product_requests" %>

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
                    Dispatch Product
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <table>
                            <tr>
                                <td>
                                    <b style="font-weight: bold;">Username</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" Width="200px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="View All" OnClick="Button2_Click" />
                                </td>
                                 <td>
                                    <asp:Button ID="Button4" runat="server" Text="View in Excel" OnClick="Button4_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Label ID="Label3" runat="server" ForeColor="#009933" Text="Product Successfully assigned to the member"
                            Visible="False"></asp:Label>
                        &nbsp;<asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" ShowSummary="true"
                            runat="server"></asp:ValidationSummary>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="pid"
                            OnRowCommand="GridView1_RowCommand" Width="100%" CellPadding="3" CellSpacing="1"
                           AllowPaging="true" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" ItemStyle-Width="100px">
                                    <ItemStyle Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="plan" HeaderText="Plan" SortExpression="username" />
                                <asp:BoundField DataField="pin_number" HeaderText="PIN" SortExpression="username" />
                                <asp:BoundField DataField="joindate" HeaderText="Date" ItemStyle-HorizontalAlign="Left"
                                    DataFormatString="{0:dd/MMM/yyyy }">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="usedbyname" HeaderText="Used By" SortExpression="username" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Products
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProduct" runat="server" Width="120px">  </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtProduct"
                                            ForeColor="Red" ValidationGroup='<%# (Container.DataItemIndex+1)+"gridVal" %>'
                                            Font-Size="11px" runat="server" ErrorMessage="">*</asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Date
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDate" TextMode="Date" runat="server" Width="120px" Text='<%# DateTime.UtcNow.ToString("M/dd/yyyy") %>'>  </asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCost" ForeColor="Red" ValidationGroup='<%# (Container.DataItemIndex+1)+"gridVal" %>' Font-Size="11px" runat="server" ErrorMessage="Please enter product cost">*</asp:RequiredFieldValidator>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Shipment
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReceiver" runat="server" Width="110px">  </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Action</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkbutton" CommandName="send" CommandArgument='<%# Eval("pid") %>'
                                            ValidationGroup='<%# (Container.DataItemIndex+1)+"gridVal" %>'>Dispatch</asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Are you sure you want to dispatch?"
                                            Enabled="True" TargetControlID="lnkbutton">
                                        </asp:ConfirmButtonExtender>
                                        |
                                        <asp:LinkButton runat="server" ID="lnkbutton1" CommandName="dispatch" CommandArgument='<%# Eval("pid") %>'
                                            ValidationGroup='<%# (Container.DataItemIndex+1)+"gridVal" %>'>Dispatch&SMS</asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure you want to dispatch?"
                                            Enabled="True" TargetControlID="lnkbutton1">
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Button ID="button3" runat="server" Text="Ok" Style="display: none" />
                        <asp:ModalPopupExtender ID="ImageButton2_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="CancelButton" Enabled="True" PopupControlID="Panel2" TargetControlID="button3">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="Panel2" runat="server" CssClass="x_content table-wrapper" Style="display: none">
                            <div class="modal-content" style="max-height: 600px; overflow: auto">
                                <span>
                                    <asp:LinkButton ID="CancelButton" runat="server" CssClass="close1">&times;</asp:LinkButton></span>
                                <asp:GridView ID="GridView2" runat="server" EmptyDataText="No pins found" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="PlanName" HeaderText="Plan" />
                                        <asp:BoundField DataField="QtySent" HeaderText="Pins Sent" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
