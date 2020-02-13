/**
 * Script to copy the svg icon code to the clipboard. Iterates over all the
 * icons and adds a click listener, on click, js tries to add the code to the
 * clipboard
 */
(function() {
  "use strict";

  window.addEventListener('load', onLoad);

  function onLoad() {
    // Get all the icons in the list
    var icons = document.querySelectorAll('.icons-holder');

    for(var i = 0; i < icons.length; i++) {
      icons.item(i).addEventListener('click', onIconClick);
    }
  }

  /**
   * Event handler for the click event
   * @param event
   */
  function onIconClick(event) {
    var textarea = event.currentTarget.querySelector('textarea');

    textarea.select();

    try {
      var successful = document.execCommand('copy');
      var msg = successful ? 'successful' : 'unsuccessful';
      console.log('Copying text command was ' + msg);
    } catch (err) {
      console.log('Oops, unable to copy');
    }
  }
}());
