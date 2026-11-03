<%@ Page Title="Fund Request History" Language="C#" MasterPageFile="~/Admin/admin.master"
    AutoEventWireup="true" CodeFile="vFundHistory.aspx.cs" Inherits="Admin_vFundHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Fund Request History
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            CssClass="table table-bordered" CellSpacing="1" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging"
                            PageSize="50" HeaderStyle-BackColor="#1f5fc0" HeaderStyle-ForeColor="White">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="createdDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                                <asp:BoundField DataField="amountRequired" HeaderText="Req.Amount (Rs)" SortExpression="amount" />
                                <asp:BoundField DataField="paymode" HeaderText="Payment Mode" SortExpression="paymode" />
                                <asp:TemplateField HeaderText="Proof">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# Eval("counterfoil") %>' Target="_blank"
                                            runat="server" Visible='<%# Eval("counterfoil")!=null  %>'>View</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
                                <asp:BoundField DataField="status" ItemStyle-Font-Bold="true" HeaderText="Status"
                                    SortExpression="remarks" />
                                <asp:BoundField DataField="ActionOn" HeaderText="Action On" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                            </Columns>
                            <HeaderStyle CssClass="bg-primary" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
