<%@ Page Title="PIN Requests" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="admin.aspx.cs" Inherits="admin_admin" %>

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
                    PIN Requests
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label3" runat="server" ForeColor="#009933" Text="" Visible="False"></asp:Label>
                    <br />
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2"
                            OnRowCommand="GridView1_RowCommand" Width="100%" EmptyDataText="No new requests"
                            OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                            <Columns>
                                <asp:BoundField DataField="createdDate" HeaderText="Req Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="username" />
                                <asp:BoundField DataField="amount" HeaderText="Amount Paid" SortExpression="amount" />
                                <asp:BoundField DataField="paymode" HeaderText="Payment Mode" SortExpression="paymode" />
                                <asp:BoundField DataField="transactionno" HeaderText="UTR" SortExpression="amount" />
                                <asp:TemplateField HeaderText="Counterfoil">
                                    <ItemTemplate>
                                        <a href='<%# Eval("counterfoil") %>' runat="server" visible='<%# Eval("counterfoil")!=null %>'
                                            target="_blank">View</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" SortExpression="remarks" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px">
                                    <HeaderTemplate>
                                        Action</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" CommandName="assign" CommandArgument='<%#Eval("pid")%>'
                                            runat="server">Assign Pin</asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="LinkButton2" CommandName="rem" CommandArgument='<%#Eval("pid")%>'
                                            runat="server">Remove</asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="Are you sure?"
                                            Enabled="True" TargetControlID="LinkButton2">
                                        </asp:ConfirmButtonExtender>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LotusBizConnectionString %>"
                            SelectCommand="(SELECT pid, p.username, noofPins, p.amount, t.name, contact, paymode,PlanName, remarks, bank, counterfoil, [transactionno],p.createdDate 
                            FROM PinRequest p inner join tblMember t on p.username=t.username WHERE (p.status = @status) Union 
                           SELECT pid, p.username, noofPins, p.amount, t.VendorName as name, contact, paymode,PlanName, remarks, bank, counterfoil, [transactionno],p.createdDate
                            FROM PinRequest p inner join tblVendor t on p.username=t.Username WHERE (p.status = @status) )">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="Pending" Name="status" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />
                        <center>
                            <asp:Label ID="Label2" runat="server" Font-Size="Medium" ForeColor="#993300" Text="No New Requests"
                                Visible="False"></asp:Label></center>
                    </div>
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
