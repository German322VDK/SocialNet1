var Index = 1;

//showSlides(Index);

function plusSlides() {
  showSlides(parseInt(Index)+1);
}

function minusSlides() {
  showSlides(Index - 1);
}

function currentSlide() {
  showSlides(Index);
}

function formShow(id){
  var modal_1 = $('.modal_1');

  modal_1.toggleClass("modal_1__is_open");

  showSlides(id);
}

function showSlides(n) {

  var slides = document.getElementsByClassName("mySlides");

  if (n > slides.length) {
        Index = 1;
    }
  else if (n < 1) {
        Index = slides.length;
    }
    else{
        Index = n;
    }
  for (var i = 0; i < slides.length; i++) {
      slides[i].style.display = "none";
  }

  var normSlide = document.getElementById(Index);

  normSlide.style.display = "block";
}

function closeForm(){

  var modal_1 = $('.modal_1');

  modal_1.toggleClass("modal_1__is_open");
} 
