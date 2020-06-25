$(document).ready(function () {

    intializepagination();
    //viewNoticeProject


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

        //var path = "/Admin/Category/PartialIndex/" + e.PageNumber + "/" + e.Category + "/" + e.SubCategory + "/" + e.SortBy + "/" + e.SortDirection;
        //var UrlParameter = "?" + ((e.PageNumber != '') ? 'PageIndex=' + e.PageNumber : '') + "&" + ((e.Year != '') ? 'Year=' + e.Year : '') + "&" + ((e.NewsType != '') ? 'NewsType=' + e.NewsType : 'NewsType=');
        var UrlParameter = "?" + ((e.PageNumber != '') ? 'PageIndex=' + e.PageNumber : '') + "&" + ((e.Year != '') ? 'Year=' + e.Year : '');

        var path = apppath + "/Admin/TownImprovementProject/PartialViewNoticeProject" + UrlParameter

        $.ajax({
            url: path,
            dataType: 'html',
            success: function (response) {
                if (response === undefined && response == '') {
                    response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
                }

                $("#NoticeProjecttbl tbody").html(response);
                var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
                //console.log(pageIndex);
                var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
                var lastItem = startItem + ($("#NoticeProjecttbl tbody tr").length - 1);
                $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

            },
            failure: function (response) {
                console.log(response);
                $("#NoticeProjecttbl tbody").html('<tr><td colspan="7" class="center error">Error occured</td></tr>');
            }
        });

    }




    $(document).on("click", "#deletebtn", function () {
        debugger
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#deleteeventId").val(($(this).attr("data-uniquecode")));
    });
    $(document).on("click", "#confirm_deleteiconBtn", function () {
        $("#DeleteEventsForm").submit();
    });


    //endNoticeProject

    jQuery("#AddNoticeProject").validate({
        errorPlacement: function (error, element) {
            $("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
        },
        rules: {
            "NoticeType": { required: true },
            "Location": { required: true },
            "Status": { required: true },

        }
    });
});

function editNoticeProject(id) {
    debugger
    var url = "/Admin/TownImprovementProject/GetEditNoticeProject?EncryptedId=" + id;
    $.ajax({
        url: url,
        type: 'GET',
        success: function (result) {
            debugger

            $('#modalWrapper').html(result); // This should be an empty div where you can inject your new html (the partial view)
            $('#editNoticepr').modal('show')
        }
    });

    console.log(id)
}




$("#NoticeType").on("change", function () {
    debugger
    var value = $(this).val().toLowerCase();
    $("#NoticeProjecttbl tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});
