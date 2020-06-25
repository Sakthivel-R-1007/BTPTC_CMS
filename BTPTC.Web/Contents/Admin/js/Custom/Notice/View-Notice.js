﻿$(document).ready(function () {



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

        var path = apppath + "/Admin/TownImprovementProject/PartialView" + UrlParameter

        $.ajax({
            url: path,
            dataType: 'html',
            success: function (response) {
                if (response === undefined && response == '') {
                    response = '<tr><td colspan="6" class="center">No Results Found</td></tr>';
                }

                $("#Noticetbl tbody").html(response);
                var pageIndex = parseInt($('.pagination').attr('data-pageindex'));
                //console.log(pageIndex);
                var startItem = (pageIndex == 1 ? 0 : (pageIndex - 1) * pageSize) + 1;
                var lastItem = startItem + ($("#Noticetbl tbody tr").length - 1);
                $(".page").html("Showing " + startItem + " to " + lastItem + " of " + $('.pagination').data('totalitems') + "entries");

            },
            failure: function (response) {
                console.log(response);
                $("#Noticetbl tbody").html('<tr><td colspan="7" class="center error">Error occured</td></tr>');
            }
        });

    }

    //end page



  
    

   
   

    //function miniReport() {
    //    var client_account_number = localStorage.getItem('numb');
    //    $.ajax({
    //        url: server_url + '/ws_report',
    //        timeout: 30000,
    //        type: "POST",
    //        data: {
    //            'client_language': client_language,
    //            'PIN_code': pin,
    //            'client_phone': number
    //        },
    //        success: function (msg) {
    //            if (msg.ws_resultat.result_ok == true) {
    //                alert('success!');
    //                window.open("account_details.html");
    //            }
    //        },
    //        error: function (jqXHR, textStatus) {
    //            //Manage your error.   
    //        }
    //    });
    //}







    $(document).on("click", "#deletebtn", function () {
        debugger
        var modal = document.getElementById("delete");
        $(modal).modal('show');
        $("#deleteeventId").val(($(this).attr("data-uniquecode")));
    });
    $(document).on("click", "#confirm_deleteiconBtn", function () {
        $("#DeleteEventsForm").submit();
    });



    //$(document).on('click', '.editNoticeType', function () {
    //    $('#editNotice').modal('show');
    //    $('#NoticeType').val($(this).parent().parent().find('td:eq(2)').text());
    //    $('#GUID').val($(this).parent().parent().find('td:eq(1)').text());
    //})
})




function EditNoticeType(id) {
    debugger
    var url = "/Admin/TownImprovementProject/EditNoticeProject?EncryptedId=" + id;
    $.ajax({
        url: url,
        type: 'GET',
        success: function (result) {
            debugger

            $('#modalWrapper').html(result); // This should be an empty div where you can inject your new html (the partial view)
            $('#editNoticeType').modal('show')
        }
    });



}