$(document).ready(function () {
    $.ajax({
        url: 'https://localhost:44331/api/Lista',
        type: 'GET',
        dataType: 'json',
        success: function (lists) {
            var practiceList = $('#practice-list');
            lists.forEach(function (lista) {
                var li = $('<li></li>').text(
                    lista.ListId + " - " + lista.ListName + " - " + lista.ListDescription + " - " + lista.ClassificationId
                );
                practiceList.append(li);
            });
        },
        error: function (xhr, status, error) {
            console.error('Error fetching lists:', error);
        }
    });
});
