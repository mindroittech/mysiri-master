<%@ Page Title="Sponsor PV Report" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="sponsorBV.aspx.cs" Inherits="Admin_Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                  Direct Sponsor (Binary) PV Report

                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="table-responsive">
                         <table style="width: 100%">
                            <tr>
                               
                                <td>
                                    <b style="font-weight: bold;">Start Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" Width="140px" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <b style="font-weight: bold;">End Date</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" Width="140px" TextMode="Date" runat="server"></asp:TextBox>
                                </td>
                                  <td>
                                    PV
                                 <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                                </td>
                                <td>
                                     <asp:Button ID="Button2" runat="server" Style="float: right;" Text="View in Excel"
                                    CssClass="btn btn-info" OnClick="Button2_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                   
                </div>
                
                <div class="col-md-12">
                    <div class="table-responsive">
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            EmptyDataText="Repurchase rank not achieved" CellSpacing="1" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="id" HeaderText="Member ID" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="name" HeaderText="Member Name" SortExpression="initialTransfer" />
                                <asp:BoundField DataField="mobile" HeaderText="Mobile No" SortExpression="name" />
                                <asp:BoundField DataField="pv" HeaderText="Direct PV" SortExpression="name" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        return true;
    }
</script>
</asp:Content>
