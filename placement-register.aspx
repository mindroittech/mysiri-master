<%@ Page Title="Placement Registration" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="placement-register.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="container form-container">
        <div class="row">
            <div class="col-md-4">
            </div>
            <div class="col-md-4 login-sect">
                <div class="login-form ">
                    <h2>
                        Register Your Account</h2>
                    <!-- <p>I am a returning customer</p> -->
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Green" Text=""
                        Visible="False"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"
                        Visible="False"></asp:Label>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                         <ContentTemplate>
                             <div class="form-group">
                                 <label class="control-label">
                                     Sponsor ID</label>
                                 <asp:Label ID="lblName" Style="float: right; color: Green" Font-Bold="true" runat="server"
                                     Text=""></asp:Label>
                                 <asp:TextBox ID="TextBox20" CssClass="form-control" OnTextChanged="TextBox20_TextChanged" Enabled="false"
                                     AutoPostBack="true" required runat="server"></asp:TextBox>
                             </div>
                             <div class="form-group">
                                 <label class="control-label">
                                     Placement ID</label>
                                 <asp:Label ID="lblplacement" Style="float: right; color: Green" Font-Bold="true" runat="server"
                                     Text=""></asp:Label>
                                 <asp:TextBox ID="txtplacement" CssClass="form-control" OnTextChanged="txtplacement_TextChanged" Enabled="false"
                                     AutoPostBack="true" required runat="server"></asp:TextBox>
                             </div>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                    <div class="form-group">
                        <label class="control-label">
                            Team</label>
                        <br>
                        <asp:RadioButtonList ID="RadioButtonList1" required RepeatDirection="Horizontal" Enabled="false"
                            CssClass="radio-inline" runat="server">
                            <asp:ListItem>Left</asp:ListItem>
                            <asp:ListItem>Right</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadioButtonList1"
                            Display="Dynamic" ErrorMessage="Select Team" ForeColor="Red" Font-Size="Small"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            Full Name</label>
                        <asp:TextBox ID="TextBox2" CssClass="form-control" placeholder="Full Name" required
                            runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            Mobile Number</label>
                        <asp:TextBox ID="TextBox4" CssClass="form-control" required placeholder="Mobile Number"
                            runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox4"
                            Display="Dynamic" ErrorMessage="Enter your mobile no" ForeColor="Red" Font-Size="Small"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox4"
                            Display="Dynamic" ErrorMessage="Enter 10 digit number" ValidationExpression="[0-9]{10}"
                            ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            State
                        </label>
                        <asp:DropDownList ID="drpState" runat="server" required CssClass="form-control">
                            <asp:ListItem Value="">Select State</asp:ListItem>
                            <asp:ListItem>Andaman and Nicobar</asp:ListItem>
                            <asp:ListItem>Andhra Pradesh</asp:ListItem>
                            <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                            <asp:ListItem>Assam</asp:ListItem>
                            <asp:ListItem>Bihar</asp:ListItem>
                            <asp:ListItem>Chandigadh</asp:ListItem>
                            <asp:ListItem>Chattisgadh</asp:ListItem>
                            <asp:ListItem>Dadra and Nagar Haveli</asp:ListItem>
                            <asp:ListItem>Daman and Diu</asp:ListItem>
                            <asp:ListItem>Delhi</asp:ListItem>
                            <asp:ListItem>Goa</asp:ListItem>
                            <asp:ListItem>Gujarat</asp:ListItem>
                            <asp:ListItem>Haryana</asp:ListItem>
                            <asp:ListItem>Himachal Pradesh</asp:ListItem>
                            <asp:ListItem>Jammu and Kashmir</asp:ListItem>
                            <asp:ListItem>Jharkhand</asp:ListItem>
                            <asp:ListItem>Karnataka</asp:ListItem>
                            <asp:ListItem>Kerala</asp:ListItem>
                            <asp:ListItem>Lakshadweep</asp:ListItem>
                            <asp:ListItem>Leh Ladakh</asp:ListItem>
                            <asp:ListItem>Madhya Pradesh</asp:ListItem>
                            <asp:ListItem>Maharashtra</asp:ListItem>
                            <asp:ListItem>Manipur</asp:ListItem>
                            <asp:ListItem>Meghalaya</asp:ListItem>
                            <asp:ListItem>Mizoram</asp:ListItem>
                            <asp:ListItem>Nagaland</asp:ListItem>
                            <asp:ListItem>Odissa</asp:ListItem>
                            <asp:ListItem>Puducherry</asp:ListItem>
                            <asp:ListItem>Punjab</asp:ListItem>
                            <asp:ListItem>Rajasthan</asp:ListItem>
                            <asp:ListItem>Sikkim</asp:ListItem>
                            <asp:ListItem>Tamil Nadu</asp:ListItem>
                            <asp:ListItem>Telangana</asp:ListItem>
                            <asp:ListItem>Tripura</asp:ListItem>
                            <asp:ListItem>Uttar Pradesh</asp:ListItem>
                            <asp:ListItem>Uttarakhand</asp:ListItem>
                            <asp:ListItem>West Bengal</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            District</label>
                        <asp:TextBox ID="txtDistrict" CssClass="form-control" placeholder="District" required
                            runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label class="control-label">
                            PAN Number</label>
                        <asp:TextBox ID="txtPan" CssClass="form-control" placeholder="PAN Number" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPan"
                            Display="Dynamic" ErrorMessage="PAN Number is required" ForeColor="Red" Font-Size="Small"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan"
                            Display="Dynamic" ErrorMessage="Enter valid PAN" ValidationExpression="[0-9a-zA-Z]{10}"
                            ForeColor="Red" Font-Size="Small"></asp:RegularExpressionValidator>
                    </div>
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" OnClick="Button1_Click"
                        Text="Submit" />
                </div>
            </div>
            <div class="col-md-4">
            </div>
        </div>
    </div>
</asp:Content>
