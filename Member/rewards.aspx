<%@ Page Title="Achievement Awards" Language="C#" MasterPageFile="~/Member/member.master" AutoEventWireup="true"
    CodeFile="rewards.aspx.cs" Inherits="Member_rewards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Achievement Awards
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="table table-bordered" 
                            EmptyDataText="No awards found" CellSpacing="1"  Width="100%"
                            PageSize="50" HeaderStyle-ForeColor="White" AllowPaging="false">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        No.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Date" HeaderText="Achieved Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="Match" HeaderText="BV Match" SortExpression="package" />
                                <asp:BoundField DataField="Award" HeaderText="Rewards" ReadOnly="True" SortExpression="pinnumbers" />
                                <asp:BoundField DataField="Bonus" HeaderText="Bonus" ReadOnly="True" SortExpression="pinnumbers" />
                                <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Font-Bold="true"
                                    SortExpression="pinnumbers" />
                                <asp:BoundField DataField="senddate" HeaderText="Received Date" DataFormatString="{0:dd-MMM-yyyy}"
                                    SortExpression="pinnumbers" />
                            </Columns>
                            <HeaderStyle BackColor="#1f5fc0" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
