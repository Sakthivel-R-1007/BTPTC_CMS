$(document).ready(function () {



    

   


    //validation 
    jQuery("#AddNoticeForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            Status: { required: true },
            "PDFFileName":
            {
                required: true,
                extension: 'pdf',
                //filesize: 52428800 /*50MB*/
            },

            Project: {
                required: true,
                remote: {
                    url: "/Admin/NewsRoom/CheckTenderTitle",
                    type: "GET",
                    async: true,
                    cache: false,
                    data: {
                        NewsTitle: function () {
                            return $("#Project").val();
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
            Project: { remote: 'Title is duplicated' },
            "PDFFileName": {
                //required: true,
                extension: "Please upload valid file formats Pdf only."
            }

        }
    });

    //end validation


    $(".Date").datetimepicker({
        format: 'DD/MM/YYYY'
    });



    $(document).on('change', '#addNotice input[name=FileName],#editNotice input[name=FileName],#addResult input[name=FileName],#editResult input[name=FileName],#addAwarded input[name=FileName],#editAwarded input[name=FileName]'  , function () {
        $(this).next().children(0).val($(this)[0].files[0].name) 

        const size =
            (this.files[0].size / 1024 / 1024).toFixed(2);

        if (size <= 50) {
            $("#output").html('')
        } else {
            $("#output").html('<b>' +
                'This file size is: ' + size + " MB" + '</b>');
            $(this).val('')
        } 
    })


    //TenderNotice
    $(document).on("click", "#addNoticebtn", function ()
    {
        var modal = document.getElementById("addNotice");
        $(modal).modal('show');
        AddNotice();
    });

    $(document).on('click', ".editTenderNoticebtn", function ()
    {
        var modal = document.getElementById("editNotice");
        $(modal).modal('show');
        EditTenderNotice($(this).attr('data-id'))
    });


    $(document).on('click', '#Addsubmitbtn', function () {
        if ($("#AddNoticeForm").valid()) {
            $("#AddNoticeForm").submit()
        }
    })


    $(document).on('click', '#EditSubmitNoticeBtn', function () {
        if ($("#EditNoticeForm").valid()) {
            $("#EditNoticeForm").submit()
        }
    })
    //End TenderNotice




    //TenderResult
    $(document).on("click", "#addResultebtn", function () {
        var modal = document.getElementById("addResult");
        $(modal).modal('show');
        AddTenderResult();
    });


    $(document).on('click', ".editTenderResultbtn", function () {
        var modal = document.getElementById("editResult");
        $(modal).modal('show');
        EditTenderResult($(this).attr('data-id'))
    });

    $(document).on('click', '#AddResultSubmitbtn', function () {
        if ($("#AddResultForm").valid()) {
            $("#AddResultForm").submit()
        }
    })


    $(document).on('click', '#EditResultSubmitbtn', function () {
        if ($("#EditResultForm").valid()) {
            $("#EditResultForm").submit()
        }
    })
    //TenderEndResult 


    //TenderAwarded

    $(document).on("click", "#addAwardedebtn", function () {
        var modal = document.getElementById("addAwarded");
        $(modal).modal('show');
        AddTenderAwarded();
    });


    $(document).on('click', ".editTenderAwardedbtn", function () {
        var modal = document.getElementById("editAwarded");
        $(modal).modal('show');
        EditTenderAwarded($(this).attr('data-id'))
    });

    $(document).on('click', '#AddAwardedSubmitbtn', function () {
        if ($("#AddAwardedForm").valid()) {
            $("#AddAwardedForm").submit()
        }
    })


    $(document).on('click', '#EditAwardedSubmitbtn', function () {
        if ($("#EditAwardedForm").valid()) {
            $("#EditAwardedForm").submit()
        }
    })

    //TenderAwarded




    $('#filterByType').trigger('change');

    
})


$(document).on("click", "#deletebtn", function () {
    var modal = document.getElementById("delete");
    $(modal).modal('show');
    $("#deleteeventId").val(($(this).attr("data-uniquecode")));
});
$(document).on("click", "#confirm_deleteiconBtn", function () {
    $("#DeleteMediaReleaseForm").submit();
});


function AddNotice() {

    var path = "/Admin/NewsRoom/AddNotice"
    $.ajax({
        url: path,
        //type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

            $("#AddNoticeForm").html('');
            $("#AddNoticeForm").append(response);

          

        },
        failure: function (response) {
            console.log(response);
            $("#AddNoticeForm").append(('Error occured'));
        }
    });
}

function EditTenderNotice(GUID) {

    var path = "/Admin/NewsRoom/EditNotice?EncDetail=" + GUID;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }


            $("#EditNoticeForm").html('');
            $("#EditNoticeForm").append(response);
          //  initializeEditResourcesValidate();

         
        },
        failure: function (response) {
            console.log(response);
            $("#EditNoticeForm").append(('Error occured'));
        }
    });
}

