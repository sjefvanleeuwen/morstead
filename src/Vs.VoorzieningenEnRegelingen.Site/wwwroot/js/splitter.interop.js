

function split(dotnetResizeMethodreference, callbackMethodName) {
    $(window).resize(function () {
        var window_height = $(window).height(),
            header_height = $(".main-header").height();
            $("#splitcontainer").css("height", window_height - header_height - 17);
        if (dotnetResizeMethodreference) dotnetResizeMethodreference.invokeMethodAsync(callbackMethodName);
        });
    $(window).resize();
    var splitobj = Split(["#left-half", "#right-half"], {
        elementStyle: function (dimension, size, gutterSize) {
            $(window).trigger('resize'); // Optional
            return { 'flex-basis': 'calc(' + size + '% - ' + gutterSize + 'px)' }
        },
        gutterStyle: function (dimension, gutterSize) { return { 'flex-basis': gutterSize + 'px' } },
        sizes: [80, 20],
        minSize: 150,
        gutterSize: 6,
        cursor: 'col-resize'
    });
}
/*
function split() {
  $(window).resize(function () {
    var window_height = $(window).height(),
      header_height = $(".main-header").height();
    $("#splitcontainer").css("height", window_height - header_height - 17);
  });
  $(window).resize();
  var splitobj = Split(["#left-half", "#right-half"], {
    elementStyle: function (dimension, size, gutterSize) {
      $(window).trigger('resize'); // Optional
      return { 'flex-basis': 'calc(' + size + '% - ' + gutterSize + 'px)' }
    },
    gutterStyle: function (dimension, gutterSize) { return { 'flex-basis': gutterSize + 'px' } },
    sizes: [80, 20],
    minSize: 150,
    gutterSize: 6,
    cursor: 'col-resize'
  });
}
*/