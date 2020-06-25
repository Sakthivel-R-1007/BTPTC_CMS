$(document).ready(function () {
    initializeValidation();
    $("#DateString").datepicker({
        dateFormat: 'dd/mm/yy',
        //minDate: new Date(),
    });
    $(document).on("click", "#submitBtn", Confirm);

  
});


function initializeValidation() {
    $("#AddGalleryForm").validate({
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
    if ($("#AddGalleryForm").valid()) {
        $("#AddGalleryForm").attr({
            action: "/admin/Gallery/Add"
        }).submit();
    }
}