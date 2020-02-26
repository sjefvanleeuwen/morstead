function InitCollapsable() {
  // Load collapse component
  System.import('/uno/components/collapse/collapse.js').then(function (module) {
    // Select all collapsible elements on the page
    var collapses = document.querySelectorAll('[x-uno-collapse]');
    // Initialize all collapses
    for (var i = 0; i < collapses.length; i++) {
      new module.Collapse(collapses.item(i));
    }
  });
}