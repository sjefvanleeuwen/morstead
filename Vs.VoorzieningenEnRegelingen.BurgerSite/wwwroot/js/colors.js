(function() {

  var hexDigits = [
    '0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f'
  ];

  var colors = document.querySelectorAll('.colors-list div');
  for(var i = 0 ; i < colors.length; i++) {
    var color = getComputedStyle(colors.item(i)).backgroundColor;
    if(color.indexOf('rgb') !== -1) {
      color = rgb2hex(color);
    }
    colors.item(i).innerHTML += '<span>' + color + '</span>';
  }

//Function to convert hex format to a rgb color
  function rgb2hex(rgb) {
    rgb = rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
    return '#' + hex(rgb[1]) + hex(rgb[2]) + hex(rgb[3]);
  }

  function hex(x) {
    return isNaN(x) ? '00' : hexDigits[(x - x % 16) / 16] + hexDigits[x % 16];
  }
}());