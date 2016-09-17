$(".addMeal").click(function () {
    window.location.href = "/Home/AddMeal/" + $(this).attr("id");
});


$(".open-menu").click(function () {
    window.location.href = "/Home/Meal/" + $(this).attr("id");
});