<%@ Page Title="Package Summary" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="package-summary.aspx.cs" Inherits="Member_package_summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="br-pagebody">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header tx-medium bd-0 tx-white bg-primary">
                        Package Detail Summary
                    </div>
                    <!-- card-header -->
                    <div class="card-body bd bd-t-0 rounded-bottom">
                        <br>
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1"
                                runat="server" EmptyDataText="" AutoGenerateColumns="False" DataKeyNames="username" CssClass="table table-bordered" 
                                Width="100%" CellPadding="3" CellSpacing="1">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SNo.</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Package" HeaderText="Package" SortExpression="plan" />
                                    <asp:BoundField DataField="activestatus" HeaderText="Active Status" SortExpression="plan" />
                                    <asp:BoundField DataField="date" HeaderText="Active Date" DataFormatString="{0:dd/MMM/yyyy}"
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
        <!-- row -->
    </div>
    <!-- br-pagebody -->
</asp:Content>
