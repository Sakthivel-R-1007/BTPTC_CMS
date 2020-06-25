$(document).ready(function () {
    initializeValidation();
    intializepagination();
    $(document).on("click", "#DeleteAnnualReport", DeletePopup);
    $(document).on("click", "#btnconfirm_delete", submitDelete);
    $(document).on("click", "#annualreportEdit", EditPopup);
    $(document).on("click", "#editannualreport", update);
    $(document).on("click", "#addannualreport", add);
    $(document).on("click", "#addNewBtn", addPopup);
   
     $(document).on("change", "input[name=uploadAttach_1]", function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#uploadAttach_1').data('imageWidth', this.width);
                $('#uploadAttach_1').data('imageHeight', this.height);

                //alert("The image width is " + this.width + " and image height is " + this.height);
            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("uploadFile_1").value = filename;
    });

    $(document).on("change", "#uploadAttach_1", function () {
        $("#ImageFilePath1").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath1");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });

    $(document).on("change", "input[name=uploadAttach_PDF_2]", function () {

        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("PDFFileName").value = filename;
    });

});


function addPopup() {
    var modal = document.getElementById("add");
    modal.style.display = "block";
    var path = apppath + "/Admin/NewsRoom/AnnualReport/AddPartialView";
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            console.log(response);
            $("#AnnualReportAddForm").html('');
            $("#AnnualReportEditForm").html('');
            $("#AnnualReportAddForm").append(response);
        },
        failure: function (response) {
            console.log(response);
            $("#AnnualReportAddForm").append(('Error occured'));
        }
    });
}
function EditPopup() {
    $("#dynamic_container").html('')
    var uniquecode = $(this).attr("data-uniquecode");
    var path = apppath + "/Admin/NewsRoom/AnnualReport/EditPartialView/" + uniquecode;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            console.log(response);
            $("#AnnualReportEditForm").html('');
            $("#AnnualReportAddForm").html('');
            $("#AnnualReportEditForm").append(response);
        },
        failure: function (response) {
            console.log(response);
            $("#AnnualReportEditForm").append(('Error occured'));
        }
    });
}
function add() {
    $("#AnnualReportAddForm").attr({
        action: "/Admin/AnnualReport/Save"
    }).submit();
}
function update() {
    $("#AnnualReportEditForm").attr({
        action: "/Admin/AnnualReport/Save"
    }).submit();
}

function initializeValidation() {
    jQuery.validator.setDefaults({
        ignore: []
    });
    jQuery.validator.addMethod('dimention', function (value, element, param) {
        if (element.files.length == 0) {
            return true;
        }
        var width = $(element).data('imageWidth');
        //alert(width);
        var height = $(element).data('imageHeight');
        //alert(height);
        if (width == param[0] && height == param[1]) {
            return true;
        } else {
            return false;
        }
    }, 'Please upload an image with valid dimension');

    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');

    $("#AnnualReportEditForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Title: {
                required: true
                //remote: {
                //    url: apppath + "/Admin/Gallery/CheckGalleryTitle",
                //    type: "GET",
                //    async: true,
                //    cache: false,
                //    data: {
                //        TicketNumber: function () {
                //            return $("#Title").val();
                //        },
                //        EncDetail: function () {
                //            return $("#GUID").val() || null;
                //        }
                //    },
                //    dataType: 'json'
                //}
            },
            uploadAttach_1: {
                //required: true,
                extension: 'jpg|jpeg|png',
                dimention: [500, 700]
            },
            uploadAttach_PDF_2: {
                //required: true,
                extension: 'pdf'
                
            },
            ImageAltTag: {
                required: true
            }
        }


    });

    $("#AnnualReportAddForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Title: {
                required: true
                //remote: {
                //    url: apppath + "/Admin/Gallery/CheckGalleryTitle",
                //    type: "GET",
                //    async: true,
                //    cache: false,
                //    data: {
                //        TicketNumber: function () {
                //            return $("#Title").val();
                //        },
                //        EncDetail: function () {
                //            return $("#GUID").val() || null;
                //        }
                //    },
                //    dataType: 'json'
                //}
            },
            uploadAttach_1: {
                required: true,
                extension: 'jpg|jpeg|png',
                dimention: [500, 700]
            },
            uploadAttach_PDF_2: {
                required: true,
                extension: 'pdf'

            },
            ImageAltTag: {
                required: true
            }
        }


    });
}


function DeletePopup() {
    $('#btnconfirm_delete').attr('data-value', $(this).attr("data-uniquecode"));
}
function submitDelete() {
    //window.location = apppath + "/Admin/UserAccounts/Delete/" + $("#EncDetail").val();
    alert($('#btnconfirm_delete').data('value'));
    $("#dynamic_container").html('').append($("<input>", {
        id: "EncDetail",
        name: "EncDetail",
        type: "hidden",
        value: $('#btnconfirm_delete').data('value')
    }));


    $("#Form").attr({
        action: "/Admin/NewsRoom/AnnualReport/Delete"
    }).submit();
}


function intializepagination() {
    $(".pagination").pagination({
        items: $('.pagination').data('totalitems'),
        itemsOnPage: 15,
        currentPage: $('.pagination').data('pageindex'),
        hrefTextPrefix: "",
        prevText: "&laquo;",
        nextText: "&raquo;",
        cssStyle: "",
        onPageClick: function (pageNumber, event) {
            event.preventDefault();
            bindPageItems({
                PageNumber: pageNumber

            });
            $('.pagination').attr('data-pageindex', pageNumber);
        }
    });
}
var pageSize = 15;
function bindPageItems(e) {
    //var path = "/Admin/Category/PartialIndex/" + e.PageNumber + "/" + e.Category + "/" + e.SubCategory + "/" + e.SortBy + "/" + e.SortDirection;
    var path = apppath + "/Admin/NewsRoom/AnnualReport/PartialView/" + e.PageNumber;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response == '') {
                response = '<tr><td colspan="5" class="center">No Results Found</td></tr>';
            }

            $(".tblgrey tbody").html(response);
            var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
            //console.log(pageIndex);
            var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
            var lastItem = startItem + ($(".tblgrey tbody tr").length - 1);
            $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

        },
        failure: function (response) {
            //console.log(response);
            $(".tblgrey tbody").html('<tr><td colspan="5" class="center error">Error occured</td></tr>');
        }
    });

}