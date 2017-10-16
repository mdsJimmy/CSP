jQuery(function ($) {
    $.validator.addMethod("phone", function (value, element) {
        //return this.optional(element) || /^[0-9]{2,4}-[0-9]{6,8}[0-9#]*$/i.test(value);
        return this.optional(element) || /(^[0-9]{2,3}(-{1})[0-9]{6,8}(#?)[0-9]{1,}$)|(^09[0-9]{2}[0-9]{6}$)/i.test(value);
    }, "錯誤的電話格式,請使用02-12345678或 0910123456格式");
    $.validator.addMethod("mobile", function (value, element) {
        return this.optional(element) || /^09[0-9]{2}[0-9]{6}$/i.test(value);
    }, "錯誤的手機格式,請使用0910123456格式");
    $.validator.addMethod("alphanumeric", function (value, element) {
        return this.optional(element) || /^[a-zA-Z0-9]+$/.test(value);
    }, "只能輸入英文、和數字");
});
/*只能輸入數字與#- 
使用方法: $('#txt_PhoneNumber').keypress(validateNumber); */
function validateNumber(event) {
    var key = window.event ? event.keyCode : event.which;

    if (event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 35 || event.keyCode == 45) {
        return true;
    }
    else if (key < 48 || key > 57) {
        return false;
    }
    else return true;
};