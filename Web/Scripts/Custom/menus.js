$(".table-row").click(function () {
    window.location.href = "/Home/Menu/" + $(this).attr("id");
});

$(".delete").click(function () {
    $.post("/Home/DeleteKettering", { id: $(this).attr('id') }, function () {
        window.location.href = "/";
    });
});

$(".kick-out").click(function () {
    $.post("/Home/KickKetteringUser", { id: $(this).attr('id') }, function () {
        window.location.reload();
    })
});