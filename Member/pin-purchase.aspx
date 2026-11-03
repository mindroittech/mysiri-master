<%@ Page Title="Wallet Pin Purchase" Language="C#" MasterPageFile="~/Member/member.master"
    AutoEventWireup="true" CodeFile="pin-purchase.aspx.cs" Inherits="Member_pin_purchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="br-pagebody">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header tx-medium bd-0 tx-white bg-primary">
                       Wallet Pin Purchase
                    </div>
                    <!-- card-header -->
                    <div class="card-body bd bd-t-0 rounded-bottom">
                        <asp:Label ID="lbErrormsg" runat="server" ForeColor="#FF3300" Text="" Visible="False"></asp:Label>
                        <br />
                        <form class="form-sample">
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        Plan</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="drpPlan" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpPlan"
                                            ErrorMessage="Select Plan" ForeColor="Red" InitialValue="0" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        No of PINs</label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtPins" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPins"
                                            ErrorMessage="Enter Pins Required" ForeColor="Red" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        Wallet Balance</label>
                                    <div class="col-sm-5">
                                        <span id="lbbal" runat="server" class="form-control">0</span>
                                    </div>
                                    <div class="col-sm-4">
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="form-group row">
                                    <label class="col-sm-4 col-form-label">
                                    </label>
                                    <div class="col-sm-5">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary mr-2" Text="Submit"
                                            OnClick="Button1_Click" ValidationGroup="sub" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <span style="font-weight: bold; font-size: 14px; color: Red">Note: </span><span style="color: Blue;
                                font-size: 13px">Minimum amount required for pin purchase is 400.</span>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
