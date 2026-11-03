<%@ Page Title="Franchisees List" Language="C#" MasterPageFile="~/Member/member.master"
    AutoEventWireup="true" CodeFile="franchisees.aspx.cs" Inherits="Member_franchisees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
    <script>
        $(function () {
            $('#filter').keyup(function () {
                var val = $(this).val().toUpperCase();

                $('#<%= GridView1.ClientID%>> tbody > tr').not(':first').each(function (index, element) {
                    if ($(this).text().toUpperCase().indexOf(val) < 0)
                        $(this).hide();
                    else
                        $(this).show();
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    View Franchisees
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-4">
                    <label for="lname">
                        Search
                    </label>
                    <input type="text" class="form-control" id="filter" tabindex="0" />
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="table table-bordered" 
                            Width="100%" EmptyDataText="No franchisees found">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="VendorName" HeaderText="Franchisee Name" ReadOnly="True"
                                    SortExpression="username" />
                                <asp:BoundField DataField="Phone" HeaderText="Mobile" SortExpression="name" />
                                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="joindate" />
                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="plan" />
                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="plan" />
                                <asp:BoundField DataField="Pincode" HeaderText="PIN" SortExpression="joindate" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
