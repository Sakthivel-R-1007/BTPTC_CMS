$(document).ready(function () {



    $(".searchBtn").click(function (e) {
        $("#SearchEventsForm").submit();
    });


    




    function bindArticleItems(e) {
        debugger
        var path = apppath + "/OurTown/PartialArticle/" + e.EventId + "/" + (e.Year === '' ? null : e.Year);
        $.ajax({
            url: path,
            dataType: 'html',
            success: function (response) {
                if (response === undefined && response == '') {
                    response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
                }
                $("#articleContent tbody").html(response);
            },
            failure: function (response) {
                console.log(response);
                $("#articleContent tbody").html('<tr><td colspan="6" class="center error">Error occured</td></tr>');
            }
        });

    }



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
        
        //var path = "/Admin/Category/PartialIndex/" + e.PageNumber + "/" + e.Category + "/" + e.SubCategory + "/" + e.SortBy + "/" + e.SortDirection;
        //var UrlParameter = "?" + ((e.PageNumber != '') ? 'PageIndex=' + e.PageNumber : '') + "&" + ((e.Year != '') ? 'Year=' + e.Year : '') + "&" + ((e.NewsType != '') ? 'NewsType=' + e.NewsType : 'NewsType=');
        var UrlParameter = "?" + ((e.PageNumber != '') ? 'PageIndex=' + e.PageNumber : '') + "&" + ((e.Year != '') ? 'Year=' + e.Year : '');

        var path = apppath + "/Admin/OurTown/PartialView" + UrlParameter

        $.ajax({
            url: path,
            dataType: 'html',
            success: function (response) {
                if (response === undefined && response == '') {
                    response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
                }

                $("#EventsContents tbody").html(response);
                var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
                //console.log(pageIndex);
                var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
                var lastItem = startItem + ($("#EventsContents tbody tr").length - 1);
                $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

            },
            failure: function (response) {
                console.log(response);
                $("#EventsContents tbody").html('<tr><td colspan="7" class="center error">Error occured</td></tr>');
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


})