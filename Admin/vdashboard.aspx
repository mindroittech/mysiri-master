<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="vdashboard.aspx.cs" Inherits="Admin_vdashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">
                Dashboard</h1>
        </div>
        <%--   <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
            border-radius: 3px; padding: 15px;">
            <marquee style="color: #f3b764; font-size: 22px;"></marquee>
        </div>--%>
        <!-- Content Row -->
        <div class="row" style="margin-top: 50px;">
            <div class="col-md-12 mb-4">
                <div class="row">
                    <div class="col-lg-3 mb-4">
                        <div class="card bg-primary text-white shadow">
                            <div class="card-body">
                                My ID: <span>
                                    <asp:Label ID="lblID" runat="server" Text="0"></asp:Label></span>
                                <p>
                                </p>
                                Name: <span>
                                    <asp:Label ID="lblName" runat="server" Text="0"></asp:Label></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="vStock.aspx">
                            <div class="card bg-primary text-white shadow" style="background: #F39C12 !important;">
                                <div class="card-body">
                                    Products
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="vCreateOrder.aspx">
                            <div class="card bg-primary text-white shadow" style="background: #00C0EF !important;">
                                <div class="card-body">
                                    New Order
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="#">
                            <div class="card bg-primary text-white shadow" style="background: Green !important;">
                                <div class="card-body">
                                    Referral Commission
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        Rs
                                        <asp:Label ID="lblKitCommission" runat="server" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="#">
                            <div class="card bg-primary text-white shadow" style="background: #F39C12 !important;">
                                <div class="card-body">
                                    Repurchase Commission
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        Rs
                                        <asp:Label ID="lblRepurchase" runat="server" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="#">
                            <div class="card bg-primary text-white shadow" style="background: #00C0EF !important;">
                                <div class="card-body">
                                    Cash Wallet
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        Rs
                                        <asp:Label ID="lblCashWallet" runat="server" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-lg-3 mb-4" id="vgodown" runat="server">
                          <a href="StockRequests.aspx">
                        <div class="card bg-primary text-white shadow" style="background: Green !important;">
                            <div class="card-body">
                                Stock Requests
                                <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                    <asp:Label ID="lblStockReq" runat="server" Text="0"></asp:Label></div>
                            </div>
                        </div>
                              </a>
                    </div>
                    <div class="col-lg-3 mb-4">
                        <a href="vFreeOrder.aspx">
                            <div class="card bg-primary text-white shadow" style="background: Green !important;">
                                <div class="card-body">
                                    Free Order
                                    <div class="text-white-50 small" style="color: #fff; font-size: 20px;">
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                     <div class="col-lg-3 col-md-6 mb-4">
                        <div class="card text-white shadow h-100"
                             style="background: Green !important;">
                            <div class="card-body d-flex flex-column justify-content-center">

                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <strong>Total Stock Value:</strong>
                                    <asp:Label ID="lblstocks" runat="server"
                                        Text="0" CssClass="font-weight-bold ml-2"></asp:Label>
                                </div>

                                <div class="d-flex justify-content-between align-items-center">
                                    <strong>Total BV Value:</strong>
                                    <asp:Label ID="lblbv" runat="server"
                                        Text="0" CssClass="font-weight-bold ml-2"></asp:Label>
                                </div>

                            </div>
                        </div>
                </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
