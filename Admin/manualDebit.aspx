<%@ Page Title="Manual Cash Wallet" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="manualDebit.aspx.cs" Inherits="portal_Admin_manualDebit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">
            Cash Wallet Credit/Debit
        </h1>
    </div>
    <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
        border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
        <div class="col-md-12">
            <div class="container1">
                <div>
                    <span id="lbSuccess" runat="server" visible="false" style="color: Green"></span>
                    <span id="lbErrormsg" runat="server" visible="false" class="text-warning"></span>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        Franchisee ID
                                    </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtUser" runat="server" MaxLength="11" AutoPostBack="true" CssClass="form-control"
                                            required OnTextChanged="txtUser_Changed"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <p>
                                            <span id="lblName" runat="server" class="text-danger"></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        Credit/Debit
                                    </label>
                                    <div class="col-sm-5">
                                        <asp:RadioButtonList ID="rdOption" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Credit</asp:ListItem>
                                            <asp:ListItem Value="2">Debit</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="Small" ForeColor="Red"
                                            ControlToValidate="rdOption" runat="server" ErrorMessage="Please Select Credit or Debit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        Cash Wallet Balance
                                        <asp:Label ID="lblSymbol" runat="server" Text=""></asp:Label>
                                    </label>
                                    <div class="col-sm-5">
                                        <span id="lbbal" runat="server" class="form-control">0</span>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        Amount (Rs)
                                    </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="TextBox2" runat="server" required CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                            TargetControlID="TextBox2" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-7">
                                <div class="form-group row">
                                    <label class="col-sm-3 col-form-label">
                                        Remarks
                                    </label>
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtRemarks" required runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="Div1" class="row" runat="server">
                    <div class="col-md-7">
                        <div class="form-group row">
                            <label class="col-sm-3 col-form-label">
                                Enter OTP
                            </label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-5">
                                <asp:LinkButton ID="LinkButton1" ForeColor="Blue" runat="server" OnClick="GenerateOTP_Click">Click Here</asp:LinkButton>
                                to generate OTP
                                <br />
                                <asp:Label ID="lblOtpMessage" ForeColor="DarkGreen" Font-Size="12px" runat="server"
                                    Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                                Text="Submit" />
                        </div>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row1" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.125);
        border-radius: 3px; padding: 15px 0px; margin-top: 20px;">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" EmptyDataText="No records found"
                            AllowPaging="True" Width="100%" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" />
                                <asp:BoundField DataField="name" HeaderText="Name" />
                                <asp:BoundField DataField="desc" HeaderText="DESC" />
                                <asp:BoundField DataField="confirmdate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" />
                                <asp:BoundField DataField="credit" HeaderText="Credit" />
                                <asp:BoundField DataField="debit" HeaderText="Debit" />
                                <asp:BoundField DataField="remarks" HeaderText="Remarks" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
