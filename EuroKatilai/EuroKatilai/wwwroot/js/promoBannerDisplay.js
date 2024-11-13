//var slides = document.querySelectorAll('.promo-slide');
//var currentSlide = 0;
//var slideInterval = setInterval(nextSlide, 4000); // Change slide every 4 seconds

//function nextSlide() {
//    slides[currentSlide].classList.remove('active');
//    currentSlide = (currentSlide + 1) % slides.length;
//    slides[currentSlide].classList.add('active');
//}

//function prevSlide() {
//    slides[currentSlide].classList.remove('active');
//    currentSlide = (currentSlide - 1 + slides.length) % slides.length;
//    slides[currentSlide].classList.add('active');
//}

//function goToSlide(index) {
//    if (index >= 1 && index <= slides.length) {
//        slides[currentSlide].classList.remove('active');
//        currentSlide = index - 1;
//        slides[currentSlide].classList.add('active');
//    }
//}

var slides = document.querySelectorAll('.promo-slide');
var dots = document.querySelectorAll('.dot');
var currentSlide = 0;
var slideInterval = setInterval(nextSlide, 4000); // Change slide every 4 seconds

function nextSlide() {
    slides[currentSlide].classList.remove('active');
    dots[currentSlide].classList.remove('dot-active'); // Remove dot-active class from current dot
    currentSlide = (currentSlide + 1) % slides.length;
    slides[currentSlide].classList.add('active');
    dots[currentSlide].classList.add('dot-active'); // Add dot-active class to new dot
}

function prevSlide() {
    slides[currentSlide].classList.remove('active');
    dots[currentSlide].classList.remove('dot-active'); // Remove dot-active class from current dot
    currentSlide = (currentSlide - 1 + slides.length) % slides.length;
    slides[currentSlide].classList.add('active');
    dots[currentSlide].classList.add('dot-active'); // Add dot-active class to new dot
}

function goToSlide(index) {
    if (index >= 1 && index <= slides.length) {
        slides[currentSlide].classList.remove('active');
        dots[currentSlide].classList.remove('dot-active'); // Remove dot-active class from current dot
        currentSlide = index - 1;
        slides[currentSlide].classList.add('active');
        dots[currentSlide].classList.add('dot-active'); // Add dot-active class to new dot
    }
}
