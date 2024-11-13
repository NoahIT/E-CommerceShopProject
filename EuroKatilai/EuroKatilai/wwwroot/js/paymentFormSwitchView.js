function switchView(view) {
    // Hide all views
    document.getElementById('view-fizinisAsmuoFormDView').style.display = 'none';
    document.getElementById('view-juridinisAsmuoFormDView').style.display = 'none';
    // Show the selected view
    document.getElementById('view-' + view).style.display = 'block';

    // Update active class on buttons
    var buttons = document.querySelectorAll('.switch-box button');
    buttons.forEach(function (button) {
        button.classList.remove('active');
    });
    document.querySelector('.switch-box button[data-view="' + view + '"]').classList.add('active');

    // Update form attribute for the continue button
    var continueButton = document.getElementById('continueButton');
    if (view === 'fizinisAsmuoFormDView') {
        continueButton.setAttribute('form', 'formFizinisAsmuo');
    } else if (view === 'juridinisAsmuoFormDView') {
        continueButton.setAttribute('form', 'formJuridinisAsmuo');
    }
}