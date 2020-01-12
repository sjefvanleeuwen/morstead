/*Only needed for the controls*/
phone = document.getElementById("phone_1"),
iframe = document.getElementById("frame_1");


/*View*/
function updateView(view) {
  if (view) {
    phone.className = "phone view_" + view;
  }
}

/*Controls*/
function updateIframe() {

  // preload iphone width and height
  phone.style.width = "375px";
  phone.style.height = "667px";

  /*Idea by /u/aerosole*/
  document.getElementById("wrapper").style.perspective = (
    document.getElementById("iframePerspective").checked ? "1300px" : "none"
  );

}

updateIframe();

/*Events*/
document.getElementById("controls").addEventListener("change", function() {
  updateIframe();
});

document.getElementById("views").addEventListener("click", function(evt) {
  updateView(evt.target.value);
});

document.getElementById("phones").addEventListener("click", function(evt) {

  if(evt.target.value == 1){
    // iphone 6
    width = 375;
    height = 667; 
  }

  if(evt.target.value == 2){
    // samsung
    width = 400;
    height = 640; 
  }

  if(evt.target.value == 3){
    // microsoft
    width = 320;
    height = 480;  
  }

  if(evt.target.value == 4){
    // htc
    width = 360;
    height = 640; 
  }

  if(evt.target.value == 5){
    // ipad mini
    width = 768;
    height = 1024; 
  }

    phone.style.width = width + "px";
    phone.style.height = height + "px"; 

});


 iframe = document.getElementById('frame_1');

  if (iframe.attachEvent){
      iframe.attachEvent("onload", function(){
          afterLoading();
      });
  } else {
      iframe.onload = function(){
          afterLoading();
      };
  }

function afterLoading(){

    setTimeout(function() {
        phone.className = "phone view_1";
        setTimeout(function() {
            // do second thing
            phone.className = "phone view_1 rotate";
        }, 1000);
    }, 1000);

}