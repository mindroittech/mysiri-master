<%@ Page Title="My Tree" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="genealogy.aspx.cs" Inherits="genealogy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .tooltip1
        {
            position: relative;
            display: inline-block;
        }
        
        .tooltip1 .tooltiptext
        {
            font-size: 12px;
            visibility: hidden;
            width: 240px;
            top: 50%;
            left: 0%;
            background-color: Teal;
            margin-left: -50px;
            text-align: center;
            border-radius: 6px;
            padding: 5px; /* Position the tooltip */
            position: absolute;
            z-index: 1;
        }
        
        .tooltip1:hover .tooltiptext
        {
            visibility: visible;
        }
        
        
        .tbl
        {
            width: 100%;
            border: none;
        }
        
        
        .tbl th, .tbl td
        {
            text-align: center;
            color: Black;
            border: none;
        }
        
        .tbl th, .tbl td, .tbl tr td
        {
            background: white;
        }
        
        .tbl th, .tbl td, .tbl tr
        {
            border-bottom: none;
        }
        
        .td1
        {
            vertical-align: top;
            width: 25%;
        }
        
        #chain3
        {
            height: 30px;
        }
        #chain4
        {
            height: 33px;
        }
        #chain5
        {
            height: 33px;
        }
        #chain6
        {
            height: 33px;
        }
        .style1
        {
            text-align: center;
            width: 100%;
            margin-top: 0px;
        }
        
        .style1 th, .style1 td
        {
            margin: 0;
            padding: 2px;
        }
        .style2 th, .style2 td
        {
            text-align: left;
            margin: 0;
            padding: 2px;
        }
        .style2
        {
            text-align: left;
            width: 100%;
            margin: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    GENEALOGY
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label10" runat="server" Text="" Visible="False" Font-Size="Small"
                        ForeColor="Red"></asp:Label>
                    <div class="row">
                        <div class="col-sm-3">
                        </div>
                        <label class="col-sm-1 control-label">
                            Search</label>
                        <div class="col-sm-4">
                            <input type="text" name="search" runat="server" id="search" class="form-control">
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="btn btn-info"
                                Text="Search" />
                        </div>
                    </div>
                </div>
                <!-- card-body -->
                <br />
                <table cellpadding="2" cellspacing="2" class="tbl">
                    <tr>
                        <td colspan="8">
                            <div runat="server" id="Image1">
                            </div>
                            <div id="chain" class='tooltip1' runat="server">
                                <asp:LinkButton ID="LinkButton1" runat="server" Enabled="False">Empty</asp:LinkButton></div>
                            <div style="border-style: solid solid none solid; margin-left: 25%; border-width: 2px;
                                width: 50%; height: 22px;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="float: none; width: 50%; vertical-align: top">
                            <%--  <asp:Image ID="Image2" ImageUrl="~/Image/empty.png"   runat="server" 
                        CssClass="circleBase" />--%>
                            <div runat="server" id="Image2">
                                <i class="fa fa-user" aria-hidden="true" style="font-size: 58px; color: #FFC107"></i>
                            </div>
                            <span id="chain1" class='tooltip1' runat="server">
                                <asp:LinkButton ID="LinkButton2" title='Add New Member' runat="server" Enabled="False">Empty</asp:LinkButton>
                                <br />
                            </span>
                            <div style="border-style: solid solid none solid; margin-left: 25%; border-width: 2px;
                                width: 50%; height: 20px;">
                            </div>
                        </td>
                        <td colspan="4" style="width: 50%; vertical-align: top">
                            <%-- <asp:Image ID="Image3" ImageUrl="~/Image/empty.png"   runat="server" 
                        CssClass="circleBase" />--%>
                            <div runat="server" id="Image3">
                                <i class="fa fa-user" aria-hidden="true" style="font-size: 58px; color: #FFC107"></i>
                            </div>
                            <span id="chain2" class='tooltip1' runat="server">
                                <asp:LinkButton ID="LinkButton3" runat="server" Enabled="False">Empty</asp:LinkButton><br />
                            </span>
                            <div style="border-style: solid solid none solid; margin-left: 25%; border-width: 2px;
                                width: 50%; height: 20px;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="td1">
                            <%-- <asp:Image ID="Image4" ImageUrl="~/Image/empty.png"   runat="server" 
                        CssClass="circleBase"  />--%>
                            <div runat="server" id="Image4">
                                <i class="fa fa-user" aria-hidden="true" style="font-size: 58px; color: #FFC107"></i>
                            </div>
                            <span id="chain3" class='tooltip1' runat="server">
                                <asp:LinkButton ID="LinkButton4" runat="server" title='Add New Member' Enabled="False">Empty</asp:LinkButton>
                                <br />
                                <br />
                            </span>
                            <%-- <div style="border-style: solid solid none solid; margin-left:25%; border-width: 2px; width:50%; height: 22px;"></div>--%>
                        </td>
                        <td colspan="2" class="td1">
                            <%-- <asp:Image ID="Image5" ImageUrl="~/Image/empty.png"   runat="server" 
                        CssClass="circleBase" />--%>
                            <div runat="server" id="Image5">
                                <i class="fa fa-user" aria-hidden="true" style="font-size: 58px; color: #FFC107"></i>
                            </div>
                            <span id="chain4" class='tooltip1' runat="server">
                                <asp:LinkButton ID="LinkButton5" title='Add New Member' runat="server" Enabled="False">Empty</asp:LinkButton>
                                <br />
                                <br />
                            </span>
                            <%-- <div style="border-style: solid solid none solid; margin-left:25%; border-width: 2px; width:50%; height: 22px;"></div>--%>
                        </td>
                        <td colspan="2" class="td1">
                            <%-- <asp:Image ID="Image6" ImageUrl="~/Image/empty.png"   runat="server" 
                        CssClass="circleBase" />--%>
                            <div runat="server" id="Image6">
                                <i class="fa fa-user" aria-hidden="true" style="font-size: 58px; color: #FFC107"></i>
                            </div>
                            <span id="chain5" class='tooltip1' runat="server">
                                <asp:LinkButton ID="LinkButton6" title='Add New Member' runat="server" Enabled="False">Empty</asp:LinkButton>
                                <br />
                                <br />
                            </span>
                            <%-- <div style="border-style: solid solid none solid;margin-left:25%; border-width: 2px; width:50%; height: 22px;"></div>--%>
                        </td>
                        <td colspan="2" class="td1">
                            <%--      <asp:Image ID="Image7" ImageUrl="~/Image/empty.png"   runat="server" 
                        CssClass="circleBase" />--%>
                            <div runat="server" id="Image7">
                                <i class="fa fa-user" aria-hidden="true" style="font-size: 58px; color: #FFC107"></i>
                            </div>
                            <span id="chain6" class='tooltip1' runat="server">
                                <asp:LinkButton ID="LinkButton7" title='Add New Member' runat="server" Enabled="False">Empty</asp:LinkButton>
                                <br />
                                <br />
                            </span>
                            <%--<div style="border-style: solid solid none solid;margin-left:25%; border-width: 2px; width:50%; height: 22px;"></div>--%>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
            </div>
            <!-- card -->
        </div>
    </div>
</asp:Content>
