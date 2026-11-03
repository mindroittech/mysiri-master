<%@ Page Title="Pin Manager" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="pinbank.aspx.cs" Inherits="pinhistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    PIN BANK
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="row">
                        <div class=" col-md-6 form-group row">
                            <label class="col-sm-3 col-form-label">
                                Select Plan</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="drpPlan" CssClass="form-control" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 four-grid">
                            <div class="four-agileits">
                                <div class="icon">
                                    <i class="glyphicon glyphicon-tag" aria-hidden="true"></i>
                                </div>
                                <div class="four-text">
                                    <h5>
                                        Total Pins</h5>
                                    <h4>
                                        <asp:Label ID="Label8" ForeColor="Red" runat="server" Text=""></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 four-grid">
                            <div class="four-agileinfo">
                                <div class="icon">
                                    <i class="glyphicon glyphicon-tag" aria-hidden="true"></i>
                                </div>
                                <div class="four-text">
                                    <h5>
                                        Total Used Pins</h5>
                                    <h4>
                                        <asp:Label ID="Label9" ForeColor="Red" runat="server" Text=""></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 four-grid">
                            <div class="four-w3ls">
                                <div class="icon">
                                    <i class="glyphicon glyphicon-tag" aria-hidden="true"></i>
                                </div>
                                <div class="four-text">
                                    <h5>
                                        Total Unused Pins</h5>
                                    <h4>
                                        <asp:Label ID="Label1" ForeColor="Red" runat="server" Text=""></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" 
                            DataKeyNames="pinnumbers" Width="100%" PageSize="50" OnRowDataBound="GridView1_RowDataBound" HeaderStyle-BackColor="#1f5fc0"
                            HeaderStyle-ForeColor="White" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="pinnumbers" HeaderText="Pin Number" ReadOnly="True" SortExpression="pinnumbers" />
                                <asp:BoundField DataField="package" HeaderText="Plan" SortExpression="package" />
                                <asp:BoundField DataField="date" HeaderText="Assigned date" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />
                                <asp:BoundField DataField="usedby" HeaderText="Used By" />
                                <asp:BoundField DataField="transferby" HeaderText="Transfer By" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
