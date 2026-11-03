<%@ Page Title="ID Change" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" CodeFile="idchange.aspx.cs" Inherits="Admin_Default3" %>


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
                   ID Change
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-6">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="ID not found" Visible="False"></asp:Label>
                    <br />
                  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group row">
                                        <label class="col-sm-6 col-form-label">
                                            Username</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="TextBox1" required runat="server" AutoPostBack="true" OnTextChanged="txtUser_Changed"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="form-group row">
                                     <label class="col-sm-6 col-form-label">
                                        Name</label>
                                     <div class="col-sm-6">
                                         <asp:TextBox ID="TextBox3" runat="server" required CssClass="form-control"></asp:TextBox>
                                     </div>
                                     
                                 </div>
                             </div>
                         </div>
                             <div class="row">
                             <div class="col-md-12">
                                 <div class="form-group row">
                                     <label class="col-sm-6 col-form-label">
                                         Mobile </label>
                                     <div class="col-sm-6">
                                         <asp:TextBox ID="TextBox4" runat="server" required
                                              CssClass="form-control"></asp:TextBox>
                                     </div>
                                     <div class="col-sm-12">
                                         <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                             TargetControlID="TextBox4" />
                                     </div>
                                 </div>
                             </div>
                         </div>
                        <div class="row">
    <div class="col-md-12">
        <div class="form-group row">
            <label class="col-sm-6 col-form-label">
               PAN</label>
            <div class="col-sm-6">
                <asp:TextBox ID="Txtpan" runat="server" required CssClass="form-control"></asp:TextBox>
            </div>
            
        </div>
    </div>
</div>

                       <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group row">
                                <label class="col-sm-6 col-form-label">
                                    Achievement ?</label>
                                <div class="col-sm-6" style="padding-top: 10px">
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                    &nbsp;&nbsp; <span style="color: blue; font-size: 12px">(Select only for Mark as Zero)</span>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                   
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group row">
                                <label class="col-sm-6 col-form-label">
                                </label>
                                <div class="col-sm-6">
                                    <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-info"
                                        OnClick="Button1_Click" />
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
         <div class="col-lg-6">
          <div style="text-align: center; font-weight:bold">
        Details
    </div>
    <table style="width: 100%; font-size: 14px;">
        <tr>
            <td style="padding: 6px;">Name</td>
            <td style="padding: 6px;">
                <asp:Label ID="lblname" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 6px;">Mobile</td>
            <td style="padding: 6px;">
                <asp:Label ID="lblmobile" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 6px;">PAN</td>
            <td style="padding: 6px;">
                <asp:Label ID="lblpan" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding: 6px;">join Date </td>
            <td style="padding: 6px;">
                  <asp:Label ID="lbljoindate" runat="server"></asp:Label>
            </td>
        </tr>
           <tr>
       <td style="padding: 6px;">Total Left</td>
       <td style="padding: 6px;">
             <asp:Label ID="Label2" runat="server"></asp:Label>
       </td>
   </tr>
                <tr>
    <td style="padding: 6px;">Total Right</td>
    <td style="padding: 6px;">
          <asp:Label ID="Label3" runat="server"></asp:Label>
    </td>
</tr>
                        <tr>
    <td style="padding: 6px;">Current Left</td>
    <td style="padding: 6px;">
          <asp:Label ID="Label4" runat="server"></asp:Label>
    </td>
</tr>
                                <tr>
    <td style="padding: 6px;">Current Right</td>
    <td style="padding: 6px;">
          <asp:Label ID="Label5" runat="server"></asp:Label>
    </td>
</tr>
    </table>
</div>



                
            </div>
            <br />
           <div class="table-responsive">
     <asp:GridView ID="GridView1" HeaderStyle-ForeColor="White" AutoGenerateColumns="False"
         PagerStyle-CssClass="PagerStyle" runat="server" EmptyDataText="No records found"
         AllowPaging="True" Width="100%" PageSize="50" OnPageIndexChanging="GridView1_PageIndexChanging"
         GridLines="None">
         <Columns>
             <asp:TemplateField>
                 <HeaderTemplate>
                     SNo.</HeaderTemplate>
                 <ItemTemplate>
                     <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                 </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="username" HeaderText="Username" />
             <asp:BoundField DataField="oldname" HeaderText="Old Name" />
             <asp:BoundField DataField="newname" HeaderText="New Name" />
             <asp:BoundField DataField="oldmobile" HeaderText="Old Mobile" />
             <asp:BoundField DataField="newmobile" HeaderText="New Mobile" />
             <asp:BoundField DataField="oldpan" HeaderText="Old PAN" />
             <asp:BoundField DataField="newpan" HeaderText="New PAN" />
         
             <asp:BoundField DataField="ChangeDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                      <asp:BoundField DataField="kyc" HeaderText="Achievement Zero" />

         </Columns>
     </asp:GridView>
 </div>


        </div>
    </div>
</asp:Content>
