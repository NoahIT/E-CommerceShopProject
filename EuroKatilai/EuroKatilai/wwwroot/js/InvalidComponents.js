document.addEventListener('DOMContentLoaded', (event) => {
    var requiredInputs = document.querySelectorAll('input[required], select[required], textarea[required]');
    requiredInputs.forEach(function (input) {
        input.oninvalid = function () {
            this.parentNode.classList.add('invalid-label');

            this.parentNode.classList.add('shake-animation');

            this.setCustomValidity('Prašome užpildyti šį lauką');

            setTimeout(() => {
                this.parentNode.classList.remove('shake-animation');
            }, 500);
        };

        input.oninput = function () {
            this.setCustomValidity('');
            this.parentNode.classList.remove('invalid-label');
        };
    });
});

function switchView(view) {
    document.getElementById('view-fizinisAsmuoFormDView').style.display = 'none';
    document.getElementById('view-juridinisAsmuoFormDView').style.display = 'none';

    document.getElementById('view-' + view).style.display = 'block';

    // Получаем обе кнопки
    var btnFiz = document.getElementById('btnfiz');
    var btnJur = document.getElementById('btnjur');

    // Удаляем класс active у обеих кнопок
    document.getElementById('btnfiz').classList.remove('active');
    document.getElementById('btnjur').classList.remove('active');

    // Установить класс 'active' для выбранной кнопки
    if (view === 'fizinisAsmuoFormDView') {
        document.getElementById('btnfiz').classList.add('active');
    } else {
        document.getElementById('btnjur').classList.add('active');
    }

    var submitBtn = document.getElementById('submit-btn');

    if (view === 'fizinisAsmuoFormDView') {
        submitBtn.setAttribute('form', 'formFizinisAsmuo');
    } else if (view === 'juridinisAsmuoFormDView') {
        submitBtn.setAttribute('form', 'formJuridinisAsmuo');
    }
}

