/// <reference path="../plugins/toastr/toastr.min.js" />

toastr.options = {
  "positionClass": "toast-bottom-right"
}

function notify(text)
{
    toastr.info(text)
}

function closeModal(el) {
    $("#" + el).modal('hide');
}