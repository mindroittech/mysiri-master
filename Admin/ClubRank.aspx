<%@ Page Title="Rank Achievers" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" 
    CodeFile="ClubRank.aspx.cs" Inherits="Admin_ClubRank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                   Rank Achievers
                </p>
            </div>

            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <span id="lbErrormsg" runat="server" visible="false" class="text-warning"></span>
                    <div class="table-responsive">
                        <table align="center" border="0" cellpadding="2" cellspacing="2">
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
                                    ID/Rank
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" Width="200px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"
                                        OnClick="Button1_Click" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Show All</asp:LinkButton>
                                </td>
                                  <td>
                                    
                           
                                <asp:Button ID="btnEXPORT" runat="server" Style="float: right;" Text="View in Excel"
                                    CssClass="btn btn-info" OnClick="btnEXPORT_Click"/>             
                          
                                   </td>                  
                             
                            </tr>
                        </table>
                        <br />
                        <div class="col-md-12" style="max-height: 650px; overflow: auto">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                EmptyDataText="No records found" CellSpacing="1" CssClass="two-axis" Width="100%"
                                PageSize="50" HeaderStyle-ForeColor="White" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            No.</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="username" HeaderText="User Id" SortExpression="initialTransfer" />
                                    <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                    <asp:BoundField DataField="mobile" HeaderText="Mobile No" SortExpression="name" />
                                    <asp:BoundField DataField="dateAchieved" HeaderText="Achieved On" DataFormatString="{0:dd-MMM-yyyy}" Visible="false" />
                                    <asp:BoundField DataField="RankName" HeaderText="Club Rank" SortExpression="package" Visible="false" />
                                    <asp:BoundField DataField="CurrentRank" HeaderText="Current Rank" />
                                </Columns>
                                <HeaderStyle BackColor="#1f5fc0" />
                            </asp:GridView>
                        </div>
                    </div>
                
            </div>
                </div>
        </div>
    </div>
</asp:Content>
