$('#formSteps').on('shown.bs.collapse', function () {
  this.scrollIntoView({behavior: "smooth", block: "end", inline: "nearest"});
});
$('div[data-toggle=collapse]').click( function() {  
    // swap chevron
    $(this).toggleClass('fa-chevron-down fa-chevron-up');
});