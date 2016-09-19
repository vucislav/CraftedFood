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

$(".your-orders").click(function () {
    //$.post("/Home/GetCompanyUserId", { userId: $(this).data["userid"], companyId: $(this).data["companyid"] }, function (data) {
    //    window.location.href = "/Home/Orders/" + data;
    //});
    window.location.href = "/Home/Orders/" + $(this).attr('id');
});