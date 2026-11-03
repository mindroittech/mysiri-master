<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="login.aspx.cs" Inherits="Member_login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container form-container">
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4 login-sect">
                <div class="login-form ">
                    <h2>
                        Login to Your Account</h2>
                    <!-- <p>I am a returning customer</p> -->
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                    <div class="form-group">
                        <label class="control-label" for="input-email">
                            User ID or Mobile</label>
                        <input type="text" name="userid" value="" placeholder="User ID" id="userid" runat="server"
                            class="form-control" required>
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="input-password">
                            Password</label>
                        <input type="password" name="password" value="" placeholder="Password" id="psswd"
                            runat="server" class="form-control">
                        <a style="color: blue;" href="forgot.aspx" class="forgot">Forgotten Password ?</a></div>
                    <asp:Button ID="Button2" runat="server" Text="Login" OnClick="login_click" data-loading-text="Loading..."
                        CssClass="btn btn-primary" />
                </div>
            </div>
            <div class="col-md-4">
            </div>
        </div>
    </div>
</asp:Content>
