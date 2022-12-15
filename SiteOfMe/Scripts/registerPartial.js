$(function () {
    $("#Email").focusout(data, function () {
        $("#DisplayName").val($(this).val());
    });

    $("#btnClose").button({
        icons: {
            primary: "ui-icon-circle-close"
        }
    });
});

function onRegisterCompleted(context) {

}