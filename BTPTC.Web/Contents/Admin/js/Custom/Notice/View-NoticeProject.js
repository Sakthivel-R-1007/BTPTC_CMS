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

    //end page




   


    $(document).on("click", "#deletebtn", function () {
        debugger
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#deleteeventId").val(($(this).attr("data-uniquecode")));
    });
    $(document).on("click", "#confirm_deleteiconBtn", function () {
        $("#DeleteEventsForm").submit();
    });


    //$(document).on('click', '.editNoticeProject', function (e) {
    //    $('#editNoticeProject').modal('show');
        
    //    $('#Location').val($(this).parent().parent().find('td:eq(3)').text());
    //    $('#Status').val($(this).parent().parent().find('td:eq(4)').text());

    //    $('#GUID').val($(this).parent().parent().find('td:eq(1)').text());

    //  //  $('#NoticeType').val($(this).parent().parent().find('td:eq(2)').text());
    //    var x = $(this).parent().parent().find('td:eq(2)').text();
    //    $('#NoticeType option:contains(' + x + ')').attr("selected", true)
        
    //})

})




    
    //function editEmp(productId) {
    //    $.ajax({
    //        url: '/EditTable/GetSingleEmp/' + productId, // The method name + paramater
    //        success: function (data) {
    //            $('#modalWrapper').html(data); // This should be an empty div where you can inject your new html (the partial view)
    //            $('#exampleModalLong').modal('show')
    //        }
    //    });
    //}



function editNoticeProject(id) {
    debugger
    var url = "/Admin/TownImprovementProject/GetEditNoticeProject?EncryptedId=" +id;
    $.ajax({
        url: url,
        type: 'GET',
        success: function (result) {
            debugger
         
            $('#modalWrapper').html(result); // This should be an empty div where you can inject your new html (the partial view)
            $('#editNoticepr').modal('show')
        }
    });


    //$("#ModalBodyDiv").load(url, function () {
    //    $("#editNoticeProject").modal("show");
    //})


    console.log(id)
}




$("#NoticeType").on("change", function () {
    debugger
    var value = $(this).val().toLowerCase();
    $("#NoticeProjecttbl tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});

