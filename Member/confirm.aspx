<%@ Page Title="" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="confirm.aspx.cs" Inherits="confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Confirmation</title>
    <style type="text/css">
        .style1
        {
            font-size: 22px;
            color: orange;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <br />
        <div class="col-md-12">
            <div class="card" style="padding: 21px;">
                <div class="card-body">
                    <asp:Panel ID="Panel1" runat="server" BackColor="#f0f5f5" Style="min-height: 250px"
                        Visible="False">
                        <div style="float: left; margin: 8px 10px; font-size: 14px;">
                            <span class="style1">Congratuations, You have successfully registered
                                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                                &nbsp;to
                                <%= ConfigurationManager.AppSettings["companyname"]%><br />
                                <br />
                            </span>
                            <div style="font-size: 15px; font-weight: bold">
                                Login Information has been sent to user&#39;s registered Mobile Number
                                <br />
                                Please collect your product from the person who sent PINs to you
                                <br />
                                <br />
                                User ID:
                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                                <br />
                                Password:
                                <asp:Label ID="lblPassword" runat="server" Text=""></asp:Label>
                                <br />
                                <br />
                                <a href="genealogy.aspx">View Tree</a> &nbsp; &nbsp;&nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
