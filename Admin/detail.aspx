<%@ Page Title="" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="detail.aspx.cs" Inherits="admin_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Details</title>
    <style type="text/css">
        .style14
        {
            border-style: solid;
            border-width: 1px;
            margin-left: 60px;
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Bank Details
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <table cellpadding="5" style="width: 50%">
                        <tr class="danger">
                            <td>
                                Member ID
                            </td>
                            <td>
                                <asp:Label ID="lblID" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="danger">
                            <td>
                                Member Name
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="danger">
                            <td>
                                Bank Name
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="danger">
                            <td>
                                Branch
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="danger">
                            <td>
                                Acc No
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr class="danger">
                            <td>
                                IFSC
                            </td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
