$(".table-row").click(function () {
    window.location.href = "/Home/Menu/" + $(this).attr("id");
});

$(".delete").click(function () {
    $.post("/Home/DeleteKettering", { id: $(this).attr('id') }, function () {

    });
});