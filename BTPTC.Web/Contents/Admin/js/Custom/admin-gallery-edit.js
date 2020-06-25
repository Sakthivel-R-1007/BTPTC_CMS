$(document).ready(function () {
    initializeValidation();
    intializepagination();
    initializePhotoValidation();
    $(document).on("click", "#galleryDelete", DeletePopup);
    $(document).on("click", "#btnconfirm_delete", submitDelete);
    $(document).on("click", "#GalleryPhotoEdit", EditPopup);
    $(document).on("click", "#editGalleryPhotoForm", updateStory);
    $(document).on("click", "#addGalleryPhotoForm", addStory);
    $(document).on("click", "#AddGalleryPhoto", addStoryPopup);
    $(document).on("click", "#submitBtn", Confirm);
    $("#DateString").datepicker({
        dateFormat: 'dd/mm/yy',
        //minDate: new Date(),
    });


    //$(document).on("change", "input[name=uploadAttach_1]", function () {
    //    var _URL = window.URL || window.webkitURL;
    //    var image, file;

    //    if ((file = this.files[0])) {
    //        image = new Image();
    //        image.onload = function () {
    //            $('#uploadAttach_1').data('imageWidth', this.width);
    //            $('#uploadAttach_1').data('imageHeight', this.height);

    //            //alert("The image width is " + this.width + " and image height is " + this.height);
    //        };
    //        image.src = _URL.createObjectURL(file);
    //    }
    //    var filename = $(this).val();
    //    if (filename.substring(3, 11) == 'fakepath') {
    //        filename = filename.substring(12);
    //    }
    //    document.getElementById("uploadFile_1").value = filename;
    //});

    $(document).on("change", "input[name=uploadAttach_1]", function (e) {
        // console.log(e.currentTarget.files);
        $("#uploadFile_1").hide();
        var numFiles = e.currentTarget.files.length;
        var extensions = ["jpg", "jpeg", "png"];
        for (i = 0; i < numFiles; i++) {
            var _URL = window.URL || window.webkitURL;
            var image, file, width;

            //if (extensions.indexOf(e.currentTarget.files[i].name.substring(
            //    e.currentTarget.files[i].name.lastIndexOf('.') + 1).toLowerCase()) < 0) {
            //    $("span[data-valmsg-for='uploadAttach_1']").html("<span class='error'>Please select valid image file( jpg, jpeg, png).</span>");
            //    break;

            //} else {
                file = this.files[i];
                image = new Image();
                image.onload = function () {
                    width = this.width;
                    //alert("The image width is " + this.width + " and image height is " + this.height);
                    if (width != 1024) {
                        $('#uploadAttach_1').data('imageWidth', this.width);
                    } else {
                        if ($('#uploadAttach_1').data('imageWidth')||"" == "") {
                            $('#uploadAttach_1').data('imageWidth', this.width);
                        }
                    }

                };
                image.src = _URL.createObjectURL(file);

                //file = this.files[i];
                //image = new Image();
                //image.onload = function () {
                //    width = this.width;
                //    //alert("The image width is " + this.width + " and image height is " + this.height);
                //    alert(width);
                //    if (width != 1024) {
                //        alert(width);
                //        $("span[data-valmsg-for='uploadAttach_1']").html("<span class='error'>Only image width 1024 is allowed.</span>");
                //        break;
                //    }
                //};
                //image.src = _URL.createObjectURL(file);

           // }
            fileSize = parseInt(e.currentTarget.files[i].size, 10) / 1024;
            filesize = Math.round(fileSize);
            $('<li />').text(this.files[i].name).appendTo($('#output'));
            //$('<span />').addClass('filesize').text('(' + filesize + 'kb)').appendTo($('#output li:last'));
        }


    });

    $(document).on("change", "input[name=uploadAttach_2]", function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#uploadAttach_2').data('imageWidth', this.width);
                $('#uploadAttach_2').data('imageHeight', this.height);

                //alert("The image width is " + this.width + " and image height is " + this.height);
            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("uploadFile_2").value = filename;
    });

    $(document).on("change", "#uploadAttach_2", function () {
        $("#ImageFilePath2").attr("src", "");
        var photo = this;
        $img = $("#ImageFilePath2");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));

    });
});


