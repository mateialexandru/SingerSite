function showAuthorizedData(isAdmin) {
    if (isAdmin) {
        var array = document.querySelectorAll('[data-authorization="Admin"]');
        var i;
        for (i = 0; i < array.length; i++) {
            array[i].hidden = false;
        }
    }
}

$(document).ready(function () {
    $('div[data-option=FileTypeDropdown]').hide();
    $('#option1').show();
    $('#FileTypeDropdown').change(function () {
        $('div[data-option=FileTypeDropdown]').hide();
        $('#' + $(this).val()).show();
    })
});

