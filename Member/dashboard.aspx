<%@ Page Title="Dashboard" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<style>
    .dashboard-page { padding-bottom: 24px; }
    .dashboard-heading {
        display: flex; align-items: center; justify-content: space-between;
        gap: 12px; margin: 4px 0 22px;
    }
    .dashboard-heading h1 {
        margin: 0; color: #172033; font-size: 1.65rem; font-weight: 750;
        letter-spacing: -.035em;
    }
    .dashboard-subtitle { margin-top: 5px; color: #748094; font-size: .9rem; }
    .dashboard-announcement {
        display: flex; align-items: center; gap: 12px; overflow: hidden;
        padding: 13px 16px; margin-bottom: 24px; border: 1px solid #f0dfb8;
        border-radius: 14px; background: linear-gradient(110deg, #fffaf0, #fff);
        box-shadow: 0 5px 18px rgba(23,32,51,.04);
    }
    .dashboard-announcement-label {
        flex: 0 0 auto; color: #8b5b0b; font-size: .76rem;
        font-weight: 800; letter-spacing: .07em; text-transform: uppercase;
    }
    .dashboard-announcement marquee { color: #79520f; font-size: .92rem; }
    .dashboard-stats { margin-right: -10px; margin-left: -10px; }
    .dashboard-item { padding-right: 10px; padding-left: 10px; }
    .dashboard-card {
        position: relative; min-height: 112px; height: 100%;
        overflow: hidden; border: 0; border-radius: 16px;
        background: linear-gradient(135deg, var(--dash-a), var(--dash-b)) !important;
        box-shadow: 0 7px 20px rgba(23,32,51,.10);
        transition: transform .18s ease, box-shadow .18s ease;
    }
    .dashboard-card::after {
        content: ""; position: absolute; width: 105px; height: 105px;
        right: -34px; top: -40px; border: 1px solid rgba(255,255,255,.18);
        border-radius: 50%; box-shadow: 0 0 0 14px rgba(255,255,255,.05),
        0 0 0 29px rgba(255,255,255,.035); pointer-events: none;
    }
    .dashboard-card:hover {
        transform: translateY(-3px);
        box-shadow: 0 12px 26px rgba(23,32,51,.15);
    }
    .dashboard-card .card-body {
        position: relative; z-index: 1; display: flex; flex-direction: column;
        justify-content: space-between; gap: 12px; min-height: 112px;
        padding: 18px 18px 17px; color: #fff;
    }
    .dashboard-card-title {
        display: block; color: rgba(255,255,255,.88); font-size: .72rem;
        line-height: 1.45; font-weight: 750; letter-spacing: .055em;
        text-transform: uppercase; overflow-wrap: anywhere;
    }
    .dashboard-card-value {
        display: block; color: #fff; font-size: clamp(1.15rem, 1.5vw, 1.55rem);
        font-weight: 750; line-height: 1.2; overflow-wrap: anywhere;
        font-variant-numeric: tabular-nums;
    }
    .dashboard-card .small { color: rgba(255,255,255,.92) !important; }
    .dashboard-card a { color: inherit; text-decoration: none; }
    .dashboard-card a:hover { color: inherit; text-decoration: none; }
    .dashboard-card a:focus-visible { outline: 2px solid #fff; outline-offset: 3px; }
    .dashboard-card .dashboard-split {
        display: flex; flex-wrap: wrap; gap: 8px 18px; align-items: center;
        font-size: .82rem; line-height: 1.5;
    }
    .dashboard-card .dashboard-split span { overflow-wrap: anywhere; }
    .dashboard-item:nth-child(6n + 1) .dashboard-card { --dash-a:#263b73; --dash-b:#4967a8; }
    .dashboard-item:nth-child(6n + 2) .dashboard-card { --dash-a:#087f8c; --dash-b:#19a7a0; }
    .dashboard-item:nth-child(6n + 3) .dashboard-card { --dash-a:#7c3e91; --dash-b:#ad62b7; }
    .dashboard-item:nth-child(6n + 4) .dashboard-card { --dash-a:#a45b17; --dash-b:#d69a39; }
    .dashboard-item:nth-child(6n + 5) .dashboard-card { --dash-a:#1e6650; --dash-b:#3b9a75; }
    .dashboard-item:nth-child(6n + 6) .dashboard-card { --dash-a:#344054; --dash-b:#667085; }
    .dashboard-card-link { display: block; height: 100%; color: inherit; text-decoration: none !important; }
    .dashboard-card-link:hover { color: inherit; }
    @media (max-width: 575.98px) {
        .dashboard-page { padding-left: 0; padding-right: 0; }
        .dashboard-heading { margin-bottom: 16px; }
        .dashboard-heading h1 { font-size: 1.4rem; }
        .dashboard-announcement { padding: 11px 12px; gap: 8px; margin-bottom: 18px; }
        .dashboard-announcement-label { font-size: .66rem; }
        .dashboard-announcement marquee { font-size: .82rem; }
        .dashboard-stats { margin-right: -6px; margin-left: -6px; }
        .dashboard-item { padding-right: 6px; padding-left: 6px; margin-bottom: 12px !important; }
        .dashboard-card, .dashboard-card .card-body { min-height: 108px; }
        .dashboard-card .card-body { padding: 13px 12px; gap: 10px; }
        .dashboard-card-title { font-size: .65rem; letter-spacing: .025em; }
        .dashboard-card-value { font-size: 1.08rem; }
        .dashboard-card .dashboard-split { gap: 5px 10px; font-size: .75rem; }
    }
    @media (prefers-reduced-motion: reduce) {
        .dashboard-card { transition: none; }
        .dashboard-card:hover { transform: none; }
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="dashboard-page">
    <!-- Page Heading -->
    <div class="dashboard-heading">
        <div>
            <h1>Dashboard</h1>
            <div class="dashboard-subtitle">Your business overview at a glance</div>
        </div>
    </div>
    <div class="dashboard-announcement">
        <span class="dashboard-announcement-label">Announcements</span>
        <marquee style="color: #E7A61C; font-size: 18px;">
         <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <%# Eval("desc")%> &emsp;
                    </ItemTemplate>
                </asp:Repeater>
        </marquee>
    </div>
    <!-- Content Row -->
    <div class="row" style="margin-top: 8px;">
        <!-- Content Column -->
        <div class="col-lg-12 mb-4">
            <div class="row dashboard-stats">
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <div class="dashboard-card-title" style="font-size: .78rem; text-transform: none; letter-spacing: 0;">
                                Current Plan :
                                <asp:Label ID="lblPlan" runat="server" Style="float: right" Text="0"></asp:Label>
                            </div>
                            <div class="dashboard-card-title" style="font-size: .78rem; text-transform: none; letter-spacing: 0; padding-top: 3px;">
                                Daily Capping :
                                <asp:Label ID="lblDailyCap" runat="server" Style="float: right" Text="0"></asp:Label>
                            </div>
                            <div class="dashboard-card-title" style="font-size: .78rem; text-transform: none; letter-spacing: 0; padding-top: 3px;">
                                Rank :
                                <asp:Label ID="lblRank" runat="server" Style="float: right" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">MY DOWNLINES</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblDownline" runat="server" Text=""></asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">TOTAL LEFT</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblLeft" runat="server" Text=""></asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">TOTAL RIGHT</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblRIght" runat="server" Text=""></asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">TODAYS EARNING</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblTodays" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">TOTAL EARNING</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblWallet" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">TOTAL WITHDRAWAL</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblDebit" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">BINARY INCOME</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblSilver" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">REPURCHASE INCOME</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblRepIncome" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">WALLET</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblEWallet" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </div>
                </div>
               <%-- <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">PIN COMMISSION</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblPinCommission" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </div>
                </div>--%>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">TOTAL SELF BV</span>
                            <div class="dashboard-card-value" style="font-size: 1rem;">
                                <asp:Label ID="lblTotalSelf" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                          <span class="dashboard-card-title">CURRENT REPURCHASE BV</span>
                            <div class="dashboard-split">
                                <span style="color: #fff">Left:</span>
                                <asp:Label ID="lblLeftBv" runat="server" Text="0"></asp:Label>
                                &emsp;<span style="color: #fff"> Right: </span>
                                <asp:Label ID="lblRightBv" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">TOTAL REPURCHASE BV</span>
                            <div class="dashboard-split">
                                <span style="color: #fff">Left:</span>
                                <asp:Label ID="lblTotalLeftBv" runat="server" Text="0"></asp:Label>
                                &emsp;<span style="color: #fff"> Right: </span>
                                <asp:Label ID="lblTotalRightBv" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <a href="selfPurchaseWallet.aspx" class="dashboard-card-link">
                        <div class="card dashboard-card text-white h-100">
                            <div class="card-body">
                               <span class="dashboard-card-title">SELF PURCHASE WALLET</span>
                                <div class="dashboard-card-value">
                                    <asp:Label ID="lblSelfPurchase" runat="server" Text="0"></asp:Label></div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">CURRENT BINARY PV</span>
                            <div class="dashboard-split">
                                <span style="color: #fff">Left:</span>
                                <asp:Label ID="lblBinaryLeft" runat="server" Text="0"></asp:Label>
                                &emsp;<span style="color: #fff"> Right: </span>
                                <asp:Label ID="lblBinaryRight" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">BINARY PV TODAY</span>
                            <div class="dashboard-split">
                                <span style="color: #fff">Left:</span>
                                <asp:Label ID="lblTodayBinaryLeft" runat="server" Text="0"></asp:Label>
                                &emsp;<span style="color: #fff"> Right: </span>
                                <asp:Label ID="lblTodayBinaryRight" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">REPURCHASE PV TODAY</span>
                            <div class="dashboard-split">
                                <span style="color: #fff">Left:</span>
                                <asp:Label ID="lblTodayRepLeft" runat="server" Text="0"></asp:Label>
                                &emsp;<span style="color: #fff"> Right: </span>
                                <asp:Label ID="lblTodayRepRight" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                           <span class="dashboard-card-title">SPONSOR INCOME</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblsponsorIncome" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div> 
                 <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                 <div class="card dashboard-card text-white h-100">
                     <div class="card-body">
                        <span class="dashboard-card-title">BOOSTER INCOME</span>
                         <div class="dashboard-card-value">
                             <asp:Label ID="lblbooster" runat="server" Text="0"></asp:Label>
                         </div>
                     </div>
                 </div>
             </div> 
                 <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                 <div class="card dashboard-card text-white h-100">
                     <div class="card-body">
                        <span class="dashboard-card-title">SPONSOR ROYALTY INCOME</span>
                         <div class="dashboard-card-value">
                             <asp:Label ID="lblsponsorroyalty" runat="server" Text="0"></asp:Label>
                         </div>
                     </div>
                 </div>
             </div> 


               <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">BONUS INCOME</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblbonus" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div> 
               <%--  <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">ACHIEVEMENT BONUS</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblachievebonus" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                  <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">CURRENT ACHIEVED PV</span>
                            <div class="dashboard-split">
                                <span style="color: #fff">Left:</span>
                                <asp:Label ID="lcachievdpv" runat="server" Text="0"></asp:Label>
                                &emsp;<span style="color: #fff"> Right: </span>
                                <asp:Label ID="Rcachievdpv" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                            <span class="dashboard-card-title">TOTAL ACHIEVED PV</span>
                            <div class="dashboard-split">
                                <span style="color: #fff">Left:</span>
                                <asp:Label ID="ltachievedpv" runat="server" Text="0"></asp:Label>
                                &emsp;<span style="color: #fff"> Right: </span>
                                <asp:Label ID="Rtachievedpv" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                  <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                      <div class="card dashboard-card text-white h-100">
                          <div class="card-body">
                              <span class="dashboard-card-title">SALARY BONUS INCOME</span>
                              <div class="dashboard-card-value">
                                  <asp:Label ID="lblsalary" runat="server" Text="0"></asp:Label>
                              </div>
                          </div>
                      </div>
                  </div>
                  <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                  <div class="card dashboard-card text-white h-100">
                      <div class="card-body">
                         <span class="dashboard-card-title">MONTHLY FIELD ALLOWANCE</span>
                          <div class="dashboard-card-value">
                              <asp:Label ID="lblmfa" runat="server" Text="0"></asp:Label>
                          </div>
                      </div>
                  </div>
              </div>
              <div class="col-6 col-md-4 col-xl-3 mb-3 dashboard-item">
                    <div class="card dashboard-card text-white h-100">
                        <div class="card-body">
                           <span class="dashboard-card-title">BONANZA WALLET</span>
                            <div class="dashboard-card-value">
                                <asp:Label ID="lblbonanza" runat="server" Text="0"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
