<%@ Page Title="Admin Login" Language="C#" MasterPageFile="../MasterPage.master"
    AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Admin_login" %>

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
                        Admin Login</h2>
                    <!-- <p>I am a returning customer</p> -->
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Invalid UserId or Password!!"
                        Visible="False"></asp:Label>
                    <div class="form-group">
                        <label class="control-label" for="input-email">
                            User ID</label>
                        <input type="text" id="TextBox1" class="form-control" required runat="server" name="firstname"
                            placeholder="Login Id.." />
                    </div>
                    <div class="form-group">
                        <label class="control-label" for="input-password">
                            Password</label>
                        <input type="password" id="TextBox2" class="form-control" required runat="server"
                            name="lastname" placeholder="Password.." />
                    </div>
                    <asp:Button ID="Button2" runat="server" Text="Login" OnClick="Button1_Click" data-loading-text="Loading..."
                        CssClass="btn btn-primary" />
                </div>
            </div>
            <div class="col-md-4">
            </div>
        </div>
    </div>
</asp:Content>
