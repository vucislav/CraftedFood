$(".delete").click(function () {
    $.post("/Home/DeleteCompany", { id: $(this).attr('id') }, function () {
        window.location.href = "/";
    });
});

$(".kick-out").click(function () {
    $.post("/Home/KickCompanyUser", { id: $(this).attr('id') }, function () {
        window.location.reload();
    })
});