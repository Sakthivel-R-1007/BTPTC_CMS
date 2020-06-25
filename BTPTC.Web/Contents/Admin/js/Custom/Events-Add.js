$(document).ready(function () {
   // $('.AddEvents').css('height', '45px !important');
    $('input[name=ThumbnailImage]').change(function () {
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


    $("#ThumbnailImage").on("change", function () {
        $("#ThumbnailImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#ThumbnailImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });
   

    //----
    $('input[name=LargeImage]').change(function () {
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


    $("#LargeImage").on("change", function () {
        $("#LargeImageFilePath").attr("src", "");
        var photo = this;
        $img = $("#LargeImageFilePath");
        $img.data("type", photo.files[0].type);
        $img.attr("src", window.URL.createObjectURL(photo.files[0]));
    });






    $("#FromDateString").datetimepicker({
        format: 'DD/MM/YYYY'

    });


    $("#StartTime").datetimepicker({
        format: 'HH:mm',
      
    })

    $("#Endtime").datetimepicker({
        format: 'HH:mm'


    })






    






   
    jQuery.validator.setDefaults({
        ignore: [],
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





    $.validator.addMethod('greaterthentime', function (value) {

        var startTime = $('#StartTime').val();
        var endtime = $('#Endtime').val();

        var start = startTime.split(":");
        var end = endtime.split(":");

        var starttotalsec = parseInt(start[0] * 60 + start[1])
        var Endtotalsec = parseInt(end[0] * 60 + end[1])

        //if (parseInt(start[0]) > parseInt(end[0]) || parseInt(start[0]) == parseInt(end[0])) 
        if (starttotalsec > Endtotalsec || starttotalsec == Endtotalsec){
            return false
        }
        else {
            return true
        }
        }, 'Please enter EndTime Must greater then StartTime')

    

    $.validator.addMethod("wordCounts", function (value) {
        var words = value.replace(/\s/g, "");
        var val = words.length;
        return val < 201;
    }, 'You can enter max 200 words');
    InitializeWordCount();

    function InitializeWordCount() {
        var value = $('#ShortDescription').val();
        var word = value.replace(/\s/g, "");
        $('#wordCount').html(word.length);
        $("#ShortDescription").on("keyup", function (e) {
            var value = $('#ShortDescription').val();
            var words = value.replace(/\s/g, "");
            $('#wordCount').html(words.length);
        });
    }

    $(document).on("click", ".submitBtn", Confirm);


    function Confirm() {

        if ($("#AddEventForm").valid()) {

            $("#AddEventForm").attr({
                //action: apppath + "/OurTown/AddEvents"
            }).removeAttr("target").submit();
        }

    }


    jQuery("#AddEventForm").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            EventTitle: { required: true },
            GuestOfHonour: { required: true },
            "FromDateString": { required: true },

            "StartEventTime": { required: true },
            "EndEventTime": { required: true, greaterthentime:true },


            Venue: { required: true },

            "ThumbnailImage": {
                required: true,
                dimention: [500, 280],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
            ThumbnailImageAltTag: { required: true },

            "LargeImage": {
                required: true,
                //dimention: [679, 960],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            },
            LargeImageAltTag: { required: true },
            ShortDescription: { required: true, wordCounts: true },
            Contents: {
                required: function (e) {
                    CKEDITOR.instances[e.id].updateElement();
                    var editorcontent = e.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                }
            }

        }
    });


})