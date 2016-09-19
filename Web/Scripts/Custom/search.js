$(".company-btn").click(function () {
    var type;
    if ($(this).hasClass("kettering")) type = "Kettering";
    else type = "Company";
    window.location.href = "/Home/" + type + "/" + $(this).attr('id');
});