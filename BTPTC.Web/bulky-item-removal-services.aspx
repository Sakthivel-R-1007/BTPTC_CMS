<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bulky-item-removal-services.aspx.cs" Inherits="BTPTC.Web.bulky_item_removal_services" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <title>Bishan-Toa Payoh Town Council-Bulky Item Removal Services</title>
    <!--For FB share-->
    <meta property="og:url" content="http://btptc.medialabsstreaming.com/bulky-item-removal-services.aspx" />
    <meta property="og:title" content="Bulky Item Removal Services | Bishan-Toa Payoh Town Council" />
    <meta property="og:description" content="" />
    <meta property="og:image" content="http://btptc.medialabsstreaming.com/images/bulky-service/banner-img.jpg" />


    <link href="Contents/css/reset.css" rel="stylesheet" />
    <link href="Contents/css/layout.css" rel="stylesheet" />
    <link href="Contents/css/common.css" rel="stylesheet" />
    <link href="Contents/css/styles.css" rel="stylesheet" />
    <link href="Contents/css/responsive.css" rel="stylesheet" />


    <%--<link rel="stylesheet" type="text/css" href="css/reset.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/layout.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/common.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/styles.css" media="all" />
    <link rel="stylesheet" type="text/css" href="css/responsive.css" media="all" />--%>
   
    <!--[if lt IE 9]
   <script src="https://code.jquery.com/jquery-1.12.4.min.js"></script>
    <![endif]-->


    <!--[if !(IE)|(gte IE 9)]><!-->
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <!--<![endif]-->
    <script src="Contents/scripts/jquery-ui.min.js"></script>

    <%-- <script src="scripts/vendor/modernizr-2.7.1.min.js"></script>
    <script src="scripts/common.js"></script>
    <script src="scripts/classie.min.js"></script>
    <script src="scripts/jquery.parallax-1.1.3.js" type="text/javascript"></script>
    <script src="scripts/jquery.scrollTo-1.4.3.1-min.js" type="text/javascript"></script>
    <script src="scripts/jquery.localscroll-1.2.7-min.js" type="text/javascript"></script>
    <script src="scripts/bootstrap/bootstrap.min.js"></script>--%>

    <script src="Contents/scripts/common.js"></script>
    <script src="Contents/Admin/js/classie.min.js"></script>
    <script src="Contents/Admin/js/jquery.parallax-1.1.3.js"></script>
    <script src="Contents/Admin/js/jquery.scrollTo-1.4.3.1-min.js"></script>
    <script src="Contents/Admin/js/jquery.localscroll-1.2.7-min.js"></script>
    <script src="Contents/Admin/js/bootstrap/bootstrap.js"></script>

    <!--Date Picker-->
    <script src="Contents/scripts/daterangepicker/moment.min.js"></script>
    <script src="Contents/scripts/daterangepicker/daterangepicker.min.js"></script>
    <link href="Contents/scripts/daterangepicker/daterangepicker.css" rel="stylesheet" />


    <%-- <script type="text/javascript" src="scripts/daterangepicker/moment.min.js"></script>
    <script type="text/javascript" src="scripts/daterangepicker/daterangepicker.min.js"></script>
    <link rel="stylesheet" type="text/css" href="scripts/daterangepicker/daterangepicker.css" />--%>
    <!--Facebook-->
    <script src="//connect.facebook.net/en_US/all.js" type="text/javascript"></script>
    <div id="fb-root"></div>
    <script>
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.8&appId=506027563414721";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));

        jQuery(document).ready(function ($) {
            $('header').load('Contents/includes/header.html');
            $('.contactDetails').load('Contents/includes/contactDetails.html');
            $('.footer').load('Contents/includes/footer.html');
        });

    </script>
