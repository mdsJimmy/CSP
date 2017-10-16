/*jQuery ajax 封裝*/
$.mds = {
    ajaxload: function (url, data, successfn, errorfn) {
        $.ajax({
            type: "POST",
            data: data,
            url: url,
            dataType: "json",
            success: function (data) {
                if (data['status'] == 0) {
                    successfn(data);
                }
                else if (data['status'] == -2) {
                    errorfn(data);
                    $.alert({
                        content: "連線逾時，請重新登入！",
                        confirm: function () {
                            if (window.opener == null) {
                                location.reload();
                            }
                            else {
                                window.close();
                            }
                        }
                    });
                }
                else {
                    toastr["warning"](data['msg'], "Debug："); //debug
                    errorfn(data);
                    if (window.console) {
                        console.log('錯誤：' + data['msg'] + data['errtrace']);
                    }
                }
            },
            error: function (xhr, status, error) {
                err = { status: -1, code: xhr.status, msg: "失敗：網路錯誤(" + xhr.status + ")" }; //status
                toastr["warning"](err['msg'], "Debug："); //debug
                errorfn(err);
                if (window.console) {   //debug
                    console.log('Debug：');
                    console.log('xhr.responseText：' + xhr.responseText);
                }
            }
        });
    }
}