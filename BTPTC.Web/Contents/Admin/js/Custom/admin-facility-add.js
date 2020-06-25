$(document).ready(function () {

    initializeValidation();

    $(document).on("click", ".submitBtn", Confirm);


    function Confirm() {

        if ($("#PhotoFacilityFrom").valid()) {

          
        }

    }


    jQuery("#PhotoFacilityFrom").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            "Description": { required: true },
            "ImageAltTag": { required: true },
            "LargeImage": {
                required: true,
                dimention: [1024, 655],
                filesize: 2097152, /*2MB*/
                extension: 'jpg|jpeg|png'
            }
        }
    });
});








    function initializeValidation() {

        jQuery.validator.setDefaults({
            ignore: []
        });

        $("#AddFacilityFrom").validate({
            errorPlacement: function (error, element) {
                $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
            },
            rules: {
                Name: {
                    required: true,
                    remote: {
                        url: "/Admin/OurTown/CheckFacilityTitle",
                        type: "GET",
                        async: true,
                        cache: false,
                        data: {
                            Name: function () {
                                return $("#Name").val();
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
                Name: { remote: 'FacilityType is duplicated' },
            }
        });
    }




$(document).on('click', '.Editlink', function () {
   

    $('#btnupdatefacility').show();
    $('#btnfacility').hide();

    let guid = $(this).attr('data-uniquecode');

    
    $('#Description').val($(this).parent().parent().find('td:eq(2)').text());
    $('#LargeImageFileName').val($(this).parent().parent().find('td:eq(1)').children().eq(3).val())    
    $('#LargeImageFilePath').attr('src', $(this).parent().parent().find('td:eq(1) img').attr('src'))

    $('#hiddenLargeImageFilePath').attr('src', $(this).parent().parent().find('td:eq(1) img').attr('src'))
    //$('#LargeImage').val($(this).parent().parent().find('td:eq(1)').children().eq(3).val());
    $('#ImageAltTag').val($(this).parent().parent().find('td:eq(1)').children().eq(4).val());

    
    

    console.log('ImageAltTag')
    console.log(ImageAltTag)
    $('#hiddenTableRowid').val(guid);

    $('#imagemodeladd').modal('show');
    //if (guid != '' && guid != null) {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/Admin/OurTown/GetFaciltyImage?EncryptedId=' + guid,
    //        success: function (data) {
    //            if (data != null) {
    //                $('#Description').val(data.Description);
    //                $('#LargeImageFileName').val(data.LargeImageFileName);
    //                $('#LargeImageFilePath').attr('src', "/Resources/Images/Facility/" + data.ImageGUID.replace(/-/g, '') + '.' + data.ImageExtension);
    //                $('#hiddenLargeImageFilePath').attr('src', "/Resources/Images/Facility/" + data.ImageGUID.replace(/-/g, '') + '.' + data.ImageExtension);
    //                $('#ImageAltTag').val(data.ImageAltTag);
    //                $('#facilityImageGuid').val(guid);
    //                $('#imagemodeladd').modal('show');
    //            }
    //        }
    //    })
    //}
    //else {
    //    alert('empty guid')
    //}
})


    var FacilityObj = [];
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




$('#FacilitySubmit').click(function () {


    var TblLenth = $('#FacilityTbl tbody tr').length;

    if (TblLenth > 0) {
        $("#AddFacilityFrom").submit();
        

    } else {

        $("#TableRowCheck").text("Table row Empty");
        
    }
       

    })






    $("#btnfacility").click(function () {

        $("#PhotoFacilityFrom").submit();
        $("#TableRowCheck").text("");

    });



    $("#PhotoFacilityFrom").submit(function (e) {
        e.preventDefault();
        if ($(this).valid()) {
            $.ajax({
                url: this.action,
                data: new FormData(this),
                contentType: false,
                processData: false,
                type: this.method,
                success: function (data) {
                    console.log(data);
                   
                    var rowCount = $('.data-contact-person').length + 0;
                    var date = new Date(parseInt((data.ModifiedOn).replace('/Date(', ''))).toLocaleString('en-GB', { timeZone: 'UTC' });
                   
                    var imgpath = window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/Resources/Images/Facility/" + data.ImageGUID + '.' + data.ImageExtension;
                    var imageadd = '<tr class="data-contact-person" id=' + rowCount + '>' +
                        '<td class="center">' + parseInt(parseInt(rowCount) + parseInt(1)) + '</td>' +
                        '<td class="center"><img class="icon" id="Product_productImages_' + rowCount + '_ImageGUID1" src="' + imgpath + '" name="productImages[' + rowCount + '].ImageGUID1"/><input id = "Facility_FacilityImage_' + rowCount + '_ImageGUID" readonly type = "hidden" name = "FacilityImage[' + rowCount + '].ImageGUID"  value = "' + data.ImageGUID + '" /><input id = "Facility_FacilityImage_' + rowCount + '_ImageExtension" readonly type = "hidden" name = "FacilityImage[' + rowCount + '].ImageExtension"  value = "' + data.ImageExtension + '" /><input id = "Facility_FacilityImage_' + rowCount + '_ImageName" readonly type = "hidden" name = "FacilityImage[' + rowCount + '].ImageName"  value = "' + data.ImageName + '" /> <input id = "Facility_FacilityImage_' + rowCount + '_ImageAltTag" readonly type = "hidden" name = "FacilityImage[' + rowCount + '].ImageAltTag"  value = "' + data.ImageAltTag + '" /> </td > ' +
                        '<td class="center">' + data.Description + '<input id="Facility_FacilityImage_' + rowCount + '_Description" readonly type="hidden" name="FacilityImage[' + rowCount + '].Description"  value="' + data.Description + '" /></td>' +
                        '<td class="center">' + date + '<input id="Facility_FacilityImage_' + rowCount + '_ModifiedOn" readonly type="hidden" name="FacilityImage[' + rowCount + '].ModifiedOn"  value="' + date + '" /></td>' +
                        '<td class="center"><a id="Editdiv_' + rowCount + '"  class="Editlink" href="javascript:void(0)">Edit</a> | <a id="deldiv_' + rowCount + '"  class="linelink" href="javascript:void(0)">Delete</a></td>' +
                        '</tr>';
                    $('#imageadd').append(imageadd);

                    console.log(imageadd);

                    var modal = document.getElementById("imagemodeladd");
                    $('#Description').val('');
                    $('#ImageAltTag').val(''); 
                    $('#LargeImageFilePath').attr('src', '');
                  
                    $('#LargeImage').val('');
                    $('#LargeImageFileName').val('');
                    $(modal).modal('hide');

                    //markup += "<td>" + (_cnt+ 1) + "</td>";
                    //markup += "<td>";
                    //markup += " <img src='" + data.Imagepath + "' width ='50' height ='50'>";
                    //markup += " <input type='hidden' value='" + data.Imagepath + "' name='[" + _cnt + "].Imagepath'/>";
                    //markup += "</td>";
                    //markup += "<td>" + data.description + "</td>";
                    //markup += " <input type='hidden' value='" + data.description + "' name='[" + _cnt + "].description'/>";
                    //markup += "<td></td>";
                    //markup += "<td>" + 'delete' + "</td></tr> ";
                    //$("#FacilityTbl tbody.photos").append(markup);

                }, error: function (err) {
                    debugger
                    console.log('Error : ' + err);
                }
            })
        }
    })


//})



$("#FacilityTbl").on("click", ".linelink", function (ev) {
    var $currentTableRow = $(ev.currentTarget).parents('tr')[0];
    $currentTableRow.remove();

    $('#FacilityTbl tbody').find('tr').each(function (i) {
        let parent = $(this);
        let $parentE = $(parent);
        $parentE.attr('id', i);
        $parentE.find('td:eq(0)').text(i + 1);
        let $imgDetails = $parentE.find('td:eq(1)').children();
        $imgDetails.eq(0).attr('id', 'Product_productImages_' + i + '_ImageGUID1').attr('name', 'productImages[' + i + '].ImageGUID1');
        $imgDetails.eq(1).attr('id', 'Facility_FacilityImage_' + i + '_ImageGUID').attr('name', 'FacilityImage[' + i + '].ImageGUID');
        $imgDetails.eq(2).attr('id', 'Facility_FacilityImage_' + i + '_ImageExtension').attr('name', 'FacilityImage[' + i + '].ImageExtension');
        $imgDetails.eq(3).attr('id', 'Facility_FacilityImage_' + i + '_ImageName').attr('name', 'FacilityImage[' + i + '].ImageName');
        $imgDetails.eq(4).attr('id', 'Facility_FacilityImage_' + i + '_ImageAltTag').attr('name', 'FacilityImage[' + i + '].ImageAltTag');

        let $description = $parentE.find('td:eq(2)').children();
        $description.eq(0).attr('id', 'Facility_FacilityImage_' + i + '_Description').attr('name', 'FacilityImage[' + i + '].Description');

        let $modified = $parentE.find('td:eq(3)').children();
        $modified.eq(0).attr('id', 'Facility_FacilityImage_' + i + '_ModifiedOn').attr('name', 'FacilityImage[' + i + '].ModifiedOn');

        let $buttons = $parentE.find('td:eq(4)').children();
        $buttons.eq(0).attr('id', 'Editdiv_' + i);
        $buttons.eq(1).attr('id', 'deldiv_' + i);
    });
});



$(document).on("click", ".Name", function () {

    debugger
    $("#AddFacilityFrom").submit();

});




$(document).on('click', '#btnupdatefacility', function () {
    
    if ($("#PhotoFacilityFrom").valid()) {

        let guid = $('#facilityImageGuid').val();
        let formData = new FormData();
        if ($('#LargeImage')[0].files.length > 0) {
            formData.append('LargeImage', $('#LargeImage')[0].files[0]);
        }
        formData.append('Name', $('#Name').val());
        formData.append('Description', $('#Description').val());
        formData.append('ImageAltTag', $('#ImageAltTag').val());
        let imageNme = $('#hiddenLargeImageFilePath').attr('src').split('/').pop().split('.');
        formData.append('ImageGUID', imageNme[0]);
        formData.append('ImageExtension', imageNme[1]);
        formData.append('GUID', guid);

        if (guid != '') {
            $.ajax({
                url: '/Admin/OurTown/AddPhotoFacility',
                data: formData,
                contentType: false,
                processData: false,
                type: 'POST',
                success: function (data) {
                    if (data != null) {
                        $.each($('#imageadd tr'), function () {
                            console.log($(this).find('td:eq(4) .Editlink').attr('data-uniquecode') + '   ' + guid)
                            if ($(this).find('td:eq(4) .Editlink').attr('data-uniquecode') == guid) {

                                $(this).html('');
                                var rowCount = $(this).attr('id');
                                var date = new Date(parseInt((data.ModifiedOn).replace('/Date(', ''))).toLocaleString('en-GB', { timeZone: 'UTC' });

                                var imgpath = window.location.protocol + '//' + window.location.hostname + (window.location.port ? ':' + window.location.port : '') + "/Resources/Images/Facility/" + data.ImageGUID + '.' + data.ImageExtension;
                                var imageadd = '<td class="center">' + parseInt(parseInt(rowCount) + parseInt(1)) + '</td>' +
                                    '<td class="center"><img class="icon" id="Product_productImages_' + rowCount + '_ImageGUID1" src="' + imgpath + '" name="productImages[' + rowCount + '].ImageGUID1"/><input id = "Facility_FacilityImage_' + rowCount + '_ImageGUID" readonly type = "hidden" name = "FacilityImage[' + rowCount + '].ImageGUID"  value = "' + data.ImageGUID + '" /><input id = "Facility_FacilityImage_' + rowCount + '_ImageExtension" readonly type = "hidden" name = "FacilityImage[' + rowCount + '].ImageExtension"  value = "' + data.ImageExtension + '" /><input id = "Facility_FacilityImage_' + rowCount + '_ImageName" readonly type = "hidden" name = "FacilityImage[' + rowCount + '].ImageName"  value = "' + data.ImageName + '" /> <input id = "Facility_FacilityImage_' + rowCount + '_ImageAltTag" readonly type = "hidden" name = "FacilityImage[' + rowCount + '].ImageAltTag"  value = "' + data.ImageAltTag + '" /> </td > ' +
                                    '<td class="center">' + data.Description + '<input id="Facility_FacilityImage_' + rowCount + '_Description" readonly type="hidden" name="FacilityImage[' + rowCount + '].Description"  value="' + data.Description + '" /></td>' +
                                    '<td class="center">' + date + '<input id="Facility_FacilityImage_' + rowCount + '_ModifiedOn" readonly type="hidden" name="FacilityImage[' + rowCount + '].ModifiedOn"  value="' + date + '" /></td>' +
                                    '<td class="center"><a id="Editdiv_' + rowCount + '" data-uniquecode=' + data.GUID + ' class="Editlink" href="javascript:void(0)">Edit</a> | <a id="deldiv_' + rowCount + '"  class="linelink" href="javascript:void(0)">Delete</a></td>';

                                $(this).append(imageadd);
                                $('#imagemodeladd').modal('hide');
                                return false;
                            }
                        })
                    }
                }
            })
        }
    }
})



function onAddModelPopup() {

    $('#btnupdatefacility').hide();
    $('#btnfacility').show();


    $('#Description').val('');
    $('#LargeImageFileName').val('');
    $('#LargeImageFilePath').attr('src', '');
    $('#ImageAltTag').val('');
    $('#facilityImageGuid').val('');
    $('#imagemodeladd').modal('show');
}
