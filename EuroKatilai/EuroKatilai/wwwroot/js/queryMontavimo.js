document.getElementById('montBtnId').addEventListener('click', function () {
    var panel = document.getElementById('ch-contact-form-panel');
    if (panel.style.display === "none" || panel.style.display === "") {
        panel.style.display = 'block';
        panel.style.bottom = '100px'; // Slide up from the bottom
    } else {
        panel.style.bottom = '-500px'; // Slide down out of view
        setTimeout(function () {
            panel.style.display = 'none';
        }, 500); // Wait for the slide-out transition
    }
})