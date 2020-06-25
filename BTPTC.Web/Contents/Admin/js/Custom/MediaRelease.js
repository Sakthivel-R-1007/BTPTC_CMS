$(document).ready(function () {


    jQuery("#AddMediaReleaseForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Date: { required: true },
            "PDFFileName":
            {
                required: true,
                extension: 'pdf',
                //filesize: 52428800 /*50MB*/
            },

            Title: {
                required: true,
                remote: {
                    url: "/Admin/NewsRoom/CheckMediaTitle",
                    type: "GET",
                    async: true,
                    cache: false,
                    data: {
                        NewsTitle: function () {
                            return $("#Title").val();
                        },
                        EncDetail: function () {
                            return null;
                        }
                    },
                    dataType: 'json'
                }
            },
        },
        messages:
        {
            Title: { remote: 'Title is duplicated' },
            "PDFFileName": {
               //required: true,
                extension: "Please upload valid file formats Pdf only."
            }

        }
    });


    intializepagination();

    //start pagination

    function intializepagination() {

        $(".pagination").pagination({
            items: $('.pagination').data('totalitems'),
            itemsOnPage: 10,
            currentPage: $('.pagination').data('pageindex'),
            hrefTextPrefix: "",
            prevText: "&laquo;",
            nextText: "&raquo;",
            cssStyle: "",
            onPageClick: function (pageNumber, event) {
                event.preventDefault();
                bindPageItems({
                    PageNumber: pageNumber,
                    Year: $("#Year").val() || '',


                });
                $('.pagination').attr('data-pageindex', pageNumber);
            }
        });
    }

    var pageSize = 10;
    function bindPageItems(e) {


        var UrlParameter = "?" + ((e.PageNumber != '') ? 'PageIndex=' + e.PageNumber : '') + "&" + ((e.Year != '') ? 'Year=' + e.Year : '');

        var path = apppath + "/Admin/NewsRoom/PartialViewMediaRelease" + UrlParameter

        $.ajax({
            url: path,
            dataType: 'html',
            success: function (response) {
                if (response === undefined && response == '') {
                    response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
                }

                $("#MediaReleaseTbl tbody").html(response);
                var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
                //console.log(pageIndex);
                var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
                var lastItem = startItem + ($("#MediaReleaseTbl tbody tr").length - 1);
                $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

            },
            failure: function (response) {
                console.log(response);
                $("#MediaReleaseTbl tbody").html('<tr><td colspan="7" class="center error">Error occured</td></tr>');
            }
        });

    }



    //jQuery.validator.addMethod('filesize', function (value, element, param) {
    //    return this.optional(element) || (element.files[0].size <= param)
    //}, 'File size must be less than 2MB');



    $(document).on("change", "#addmediarelease input[name=FileName],#editMediaRelease input[name=PdfFileName]", function () {
        $(this).next().children(0).val($(this)[0].files[0].name)  

        const size =
            (this.files[0].size / 1024 / 1024).toFixed(2);

        if (size<=50) {
            $("#output").html('')
        } else {
            $("#output").html('<b>' +
                'This file size is: ' + size + " MB" + '</b>');
            $(this).val('')
        } 

    });


    $(".Date").datetimepicker({
        format: 'DD/MM/YYYY'
    });



    $(document).on("click", "#addmediareleasebtn", function () {
        var modal = document.getElementById("addmediarelease");
        $(modal).modal('show');
        AddMediaRelease();
    });


    $(document).on("click", ".editMediaReleasebtn", function () {
        var modal = document.getElementById("editMediaRelease");
        $(modal).modal('show');
        EditMediaRelease($(this).attr("data-id"));
    });


    initializeEditResourcesValidate();

    $(document).on("click", "#AddresourcessubmitBtn", function () {
        if ($("#AddMediaReleaseForm").valid()) {
            $("#AddMediaReleaseForm").submit()
        }
    });

    $(document).on("click", "#editresourcessubmitBtn", function () {
        debugger
       //if ($("#EditMediaReleaseForm").valid()) {
            $("#EditMediaReleaseForm").submit()
       // }
    });


    $(document).on("click", "#deletebtn", function () {
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#deleteeventId").val(($(this).attr("data-uniquecode")));
    });
    $(document).on("click", "#confirm_deleteiconBtn", function () {
        $("#DeleteMediaReleaseForm").submit();
    });


    $(document).ready(function () {
        $("#AddEvents").attr("style", 'height:45px !important')
    });

});








function AddMediaRelease() {

    var path = "/Admin/NewsRoom/AddMediaRelease"
    $.ajax({
        url: path,
        //type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

           $("#AddMediaReleaseForm").html('');
            $("#AddMediaReleaseForm").append(response);

            $(".Date").datetimepicker({
                format: 'DD/MM/YYYY'
            });

        },
        failure: function (response) {
            console.log(response);
            $("#AddMediaReleaseForm").append(('Error occured'));
        }
    });
}

function EditMediaRelease(GUID) {

    var path = "/Admin/NewsRoom/EditMediaRelease?EncDetail=" + GUID;
    $.ajax({
        url: path,
       
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

            $("#EditMediaReleaseForm").html('');
            $("#EditMediaReleaseForm").append(response);
              initializeEditResourcesValidate();

            $(".Date").datetimepicker({
                format: 'DD/MM/YYYY'
            });
        },
        failure: function (response) {
            console.log(response);
            $("#EditMediaReleaseForm").append(('Error occured'));
        }
    });
}


function initializeEditResourcesValidate() {
    jQuery("#EditMediaReleaseForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Date: { required: true },
            "PdfFileName": {
                required: true,
                extension: 'pdf',
               // filesize: 52428800 /*50MB*/
            },

            Title: {
                required: true,
                //remote: {
                //    url: "/Admin/NewsRoom/CheckMediaTitle",
                //    type: "GET",
                //    async: true,
                //    cache: false,
                //    data: {
                //        NewsTitle: function () {
                //            return $("#Title").val();
                //        },
                //        EncDetail: function () {
                //            return null;
                //        }
                //    },
                //    dataType: 'json'
                //}
            },
        },
        messages: {
            //Title: { remote: 'Title is duplicated' },
            "PdfFileName": {
                required: true,
                extension: "Please upload valid file formats Pdf only."
            }

        }
    });
};