<%@ Page Title="Add Products" Language="C#" MasterPageFile="admin.master" AutoEventWireup="true"
    CodeFile="add-product-master.aspx.cs" Inherits="Admin_add_product_master" %>

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
                    Add Products
                </p>
            </div>
            <div class="row" style="background: #fff; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-md-12">
                    <div class="container1">
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="form-row">
                                    <div class="form-group col-md-12">
                                        <label for="remarks">
                                            Product Name</label>
                                        <input type="text" class="form-control" id="txtProduct" runat="server" required>
                                    </div>
                                     <div class="form-group col-md-12">
                                        <label for="remarks">
                                            Product HSN Code </label>
                                       <asp:TextBox ID="txthsncode" runat="server" CssClass="form-control" required></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="inputState">
                                            Description</label>
                                        <textarea class="form-control" id="txtDescription" maxlength="500" rows="2" runat="server"></textarea>
                                    </div>
                                    <div class="form-group col-md-10">
                                        <label for="inputState">
                                            Main Image</label>
                                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="form-group col-md-2">
                                        <asp:Image ID="ImageUrl" Width="77px" ImageUrl="img/no-image.png" Height="77px" runat="server" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="remarks">
                                            GST %</label>
                                        <asp:TextBox ID="txtGST" runat="server" CssClass="form-control" required></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtGST" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="remarks">
                                            MRP (INR)
                                        </label>
                                        <asp:TextBox ID="txtMRP" runat="server" CssClass="form-control" required></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtMRP" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="remarks">
                                            DP (Member Price)
                                        </label>
                                        <asp:TextBox ID="txtDP" runat="server" CssClass="form-control" required></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtDP" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label for="remarks">
                                            BV
                                        </label>
                                        <asp:TextBox ID="txtPV" runat="server" CssClass="form-control" required></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,Custom"
                                            ValidChars="." TargetControlID="txtPV" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Eligible for Wallet Purchase?"
                                            TextAlign="Left" />
                                            <asp:CheckBox ID="CheckBox2" runat="server" Text="Only for Awards & Rewards?"
                                            TextAlign="Left" /> <br />
                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Active For Franchisee"
                                            TextAlign="Left" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click"
                                            Text="Submit" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-3">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
