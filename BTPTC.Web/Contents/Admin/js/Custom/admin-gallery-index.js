$(document).ready(function () {
    intializepagination();
    $(document).on("click", "#galleryDelete", DeletePopup);
    $(document).on("click", "#btnconfirm_delete", submitDelete);

});

function DeletePopup() {
    $('#btnconfirm_delete').attr('data-value', $(this).attr("data-uniquecode"));
}
function submitDelete() {
    //window.location = apppath + "/Admin/UserAccounts/Delete/" + $("#EncDetail").val();
    $("#dynamic_container").html('').append($("<input>", {
        id: "EncDetail",
        name: "EncDetail",
        type: "hidden",
        value: $('#btnconfirm_delete').data('value')
    }));

    $("#Form").attr({
        action: "/Admin/Gallery/Delete"
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
                    PageNumber: pageNumber,
                    Keyword: $("#solution").val() || ''

                });
                $('.pagination').attr('data-pageindex', pageNumber);
            }
        });
    }
    var pageSize = 15;
    function bindPageItems(e) {
        //var path = "/Admin/Category/PartialIndex/" + e.PageNumber + "/" + e.Category + "/" + e.SubCategory + "/" + e.SortBy + "/" + e.SortDirection;
        var path = apppath + "/Admin/Gallery/PartialView/" + e.PageNumber;
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