$(document).ready(function () {



    intializepagination();

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
                    // NewsType: $("#NewsType").val() || ''

                });
                $('.pagination').attr('data-pageindex', pageNumber);
            }
        });
    }


    var pageSize = 10;
    function bindPageItems(e) {


        var UrlParameter = "?" + ((e.PageNumber != '') ? 'PageIndex=' + e.PageNumber : '') + "&" + ((e.Year != '') ? 'Year=' + e.Year : '');

        var path = apppath + "/Admin/TownImprovementProject/PartialView" + UrlParameter

        $.ajax({
            url: path,
            dataType: 'html',
            success: function (response) {
                if (response === undefined && response == '') {
                    response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
                }

                $("#FacilityTbl tbody").html(response);
                var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
                //console.log(pageIndex);
                var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
                var lastItem = startItem + ($("#FacilityTbl tbody tr").length - 1);
                $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

            },
            failure: function (response) {
                console.log(response);
                $("#FacilityTbl tbody").html('<tr><td colspan="7" class="center error">Error occured</td></tr>');
            }
        });

    }

    //end page




    $(document).on("click", "#deletebtn", function () {
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#deleteeventId").val(($(this).attr("data-uniquecode")));
    });
    $(document).on("click", "#confirm_deleteiconBtn", function () {
        $("#DeleteEventsForm").submit();
    });


});





$(document).on("click", ".sortbtn", function () {
    debugger
    var modal = document.getElementById("sortSeq");
    $(modal).modal('show');
    SortSolutionPillar();
});


function SortSolutionPillar() {
    debugger
    var path1 = "Admin/OurTown/GetFacilitySorting"
    $.ajax({
        url: path1,
        dataType: 'html',
        success: function (response) {
            debugger
            if (response === undefined && response === '') {
                response = 'No Results Found';
            }
            // console.log(response);
            $("#StaticBannerSortingForm").html('');
            $("#StaticBannerSortingForm").append(response);


            $(document).ready(function () {
                $("#sortable").sortable({
                    stop: function (event, ui) {

                        var itemOrder = $('#sortable').sortable("toArray");

                        for (var i = 0; i < itemOrder.length; i++) {
                            var res = itemOrder[i].split("_");


                            var Id = "#" + itemOrder[i];
                            var NewId = 'Set_' + i;
                        }

                    }
                });
                $("#sortable").disableSelection();
            });//ready

        },
        failure: function (response) {
            console.log(response);
            $("#StaticBannerSortingForm").append(('Error occured'));
        }
    });
}
