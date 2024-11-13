function switchView(view) {
    // Hide all views
    document.getElementById('view-profile-content').style.display = 'none';
    document.getElementById('view-profile-delivery').style.display = 'none';
    document.getElementById('view-profile-password-diff').style.display = 'none';
    document.getElementById('view-profile-remove-profile').style.display = 'none';
    document.getElementById('view-profile-logout').style.display = 'none';

    // Show the selected view
    document.getElementById('view-' + view).style.display = 'block';

    // Update active class on buttons
    var buttons = document.querySelectorAll('.side-panel button');
    buttons.forEach(function (button) {
        button.classList.remove('active');
    });
    document.querySelector('.sidepanel button[data-view="' + view + '"]').classList.add('active');
}

// Call switchView('fizinisAsmuoFormView') to show the first view by default on page load
window.onload = function () {
    switchView('profile-content');
};