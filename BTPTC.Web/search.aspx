<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="BTPTC.Web.search" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <title>Bishan-Toa Payoh Town Council-Search</title>
    <!--For FB share-->
    <meta property="og:url" content="http://btptc.medialabsstreaming.com/join-us.html" />
    <meta property="og:title" content="Join Us | Bishan-Toa Payoh Town Council" />
    <meta property="og:description" content="" />
    <meta property="og:image" content="http://btptc.medialabsstreaming.com/images/join-us/banner-img.jpg" />
   
    <link href="Contents/css/reset.css" rel="stylesheet" />
    <link href="Contents/css/layout.css" rel="stylesheet" />
    <link href="Contents/css/common.css" rel="stylesheet" />
    <link href="Contents/css/styles.css" rel="stylesheet" />
    <link href="Contents/css/responsive.css" rel="stylesheet" />

    <!--[if lt IE 9]>
<script src="https://code.jquery.com/jquery-1.12.4.min.js"></script>
<![endif]-->
    <!--[if !(IE)|(gte IE 9)]><!-->
    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <!--<![endif]-->
    <script src="Contents/scripts/jquery-ui.min.js"></script>
    
<%--    <script src="scripts/vendor/modernizr-2.7.1.min.js"></script>--%>
   
     <script src="Contents/scripts/common.js"></script>
    <script src="Contents/Admin/js/classie.min.js"></script>
    <script src="Contents/Admin/js/jquery.parallax-1.1.3.js"></script>
    <script src="Contents/Admin/js/jquery.scrollTo-1.4.3.1-min.js"></script>
    <script src="Contents/Admin/js/jquery.localscroll-1.2.7-min.js"></script>
    <script src="Contents/Admin/js/bootstrap/bootstrap.js"></script>

    <!--Facebook-->
    <script src="//connect.facebook.net/en_US/all.js" type="text/javascript"></script>
    <div id="fb-root"></div>
    <script>(function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.8&appId=506027563414721";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));</script>
</head>
<body class="CONTACTUS">
    <div class="wrapper">
        <header>
            <div class="header"></div>
        </header>
        <!--End Header-->


        <div class="mainContents">
            <div class="contactDetails desktopOnly"></div>
            <!--End Contact Details (Contact Num, Font size adjustment and Social Media)-->
            <div class="contactDetails mobileOnly"></div>
            <!--End Contact Details (Contact Numbers)-->

            <div class="searchResult">
                <div class="pageContents">
                    <div class="container">
                        <div class="topBar">
                            <div class="breadcrumb"><a href="index.html">Home</a><span></span>Search Result(s)</div>
                        </div>
                        <div class="searchResultContainer">
                            <h1>Search Result(s)</h1>

                            <p class="searchIcon"><strong><label id="countId"></label></strong> item(s) found on "<strong><%=srchSite!=null ?srchSite.SearchWords: "-" %></strong>"</p>

                            <ul>
                                <asp:PlaceHolder ID = "PlaceHolder1" runat="server" />
                                
                            </ul>

                        </div>
                    </div>
                </div>
            </div>
            <!--End Search Result-->
        </div>
        <!-- End Main Contents -->

        <div class="footer"></div>
    </div>
    <!--End Wrapper-->
    <script>

        $('header').load('includes/header.html');
        $('.contactDetails').load('includes/contactDetails.html');
        $('.footer').load('includes/footer.html');

        $(document).ready(function () {
            $("#countId").html($('ul li').length)

           
        });
    </Script>
</body>
</html>

