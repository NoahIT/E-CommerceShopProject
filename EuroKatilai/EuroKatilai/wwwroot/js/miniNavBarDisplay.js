// Get the mini header element
var miniHeader = document.querySelector('.mini-navbars.mini-bot-nav');

// Function to run when the user scrolls
window.onscroll = function () {
    // Get the current scroll position
    var scrollPosition = window.pageYOffset || document.documentElement.scrollTop;

    // Set a scroll position threshold to hide the mini header, e.g., 100 pixels
    if (scrollPosition > 100) {
        miniHeader.classList.add('hide-mini-header');
    } else {
        miniHeader.classList.remove('hide-mini-header');
    }
};