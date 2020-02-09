/// <reference path="../plugins/toastr/toastr.min.js" />

function notify(text)
{
    toastr.info(text)
}

function closeModal(el) {
    $("#" + el).modal('hide');
}