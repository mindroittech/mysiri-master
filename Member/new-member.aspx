<%@ Page Title="Member Registration" Language="C#" MasterPageFile="member.master"
    AutoEventWireup="true" CodeFile="new-member.aspx.cs" Inherits="new_member" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }


        function GetSponsorName() {
            $.ajax({
                type: "POST",
                url: "new-member.aspx/txtSponsorChange",
                data: '{ refId: "' + $("#<%=txtSponsor.ClientID%>")[0].value + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            $("#<%=lblName.ClientID%>").text(response.d);
        }

    
    </script>
    <style>
        table
        {
            margin: 0;
        }
        
        table tr, table tr td, table tbody tr td
        {
            background: white;
            border: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="br-pagebody">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="row">
            <div class="col-xl-6">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="form-layout form-layout-4">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Name :</label>
                                    <asp:TextBox ID="TextBox3" runat="server" required CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-12">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Gender</label>
                                    <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem>Male</asp:ListItem>
                                        <asp:ListItem>Female</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Mobile No :
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator101" runat="server"
                                            ControlToValidate="TextBox6" ErrorMessage="Enter 10 digit number" Font-Size="Small"
                                            ForeColor="Red" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                    </label>
                                    <div class="row">
                                        <div class="col-md-4" style="padding-top: 0px;">
                                            <input name="ctl00$cntMainContent$txtcountrycode" type="text" value="91" readonly="readonly"
                                                id="cntMainContent_txtcountrycode" class="form-control" placeholder="Country Code">
                                        </div>
                                        <div class="col-md-8" style="padding-top: 0px;">
                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" MaxLength="10"
                                                required></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>
                                        Email ID :
                                    </label>
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12" runat="server" id="spnInfo">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Sponsor Id</label>
                                    <asp:Label ID="lblName" Style="float: right; color: Green" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtSponsor" AutoPostBack="true" OnTextChanged="txtRef_Changed" runat="server"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Team</label>
                                    <select name="ctl00$cntMainContent$ddlPosition" id="ddlPosition" runat="server" required
                                        class="form-control">
                                        <option value="">--Select--</option>
                                        <option value="Left">Left</option>
                                        <option value="Right">Right</option>
                                    </select>
                                    <span id="cntMainContent_RequiredFieldValidator6" style="color: Red; font-weight: bold;
                                        display: none;">Required</span>
                                </div>
                                <div class="col-md-12">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        Joining Plan</label>
                                    <asp:DropDownList ID="drpPlan" runat="server" required AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="drpPlan_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span id="Span1" style="color: Red; font-weight: bold; display: none;">Required</span>
                                </div>
                                <div class="col-md-12">
                                    <label>
                                        <span class="redRequired" title="This field is required." style="color: red">*</span>
                                        PIN Number</label>
                                    <asp:DropDownList ID="drpPIN" required runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <span id="Span2" style="color: Red; font-weight: bold; display: none;">Required</span>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <label>
                                            <span class="redRequired" title="This field is required." style="color: red">*</span>
                                            PAN No</label>
                                        <asp:TextBox ID="txtPan" runat="server" required MaxLength="50" CssClass="form-control"
                                            placeholder="PAN No"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- form-layout -->
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!-- col-6 -->
            <div class="col-xl-6">
                <div class="form-layout form-layout-4">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>
                                        A/C Holder Name
                                    </label>
                                    <input name="ctl00$cntMainContent$txtAccountHolderName" type="text" maxlength="50"
                                        runat="server" id="txtAccountHolderName" class="form-control" placeholder="A/C Holder Name" />
                                </div>
                                <div class="col-md-12">
                                    <label>
                                        IFSC Code</label>
                                    <asp:TextBox ID="txtIfsc" runat="server" CssClass="form-control" OnTextChanged="TextBox15_TextChanged"
                                        AutoPostBack="True" placeholder="IFSC Code"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>
                                        Bank A/C No</label>
                                    <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label>
                                        Bank Name</label>
                                    <asp:TextBox ID="txtBank" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>
                                        Branch Name
                                    </label>
                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label>
                                        Account Type
                                    </label>
                                    <select name="ctl00$cntMainContent$ddlAccounttype" runat="server" id="ddlAccounttype"
                                        class="form-control">
                                        <option value="">-- Select --</option>
                                        <option value="Saving">Saving</option>
                                        <option value="Current">Current</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>
                                        Street/Landmark/Building (Address) :
                                    </label>
                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" Rows="2" placeholder="Street/Landmark/Building (Address)"
                                        TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label>
                                        Pincode :
                                    </label>
                                    <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label>
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
                                </div>
                                <div class="col-md-12">
                                    <label>
                                        District :
                                    </label>
                                    <asp:TextBox ID="txtDistrict" required runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    By submitting I hereby accept all <a href="../privacy-policy.html" target="_blank">Terms
                                        and Conditions</a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-4">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" OnClick="btnContinueMember_Click"
                                        Text="Submit" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <!-- form-layout -->
            </div>
            <!-- col-6 -->
        </div>
        <!-- row -->
    </div>
</asp:Content>