</head>
<body class="OURSERVICES">
    <div class="wrapper">
        <header>
            <div class="header">

            </div>
        </header>
        <!--End Header-->

        <div class="mainContents">
            <div class="contactDetails desktopOnly"></div>
            <div class="contactDetails mobileOnly"></div>

            <div class="pageBanner">
                <img src="Contents/images/bulky-service/banner-img.jpg" alt="">
            </div>

            <div class="bulkyRemoveService">
                <div class="pageContents">
                    <div class="container">
                        <div class="topBar">
                            <div class="breadcrumb"><a href="index.html">Home</a><span></span><a href="#">Our Services</a><span></span>Bulky Item Removal Services</div>
                            <div class="socialShareBtns">
                                <ul>
                                    <li>Share:</li>
                                    <li><a href="http://www.facebook.com/sharer.php?u=http://btptc.medialabsstreaming.com/bulky-item-removal-services.aspx" target="_blank">
                                        <img src="Contents/images/common/fb-share-btn.png" alt=""></a></li>
                                    <li><a href="https://twitter.com/share?text=Bulky Item Removal Services | Bishan-Toa Payoh Town Council,;url=http://btptc.medialabsstreaming.com/bulky-item-removal-services.aspx" target="_blank">
                                        <img src="Contents/images/common/twitter-share-btn.png" alt=""></a></li>
                                    <!--li><a href="#" target="_blank"><img src="images/common/google-share-btn.png" alt="" target="_blank"></a></li-->
                                    <li><a href="mailto:?subject=Bulky Item Removal Services | Bishan-Toa Payoh Town Council&Body=I thought you might be interested in this article: http://btptc.medialabsstreaming.com/bulky-item-removal-services.aspx">
                                        <img src="Contents/images/common/email-share-btn.png" alt="" target="_blank"></a></li>
                                    <li class="print"><a href="#" onclick="window.print();">
                                        <img src="Contents/images/common/print-btn.png" alt="" target="_blank"></a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="bulkyRemoveServiceForm">
                            <h1>Bulky Item Removal Services</h1>

                            <div class="bulkyTermsConditions" id="divBTC" runat="server">
                                <p><strong>(Not applicable for commercial units)</strong></p>

                                <p>Read the terms and conditions before proceeding to online application.</p>

                                <h3>Terms and Conditions</h3>
                                <ul>
                                    <li>This service is free of charge and request can be:
										<ul>
                                            <li>Phone call</li>
                                            <li>Online Submission</li>
                                            <li>Walk-in at Town Council Office</li>
                                        </ul>
                                    </li>

                                    <li>The service is available from Mondays to Fridays between 2 to 5 pm. The applicant is to give at least 2 working days advance notice upon request (except Saturdays, Sundays and Public Holidays).</li>

                                    <li>Town Council will remove up to 3 items each time, once a month.</li>

                                    <li>Town Council removal service does not cover the following:
										<ul>
                                            <li>Renovation items, eg. customised & built-in cabinet, window, frame, door, toilet bowl, sink, and the like</li>
                                            <li>Moving out of unit</li>
                                            <li>Heavy items, eg. fish tank, organ, piano, safe and the like</li>
                                            <li>Small items, eg. newspaper, boxes, clothing and the like</li>
                                            <li>Furniture too large to move out of the unit.</li>
                                        </ul>
                                        (Note: Applicant has to dismantle the item prior to placing outside the unit)
                                    </li>

                                    <li>Applicant to tag the items for removal and place them outside the unit on removal day. However, for situations where the items cannot be placed outside the unit, our workers will enter the unit with the resident consent, to remove the item from the unit. (Note: Our Conservancy Workers will not be held liable for any damages during the removal of the items).</li>

                                    <li>For requests to remove more than 3 items, the applicant can make their own private arrangement with the conservancy manager with regards to the fee. Town Council shall not be responsible for matters pertaining to this private arrangement.</li>
                                </ul>

                                <a href="#serviceForm" class="submitBtn">Agree</a>
                            </div>

                            <div id="serviceForm" runat="server">
                                <form enctype="multipart/form-data" runat="server" id="BulkyItemRemovalForm" style="display: none;">
                                    <h2>Resident Information</h2>
                                    <div class="commonForm">
                                        <span class="fieldName">Name: *</span>
                                        <span>
                                            <input type="text" name="Name" id="Name" placeholder="">
                                            <label id="Name-error" class="error" for="Name"></label>
                                            <!--div class="error">Do not leave this field empty.</div-->
                                        </span>

                                        <span class="fieldName">Email:</span>
                                        <span>
                                            <input type="text" name="Email" id="Email" placeholder="">
                                            <label id="Email-error" class="error" for="Email"></label>
                                        </span>

                                        <span class="fieldName">Blk No: *</span>
                                        <span>
                                            <div class="blkNum">
                                                <input type="text" name="BlkNo" id="BlkNo" placeholder=""><label id="BlkNo-error" class="error" for="BlkNo"></label>
                                            </div>
                                            <div class="UnitNo">
                                                <div class="subFieldName">Unit No: *</div>
                                                <div class="symbol">#</div>
                                                <div class="floor">
                                                    <input type="text" name="UnitNo1" id="UnitNo1" placeholder=""><label id="UnitNo1-error" class="error" for="UnitNo1"></label>
                                                </div>
                                                <div class="symbol">-</div>
                                                <div class="unit">
                                                    <input type="text" name="UnitNo2" id="UnitNo2" placeholder=""><label id="UnitNo2-error" class="error" for="UnitNo2"></label>
                                                </div>
                                            </div>
                                            <!--div class="error">Do not leave this field empty.</div-->
                                        </span>

                                        <span class="fieldName">Street Name: *</span>
                                        <span>
                                            <select name="StreetName" id="StreetName">
                                                <option selected disabled>Please Select</option>
                                                <option value="AH HOOD ROAD">Ah Hood Road</option>
                                                <option value="BALESTIER ROAD">Balestier Road</option>
                                                <option value="BISHAN STREET 11">Bishan Street 11</option>
                                                <option value="BISHAN STREET 12">Bishan Street 12</option>
                                                <option value="BISHAN STREET 13">Bishan Street 13</option>
                                                <option value="BISHAN STREET 22">Bishan Street 22</option>
                                                <option value="BISHAN STREET 23">Bishan Street 23</option>
                                                <option value="BISHAN STREET 24">Bishan Street 24</option>
                                                <option value="BRIGHT HILL DRIVE">Bright Hill Drive</option>
                                                <option value="JALAN DUSUN">Jalan Dusun</option>
                                                <option value="KIM KEAT AVENUE">Kim Keat Avenue</option>
                                                <option value="KIM KEAT LINK">Kim Keat Link</option>
                                                <option value="LORONG 1 TOA PAYOH">Lorong 1 Toa Payoh</option>
                                                <option value="LORONG 1A TOA PAYOH">Lorong 1A Toa Payoh</option>
                                                <option value="LORONG 2 TOA PAYOH">Lorong 2 Toa Payoh</option>
                                                <option value="LORONG 3 TOA PAYOH">Lorong 3 Toa Payoh</option>
                                                <option value="LORONG 4 TOA PAYOH">Lorong 4 Toa Payoh</option>
                                                <option value="LORONG 5 TOA PAYOH">Lorong 5 Toa Payoh</option>
                                                <option value="LORONG 6 TOA PAYOH">Lorong 6 Toa Payoh</option>
                                                <option value="LORONG 7 TOA PAYOH">Lorong 7 Toa Payoh</option>
                                                <option value="LORONG 8 TOA PAYOH">Lorong 8 Toa Payoh</option>
                                                <option value="MOULMEIN ROAD">Moulmein Road</option>
                                                <option value="SECTOR A SIN MING INDUSTRIAL ESTATE">Sector A Sin Ming Industrial Estate</option>
                                                <option value="SHUNFU ROAD">Shunfu Road</option>
                                                <option value="SIN MING AVENUE">Sin Ming Avenue</option>
                                                <option value="SIN MING ROAD">Sin Ming Road</option>
                                                <option value="THOMSON ROAD">Thomson Road</option>
                                                <option value="TOA PAYOH NORTH">Toa Payoh North</option>
                                                <option value="TOA PAYOH EAST">Toa Payoh East</option>
                                                <option value="TOA PAYOH CENTRAL">Toa Payoh Central</option>
                                            </select>
                                            <label id="StreetName-error" class="error" for="StreetName"></label>
                                            <!--div class="error">Do not leave this field empty.</div-->
                                        </span>

                                        <span class="fieldName vtop">Contact No.: *</span>
                                        <span>
                                            <div class="telFieldContainer">
                                                <div class="telFieldName">Mobile No.:</div>
                                                <div class="telSymbol">+65</div>
                                                <div class="telField">
                                                    <input type="text" name="Mobile" id="Mobile" placeholder="" class="Numeric">
                                                    <label id="Mobile-error" class="error" for="Mobile"></label>
                                                </div>
                                                <div class="padding5"></div>
                                                <div class="telFieldName">Office / Residential:</div>
                                                <div class="telSymbol">+65</div>
                                                <div class="telField">
                                                    <input type="text" name="OfficeResidential" id="OfficeResidential" placeholder="" class="Numeric">
                                                    <label id="OfficeResidential-error" class="error" for="OfficeResidential"></label>
                                                </div>
                                            </div>
                                            <!--div class="error">Please enter at least one contact no.</div-->
                                        </span>
                                    </div>

                                    <h2>Appointment Details</h2>

                                    <div class="commonForm">
                                        <span class="fieldName">Appointment Date:</span>
                                        <span>
                                            <input type="text" name="AppointmentDate" id="AppointmentDate" class="calendar" readonly>
                                            <label id="AppointmentDate_error" class="error" for="AppointmentDate" runat="server"></label>
                                        </span>

                                        <span class="fieldName">Appointment Time:</span>
                                        <span>
                                            <p>2pm to 5pm</p>
                                        </span>
                                        <input type="hidden" name="AppointmentTime" id="AppointmentTime" value="2pm to 5pm">

                                        <span class="fieldName vtop">Item Description:</span>
                                        <span>
                                            <input type="text" name="ItemDescription1" id="ItemDescription1" placeholder="Item 1">
                                            <div class="padding5"></div>
                                            <input type="text" name="ItemDescription2" id="ItemDescription2" placeholder="Item 2">
                                            <div class="padding5"></div>
                                            <input type="text" name="ItemDescription3" id="ItemDescription3" placeholder="Item 3">
                                        </span>

                                        <span class="fieldName vtop">Attachment: </span>
                                        <span>
                                            <div class="upload">
                                                <input type="button" class="uploadBtn" value="Choose File"><input type="file" name="uploadAttach_1" />
                                                <!--label id="uploadFile">No file chosen</label-->
                                                <label>
                                                    <input type="text" name="uploadFile_1" id="uploadFile_1" placeholder="No file chosen"></label>
                                            </div>
                                            <label id="uploadAttach_1-error" class="error" for="uploadAttach_1"></label>

                                            <div class="upload">
                                                <input type="button" class="uploadBtn" value="Choose File"><input type="file" name="uploadAttach_2" />
                                                <!--label id="uploadFile">No file chosen</label-->
                                                <label>
                                                    <input type="text" name="uploadFile_2" id="uploadFile_2" placeholder="No file chosen"></label>
                                            </div>
                                            <label id="uploadAttach_2-error" class="error" for="uploadAttach_2"></label>
                                            <div class="upload">
                                                <input type="button" class="uploadBtn" value="Choose File"><input type="file" name="uploadAttach_3" />
                                                <!--label id="uploadFile">No file chosen</label-->
                                                <label>
                                                    <input type="text" name="uploadFile_3" id="uploadFile_3" placeholder="No file chosen"></label>
                                            </div>
                                            <label id="uploadAttach_3-error" class="error" for="uploadAttach_3"></label>
                                            <em class="smallNote">Note: File attachment's size should not exceed 5 Megabytes (MB).</em>
                                        </span>

                                        <hr class="divider" />

                                        <span class="fieldName vtop">Captcha *</span>
                                        <span>
                                            <div class="captcha">
                                                <div class="imgContainer">
                                                    <asp:Image ImageUrl="CaptchaHandler.ashx" runat="server" ID="imgCaptcha" />
                                                </div>
                                                <a href="javascript:void(0);" class="refreshBtn">
                                                    <img src="Contents/images/common/refresh-btn.png"></a>
                                            </div>
                                            <asp:TextBox ID="captcha" placeholder="Security Code" runat="server" AutoCompleteType="Disabled" MaxLength="5"></asp:TextBox>
                                            <label id="captcha_error" class="error" for="captcha" runat="server"></label>
                                            <!--div class="error">Do not leave this field empty.</div-->
                                        </span>
                                    </div>

                                    <div class="commonTermsLayout">
                                        <p><em>* These fields have to be completed.</em></p>

                                        <p>
                                            <strong>Important:</strong><br />
                                            <em>By submitting this form, I agree that the Town Council may collect, use and disclose all information as contained in this form for the following purposes:</em>
                                        </p>

                                        <ul>
                                            <li>to provide me with information as requested;</li>
                                            <li>to respond to enquiries made or purportedly made by me;</li>
                                            <li>to compile and analyse feedback for internal use only; and/or</li>
                                            <li>to contact me to request for more information where required.</li>
                                        </ul>

                                        <p>I further agree that the Town Council may disclose any or all of such information to:</p>

                                        <ol>
                                            <li>its affiliates, service providers and agents for the above purposes;</li>
                                            <li>public agencies for funding, reporting, statistical, research and survey purposes; and</li>
                                            <li>the Member of Parliament for Bishan-Toa Payoh GRC for communication purposes.</li>
                                        </ol>

                                        <p>I warrant that where I have disclosed personal data of other individuals in connection with this application, I have obtained the prior consent of such individuals for the Town Council to collect, use and disclose such data for the above purposes. </p>
                                    </div>

                                    <asp:Button class="submitBtn" Text="Submit" runat="server" OnClick="Submit" />
                                    <input type="reset" class="resetBtn" value="Reset">
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--End Bulky Remove Services-->
        </div>
        <!-- End Main Contents -->

        <div class="footer"></div>
    </div>
    <!--End Wrapper-->
    <script type="text/javascript" src="Contents/scripts/jqueryValidate/jquery.validate.js"></script>
    <script type="text/javascript" src="Contents/scripts/jqueryValidate/jquery.validate.additional-methods.js"></script>
    <script type="text/javascript" src="Contents/scripts/jqueryValidate/jquery.alphanum.js"></script>
   <%-- <script src="jquery.flex-modal.js"></script>--%>
    <script>

        $('header').load('includes/header.html');
        $('.contactDetails').load('includes/contactDetails.html');
        $('.footer').load('includes/footer.html');

        //$('.bulkyRemoveServiceForm form').hide();

        $('.bulkyTermsConditions a').click(function () {
            $(this).hide();
            $('.bulkyRemoveServiceForm form').slideDown(500);
        });

        $('input[name=uploadAttach_1]').change(function () {
            document.getElementById("uploadFile_1").value = $(this).val();
        });

        $('input[name=uploadAttach_2]').change(function () {
            document.getElementById("uploadFile_2").value = $(this).val();
        });

        $('input[name=uploadAttach_3]').change(function () {
            document.getElementById("uploadFile_3").value = $(this).val();
        });


        $(function () {
            /****/

            var startDate = new Date();
            var endDate = "", noOfDaysToAdd = 3, count = 0;
            while (count < noOfDaysToAdd) {
                endDate = new Date(startDate.setDate(startDate.getDate() + 1));
                if (endDate.getDay() != 0 && endDate.getDay() != 6) {
                    //Date.getDay() gives weekday starting from 0(Sunday) to 6(Saturday)
                    count++;
                }
            }
            //alert(today);
            $('input[name="AppointmentDate"]').daterangepicker({
                minDate: new Date(endDate),
                singleDatePicker: true,
                showDropdowns: true,
                autoUpdateInput: false,
                isInvalidDate: function (date) {
                    return (date.day() == 0 || date.day() == 6);
                }
            });
            $('input[name="AppointmentDate"]').on('apply.daterangepicker', function (ev, picker) {
                $(this).val(picker.startDate.format('DD/MM/YYYY'));
            });
        });

        $(document).ready(function () {
            IntializeValidation();
            $(".refreshBtn").on("click", function () {
                $.ajax({
                    type: "POST",
                    url: 'bulky-item-removal-services.aspx/RefreshCaptcha',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        $("#captcha").val('');
                        $.ajax({
                            url: 'CaptchaHandler.ashx?method=ajax&' + response.d,
                            type: 'POST',
                            contentType: "image/jpeg",
                            success: function (data) {
                                document.getElementById('imgCaptcha').src = data;
                            },
                            error: function (errorText) {
                                console.log("Wwoops something went wrong !");
                            }
                        });
                    }
                });
            });
        });
        function IntializeValidation() {

            jQuery.validator.addMethod('filesize', function (value, element, param) {
                return this.optional(element) || (element.files[0].size <= param)
            }, 'File size must be less than 5MB');

            //jQuery.validator.addMethod('lessthen', function (value, element, param) {
            //	return this.optional(element) || parseInt(value) <= parseInt($(param).val());
            //}, 'Invalid value');

            $("#BulkyItemRemovalForm").validate({
                errorPlacement: function (error, element) {
                    $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
                },
                rules: {
                    Name: {
                        required: true
                    },
                    Mobile: {
                        required: true,
                        minlength: 8,
                        maxlength: 8
                    },
                    OfficeResidential: {
                        required: true,
                        minlength: 8,
                        maxlength: 8
                    },
                    Email: {
                        required: true,
                        email: true
                    },
                    BlkNo: {
                        required: true
                    },
                    UnitNo1: {
                        required: true
                    },
                    UnitNo2: {
                        required: true
                    },
                    StreetName: {
                        required: true
                    },
                    uploadAttach_1: {
                        extension: 'jpg|jpeg|png',
                        filesize: 5242880 /*5MB*/
                    },
                    uploadAttach_2: {
                        extension: 'jpg|jpeg|png',
                        filesize: 5242880 /*5MB*/
                    },
                    uploadAttach_3: {
                        extension: 'jpg|jpeg|png',
                        filesize: 5242880 /*5MB*/
                    },
                    captcha: {
                        required: true
                    }
                }
            });
        }
        $(".Numeric").numeric({
            allowMinus: false,
            allowThouSep: false,
            maxDigits: 10
        });
    </script>
</body>
</html>


