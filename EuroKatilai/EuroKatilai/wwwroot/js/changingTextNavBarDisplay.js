// Array of phrases to rotate
var phrases = [
    "Nemokamas pristatymas užsakymams nuo 100 €",
    "Susisiekite su mumis ir gaukite ypatingus pasiūlymus",
    "Konsultuojame visais klausimais"
];
var currentPhraseIndex = 0;

// Function to change text
function changeText(direction) {
    // Update the current phrase index based on the direction
    currentPhraseIndex += direction;
    if (currentPhraseIndex < 0) {
        currentPhraseIndex = phrases.length - 1;
    } else if (currentPhraseIndex > phrases.length - 1) {
        currentPhraseIndex = 0;
    }

    // Set the new phrase
    document.getElementById('rotating-text').textContent = phrases[currentPhraseIndex];
}

// Function to rotate text every 3 seconds
function rotateText() {
    setInterval(function () {
        changeText(1); // Change to the next phrase
    }, 4000); // 3000 milliseconds = 3 seconds
}

// Start rotating text when the page loads
window.onload = rotateText;