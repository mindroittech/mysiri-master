<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fInvoice.aspx.cs" Inherits="Admin_fInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Invoice | Dhanvantarilokasiri Biotech Private Ltd</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css"
        rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC"
        crossorigin="anonymous">
    <script type="text/javascript">
        function printDiv(printableArea) {
            var printContents = document.getElementById(printableArea).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }

    </script>
</head>
<body>
    <style>
        .container, .container-fluid, .container-lg, .container-md, .container-sm, .container-xl, .container-xxl
        {
            padding-right: 0 !important;
            padding-left: 0 !important;
        }
        
        hr
        {
            opacity: 0.5 !important;
        }
        
        .table
        {
            border-color: #929292 !important;
            border-right: none;
            margin-bottom: 0 !important;
        }
        
        .table-no-padding td
        {
            padding: 0 5px;
        }
    </style>
    <form id="form1" runat="server">
    <div class="container" style="border: 1px solid rgb(148, 145, 145); margin-bottom: 20px;">
        <div class="invoice" id='printableArea' style="margin: 20px 0 0 0;">
            <div>
                <h4 style="text-align: center; margin-bottom: 40px; color: rgba(26, 25, 25, 0.801);">
                    <u>INVOICE </u>
                </h4>
            </div>
            <div class="row">
                <div class="col-lg-1 col-md-1">
                    <img src="../image/dls-logo.png" width="100" height="100" title="Organic" alt="Organic"
                        class="img-responsive" />
                </div>
                <div class="col-lg-4 col-md-4" style="margin-left: 10px;">
                    <p>
                        DHANVANTARILOKASIRI BIOTECH (P) LTD
                        <br>
                       2nd Floor, 36, MRG Complex, Santhepete Road,
                        <br>
                        Near Dalawai School, KR Mohalla, Mysuru, Karnaraka 570004</p>
                </div>
                <div class="col-lg-2 col-md-2">
                </div>
                <div class="col-lg-4 col-md-4">
                    <table class="table table-borderless">
                        <tr>
                            <th style="padding: 0;">
                                DATE
                            </th>
                            <td style="padding: 0;">
                                :
                                <asp:Label ID="lblInvDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="padding: 0;">
                                INVOICE #
                            </th>
                            <td style="padding: 0;">
                                :
                                <asp:Label ID="lblInv" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="padding: 0;">
                                FRANCHISEE ID
                            </th>
                            <td style="padding: 0;">
                                :
                                <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th style="padding: 0;">
                                OUR GST NUMBER
                            </th>
                            <td style="padding: 0;">
                                : 29AAICD5850G1ZF
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="cust-details" style="margin-top: 20px;">
                <h4 style="font-size: 17px; margin-left: 10px;">
                    CUSTOMER DETAILS</h4>
                <hr>
                <!-- row start -->
                <div class="row">
                    <div class="col-md-6">
                        <p style="margin-left: 10px;">
                            <b>
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label></b>
                            <br>
                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblPincode" runat="server" Text=""></asp:Label>
                            <br>
                            GSTN :
                            <asp:Label ID="lblGST" runat="server" Text=""></asp:Label>
                            <br>
                            Mobile No :
                            <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <!-- <p>OUR PAN NUMBER :</p> -->
                    </div>
                </div>
                <!-- row end -->
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" CssClass="table table-bordered" AutoGenerateColumns="false"
                        runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    SN
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProductName" HeaderText="DESCRIPTION OF GOODS" SortExpression="name" />
                            <asp:BoundField DataField="HSNCode" HeaderText="HSN" SortExpression="name" />
                            <asp:BoundField DataField="QTY" HeaderText="QTY" SortExpression="name" />
                            <asp:BoundField DataField="PV" HeaderText="BV" SortExpression="description" DataFormatString="{0:0}" />
                            <asp:BoundField DataField="DP" HeaderText="DP" SortExpression="DP" DataFormatString="{0:0}" />
                            <asp:BoundField DataField="CGST" HeaderText="CGST (%)" DataFormatString="{0:0.00}"
                                SortExpression="description" />
                            <asp:BoundField DataField="CGSTAmount" HeaderText="CGST (Rs)" DataFormatString="{0:0.00}"
                                SortExpression="description" />
                            <asp:BoundField DataField="SGST" HeaderText="SGST (%)" DataFormatString="{0:0.00}"
                                SortExpression="description" />
                            <asp:BoundField DataField="SGSTAmount" HeaderText="SGST (Rs)" DataFormatString="{0:0.00}"
                                SortExpression="description" />
                            <asp:BoundField DataField="IGST" HeaderText="IGST (%)" DataFormatString="{0:0.00}"
                                SortExpression="description" />
                            <asp:BoundField DataField="IGSTAmount" HeaderText="IGST (Rs)" DataFormatString="{0:0.00}" />
                            <asp:BoundField DataField="Amount" HeaderText="TOTAL(Rs)" ItemStyle-HorizontalAlign="Right"
                                DataFormatString="{0:0.00}" />
                        </Columns>
                    </asp:GridView>
                    <table class="table table-bordered">
                        <tr class="table-no-padding">
                            <td rowspan="7" colspan="3" style="text-align: center; padding-top: 70px;">
                                Bank Account Number
                            </td>
                            <td rowspan="2" style="font-style: italic; font-size: 14px;">
                                Taxable Value (Rs)
                            </td>
                            <td colspan="2" style="text-align: center; font-style: italic; font-size: 14px;">
                                CGST
                            </td>
                            <td colspan="2" style="text-align: center; font-style: italic; font-size: 14px;">
                                SGST
                            </td>
                            <td colspan="2" style="text-align: center; font-style: italic; font-size: 14px;">
                                IGST
                            </td>
                            <td colspan="2">
                                <b>Gross Amount (Rs)</b>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblGrossAmount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="table-no-padding">
                            <td style="font-style: italic; font-size: 14px;">
                                Rate(%)
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                Amt(Rs)
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                Rate(%)
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                Amt(Rs)
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                Rate(%)
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                Amt(Rs)
                            </td>
                            <td colspan="2">
                                <b>SGST (Rs)</b>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblSGSTAmount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="table-no-padding">
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblTaxable5" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                2.5
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblCGST5" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                2.5
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblSGST5" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                5
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblIGST5" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <b>CGST (Rs)</b>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblCGSTAmount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="table-no-padding">
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblTaxable12" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                6
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblCGST12" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                6
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblSGST12" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                12
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblIGST12" runat="server" Text=""></asp:Label>
                            </td>
                            <td colspan="2">
                                <b>IGST (Rs)</b>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblIGSTAmount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="table-no-padding">
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblTaxable18" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                9
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblCGST18" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                9
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblSGST18" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                18
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblIGST18" runat="server" Text=""></asp:Label>
                            </td>
                            <td rowspan="3" colspan="2">
                                <b>Net Payable (Rs)</b>
                            </td>
                            <td rowspan="3" style="text-align: right">
                                <asp:Label ID="lblNetPayable" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="table-no-padding">
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblTaxable28" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                14
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblCGST28" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                14
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblSGST28" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                28
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblIGST28" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="table-no-padding">
                            <td style="font-style: italic; font-size: 14px;">
                                Total
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblTotalCGST" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblTotalSGST" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                            </td>
                            <td style="font-style: italic; font-size: 14px;">
                                <asp:Label ID="lblTotalIGST" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="4" colspan="8">
                                <b>TERMS & CONDITIONS</b>
                                <br>
                                <span>1. E & O.E. subject to MYSURU Jurisdiction </span>
                                <br>
                                <span>2. Goods once sold will not be Accepted or Exchanged </span>
                                <br>
                                <span>3. We certify that we are registered under GST Act and liable to pay tax.
                                </span>
                            </td>
                          <td rowspan="4" colspan="5"
                              style="text-align: center; padding-top: 30px !important;">
                              For DLS Biotech
                              <br />
                              <img src="../image/dlssi.png"
                                   style="width: 120px; height: 60px; object-fit: contain;" />
                              <br />
                              <span>Authorized Signatory</span>
                          </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <center>
        <input onclick="printDiv('printableArea')" type="button" value="Print" style="cursor: pointer;" />
    </center>
    <br />
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM"
        crossorigin="anonymous"></script>
</body>
</html>
