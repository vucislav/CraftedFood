$("#login-btn").click(function () {
    var info = {
        Login: $("#login").val(),
        Password: $("#password").val(),
        RememberMe: $("#remember-me").is(":checked")
    }
    $.post("/Home/Login", { model: info }, function (data) {
        if (data) window.location.replace("/Home/Index")
    });
});