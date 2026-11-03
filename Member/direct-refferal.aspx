<%@ Page Title="Direct Refferals" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="direct-refferal.aspx.cs" Inherits="Member_direct_refferal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Direct Referrals
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" EmptyDataText="No direct referrals" AutoGenerateColumns="False" CssClass="table table-bordered" 
                            DataKeyNames="username" Width="100%" CellPadding="3" CellSpacing="1">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" SortExpression="username" />
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="leg" HeaderText="Placement" SortExpression="name" />
                                <%--<asp:BoundField DataField="status" HeaderText="Current Package" SortExpression="plan" />--%>
                                <asp:BoundField DataField="joindate" HeaderText="Join Date" DataFormatString="{0:dd/MMM/yyyy}"
                                    SortExpression="joindate" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!-- card-body -->
            </div>
            <!-- card -->
        </div>
    </div>
</asp:Content>
