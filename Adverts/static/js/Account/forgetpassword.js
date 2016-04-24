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
    return result;
}