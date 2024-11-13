document.addEventListener("DOMContentLoaded", function () {
    // Array of image paths for the high-class manufacturers
    var highClassImages = [
        '/Images/BrandLogos/AlphaInnotecLogo.png',
        '/Images/BrandLogos/AristonLogo.png',
        '/Images/BrandLogos/BoshLogo.png'
    ];

    // Array of image paths for the economical-class manufacturers
    var economicalClassImages = [
        '/Images/BrandLogos/BrinkLogo.png',
        '/Images/BrandLogos/BuderusLogo.png',
        '/Images/BrandLogos/ViessmannLogo.png'
    ];

    // Function to change image source
    function changeImage(imageArray, imageElement) {
        let currentIndex = 0;
        setInterval(function () {
            imageElement.src = imageArray[currentIndex];
            currentIndex = (currentIndex + 1) % imageArray.length; // Loop back to the first image
        }, 4000); // Change every 4 seconds
    }

    // Get the image elements
    var highClassImageElement = document.querySelectorAll('.rotating-image')[0];
    var economicalClassImageElement = document.querySelectorAll('.rotating-image')[1];

    // Start rotating images
    changeImage(highClassImages, highClassImageElement);
    changeImage(economicalClassImages, economicalClassImageElement);
});
