<%@ Page Title="My Business" Language="C#" MasterPageFile="member.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="downline.aspx.cs" Inherits="downline" %>

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
                    My Downline
                </p>
            </div>
            <div class="row" style="background: #fff; padding: 5px 10px; border: 1px solid rgba(0, 0, 0, 0.15);
                border-bottom-right-radius: 3px !important; border-bottom-left-radius: 3px !important;">
                <div class="col-lg-12">
                    <div class="form-horizontal">
                        <div class="row form-group">
                            <%--<div class="col-md-2">
                                <label>
                                    From Date</label>
                                <input name="" type="date" id="txtFrom" runat="server" class="form-control" placeholder="From Date">
                            </div>
                            <div class="col-md-2">
                                <label>
                                    To Date</label>
                                <input name="" type="date" id="txtTo" runat="server" class="form-control" placeholder="To Date">
                            </div>
                            <div class="col-md-2">
                                <br />
                                <asp:Button ID="Button3" runat="server" Text="Search" CssClass="btn btn-primary"
                                    OnClick="Button3_Click" />
                            </div>--%>
                            <div class="col-md-3">
                                <label>
                                    Position</label>
                                <select name="" id="ddlPosition" class="form-control" runat="server">
                                    <option selected="selected" value="0">All</option>
                                    <option value="Left">Left</option>
                                    <option value="Right">Right</option>
                                </select>
                            </div>
                            <div class="col-md-3">
                                <label>
                                    <br>
                                    <asp:Button ID="Button2" runat="server" Text="Search" CssClass="btn btn-primary"
                                        OnClick="Button2_Click" />
                                    &nbsp;
                                    <asp:Button ID="Button1" runat="server" Text="View in Excel" CssClass="btn btn-primary"
                                        OnClick="Button1_Click" />
                                </label>
                            </div>
                        </div>
                    </div>
                    <br>
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" runat="server" EmptyDataText="No downline members" AutoGenerateColumns="False" PageSize="1000" OnPageIndexChanged="GridView1_PageIndexChanged" AllowPaging="true" CssClass="table table-bordered" 
                            DataKeyNames="username" Width="100%" CellPadding="3" CellSpacing="1" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SNo.</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="username" HeaderText="Username" ReadOnly="True" SortExpression="username" />
                                <asp:TemplateField HeaderText="Level" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# "Level-"+Eval("level") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                <asp:BoundField DataField="plan" HeaderText="Plan" SortExpression="name" />
                                <asp:BoundField DataField="leg" HeaderText="Position" SortExpression="plan" />
                                <asp:BoundField DataField="joindate" HeaderText="Join Date" DataFormatString="{0:dd/MMM/yyyy hh:mm tt}"
                                    SortExpression="joindate" />
                                <asp:BoundField DataField="renewal" HeaderText="Activation Date" DataFormatString="{0:dd/MMM/yyyy hh:mm tt}"
                                    SortExpression="joindate" />
                                <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Font-Bold="true"
                                    SortExpression="plan" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!-- card-body -->
            </div>
            <!-- card -->
        </div>
    </div>
</asp:Content>
