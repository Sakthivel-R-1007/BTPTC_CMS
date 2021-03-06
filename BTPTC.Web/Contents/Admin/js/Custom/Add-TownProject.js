﻿$(document).ready(function () {

	initializeValidation();
	//intializepagination();

	//Start view NoticeType

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

	//delete 


	$(document).on("click", "#deletebtn", function () {
		debugger
		var modal = document.getElementById("delete");
		$(modal).modal('show');
		$("#deleteeventId").val(($(this).attr("data-uniquecode")));
	});
	$(document).on("click", "#confirm_deleteiconBtn", function () {
		$("#DeleteEventsForm").submit();
	});

	$(document).on("click", "#addnoticebtn", function () {

		var modal = document.getElementById("addNotice");
		$(modal).modal('show');
		initializeValidation();
	});







	//end View NoticeType



	$(document).on("click", ".editMetaBtn", function () {

		$('#Location').val('');
		$('#Status').val('');
	});



	$(document).on("click", "#btnfacility", Confirm);


	function Confirm() {

		if ($("#TownProjectFrom").valid()) {

			if (locationcheking())
				NoticeTable();


			else
				$("#LocationCheck").text("Location Name already Exists");

		}
	}


	function locationcheking() {


		var x = true;
		var loc = $('#Location').val();
		var hiddenid = $('#hiddenTableRowid').val();
		$('#NoticeTbl tbody tr').each(function () {

			var location = $(this).find('td:eq(1)').text();
			var id = $(this).find('td:eq(4) .Editlink').attr('data-uniquecode')

			if (loc == location && hiddenid != id) {
				x = false;

				$("#LocationCheck").text("Location Name already Exists");
			}


		})
		return x;
	}


	$('#NoticeTypeSubmit').click(function () {

		var ProjectName = $('#NoticeType').val()

		var rowcount = $('#NoticeTbl tbody tr').length;



		var TblLenth = $('#NoticeTbl tbody tr').length;

		if (TblLenth > 0) {
			$("#AddProjectTypeFrom").submit();


		} else {

			$("#TableRowCheck").text("Table row Empty");

		}


	})


	$("#btnfacility").click(function () {

		//$("#TownProjectFrom").submit();
		$("#TableRowCheck").text("");
		$("#LocationCheck").text("");

	});


	jQuery("#TownProjectFrom").validate({
		errorPlacement: function (error, element) {
			$("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
		},
		rules: {
			"Location": { required: true },
			"Status": { required: true },

		}
	});
});


function initializeValidation() {

	jQuery.validator.setDefaults({
		ignore: []
	});

	$("#AddProjectTypeFrom").validate({
		errorPlacement: function (error, element) {
			$("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
		},
		rules: {
			NoticeType: {
				required: true,
				remote: {
					url: "/Admin/TownImprovementProject/CheckNoticeTitle",

					type: "GET",
					async: true,
					cache: false,
					data: {
						NoticeType: function () {
							return $("#NoticeType").val();
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
			NoticeType: { remote: 'NoticeType is duplicated' },
		}
	});
}

















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

function NoticeTable() {
	var rowCount = $('.data-contact-person').length + 0;

	var Location = $('#Location').val();
	var Status = $('#Status').val();
	var date = new Date();


	var ModifiedOn = ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '-' + ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '-' + date.getFullYear() + ' ' + ('0' + date.getHours()).slice(-2) + ':' + ('0' + date.getMinutes()).slice(-2) + ':' + ('0' + date.getSeconds()).slice(-2);
	//var ModifiedOn = ((date.getDate() > 9) ? date.getDate() : ('0' + date.getDate())) + '-' + ((date.getMonth() > 8) ? (date.getMonth() + 1) : ('0' + (date.getMonth() + 1))) + '-' + date.getFullYear() + ' ' + date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
	//var Guid = $('#GUID').val();
	var Guid = $('#hiddenTableRowid').val();

	if (Guid != null && Guid != '') {

		$.each($('#NoticeTbl tbody tr'), function () {

			if ($(this).find('td:eq(4) .Editlink').attr('data-uniquecode') == Guid) {

				let rowCount = $(this).find('td:eq(0)').text();

				$(this).empty();
				///var rowCount = $(this).attr('id');


				var imageadd = '<td class="center">' + parseInt(parseInt(rowCount)) + '</td>' +
					"<td>" + Location + '<input id="NoticeTypes_' + rowCount + '_Location" type="hidden" name="NoticeTypes[' + rowCount + '].Location" value="' + Location + '"/></td>' +
					"<td>" + Status + '<input id="NoticeTypes_' + rowCount + '_Status" type="hidden" name="NoticeTypes[' + rowCount + '].Status" value="' + Status + '"/></td>' +
					"<td>" + ModifiedOn + '<input id="NoticeTypes_' + rowCount + '_ModifiedOn" type="hidden" name="NoticeTypes[' + rowCount + '].ModifiedOn" value="' + ModifiedOn + '"/></td>' +
					'<td class="center"><a id="Editdiv_' + rowCount + '" data-uniquecode="' + rowCount + '"  class="Editlink" href="javascript:void(0)">Edit</a> | <a id="deldiv_' + rowCount + '"  class="linelink" href="javascript:void(0)">Delete</a></td>';

				$(this).append(imageadd);
				$('#editPopup').modal('hide');

				$('#Location').val('');
				$('#Status').val('');
				//var ModifiedOn = new Date();

				$('#hiddenTableRowid').val('');
				$('#imagemodeladd').modal('hide')
				return false;
			}
		})
	}
	else {
		$('#NoticeTbl tbody').append(

			'<tr class="data-contact-person" id=' + rowCount + '>' +
			'<td class="center">' + parseInt(parseInt(rowCount) + parseInt(1)) + '</td>' +
			"<td>" + Location + '<input id="NoticeTypes_' + rowCount + '_Location" type="hidden" name="NoticeTypes[' + rowCount + '].Location" value="' + Location + '"/></td>' +
			"<td>" + Status + '<input id="NoticeTypes_' + rowCount + '_Status" type="hidden" name="NoticeTypes[' + rowCount + '].Status" value="' + Status + '"/></td>' +
			"<td>" + ModifiedOn + '<input id="NoticeTypes_' + rowCount + '_ModifiedOn" type="hidden" name="NoticeTypes[' + rowCount + '].ModifiedOn" value="' + ModifiedOn + '"/></td>' +
			'<td class="center"><a id="Editdiv_' + rowCount + '"  data-uniquecode="' + parseInt(parseInt(rowCount) + parseInt(1)) + '"  class="Editlink" href="javascript:void(0)">Edit</a> | <a id="deldiv_' + rowCount + '"  class="linelink" href="javascript:void(0)">Delete</a></td>' +
			"</tr>"

		)
		$('#imagemodeladd').modal('hide')

	}

}




$(document).on('click', '.Editlink', function () {

	let guid = $(this).attr('data-uniquecode');

	$('#Location').val($(this).parent().parent().find('td:eq(1)').text());
	$('#Status').val($(this).parent().parent().find('td:eq(2)').text());
	$('#hiddenTableRowid').val(guid);
	$('#imagemodeladd').modal('show');
})


$(document).on('click', '.editpopup', function () {
	debugger
	let guid = $(this).attr('data-uniquecode');
	if (guid != '' && guid != null) {
		$.ajax({
			type: 'GET',
			url: '/Admin/TownImprovementProject/EditTownProjectNoticePopUp?EncryptedId=' + guid,
			success: function (data) {
				if (data != null) {
					$('#Location').val(data.Location);
					$('#Status').val(data.Status);
					$('#GUID').val(data.GUID);
					$('#editPopup').modal('show');
				}
			}
		})
	}
	else {
		alert('empty guid')
	}
})




$("#NoticeTbl").on("click", ".linelink", function (ev) {
	var $currentTableRow = $(ev.currentTarget).parents('tr')[0];
	$currentTableRow.remove();

	$('#NoticeTbl tbody').find('tr').each(function (i) {
		let parent = $(this);
		let $parentE = $(parent);
		$parentE.attr('id', i);
		$parentE.find('td:eq(0)').text(i + 1);



		let $Location = $parentE.find('td:eq(1)').children();
		$Location.eq(0).attr('id', 'Notice_NoticeTypes_' + i + '_Location').attr('name', 'NoticeTypes[' + i + '].Location');

		let $Status = $parentE.find('td:eq(2)').children();
		$Status.eq(0).attr('id', 'Notice_NoticeTypes_' + i + '_Status').attr('name', 'NoticeTypes[' + i + '].Status');


		let $modified = $parentE.find('td:eq(3)').children();
		$modified.eq(0).attr('id', 'Notice_NoticeTypes_' + i + '_ModifiedOn').attr('name', 'NoticeTypes[' + i + '].ModifiedOn');

		let $buttons = $parentE.find('td:eq(4)').children();
		$buttons.eq(0).attr('id', 'Editdiv_' + i);
		$buttons.eq(1).attr('id', 'deldiv_' + i);
	});
});



