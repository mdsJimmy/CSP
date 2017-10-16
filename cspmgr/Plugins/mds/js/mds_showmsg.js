var MasterCtrl = {
    showInfoMsg: function (msg) {
        $('#MsgContainer').empty();
        var tempErrMsg = '<div class="alert alert-info" style="margin-top: 3px;padding: 2px;margin-bottom: 0px;border-radius: 2px;">' +
                                    '<button type="button" class="close" data-dismiss="alert"><i class="ace-icon fa fa-times"></i></button>' +
                                    '<strong><i class="ace-icon fa fa-info-circle"></i>&nbsp;</strong>' + msg +
                                 '</div>';
        $('#MsgContainer').append(tempErrMsg);
    },
    showErrMsg: function (msg) {
       $('#MsgContainer').empty();
        var tempErrMsg = '<div class="alert alert-danger" style="margin-top: 3px;padding: 2px;margin-bottom: 0px;border-radius: 2px;">' +
                                    '<button type="button" class="close" data-dismiss="alert"><i class="ace-icon fa fa-times"></i></button>' +
                                    '<strong><i class="ace-icon fa fa-times-circle"></i>&nbsp;</strong>' + msg +
                                 '</div>';
        $('#MsgContainer').append(tempErrMsg);
    },
    showWarningMsg: function (msg) {
        $('#MsgContainer').empty();
        var tempErrMsg = '<div class="alert alert-warning" style="margin-top: 3px;padding: 2px;margin-bottom: 0px;border-radius: 2px;">' +
                                    '<button type="button" class="close" data-dismiss="alert"><i class="ace-icon fa fa-times"></i></button>' +
                                    '<strong><i class="ace-icon fa fa-exclamation-circle"></i>&nbsp;</strong>' + msg +
                                 '</div>';
        $('#MsgContainer').append(tempErrMsg);
    },
    showSucessMsg: function (msg) {
        $('#MsgContainer').empty();
        var tempSuccessMsg = '<div class="alert alert-success" style="margin-top: 3px;padding: 2px;margin-bottom: 0px;border-radius: 2px;">' +
                                        '<button type="button" class="close" data-dismiss="alert"><i class="ace-icon fa fa-times"></i></button>' +
                                        '<strong><i class="ace-icon fa fa-check-circle"></i>&nbsp;</strong>' + msg +
                                     '</div>';
        $('#MsgContainer').append(tempSuccessMsg);
    },
    clearMsg: function () {
        //$('#lblErrorMsg').text('');
        //$('#lblSucessMsg').text('');
        $('#MsgContainer').empty();
    },
    showSuccessToastr: function (msg, title) {
        toastr.options = {
            "closeButton": false,
            "positionClass": "toast-top-center",
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "3000",
            "extendedTimeOut": "1000"
        };
        toastr["success"](msg); //
        this.showSucessMsg(msg);
    },
    showErrorToastr: function (msg, title) {
        toastr.options = {
            "closeButton": true,
            "positionClass": "toast-top-center",
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "0",
            "extendedTimeOut": "0"
        };
        toastr["error"](msg, title);    //
        this.showErrMsg(msg);
    }
}