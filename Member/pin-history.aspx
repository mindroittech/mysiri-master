<%@ Page Title="Pin History" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="pin-history.aspx.cs" Inherits="pin_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    PIN History
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <h4 class="card-title" style="color: #0072bc;">
                        Pins Transfered By Me</h4>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="pinnumbers" CssClass="table table-bordered" 
                            HeaderStyle-ForeColor="White" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="pinnumbers" HeaderText="No Of Pins" ReadOnly="True" SortExpression="pinnumbers" />
                                <asp:BoundField DataField="issued" HeaderText="Transfered To" SortExpression="issued" />
                                <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd/MMM/yyyy}" />
                                <asp:BoundField DataField="package" HeaderText="Plan" SortExpression="package" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <br />
                    <h4 class="card-title" style="color: #0072bc;">
                        Pins Recieved By Me</h4>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView2" runat="server" HeaderStyle-ForeColor="White" AutoGenerateColumns="False" CssClass="table table-bordered" 
                            DataKeyNames="pinnumbers" OnRowDataBound="GridView2_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO0" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="pinnumbers" HeaderText="No Of Pins" ReadOnly="True" SortExpression="pinnumbers" />
                                <asp:BoundField DataField="transferby" HeaderText="Recieved From" />
                                <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd/MMM/yyyy}" />
                                <asp:BoundField DataField="package" HeaderText="Plan" SortExpression="package" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
