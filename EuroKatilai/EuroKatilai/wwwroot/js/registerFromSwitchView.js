function switchView(view) {
    // Hide all views
    document.getElementById('view-fizinisAsmuoFormView').style.display = 'none';
    document.getElementById('view-juridinisAsmuoFormView').style.display = 'none';
    // Show the selected view
    document.getElementById('view-' + view).style.display = 'block';

    // Update active class on buttons
    var buttons = document.querySelectorAll('.switch-box button');
    buttons.forEach(function (button) {
        button.classList.remove('active');
    });
    document.querySelector('.switch-box button[data-view="' + view + '"]').classList.add('active');
}

// Call switchView('fizinisAsmuoFormView') to show the first view by default on page load
window.onload = function () {
    document.getElementById('view-fizinisAsmuoFormView').click();
};