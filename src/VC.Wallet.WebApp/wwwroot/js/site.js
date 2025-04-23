// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('#frm_submit').submit(function () {
        if ($(this).valid()) {
            $('#overlay').show();
            return true;
        }
    });
    $('#frm_delete').submit(function () {
        $('#overlay').show();
        return true;
    });
    $('[data-toggle="tooltip"]').tooltip();
});