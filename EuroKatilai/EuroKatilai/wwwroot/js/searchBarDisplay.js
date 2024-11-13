document.getElementById('search-icon').addEventListener('click', function (e) {
    e.preventDefault(); // Prevent the default anchor action
    var searchInput = document.getElementById('search-input');

    // Toggle the active class to slide in the search bar
    if (searchInput.classList.contains('active')) {
        searchInput.classList.remove('active');
        searchInput.style.display = 'none'; // Hide input
    } else {
        searchInput.classList.add('active');
        searchInput.style.display = 'block'; // Show input
        searchInput.style.width = '200px'; // Set the width to slide in
        searchInput.focus(); // Optional: bring focus to the input
    }
});