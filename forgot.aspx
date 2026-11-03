<%@ Page Title="Forgot Password" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="forgot.aspx.cs" Inherits="forgot" %>

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
                        Forgot Password</h2>
                    <!-- <p>I am a returning customer</p> -->
                    <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Username does not exist"
                        Visible="False" Font-Size="Small"></asp:Label>
                    <br />
                    <asp:Label ID="Label2" runat="server" ForeColor="#009900" Text="Password has been sent to your registered mobile number."
                        Visible="False"></asp:Label>
                    <div class="form-group">
                        <label class="control-label" for="input-email">
                            User ID</label>
                        <input type="text" name="userid" value="" runat="server" placeholder="Enter Your LoginID"
                            id="userid" class="form-control" required>
                    </div>
                    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" data-loading-text="Loading..."
                        CssClass="btn btn-primary" />
                    <a href="login.aspx" style="color: blue; padding-left: 15px;">Back To Login</a>
                </div>
            </div>
            <div class="col-md-4">
            </div>
        </div>
    </div>
</asp:Content>
