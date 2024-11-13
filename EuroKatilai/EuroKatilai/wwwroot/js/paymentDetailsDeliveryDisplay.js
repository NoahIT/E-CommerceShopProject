document.querySelectorAll('input[name="delivery"]').forEach((input) => {
    input.addEventListener('change', function () {
        document.querySelectorAll('.delivery-option').forEach((div) => {
            div.classList.remove('active');
        });
        document.querySelector('.delivery-option.' + this.value).classList.add('active');
    });
});