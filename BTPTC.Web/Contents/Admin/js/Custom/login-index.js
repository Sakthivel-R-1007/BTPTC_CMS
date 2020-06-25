$(document).ready(function () {

	IntializeValidation();
	IntializeModelClose();

	$('#ForceLoginPopup').modal('show');
	$('.close-modal').hide();
	$("#yes").on("click", function () {
		$("#ForceLoginForm").submit();
	});

	$("#no").on("click", function () {
		$("#ForceLoginPopup").css("display", "none");
	});

	$(document).on("click", ".submitBtn", function () {
		if ($("#LoginForm").valid()) {
			$("#LoginForm").submit();
		}
	});
});

function IntializeValidation() {

	$("#LoginForm").validate({
		errorPlacement: function (error, element) {
			$("span[data-valmsg-for='" + $(element).attr("name") + "']").html($(error));
		},
		rules: {
			UserName: {
				required: true
			},
			Password: {
				required: true
			}
		}
	});
}

function IntializeModelClose() {
	$('a.openModal').click(function (event) {
		$(this).modal({
			fadeDuration: 250,
			showClose: false
		});
		return false;
	});
}







