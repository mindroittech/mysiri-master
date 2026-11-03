<%@ Page Title="Member Profile" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="mprofile.aspx.cs" Inherits="Admin_mprofile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Member Profile
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Panel ID="Panel1" runat="server" ForeColor="#009933" Visible="False">
                        Your profile has been successfully updated.</asp:Panel>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label5" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                            <p class="card-description" style="font-size: 15px; color: #ff9c00; font-weight: 600;">
                                Personal info
                            </p>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Name</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox2" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Username</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox1" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Email</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Mobile No*</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox4" runat="server" OnTextChanged="txtMobile_TextChanged" AutoPostBack="true" Enabled="false"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Gender</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                                <asp:ListItem>Select Gender</asp:ListItem>
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Date of Birth</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" placeholder="mm/dd/yyyy"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            State*</label>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
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
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            City*</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Address*</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Pincode</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <p class="card-description" style="font-size: 15px; color: #ff9c00; font-weight: 600;">
                                Nominee Details
                            </p>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Nominee</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtNominee" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Nominee Age</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtAge" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                TargetControlID="txtAge" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Relationship with Nominee</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtRelation" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <p class="card-description" style="font-size: 15px; color: #ff9c00; font-weight: 600;">
                                Bank Information
                            </p>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-4 col-form-label">
                                            A/c Holder Name</label>
                                        <div class="col-sm-8">
                                            <input name="" type="text" value="" runat="server" maxlength="150" id="txtaccNAme"
                                                tabindex="22" class="form-control" placeholder="Account Holder Name">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Account Type</label>
                                        <div class="col-sm-9">
                                            <select name="" id="ddAccountType" runat="server" class="form-control">
                                                <option value="">-- Select --</option>
                                                <option value="Saving">Saving</option>
                                                <option value="Current">Current</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Bank Name*</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Branch*</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            A/c Number*</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            IFSC*</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox11" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            PAN</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox12" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Aadhar*</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtAdhar" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <p class="card-description" style="font-size: 15px; color: #ff9c00; font-weight: 600;">
                                Login Details
                            </p>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Username</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox14" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group row">
                                        <label class="col-sm-3 col-form-label">
                                            Password</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <p class="card-description" style="font-size: 15px; color: #ff9c00; font-weight: 600;">
                        Sponsor Details
                    </p>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Referrer Id</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="Label1" runat="server" Style="font-size: 13px" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Referrer Name</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="Label2" runat="server" Style="font-size: 13px" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Upline Id</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="Label3" runat="server" Style="font-size: 13px" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group row">
                                <label class="col-sm-3 col-form-label">
                                    Upline Name</label>
                                <div class="col-sm-9">
                                    <asp:Label ID="Label4" runat="server" Style="font-size: 13px" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <center>
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" OnClick="Button1_Click"
                            Text="Update" />
                    </center>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
