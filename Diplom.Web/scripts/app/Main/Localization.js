$(document).ready(function ($) {
    window.localizedJsFileText = function (parameter) {
        var result;
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: "/Localization/LocalizedJsFileText/",
            data: JSON.stringify({ parameters: parameter }),
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                result = data;
            }
        });
        return result;
    }
});