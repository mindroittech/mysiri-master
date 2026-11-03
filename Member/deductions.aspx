<%@ Page Title="Deductions" Language="C#" MasterPageFile="~/Member/member.master" AutoEventWireup="true" CodeFile="deductions.aspx.cs" Inherits="Member_deductions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="br-pagebody">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header tx-medium bd-0 tx-white bg-primary">
                        Deductions
                    </div>
                    <!-- card-header -->
                    <div class="card-body bd bd-t-0 rounded-bottom">


                      <div class="table-responsive">
                            <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover dataTable no-footer"
                                AutoGenerateColumns="False" runat="server" AllowPaging="False" Font-Size="15px" Width="100%" EmptyDataText="No records found"
                                PageSize="50">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="desc" HeaderText="Deduction Type"/>
                                    <asp:BoundField DataField="confirmdate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy hh:59 tt}" />
                                    <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                                    <asp:BoundField DataField="debit" HeaderText="Debit (Rs)" DataFormatString="{0:0}" />
                                    <%--<asp:BoundField DataField="remarks" HeaderText="Remarks" />--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    

</asp:Content>

