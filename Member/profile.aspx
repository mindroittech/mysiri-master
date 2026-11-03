<%@ Page Title="My Profile" Language="C#" MasterPageFile="member.master" AutoEventWireup="true"
    CodeFile="profile.aspx.cs" Inherits="profile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #1a325d;">
                <p style="color: #fff; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    My Profile
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <asp:Panel ID="Panel1" runat="server" ForeColor="#009933" Visible="False">
                    Your profile has been successfully updated.</asp:Panel>
                <asp:Label ID="Label2" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                <div class="col-md-10">
                    <div class="card-header tx-medium bd-0 tx-white">
                        Placement & Sponsor Details
                    </div>
                    <!-- card-header -->
                    <div class="card-body bd bd-t-0 rounded-bottom">
                        <div class="form-horizontal">
                            <div class="row form-group">
                                <div class="col-sm-6">
                                    <label>
                                        Associate ID</label>
                                    <input name="" type="text" value="" readonly="readonly" id="txtuser" runat="server"
                                        class="form-control">
                                </div>
                                <div class="col-sm-6">
                                    <label>
                                        Joining Date</label>
                                    <input name="" type="text" value="" id="joinDate" runat="server" disabled="disabled"
                                        tabindex="2" class="form-control">
                                </div>
                            </div>
                            <div id="">
                                <div class="row form-group">
                                    <div class="col-sm-6">
                                        <label>
                                            Referral Id</label>
                                        <input name="" type="text" value="" readonly="readonly" id="sponsorId" class="form-control"
                                            placeholder="Sponsor Id" runat="server">
                                    </div>
                                    <div class="col-sm-6">
                                        <label>
                                            Referral Name</label>
                                        <input name="" type="text" value="" runat="server" readonly="readonly" id="sponsorName"
                                            class="form-control" placeholder="Sponsor Name">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- card-body -->
                </div>
                <!-- card -->
                <br>
                <div class="col-md-10">
                    <div class="card-header tx-medium bd-0 tx-white">
                        Personal Details
                    </div>
                    <!-- card-header -->
                    <div class="card-body bd bd-t-0 rounded-bottom">
                        <div class="form-horizontal">
                            <div class="row form-group">
                                <div class="col-sm-6">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Name
                                    </label>
                                    <asp:TextBox ID="TextBox2" runat="server" Enabled="false" CssClass="form-control"
                                        required></asp:TextBox>
                                    <span id="" style="color: Red; visibility: hidden;">Required</span>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Gender</label>
                                    <asp:DropDownList ID="drpGender" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">Select Gender</asp:ListItem>
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-sm-6">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Mobile No</label>
                                    <div class="row">
                                        <div class="col-md-4" style="padding-top: 0px;">
                                            <input name="" type="text" value="91" readonly="readonly" id="Text1" class="form-control"
                                                placeholder="Country Code">
                                        </div>
                                        <div class="col-md-8" style="padding-top: 0px;">
                                            <asp:TextBox ID="TextBox4" runat="server" Enabled="false" CssClass="form-control"
                                                required></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        State :
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
                                    <span id="Span1" style="color: Red; visibility: hidden;">Required</span>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-6">
                                    <label>
                                        District</label>
                                    <asp:TextBox ID="txtDistrict" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        City</label>
                                    <asp:TextBox ID="TextBox13" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-6">
                                    <label>
                                        Address*</label>
                                    <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        Pincode</label>
                                    <asp:TextBox ID="TextBox7" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-6">
                                    <label>
                                        Email Id</label>
                                    <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label>
                                        PAN No</label>
                                    <asp:TextBox ID="TextBox12" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                                    <span id="" style="color: Red; display: none;">Please Enter Valid Pan</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- card-body -->
                </div>
                <!-- card -->
                <br>
                <div class="col-md-10">
                    <div class="card-header tx-medium bd-0 tx-white">
                        Nominee Details
                    </div>
                    <!-- card-header -->
                    <div class="card-body bd bd-t-0 rounded-bottom">
                        <div class="form-horizontal">
                            <div class="row form-group">
                                <div class="col-sm-6">
                                    <label>
                                        Nominee Name</label>
                                    <input name="" type="text" id="nomineeName" runat="server" class="form-control" placeholder="Nominee Name">
                                </div>
                                <div class="col-sm-6">
                                    <label>
                                        Nominee Relation</label>
                                    <select name="" onchange="" id="nomineeRelation" runat="server" class="form-control">
                                        <option selected="selected" value="0">-- Select Relation--</option>
                                        <option value="Father">Father</option>
                                        <option value="Mother">Mother</option>
                                        <option value="Son">Son</option>
                                        <option value="Daughter">Daughter</option>
                                        <option value="Wife">Wife</option>
                                        <option value="Husband">Husband</option>
                                        <option value="Owner">Owner</option>
                                        <option value="Other">Other</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- card-body -->
                </div>
                <!-- card -->
                <br>
                <div class="col-md-10">
                    <div class="card-header tx-medium bd-0 tx-white">
                        Bank Details
                    </div>
                    <!-- card-header -->
                    <div class="card-body bd bd-t-0 rounded-bottom">
                        <div class="form-horizontal">
                            <div class="row form-group">
                                <div class="col-sm-6">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Account Holder Name</label>
                                    <input name="" type="text" value="" runat="server" maxlength="150" id="txtaccNAme"
                                        tabindex="22" class="form-control" placeholder="Account Holder Name">
                                </div>
                                <div class="col-sm-6">
                                    <label>
                                        Account Type
                                    </label>
                                    <select name="" id="ddAccountType" disabled runat="server" class="form-control">
                                        <option value="">-- Select --</option>
                                        <option selected="selected" value="Saving">Saving</option>
                                        <option value="Current">Current</option>
                                    </select>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="row form-group">
                                        <div class="col-sm-6">
                                            <label>
                                                <span class="redRequired" title="This field is required." style="color: red">*</span>
                                                IFSC Code</label>
                                            <asp:TextBox ID="TextBox11" OnTextChanged="TextBox11_TextChanged" AutoPostBack="True"
                                                CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>
                                                <span class="redRequired" title="This field is required." style="color: red">*</span>
                                                Account No</label>
                                            <asp:TextBox ID="TextBox10" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-sm-6">
                                            <label>
                                                <span class="redRequired" title="This field is required." style="color: red">*</span>
                                                Bank Name</label>
                                            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>
                                                <span class="redRequired" title="This field is required." style="color: red">*</span>
                                                Branch Name</label>
                                            <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="row form-group">
                                <div class="col-sm-6">
                                    <label>
                                        Upload Image</label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" placeholder="Attachment" />
                                </div>
                                <div class="col-sm-6">
                                    <label>
                                        Profile Image
                                    </label>
                                    <img id="cntMainContent_img_banner" src="" runat="server" style="height: 120px; width: 120px;">
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- card-body -->
                </div>
                <div class="col-md-10">
                    <center>
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" OnClick="Button1_Click"
                            Text="Update" />
                    </center>
                </div>
                <!-- card -->
            </div>
        </div>
        <!-- row -->
    </div>
</asp:Content>
