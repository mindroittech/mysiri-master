<%@ Page Language="C#" AutoEventWireup="true" CodeFile="export.aspx.cs" EnableEventValidation="false"
    Inherits="admin_export" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


.Grid {background-color: #fff; margin: 5px 0px 10px 10px; 
border: solid 1px #525252; border-collapse:collapse; color: #474747;
}
table{border-collapse:collapse;border-spacing:0; } 

table {
  border-collapse: collapse;
  border-spacing: 0;
}

a { color: #0070A6; }
	
a { color:#323232; text-decoration:none; }
a

{
  text-decoration:none;

}

.HeaderStyle, .PagerStyle /*Common Styles*/
{
   
    background-position:center;
    background-repeat:repeat-x;
    background-color:#0a4775;
	border-bottom:solid 1px #999;
	
}
        .style1
        {
            width: 98%;
            border-collapse: collapse;
            font-size: 100%;
            vertical-align: baseline;
            border-style: none;
            border-color: inherit;
            border-width: 0;
            margin: 0;
            padding: 0;
        }
        .style2
        {
            font-size: 100%;
            vertical-align: baseline;
            border-style: none;
            border-color: inherit;
            border-width: 0;
            margin: 0;
            padding: 0;
        }
        .style3
        {
            font-size: 100%;
            vertical-align: baseline;
            font-weight: normal;
            font-family: OpenSansRegular;
            border-style: none;
            border-color: inherit;
            border-width: 0;
            margin: 0;
            padding: 0;
            background:;
        }
        .style4
        {
            width: 140px;
            font-size: 100%;
            vertical-align: baseline;
            border-style: none;
            border-color: inherit;
            border-width: 0;
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="Grid"
            PagerStyle-CssClass="PagerStyle" DataKeyNames="username" Width="98%">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        SNo.</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="username" HeaderText="Username" SortExpression="username" />
                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                <asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="name" />
                <asp:BoundField DataField="RankName" HeaderText="Club Rank" SortExpression="cashavailable" />
                <asp:BoundField DataField="amount" HeaderText="Amount Requested" SortExpression="amount" />
                <asp:BoundField DataField="tax" HeaderText="TDS(5%)" />
                <asp:BoundField DataField="sc" HeaderText="Admin Charges(5%)" />
                <asp:BoundField DataField="deduction" HeaderText="Deduction" />
                <asp:BoundField DataField="netpay" HeaderText="NetPay" />
                <asp:BoundField DataField="date" HeaderText="Date" SortExpression="date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="account" HeaderText="Acc No" SortExpression="accno" />
                <asp:BoundField DataField="bankname" HeaderText="Bank Name" SortExpression="bankname" />
                <asp:BoundField DataField="branch" HeaderText="Branch" SortExpression="branch" />
                <asp:BoundField DataField="ifsc" HeaderText="Ifsc" SortExpression="ifsc" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
