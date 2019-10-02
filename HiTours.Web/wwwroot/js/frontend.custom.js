$(function () {

    if ($.fn.format != undefined) {
        $(".numericOnly").format({ precision: 0, autofix: true, allow_negative: false });
        $(".decimalOnly").format({ precision: 2, autofix: true });
    }

    $("[data-val-length-max]").each(function () {
        $(this).attr("maxlength", $(this).data("val-length-max"));
    });
    var remember = $.cookie('remember');
    if (remember == 'true') {
        var username = $.cookie('username');
        var password = $.cookie('password');
        // autofill the fields
        $('#Email').val(username);
        $('#Password').val(password);
    }
});
