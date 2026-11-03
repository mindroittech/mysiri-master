<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="dashboard.aspx.cs" Inherits="admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Begin Page Content -->
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">
                Admin Dashboard</h1>
        </div>
        <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
            border-radius: 3px; padding: 15px;">
            <marquee style="color: #f3b764; font-size: 22px;">Welcome Back Admin</marquee>
        </div>
        <!-- Content Row -->
        <div class="row" style="margin-top: 50px;" id="divAdmin" runat="server">
            <!-- Content Column -->
            <div class="col-lg-12 mb-4">
                <div class="row">
                    <div class="col-lg-3 mb-4">
                        <a href="admin.aspx">
                            <div class="card bg-primary text-white shadow" style="background: #00b297 !important;">
                                <div class="card-body">
                                    PIN REQUESTS
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        <asp:Label ID="lblPin" runat="server" Text="0"></asp:Label></div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="view-members.aspx">
                            <div class="card bg-success text-white shadow" style="background: #DC3545!important;">
                                <div class="card-body">
                                    VIEW MEMBERS
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="withdraw-request.aspx?p=Auto Pay">
                            <div class="card bg-info text-white shadow" style="background: #0866C6 !important;">
                                <div class="card-body">
                                    WITHDRAW REQUESTS
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="generate-pin.aspx">
                            <div class="card bg-warning text-white shadow" style="background: #1D2939 !important;">
                                <div class="card-body">
                                    GENERATE PIN
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-lg-12 mb-4">
                <div class="row">
                    <div class="col-lg-5 mb-4">
                        <div style="border: 2px solid black; background: white; padding: 10px; height: 250px">
                            <h6>
                                <center>
                                    STOCK ALERTS
                                </center>
                            </h6>
                            <marquee direction="up" scrollamount="2" style='height: 200px'>
                            <asp:Repeater ID="Repeater1" runat="server">
                       
                            <ItemTemplate>
                          
                      
                         
                          <span>  <%# Eval("ProductName")%> </span> &emsp;  <span style="float:right"> <%# Eval("Quantity")%> </span>
                            </ItemTemplate>
                          
                            <SeparatorTemplate>
                            <br />
                            </SeparatorTemplate>
                            </asp:Repeater>
                            </marquee>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </div>
    <!-- End of Main Content -->
</asp:Content>
