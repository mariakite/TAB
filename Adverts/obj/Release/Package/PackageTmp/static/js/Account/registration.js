function ValidInsertData() {
    var result = true;
    $(".has-error").removeClass("has-error");
    $(".help-block").hide();

    var email = $.trim($("#input-email").val());
    if (email.search(/.+@.+\..+/i) < 0) {
        $("#div-email").addClass("has-error");
        $("#div-email .help-block").show();
        result = false;
    }

    var pass = $.trim($("#input-password").val());
    if (pass.length < 6 || pass.search(/[a-z]/) < 0 || pass.search(/[^\w\s]/i) < 0 && pass.search(/[\d]/i) < 0) {
        $("#div-password").addClass("has-error");
        $("#div-password .help-block").show();
        result = false;
    }

    var confirm_pass = $.trim($("#input-confirmpassword").val());
    if (confirm_pass != pass) {
        $("#div-confirmpassword").addClass("has-error");
        $("#div-confirmpassword .help-block").show();
        result = false;
    }

    return result;
}