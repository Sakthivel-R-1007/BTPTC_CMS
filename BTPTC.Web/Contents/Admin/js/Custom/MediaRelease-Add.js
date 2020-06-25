
$(document).ready(function () {

    $(document).on("click", ".editMediaReleasebtn", function () {

        var modal = document.getElementById("editMediaRelease");
        $(modal).modal('show');

        EditMediaRelease($(this).attr("data-id"));
    }); EditMediaRelease


    $(document).on("click", "input[name=FileName]", function () {
        document.getElementById("PdfFileName").value = $(this)[0].files[0].name
    })
    //$('input[name=FileName]').change(function () {
    //    document.getElementById("PdfFileName").value = $(this)[0].files[0].name
    //})



    $("#Date").datetimepicker({
        format: 'DD/MM/YYYY'
    });

    $(document).on("click", "#editresourcessubmitBtn", function () {
        if ($("#EditMediaReleaseForm").valid()) {
            $("#EditMediaReleaseForm").submit()
        }
    });

    $(document).on("click", ".submitBtn", function () {
        if ($("#AddMediaReleaseForm").valid()) {
            $("#AddMediaReleaseForm").submit()
        }
    });




   

 



    jQuery("#AddMediaReleaseForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Date: { required: true},
            PdfFileName: {
                required: true,
                extension: 'pdf',
                filesize: 52428800 /*50MB*/
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
        messages: {
            Title: { remote: 'Title is duplicated' },
            PdfFileName: {
                //required: true,
                extension: "Please upload valid file formats Pdf only."
            }

        }
    });



})

$(document).on("click", "#deletebtn", function () {
    var modal = document.getElementById("delete");
    $(modal).modal('show');
    $("#deleteeventId").val(($(this).attr("data-uniquecode")));
});
$(document).on("click", "#confirm_deleteiconBtn", function () {
    $("#DeleteMediaReleaseForm").submit();
});



function initializeEditResourcesValidate() {
    jQuery("#EditMediaReleaseForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Date: { required: true },
            PdfFileName: {
                required: true,
                extension: 'pdf',
                filesize: 52428800 /*50MB*/
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
          //  Title: { remote: 'Title is duplicated' },
            PdfFileName: {
                //required: true,
                extension: "Please upload valid file formats Pdf only."
            }

        }
    });
};


function EditMediaRelease(GUID) {

    var path = "/Admin/NewsRoom/EditMediaRelease?EncDetail=" + GUID;
    $.ajax({
        url: path,
      //  type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

            debugger
            //$('#modalWrapper').html(response);
            //$('.editMediareleseGuid').modal('show')

            $("#EditMediaReleaseForm").html('');
            $("#EditMediaReleaseForm").append(response);
            initializeEditResourcesValidate();
        
        },
        failure: function (response) {
            console.log(response);
            $("#EditMediaReleaseForm").append(('Error occured'));
        }
    });
}
