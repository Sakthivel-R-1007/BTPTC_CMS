$(document).ready(function () {




    IntializeUploadValidation();
    $("#uploadAttach_1").on("change", function () {

        $(".tblView").html('');      
        var photo = this;
        $("#uploadFile_1value").val(photo.files[0].name);
       
    });

    $("#excelbtn").click(function () {
        $("#MaintenanceexceluploadFrom").submit();
    });
});






function IntializeUploadValidation() {



    $("#MaintenanceexceluploadFrom").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {

            "uploadAttach_1": {
                required: true,
                extension: 'xlsx|xls',

            }
        },
        messages: {
            "uploadAttach_1": {
                required: "Please upload excel file"
            }
        }
    });
}