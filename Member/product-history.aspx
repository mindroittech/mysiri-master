<%@ Page Title="Product History" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="product-history.aspx.cs" Inherits="product_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Products
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <br />
                    <asp:GridView ID="GridView1" EmptyDataText="No products found" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" 
                        DataKeyNames="username" Width="70%" CellPadding="3" CellSpacing="1" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="product" HeaderText="Product Name" SortExpression="product" />
                            <asp:BoundField DataField="cost" HeaderText="Product Cost (Rs)" SortExpression="cost " />
                            <asp:BoundField DataField="senddate" HeaderText="Received Date" SortExpression="senddate"
                                DataFormatString="{0:dd/MMM/yyyy}" />
                            <asp:BoundField DataField="shipment" HeaderText="Shipment Details" SortExpression="name" />
                        </Columns>
                    </asp:GridView>
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
