<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Receipt.aspx.cs" Inherits="Member_Receipt" %>

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
    <asp:Label ID="lberror" runat="server" Visible="false"></asp:Label>
    <form id="form1" runat="server">
    <div class="container" runat="server" id="inv" style="border: 1px solid rgb(148, 145, 145); margin-bottom: 20px;">
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
                                CUSTOMER ID
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
                    <table class="table table-bordered">
                        <tr>
                            <th>
                                SN
                            </th>
                            <th>
                                DESCRIPION OF PRODUCT
                            </th>
                            <th>
                                QTY
                            </th>
                            <th>
                                PRICE
                            </th>
                            <th>
                                CGST (%)
                            </th>
                            <th>
                                CGST (Rs)
                            </th>
                            <th>
                                SGST (%)
                            </th>
                            <th>
                                SGST (Rs)
                            </th>
                            <th>
                                TOTAL
                            </th>
                        </tr>
                        <tr>
                            <td>
                                1
                            </td>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                1
                            </td>
                            <td>
                                <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                6
                            </td>
                            <td>
                               <asp:Label ID="lblCGST" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                6
                            </td>
                            <td>
                                <asp:Label ID="lblSGST" runat="server" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-bordered">
                       
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
    <center runat="server" id="print">
        <input onclick="printDiv('printableArea')" type="button" value="Print" style="cursor: pointer;" />
    </center>
    <br />
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM"
        crossorigin="anonymous"></script>
</body>
</html>
