$(document).ready(function () {
    
    initializeAddValidation();
    intializepagination();

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

    $(document).on("click", "#submitBtn", Confirm);


    //filter

    $(".searchBtn").click(function () {
     
        var year = $('#yearfilter').val()
      
        $('#NewsLetterTbl tbody tr').each(function () {
            var sel = $(this);
            var txt = sel.find('td:eq(3)').text();
          
            if (year != 'All') {
                if (txt.indexOf(year) === -1)
                {
                    $(this).hide();
                }
                else
                {
                    $(this).show();
                }
            }
            else {
                $('#NewsLetterTbl tbody tr').show();
            }
        });

    })


    intializepagination();

    //start pagination



    //$('input[name=ThumbnailImage]').change(function () {
    $(document).on('change', 'input[name=ThumbnailImage]', function () {
        
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#ThumbnailImage').data('imageWidth', this.width);
                $('#ThumbnailImage').data('imageHeight', this.height);
                //alert("The image width is " + this.width + " and image height is " + this.height);
            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("ThumbnailImageFileName").value = filename;
    });


    //$("#ThumbnailImage").on("change", function () {
    $(document).on('change', '#ThumbnailImage', function () {
        $("#ThumbnailImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ThumbnailImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });


    //----
    //$('input[name=LargeImage]').change(function () {
    $(document).on('change', 'input[name=LargeImage]', function () {
        var _URL = window.URL || window.webkitURL;
        var image, file;

        if ((file = this.files[0])) {
            image = new Image();
            image.onload = function () {
                $('#LargeImage').data('imageWidth', this.width);
                $('#LargeImage').data('imageHeight', this.height);
                //alert("The image width is " + this.width + " and image height is " + this.height);
            };
            image.src = _URL.createObjectURL(file);
        }
        var filename = $(this).val();
        if (filename.substring(3, 11) == 'fakepath') {
            filename = filename.substring(12);
        }
        document.getElementById("LargeImageFileName").value = filename;
    });


    //$("#LargeImage").on("change", function () {
    $(document).on('change', '#LargeImage', function () {
        $("#LargeImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#LargeImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });

    jQuery.validator.setDefaults({
        ignore: [],
    });

   



    //$('input[name=FileName]').change(function () {
    //    document.getElementById("PdfFileName").value = $(this)[0].files[0].name
    //})


    //$(document).on("change", "#addNewletter input[name=FileName],#editNewsletter input[name=FileName]", function () {
    //    $(this).next().children(0).val($(this)[0].files[0].name)
    //});

    $(document).on('click', ".editNewsLetterbtn", function () {
        var modal = document.getElementById("editNewsletter");
        $(modal).modal('show');
        EditNewLetter($(this).attr('data-id'))
    });

})




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

    var path = apppath + "/Admin/NewsRoom/PartialViewNewsletter" + UrlParameter

    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response == '') {
                response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
            }

            $("#NewsLetterTbl tbody").html(response);
            var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
            //console.log(pageIndex);
            var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
            var lastItem = startItem + ($("#NewsLetterTbl tbody tr").length - 1);
            $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

        },
        failure: function (response) {
            console.log(response);
            $("#NewsLetterTbl tbody").html('<tr><td colspan="7" class="center error">Error occured</td></tr>');
        }
    });

}




function Confirm() {
  
    if ($("#AddNewsLetterForm").valid()) {
        $("#AddNewsLetterForm").attr({
            action: "/admin/NewsRoom/AddNewsLetter"
        }).submit();

    }
}

function EditNewLetter(id)
{
    //$("#AddNewsLetterForm").html('');
    //$('#LargeImage').val('');
    //$('#ThumbnailImage').val('');

    var url = "/Admin/NewsRoom/EditPartialNewsLetterGUID?EncryptedId=" + id;
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'html',
        success: function (response) {
            $("#EditNewsLetterForm").html('');
            $("#EditNewsLetterForm").append(response);            
        },
        failure: function (response) {
            console.log(response);
            $("#EditNewsLetterForm").append(('Error occured'));
        }

    })
}

$(document).on("change", "input[name=FileName]", function () {

    var filename = $(this).val();
    if (filename.substring(3, 11) == 'fakepath') {
        filename = filename.substring(12);
    }
    document.getElementById("PdfFileName").value = filename;
});


$(document).on("click", "#deletebtn", function () {
    var modal = document.getElementById("delete");
    $(modal).modal('show');
    $("#deleteeventId").val(($(this).attr("data-uniquecode")));
});
$(document).on("click", "#confirm_deleteiconBtn", function () {
    $("#DeleteNewsLetterForm").submit();
});

function initializeAddValidation() {
   
    $("#AddNewsLetterForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            Title: {
                required: true,
                remote: {
                    url: "/Admin/NewsRoom/CheckNewsTitle",
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
            Quarter: { required: true },

            Year: { required: true },
            LargeImage: {
                required: true,
                dimention: [500, 688],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },

            LargeImageAltTag: { required: true },

            ThumbnailImage: {
                required: true,
                dimention: [500, 280],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
            ThumbnailImageAltTag: { required: true },          

            PdfFileName: {
                required: true,
                extension: 'pdf',
                filesize: 52428800 /*50MB*/
            }
            
        },
        messages: {
            Title: { remote: 'Title is duplicated' }
            //extension: "Please upload valid file formats Pdf only."
        }
    });

  
    $("#EditNewsLetterForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            //Title: { required: true },
            Quarter: { required: true },

            Year: { required: true },

            "ThumbnailImage": {
                required: true,
                dimention: [500, 280],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
            ThumbnailImageAltTag: { required: true },

            "LargeImage": {
                required: true,
                dimention: [500, 688],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },

            LargeImageAltTag: { required: true },

            PdfFileName: {
                required: true,
                extension: 'pdf',
                filesize: 52428800 /*50MB*/
            },
            Title: {
                required: true,
                //remote: {
                //    url: "/Admin/NewsRoom/CheckNewsTitle",
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
           // Title: { remote: 'Title is duplicated' },
            PdfFileName: {
                required: true,
                extension: "Please upload valid file formats Pdf only."
            }

        }
    });

}




