<%@ Page Title="Pin Req History" Language="C#" MasterPageFile="~/Member/member.master"
    AutoEventWireup="true" CodeFile="PinReqHistory.aspx.cs" Inherits="Member_PinReqHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    PIN Request History
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            CssClass="table table-bordered" CellSpacing="1" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowCommand="GridView1_RowCommand" PageSize="50" HeaderStyle-BackColor="#1f5fc0"
                            HeaderStyle-ForeColor="White">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="createdDate" HeaderText="Request Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        No Of Pins
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton5" CommandName="pins" CommandArgument='<%# Eval("pid") %>'
                                            runat="server" ToolTip="View Pins">View</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount" />
                                <asp:BoundField DataField="PlanName" HeaderText="Plan" />
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="remarks" />
                                <asp:BoundField DataField="paymode" HeaderText="Payment Mode" SortExpression="paymode" />
                                <asp:TemplateField HeaderText="Proof">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("counterfoil") %>' Target="_blank"
                                            runat="server" Visible='<%# Eval("counterfoil")!=null  %>'>View</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
                            </Columns>
                            <HeaderStyle CssClass="bg-primary" />
                        </asp:GridView>
                        <asp:Button ID="button3" runat="server" Text="Ok" Style="display: none" />
                        <asp:ModalPopupExtender ID="ImageButton2_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                            CancelControlID="CancelButton" Enabled="True" PopupControlID="Panel2" TargetControlID="button3">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="Panel2" runat="server" CssClass="x_content table-wrapper" Style="display: none">
                            <div class="modal-content" style="max-height: 600px; overflow: auto">
                                <span>
                                    <asp:LinkButton ID="CancelButton" runat="server" CssClass="close1">&times;</asp:LinkButton></span>
                                <asp:GridView ID="GridView2" runat="server" EmptyDataText="No pins found" AutoGenerateColumns="False" CssClass="table table-bordered" >
                                    <Columns>
                                        <asp:BoundField DataField="PlanName" HeaderText="Plan" />
                                        <asp:BoundField DataField="QtyReq" HeaderText="Req Pins" />
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