function addStoryPopup() {
    var modal = document.getElementById("add");
    modal.style.display = "block";
    var uniquecode = $(this).attr("data-uniquecode");
    var path = apppath + "/Admin/Gallery/AddGalleryPhotoPartialView/" + uniquecode;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

            $("#GalleryPhotoAddFrom").html('');
            $("#GalleryPhotoAddFrom").append(response);
        },
        failure: function (response) {
            console.log(response);
            $("#GalleryPhotoAddFrom").append(('Error occured'));
        }
    });
}
function EditPopup() {
    var uniquecode = $(this).attr("data-uniquecode");
    var uniquecodeGuid = $("#AddGalleryPhoto").attr("data-uniquecode");
    var path = apppath + "/Admin/Gallery/EditGalleryPhotoPartialView/" + uniquecode + "/" + uniquecodeGuid;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

            $("#PhotoGalleryEditForm").html('');
            $("#PhotoGalleryEditForm").append(response);
        },
        failure: function (response) {
            console.log(response);
            $("#PhotoGalleryEditForm").append(('Error occured'));
        }
    });
}
function addStory() {
    $("#GalleryPhotoAddFrom").attr({
        action: "/Admin/Gallery/AddGalleryPhotoPartialView"
    }).submit();
}
function updateStory() {
    $("#PhotoGalleryEditForm").attr({
        action: "/Admin/Gallery/EditGalleryPhotoPartialView"
    }).submit();
}

function initializeValidation() {
    $("#EditGalleryForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Title: {
                required: true,
                remote: {
                    url: apppath + "/Admin/Gallery/CheckGalleryTitle",
                    type: "GET",
                    async: true,
                    cache: false,
                    data: {
                        TicketNumber: function () {
                            return $("#Title").val();
                        },
                        EncDetail: function () {
                            return $("#GUID").val() || null;
                        }
                    },
                    dataType: 'json'
                }
            },
            DateString: {
                required: true
            },
            ShortDescription: {
                required: function (e) {
                    CKEDITOR.instances[e.id].updateElement();
                    var editorcontent = e.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                },
            }

        },
        messages: {
            Title: { remote: 'Title is duplicated' },
        }


    });
}
function Confirm() {
    $("#EditGalleryForm").attr({
        action: "/admin/Gallery/Add"
    }).submit();


}

function DeletePopup() {
    $('#btnconfirm_delete').attr('data-value', $(this).attr("data-uniquecode"));
    $('#btnconfirm_delete').attr('data-encval', $("#AddGalleryPhoto").attr("data-uniquecode"));
}
function submitDelete() {
    //window.location = apppath + "/Admin/UserAccounts/Delete/" + $("#EncDetail").val();
    $("#dynamic_container").html('').append($("<input>", {
        id: "EncDetail",
        name: "EncDetail",
        type: "hidden",
        value: $('#btnconfirm_delete').data('value') + "_" + $('#btnconfirm_delete').data('encval')
    }));


    $("#Form").attr({
        action: "/Admin/Gallery/DeleteGalleryPhoto"
    }).submit();
}

function initializePhotoValidation() {

    jQuery.validator.setDefaults({
        ignore: []
    });
    jQuery.validator.addMethod('dimention', function (value, element, param) {
        if (element.files.length == 0) {
            return true;
        }
        var width = $(element).data('imageWidth');
        //alert(width);
        //var height = $(element).data('imageHeight');
        //alert(height);
        //alert(param[0]);
        if (width == param[0]) {
            return true;
        } else {
            return false;
        }
    }, 'Please upload an image with valid dimension');

    jQuery.validator.addMethod('filesize', function (value, element, param) {
        return this.optional(element) || (element.files[0].size <= param)
    }, 'File size must be less than 2MB');


    $("#GalleryPhotoAddFrom").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            "galleryPhotoDescription.Description": {
                required: true
            },
            //},
            uploadAttach_1: {
                required: true,
                extension: 'jpg|jpeg|png',
                dimention: [1024]
            }
        }, messages: {
            uploadAttach_1: {
                extension: 'Image format should be jpg/jpeg only.'
            },

        }
    });

    $("#PhotoGalleryEditForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Description: {
                required: true
            },
            uploadAttach_2: {
                required: true,
                extension: 'jpg|jpeg|png',
                dimention: [1024]
                //filesize: 2097152 /*2MB*/
            }
        }, messages: {
            uploadAttach_2: {
                extension: 'Image format should be jpg/jpeg only.'
            },
        }
    });
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
                PageNumber: pageNumber,
                GUID: $("#GUID").val() || ''

            });
            $('.pagination').attr('data-pageindex', pageNumber);
        }
    });
}
var pageSize = 15;
function bindPageItems(e) {
    //var path = "/Admin/Category/PartialIndex/" + e.PageNumber + "/" + e.Category + "/" + e.SubCategory + "/" + e.SortBy + "/" + e.SortDirection;
    var path = apppath + "/Admin/Gallery/GalleryPhotoPartialView/" + e.GUID + "/" + e.PageNumber;
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