function AddTenderResult() {

    var path = "/Admin/NewsRoom/AddResult"
    $.ajax({
        url: path,
        //type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

            $("#AddResultForm").html('');
            $("#AddResultForm").append(response);

            $(".Date").datetimepicker({
                format: 'DD/MM/YYYY'
            });

        },
        failure: function (response) {
            console.log(response);
            $("#AddResultForm").append(('Error occured'));
        }
    });
}

function EditTenderResult(GUID) {

    var path = "/Admin/NewsRoom/EditResult?EncDetail=" + GUID;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }


            $("#EditResultForm").html('');
            $("#EditResultForm").append(response);
            //  initializeEditResourcesValidate();

            $(".Date").datetimepicker({
                format: 'DD/MM/YYYY'
            });

        },
        failure: function (response) {
            console.log(response);
            $("#EditResultForm").append(('Error occured'));
        }
    });
}


function AddTenderAwarded() {

    var path = "/Admin/NewsRoom/AddAwarded"
    $.ajax({
        url: path,
        //type: 'GET',
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }

            $("#AddAwardedForm").html('');
            $("#AddAwardedForm").append(response);

            $(".Date").datetimepicker({
                format: 'DD/MM/YYYY'
            });

        },
        failure: function (response) {
            console.log(response);
            $("#AddAwardedForm").append(('Error occured'));
        }
    });
}

function EditTenderAwarded(GUID) {

    var path = "/Admin/NewsRoom/EditAwarded?EncDetail=" + GUID;
    $.ajax({
        url: path,
        dataType: 'html',
        success: function (response) {
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }


            $("#EditAwardedForm").html('');
            $("#EditAwardedForm").append(response);
            //  initializeEditResourcesValidate();

            $(".Date").datetimepicker({
                format: 'DD/MM/YYYY'
            });

        },
        failure: function (response) {
            console.log(response);
            $("#EditAwardedForm").append(('Error occured'));
        }
    });
}


$(document).on('change', '#filterByType', function () {
    
    var selectedvalue = $('#filterByType').val();
    var path = "";
    if (selectedvalue == "notices") {
        path = apppath + "/Admin/NewsRoom/PartialViewNotice?FilterValue=" + selectedvalue + ""

    }
    if (selectedvalue == "results")
    {
         path = apppath + "/Admin/NewsRoom/PartialResultView?FilterValue=" + selectedvalue + ""
    }

    if (selectedvalue == "awarded") {
        path = apppath + "/Admin/NewsRoom/PartialAwardedView?FilterValue=" + selectedvalue + ""
    }

    
    $.ajax({
        url: path,
        type: 'GET',
        success: function (response) {
            if (selectedvalue == "notices") {
                $('#NoticeTbl').html(response);
            }
            if (selectedvalue == "results") {
                $('#ResultTbl').html(response);
            }
            if (selectedvalue == "awarded") {
                $('#AwardedTbl').html(response);
            }
            
        }

    })



})