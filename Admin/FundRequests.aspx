<%@ Page Title="Fund Requests" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="FundRequests.aspx.cs" Inherits="Admin_FundRequests" %>

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
                    Fund Requests
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label3" runat="server" ForeColor="#009933" Text="" Visible="False"></asp:Label>
                    <br />
                    <asp:Label ID="lbErrormsg" runat="server" Font-Size="Medium" ForeColor="Red" Text="No New Requests"
                        Visible="False"></asp:Label>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand"
                                    Width="100%" EmptyDataText="No new requests">
                                    <Columns>
                                        <asp:BoundField DataField="Username" HeaderText="Franchisee ID" SortExpression="username" />
                                        <asp:BoundField DataField="VendorName" HeaderText="Franchisee Name" SortExpression="username" />
                                        <asp:BoundField DataField="amountRequired" HeaderText="Req.Amount" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="paymode" HeaderText="Payment Mode" SortExpression="paymode" />
                                          <asp:BoundField DataField="createdDate" HeaderText="Req Date" SortExpression="name"
                                    DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" />
                                        <asp:TemplateField HeaderText="Counterfoil">
                                            <ItemTemplate>
                                                <a id="A1" href='<%# Eval("counterfoil") %>' runat="server" visible='<%# Eval("counterfoil")!=null %>'
                                                    target="_blank">View</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="transactionno" HeaderText="UTR" SortExpression="remarks" />
                                        <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Action</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" CommandName="approve" CommandArgument='<%#Eval("pid")%>'
                                                    runat="server">Approve</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" CommandName="reject" ForeColor="Red" CommandArgument='<%#Eval("pid")%>'
                                                    runat="server">Reject</asp:LinkButton>
                                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure want to Reject?"
                                                    Enabled="True" TargetControlID="LinkButton2">
                                                </asp:ConfirmButtonExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <asp:Button ID="button3" runat="server" Text="Ok" Style="display: none" />
                                <asp:ModalPopupExtender ID="ImageButton2_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                    CancelControlID="CancelButton" Enabled="True" PopupControlID="Panel2" TargetControlID="button3">
                                </asp:ModalPopupExtender>
                                <asp:Panel ID="Panel2" runat="server" CssClass="x_content table-wrapper" Style="display: none">
                                    <div class="modal-content" style="height: 220px; overflow: auto">
                                        <span>
                                            <asp:LinkButton ID="CancelButton" runat="server" CssClass="close1">&times;</asp:LinkButton></span>
                                        <br />
                                        <div id="Div1" class="row" runat="server">
                                            <div class="col-md-12">
                                                 <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">
                                                       UTR No :
                                                    </label>
                                                    <div class="col-sm-4">
                                                      <asp:Label ID="lblutr" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-5">
                                                      <asp:HyperLink ID="LinkButton3" runat="server" Text="View" Target="_blank" ForeColor="Blue" />
                                                       
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label">
                                                        Enter OTP
                                                    </label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:LinkButton ID="LinkButton1" ForeColor="Blue" runat="server" OnClick="GenerateOTP_Click">Click Here</asp:LinkButton>
                                                        to generate OTP
                                                        <br />
                                                        <asp:Label ID="lblOtpMessage" ForeColor="DarkGreen" Font-Size="12px" runat="server"
                                                            Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <asp:HiddenField ID="hdnReqId" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                                                        Text="Submit" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
