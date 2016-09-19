$(".addMeal").click(function () {
    window.location.href = "/Home/AddMeal/" + $(this).attr("id");
});


$(".open-menu").click(function () {
    window.location.href = "/Home/Meal/" + $(this).attr("id");
});

$("select").change(function () {
    $(this).siblings('input').attr('value', $(this).val());
    $(this).siblings('input').val($(this).val());
    $(this).siblings('input').html($(this).val());
})