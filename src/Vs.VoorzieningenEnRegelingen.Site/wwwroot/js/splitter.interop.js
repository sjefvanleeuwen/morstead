function split(dotNetReference, onResizeCallBackMethodName) {
  $(window).resize(function () {
    var window_height = $(window).height(),
      header_height = $(".main-header").height();
    $("#splitcontainer").css("height", window_height - header_height - 17);
    if (dotNetReference && onResizeCallBackMethodName) dotNetReference.invokeMethodAsync(onResizeCallBackMethodName);
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

function splitYamlContentEditor(dotNetReference, onResizeCallBackMethodName) {
  $(window).resize(function () {
    var window_height = $(window).height(),
      header_height = $(".main-header").height();
    $("#splitcontainer").css("height", window_height - header_height - 17);
    if (dotNetReference && onResizeCallBackMethodName) dotNetReference.invokeMethodAsync(onResizeCallBackMethodName);
  });
  $(window).resize();
  var minSize = 310;
  var splitobj = Split(["#left-half_contenteditor", "#right-half_contenteditor"], {
    elementStyle: function (dimension, size, gutterSize, index) {
      $(window).trigger('resize'); // Optional
      if (index === 0)
        return { 'flex-basis': 'calc(100% - 10px - max(calc(' + (100 - size) + '% - ' + gutterSize + 'px), ' + (minSize + gutterSize) + 'px))' }
      if (index === 1)
        return { 'flex-basis': 'max(calc(' + size + '% - ' + gutterSize + 'px), ' + (minSize + gutterSize) + 'px)' }

    },
    gutterStyle: function (dimension, gutterSize) { return { 'flex-basis': gutterSize + 'px' } },
    sizes: [80, 20],
    minSize: minSize,
    gutterSize: 6,
    cursor: 'col-resize'
  });
}