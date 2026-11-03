<%@ Page Title="ID Card" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="id.aspx.cs" Inherits="id" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @import url(http://fonts.googleapis.com/earlyaccess/notosanskannada.css);
        body
        {
            font-family: "Noto Sans Kannada" , sans-serif;
            font-size: 19.0px;
            line-height: 1.11em;
        }
    </style>
    <style>
        .front
        {
            background-image: url('./image/slider/Id\ 2\ NOS.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            padding: 30px;
            width: 100% !important;
        }
        
        .back
        {
            background-image: url('./image/slider/Id\ 2\ NOS.png');
            background-size: cover;
            background-position: bottom;
            background-repeat: no-repeat;
            padding: 30px;
            width: 100% !important;
        }
        
        .back1
        {
            background-color: #EDF2D4;
            width: 100%;
        }
        
        .back-body1
        {
            padding: 30px;
            background-color: #298637;
            background: #298637;
            width: 100%;
            clip-path: polygon(100% 0, 100% 57%, 54% 100%, 0 100%, 0 0);
        }
        
        .head-card
        {
            padding: 20px;
            color: #0F9C4C;
        }
        
        .card-title
        {
            color: #E7E117;
            font-size: 20px;
            font-weight: bold;
            text-transform: uppercase;
            transform: rotate(-90deg);
            position: relative;
            left: -180px;
            margin-top: 100px;
        }
        
        .img-bg
        {
            justify-content: center;
            background-color: #fff;
            border: 5px solid #95B339;
            border-radius: 20px;
            width: 200px;
            height: 250px;
            margin-left: 25%;
            margin-top: 40px; /* position: absolute;
        top: 80%;
        left: 40%;
        margin-right: -50%;
        transform: translate(-50%, -50%); */
        }
        
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            border: none;
        }
        
        .table > tbody > tr > th
        {
            color: #71628E;
        }
        
        .front-card-body
        {
            position: relative;
            top: 70px;
        }
        
        .office-add p, .office-add p > a
        {
            color: #4A3376;
            font-weight: bold;
        }
        /* different devices */
        /* On screens that are 992px or less, set the background color to blue */
        
        @media screen and (max-width: 992px)
        {
            .col-md-6
            {
                margin-bottom: 10%;
            }
            .front-card-body
            {
                height: auto;
            }
            .table
            {
                float: none;
            }
            .card-title
            {
                position: relative;
                left: -101px;
                margin-top: 78px;
            }
            .img-bg
            {
                position: relative;
                top: -60px;
            }
            .office-add
            {
                font-size: 16px;
            }
        }
        /* On screens that are 600px or less, set the background color to olive */
        
        @media screen and (max-width: 600px)
        {
            .col-md-6
            {
                margin-bottom: 10%;
            }
            .front-card-body
            {
                height: auto;
            }
            .table
            {
                float: none;
            }
            .card-title
            {
                position: relative;
                left: -101px;
                margin-top: 78px;
                top: 40px;
            }
            .img-bg
            {
                position: relative;
                top: -60px;
            }
            .office-add
            {
                font-size: 14px;
                margin-top: 119px;
            }
            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td
            {
                font-size: 16px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="breadcrumb parallax-container">
        <div class="parallax">
            <img src="image/prlx.jpg" alt="#"></div>
        <h1>
            ID Card</h1>
        <ul>
            <li><a href="index.html">Home</a></li>
            <li><a href="id.aspx">ID Card</a></li>
        </ul>
    </div>
    <!-- about section -->

   <div class="text-center" style="margin: 25px 0;">
    <button type="button" id="whatsappShare"
        class="btn btn-success"
        style="background-color: #25D366; color: white;
        padding: 12px 25px; border: none; border-radius: 6px;
        font-weight: bold; cursor: pointer;">
        <i class="fa fa-whatsapp"></i> Share ID Card on WhatsApp
    </button>
</div>

   <div class="container" id="idCardShareArea"
     style="background-color: #ffffff; padding: 15px;">
        <div class="row">
            <div class="col-md-6">
                <div class="card front" style="width: 45rem; background-color: #EDF2D4;">
                    <div class="card-body front-card-body">
                        <h5 class="card-title">
                            Identity card</h5>
                        <div class="img-bg">
                            <img src="" runat="server" id="profilePic" style="height: 240px; width: 190px; border-radius: 15px;"
                                alt="">
                        </div>
                        <table class="table table-borderless" style="margin-top: 20px;">
                            <tbody>
                                <tr>
                                    <th scope="col">
                                        Name
                                    </th>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="col">
                                        Designation
                                    </th>
                                    <td>
                                        <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="col">
                                        ID No.
                                    </th>
                                    <td>
                                        <asp:Label ID="lblID" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="col">
                                        Mobile No.
                                    </th>
                                    <td>
                                        <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="col">
                                        Joining Date
                                    </th>
                                    <td>
                                        <asp:Label ID="lblJoining" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th scope="col">
                                    </th>
                                    <td>
                                        <div class="signature" style="float: right; margin-top: -30px;">
                                            <img src="./image/slider/signature.png" alt="">
                                            <p>
                                                Authorised Signature</p>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card back" style="width: 45rem; background-color: #EDF2D4; height: 800px;">
                    <div class="card-body ">
                        <div class="back-body">
                            <h3 style="text-align: center; color: #fff; font-size: 22px;">
                                <u>Instructions</u></h3>
                            <br>
                            <p style="text-align: justify; color: #fff;">
                                1. The company DOES NOT undertake any responsibility for any action of THE CARD
                                HOLDER. Any misuse of the Card will be at the sole responsibility of the Card Holder.
                            </p>
                            <p style="text-align: justify; color: #fff;">
                                2. In the event of loss of the card, the finder may kindly return the same to HR
                                Department.
                            </p>
                            <p style="text-align: justify; color: #fff;">
                                3. In case the card is not in use, it should be surrendered to issuing Authority.
                            </p>
                        </div>
                    </div>
                    <div class="office-add" style="text-align: right; margin-top: 200px;">
                        <h3 style="font-size: 22px; color: #20A453; font-weight: bold;">
                            Head Office :</h3>
                        <p style="text-align: right;">
                             2nd Floor, 36, MRG Complex, Santhepete Road,
                            <br>
                           Near Dalawai School, KR Mohalla, Mysuru, Karnaraka 570004
                            <br>
                        </p>
                        <p>
                            <a href="">dlsbiotech01@gmail.com <i class="fa fa-envelope"></i></a>
                            <br>
                            <a href="">www.dlsbiotech.com <i class="fa fa-globe"></i></a>
                            <br>
                            <a href=""> 0821-2867298 <i class="fa fa-phone"></i></a>
                            <!-- <a href="mailto:"></a> -->
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

<script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"></script>

<script type="text/javascript">
    document.getElementById("whatsappShare").onclick = function () {

        var button = this;
        var cardArea = document.getElementById("idCardShareArea");

        if (!cardArea) {
            alert("ID card section not found.");
            return;
        }

        button.disabled = true;
        button.innerHTML = "Preparing ID Card...";

        html2canvas(cardArea, {
            scale: 2,
            useCORS: true,
            allowTaint: false,
            backgroundColor: "#ffffff",
            logging: false,
            scrollX: 0,
            scrollY: -window.scrollY
        }).then(function (canvas) {

            canvas.toBlob(function (blob) {

                if (!blob) {
                    alert("Unable to generate ID card image.");
                    button.disabled = false;
                    button.innerHTML =
                        '<i class="fa fa-whatsapp"></i> Share ID Card on WhatsApp';
                    return;
                }

                var file = new File(
                    [blob],
                    "DLS_Biotech_ID_Card.png",
                    { type: "image/png" }
                );

                // Mobile browsers supporting image sharing
                if (navigator.share &&
                    navigator.canShare &&
                    navigator.canShare({ files: [file] })) {

                    navigator.share({
                        files: [file],
                        title: "DLS Biotech ID Card",
                        text: "My DLS Biotech ID Card"
                    }).catch(function (error) {
                        if (error.name !== "AbortError") {
                            downloadAndOpenWhatsApp(blob);
                        }
                    }).finally(function () {
                        button.disabled = false;
                        button.innerHTML =
                            '<i class="fa fa-whatsapp"></i> Share ID Card on WhatsApp';
                    });

                } else {
                    // Desktop or browsers without file sharing
                    downloadAndOpenWhatsApp(blob);

                    button.disabled = false;
                    button.innerHTML =
                        '<i class="fa fa-whatsapp"></i> Share ID Card on WhatsApp';
                }

            }, "image/png");

        }).catch(function (error) {

            console.error(error);
            alert("Unable to capture the ID card. Please try again.");

            button.disabled = false;
            button.innerHTML =
                '<i class="fa fa-whatsapp"></i> Share ID Card on WhatsApp';
        });
    };

    function downloadAndOpenWhatsApp(blob) {

        var imageUrl = URL.createObjectURL(blob);
        var downloadLink = document.createElement("a");

        downloadLink.href = imageUrl;
        downloadLink.download = "DLS_Biotech_ID_Card.png";

        document.body.appendChild(downloadLink);
        downloadLink.click();
        document.body.removeChild(downloadLink);

        setTimeout(function () {
            URL.revokeObjectURL(imageUrl);
        }, 60000);

        var message = encodeURIComponent(
            "My DLS Biotech ID Card is attached. Please find the downloaded image."
        );

        window.open(
            "https://wa.me/?text=" + message,
            "_blank"
        );
    }
</script>
</asp:Content>
