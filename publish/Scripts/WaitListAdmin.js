$('#pagesizelist').on('change', function (event) {
    var form = $(event.target).parents('form');

    form.submit();
});
