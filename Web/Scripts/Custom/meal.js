$(".mealCard").click(function () {
    window.location.href = "/Home/Meal/" + $(this).attr("id");
});