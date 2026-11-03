<%@ Page Title="KYC" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"
    CodeFile="vKyc.aspx.cs" Inherits="Admin_vKyc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row" style="margin-top: 20px;">
        <div class="col-lg-12 mb-4">
            <div class="row" style="background: #F5BE6D;">
                <p style="color: #000; margin-top: 13px; font-size: 18px; padding-left: 14px;">
                    Complete Your KYC
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <asp:Label ID="Label1" Visible="false" ForeColor="Green" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label2" Visible="false" ForeColor="Red" runat="server" Text=""></asp:Label>
                    <br>
                    <div class="card-body bd bd-t-0 rounded-bottom">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <label>
                                    Upload PAN : &nbsp;
                                </label>
                                <asp:FileUpload ID="FileUpload1" runat="server" /><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                    ForeColor="Red" ControlToValidate="FileUpload1" ErrorMessage="Upload PAN Card"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.pdf|.png)$"
                                    ControlToValidate="FileUpload1" runat="server" ForeColor="Red" ErrorMessage="Valid formats are JPG,JPEG,PNG,PDF and should not contain empty space"
                                    Display="Dynamic" />
                            </div>
                        </div>
                        <br />
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <label>
                                    Upload Bank Passbook : &nbsp;
                                </label>
                                <asp:FileUpload ID="FileUpload2" runat="server" /><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ForeColor="Red" ControlToValidate="FileUpload2" ErrorMessage="Upload Bank Passbook"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.pdf|.png)$"
                                    ControlToValidate="FileUpload2" runat="server" ForeColor="Red" ErrorMessage="Valid formats are JPG,JPEG,PNG,PDF and should not contain empty space"
                                    Display="Dynamic" />
                            </div>
                        </div>
                        <br />
                        <div class="col-md-12">
                            <div class="col-md-6" style="text-align: center">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary mr-2" Text="Upload"
                                    OnClick="Button1_Click" />
                            </div>
                        </div>
                        <br />
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                CellSpacing="1" CssClass="table table-bordered" Width="100%" PageSize="50" HeaderStyle-BackColor="#1f5fc0"
                                HeaderStyle-ForeColor="White">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SNo.</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="pan" HeaderText="PAN Number" />
                                    <asp:HyperLinkField Target="_blank" Text="View" DataNavigateUrlFields="UploadPan"
                                        DataNavigateUrlFormatString="{0}" HeaderText="PAN" ItemStyle-Width="150" />
                                    <asp:HyperLinkField Target="_blank" Text="View" DataNavigateUrlFields="UploadBank"
                                        DataNavigateUrlFormatString="{0}" HeaderText="Bank Passbook" ItemStyle-Width="150" />
                                    <asp:BoundField DataField="IsApproved" HeaderText="Status" ReadOnly="True" HtmlEncode="false"
                                        ItemStyle-Font-Bold="true" SortExpression="pinnumbers" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
