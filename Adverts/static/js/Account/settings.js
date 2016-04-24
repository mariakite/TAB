$(document).ready(function () {
    $("#form-setting-email").on("submit", function (e) {
        e.preventDefault();
        SaveEmail();
    });

    $("#form-setting-password").on("submit", function (e) {
        e.preventDefault();
        SavePassword();
    });
});

function SaveEmail() {
    if (ValidInsertEmailData()) {
        var email = $.trim($("#input-email").val());
        $.ajax({
            type: 'post',
            url: '/account/settings/',
            data: {
                "email": email
            },
            success: function (data) {
                if (data == status_success) {
                    toastr["success"]("Обновили email", "");
                }
                else if (data == status_already_insert) {
                    $("#form-setting-email .alert-danger").show();
                    toastr["error"]("Произошла ошибка", "");
                }
                else {
                    toastr["error"]("Произошла ошибка", "");
                }
            },
            error: function (data) {
                toastr["error"]("Произошла ошибка", "");
            }
        });
    }
}

function ValidInsertEmailData() {
    var result = true;
    $("#form-setting-email .has-error").removeClass("has-error");
    $("#form-setting-email .help-block").hide();
    $("#form-setting-email .alert-danger").hide();

    var email = $.trim($("#input-email").val());
    if (email.search(/.+@.+\..+/i) < 0) {
        $("#div-email").addClass("has-error");
        $("#div-email .help-block").show();
        result = false;
    }
    return result;
}

function SavePassword() {
    if (ValidInsertPasswordData()) {
        var old_password = $.trim($("#input-old-password").val());
        var new_password = $.trim($("#input-new-password").val());
        $.ajax({
            type: 'post',
            url: '/account/updatepassword/',
            data: {
                "old_password": old_password,
                "new_password": new_password
            },
            success: function (data) {
                if (data == status_success) {
                    toastr["success"]("Обновили пароль", "");
                }
                else if (data == status_incorrect_password) {
                    $("#form-setting-password .alert-danger").show();
                    toastr["error"]("Произошла ошибка", "");
                }
                else {
                    toastr["error"]("Произошла ошибка", "");
                }
            },
            error: function (data) {
                toastr["error"]("Произошла ошибка", "");
            }
        });
    }
}

function ValidInsertPasswordData() {
    var result = true;
    $("#form-setting-password .has-error").removeClass("has-error");
    $("#form-setting-password .help-block").hide();
    $("#form-setting-password .alert-danger").hide();

    var old_pass = $.trim($("#input-old-password").val());
    if (old_pass.length < 6 || old_pass.search(/[a-z]/) < 0 || old_pass.search(/[^\w\s]/i) < 0 && old_pass.search(/[\d]/i) < 0) {
        $("#div-old-password").addClass("has-error");
        $("#div-old-password .help-block").show();
        result = false;
    }

    var pass = $.trim($("#input-new-password").val());
    if (pass.length < 6 || pass.search(/[a-z]/) < 0 || pass.search(/[^\w\s]/i) < 0 && pass.search(/[\d]/i) < 0) {
        $("#div-new-password").addClass("has-error");
        $("#div-new-password .help-block").show();
        result = false;
    }

    var confirm_pass = $.trim($("#input-confirm-password").val());
    if (confirm_pass != pass) {
        $("#div-confirm-password").addClass("has-error");
        $("#div-confirm-password .help-block").show();
        result = false;
    }

    return result;
}