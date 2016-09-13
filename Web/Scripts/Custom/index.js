$(".table-row").click(function () {
    window.location.replace("/Home/Company/" + $(this).attr('id'));
});