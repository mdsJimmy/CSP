
        $(document).ready(function () {            
            Ctrl.init();
            MsgDataControler.init(Ctrl.MsgID); 
            Ctrl.setStatus();
            $.validator.addMethod("OnlyN", function (value, element) {
            return this.optional(element) || /^[N]+$/.test(value);
            }, "請選擇指定時間");
            if (Ctrl.MsgID != "0" && Parameter.CanUpdate != "Y") {
                Ctrl.DisableContentField();
            }

            //已發佈訊息所有欄位狀態
            if (_qryPushed == "Y" && _qryCopyMsg != "Y") {
                Ctrl.DisableContentField();
            } else {
                if (Ctrl.MsgID != "0") {
                    $("input[type='radio'][name='rdoPublishDttm'][value='Y']").attr('checked', true);
                } else {
                    $("#spnPublistDTTM").hide();
                }
            }

            //複製狀態
            if (_qryCopyMsg == "Y") {
                Ctrl.MsgID = "0";
                $("#msgid").val("");
                Element.hidden_MsgID.val("0");
                $("#btnCopy").remove();
                $("input[name='rdoPublishDttm']")[0].checked = false;
                $("input[name='rdoPublishDttm']")[1].checked = false;
                $("#spnPublistDTTM").hide();
            }
        });

        $(window).load(function () {
            $(".various").fancybox({
                modal: true, //If set to true, will disable navigation and closing. Default value: false. 要設true
                padding: 5, //Default value: 15
                margin: 10,  //Default value: 20
                fitToView: true, //If set to true, fancyBox is resized to fit inside viewport before opening. Default value: true. 要設true
                autoSize: false,  //If true, then sets both autoHeight and autoWidth to true. Default value: true	
                autoCenter: true, //If set to true, the content will always be centered.Default value: !isTouch. 要設true
                closeClick: false, //If set to true, fancyBox will be closed when user clicks the content. Default value: false
                scrolling: 'auto', //Default value: 'auto'
                height: 600,
                helpers: {
                    title: {
                        type: 'float'
                    },
                    overlay: {
                        locked: true
                    }
                },
                afterLoad: function () {
                    $("#fancybox-frame").css({ 'overflow-x': 'visible' });
                    $('.fancybox-iframe.no-skin').css({ 'overflow': 'auto' });
                },
                preload: true,
                scrollOutside: false, //If true, the script will try to avoid horizontal scrolling for iframes and html content. Default value: true
                openEffect: 'none',
                closeEffect: 'none'
            });

        });

        var Ctrl = {
            URLParameters: undefined,
            MsgID: false,
            DisableContentField: function () {
                $("#btnSave").remove();

                $("#ContentDiv :input").each(function (index, item) {
                    $(item).attr("disabled", true);
                });

                $('#depts').editable("disable");
                $('#users').editable("disable");
                //$("#taaEditContent").attr('contentEditable', false);

                $('#spnPublistRadio').hide(); //隱藏[發佈時間]的radio button

                $("#addrow").hide();
            },
            initStatusDataTable: function () {
                if (Ctrl.MsgID == "0" || _qryPushed == "N" || _qryCopyMsg == "Y")
                    $("#btnSurveyResult").hide();
                else
                    $("#btnSurveyResult").show();
            },
            setStatus: function () {
                //訊息類型
                var msgkind = $("input[name='msgkind']:radio");
                msgkind.change(function () {
                    var msgkind_val = $("input[name='msgkind']:checked").val();
                    if (msgkind_val == "I") {
                        $("#liSurvey").show();
                    }
                    else {
                        $("#liSurvey").hide();
                    }
                });

                //相關日期時間設定
                var rdoPublishDttm = $("input[name='rdoPublishDttm']:radio");
                rdoPublishDttm.change(function () {
                    var rdoPublishDttm_val = $("input[name='rdoPublishDttm']:checked").val();
                    if (rdoPublishDttm_val == "Y") {
                        $("#spnPublistDTTM").show();
                    }
                    else {
                        $("#spnPublistDTTM").hide();
                        //if ($("#offshelfdate").val() == "") { //[下架日期]未填寫時，才會依上架日期加一個月做為預設的下架日期
                        //    $("#offshelfdate").datepicker('setDate', '+1m');
                        //}
                    }
                });

                $("#allday").change(function () {
                    if ($(this).prop('checked')) {
                        $("#spnStarttime").hide();
                        $("#spnEndtime").hide();
                    } else {
                        $("#spnStarttime").show();
                        $("#spnEndtime").show();
                    }
                });
                $("#spnStarttime").hide();  //預設隱藏[開始時間]
                $("#spnEndtime").hide();    //預設隱藏[結束時間]

                //公告對象狀態設定
                $("#divSpecTarget").hide();
                var targetall = $("input[type='checkbox'][name='targetall']");
                targetall.change(function () {
                    var targetall_value = $("input[type='checkbox'][name='targetall']:checked").val();
                    if (targetall_value == "Y") {
                        $("#divSpecTarget").hide();
                    }
                    else {
                        $("#divSpecTarget").show();
                    }
                });
            },
            initDateSelector: function () {
                $(".mydate").val("");
                $(".mydate").datepicker({
                    autoclose: true,
                    todayHighlight: true,
                    format: 'yyyy/mm/dd',
                    todayBtn: "linked",  //linked:會將直接帶入"今天"的日期
                    language: 'zh'
                })
                //show datepicker when clicking on the icon
	            .next().on(ace.click_event, function () {
	                $(this).prev().focus();
	            });
                $("#publishtime").timepicker({
                    minuteStep: 60,
                    showInputs: false,
                    defaultTime: false,
                    showSeconds: false,
                    showMeridian: false  //是否顯示AM/PM
                }).next().on(ace.click_event, function () {
                    $(this).prev().focus();
                });
                $('#publishtime').on('show.timepicker', function (e) {
                    if (e.time.value == "0:00") {
                        $(this).timepicker('setTime', '12:00'); //預設帶出的時間
                    }
                });
                $(".mytime").timepicker({
                    showInputs: false,
                    defaultTime: false,
                    showSeconds: false,
                    showMeridian: false  //是否顯示AM/PM
                }).next().on(ace.click_event, function () {
                    $(this).prev().focus();
                });
                $('.mytime').on('show.timepicker', function (e) {
                    if (e.time.value == "0:00") {
                        $(this).timepicker('setTime', '12:00'); //預設帶出的時間
                    }
                });
                //                $("#publishdate").change(function () {
                //                    if ($("#offshelfdate").val() == "") { //[下架日期]未填寫時，才會依上架日期加一個月做為預設的下架日期
                //                        var date = $(this).datepicker('getDate');
                //                        date.setMonth(date.getMonth() + 1)
                //                        $("#offshelfdate").datepicker('setDate', date);
                //                    }
                //                });
                $("#offshelfdate").change(function () {
                    customValidate();
                });
                $("#publishtime").change(function () {
                    customValidate();
                });
            },
            initTagSelector: function () {
                $('#depts').editable({
                    mode: 'popup',
                    emptytext: '(未選擇)',
                    onblur: 'submit',
                    source: $.parseJSON(Element.hidden_depts.val()),
                    tpl: '<select id="DeptsSelector" name="" class="multiselect" multiple="multiple"></select>',
                    display: function (value, sourceData) {
                        //display checklist as comma-separated values
                        var html = [],
                                checked = $.fn.editableutils.itemsByValue(value, sourceData);

                        if (checked.length) {
                            $.each(checked, function (i, v) { html.push($.fn.editableutils.escape(v.text)); });
                            $(this).html(html.join(', '));
                        } else {
                            $(this).empty();
                        }
                    }
                });

                $('#depts').on('hidden', function () {
                    _qryDepts = $('#depts').editable('getValue')['depts'];
                    if (typeof (_qryDepts) == "undefined") {//未選擇
                        _qryDepts = "";
                    }
                });

                $('#depts').on('shown', function () {
                    $('#DeptsSelector').multiselect({
                        includeSelectAllOption: true,
                        enableFiltering: true,
                        filterBehavior: 'text',
                        enableCaseInsensitiveFiltering: true,
                        buttonClass: 'btn btn-white btn-primary',
                        templates: {
                            button: '<button type="button" class="multiselect dropdown-toggle" data-toggle="dropdown"></button>',
                            ul: '<ul class="multiselect-container dropdown-menu"></ul>',
                            filter: '<li class="multiselect-item filter"><div class="input-group"><span class="input-group-addon"><i class="fa fa-search"></i></span><input class="form-control multiselect-search" type="text"></div></li>',
                            filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default btn-white btn-grey multiselect-clear-filter" type="button"><i class="fa fa-times-circle red2"></i></button></span>',
                            li: '<li><a href="javascript:void(0);"><label></label></a></li>',
                            divider: '<li class="multiselect-item divider"></li>',
                            liGroup: '<li class="multiselect-item group"><label class="multiselect-group"></label></li>'
                        }
                    });
                });

                //部門
                if (_qryDepts != '') {
                    $('#depts').editable('setValue', _qryDepts.split(','));
                }

                //人員
                $('#users').editable({
                    mode: 'popup',
                    emptytext: '(未選擇)',
                    onblur: 'submit',
                    source: $.parseJSON(Element.hidden_users.val()),
                    tpl: '<select id="UsersSelector" name="" class="multiselect" multiple="multiple"></select>',
                    display: function (value, sourceData) {
                        //display checklist as comma-separated values
                        var html = [],
                                checked = $.fn.editableutils.itemsByValue(value, sourceData);

                        if (checked.length) {
                            $.each(checked, function (i, v) { html.push($.fn.editableutils.escape(v.text)); });
                            $(this).html(html.join(', '));
                        } else {
                            $(this).empty();
                        }
                    }
                });

                $('#users').on('hidden', function () {
                    _qryUsers = $('#users').editable('getValue')['users'];
                    if (typeof (_qryUsers) == "undefined") {//未選擇
                        _qryUsers = "";
                    }
                });

                $('#users').on('shown', function () {
                    $('#UsersSelector').multiselect({
                        includeSelectAllOption: true,
                        enableFiltering: true,
                        filterBehavior: 'text',
                        enableCaseInsensitiveFiltering: true,
                        buttonClass: 'btn btn-white btn-primary',
                        templates: {
                            button: '<button type="button" class="multiselect dropdown-toggle" data-toggle="dropdown"></button>',
                            ul: '<ul class="multiselect-container dropdown-menu"></ul>',
                            filter: '<li class="multiselect-item filter"><div class="input-group"><span class="input-group-addon"><i class="fa fa-search"></i></span><input class="form-control multiselect-search" type="text"></div></li>',
                            filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default btn-white btn-grey multiselect-clear-filter" type="button"><i class="fa fa-times-circle red2"></i></button></span>',
                            li: '<li><a href="javascript:void(0);"><label></label></a></li>',
                            divider: '<li class="multiselect-item divider"></li>',
                            liGroup: '<li class="multiselect-item group"><label class="multiselect-group"></label></li>'
                        }
                    });
                });
                //人員
                if (_qryUsers != '') {
                    $('#users').editable('setValue', _qryUsers.split(','));
                }
            },
            clickSaveButton: function () {
                MasterCtrl.clearMsg();
                checkFlag = true;
                var tag = ($("ul#tab li.active a").attr("href"));
                var custValid = customValidate();
                if ($("#form1").valid()) {
                    if ($("input[name='msgkind']:checked").val() == "I") {
                        if (tag == "#tabMessage") {
                            $('.nav-tabs a[href="#tabSurvey"]').tab('show');
                        }
                        else {
                            $('.nav-tabs a[href="#tabMessage"]').tab('show');
                        }
                        if ($("#form1").valid()) {
                            $('.nav-tabs a[href="' + tag + '"]').tab('show');
                        }
                        else {
                            MasterCtrl.showErrMsg(GetWord("incorrect_data"));
                        }
                    }
                }
                else {
                    MasterCtrl.showErrMsg(GetWord("incorrect_data"));
                }

                if ($("#form1").valid()) {
                    if ($.trim(CKEDITOR.instances["taaEditContent"].getData()) == "") {
                        MasterCtrl.showErrMsg("內容未填寫");
                        return;
                    }
                    if (!custValid) {
                        MasterCtrl.showErrMsg(GetWord("incorrect_data"));
                        return;
                    }
                    //問卷內容的檢查
                    if ($("input[name='msgkind']:checked").val() == "I") {
                        var rows = $("#data-list").dataTable().fnGetNodes();
                        if (rows.length == 0) { MasterCtrl.showErrMsg("互動式訊息須輸入問題"); return; }
                        for (var i = 0; i < rows.length; i++) {
                            if ($.trim($(rows[i]).find("td:eq(0)").find("input").val()) == "")
                            { MasterCtrl.showErrMsg("編號 未輸入"); return; }
                            if ($.trim($(rows[i]).find("td:eq(1)").find("input").val()) == "")
                            { MasterCtrl.showErrMsg("問題 未輸入"); return; }
                            if ($(rows[i]).find("td:eq(2)").find("select").val() != "T" && $.trim($(rows[i]).find("td:eq(3)").find("input").val()) == "")
                            { MasterCtrl.showErrMsg("選項 未輸入"); return; }
                        }
                    }

                    MsgDataControler.saveMsgData();
                }
            },
            setAddRow: function () {
                $('#addrow').on('click', function () {
                    myDataTable.row.add([
                        0, 1, 2, 3, 4, 5
                    ]).draw(false);
                    //初始設定訊息類型"單選"不啟用"是否必填"
                    var rows = $("#data-list").dataTable().fnGetNodes();
                    $(rows[rows.length - 1]).find("td:eq(4)").find("select").attr("disabled", true);
                });
                $('#btnSurveyResult').on('click', function () {
                    $(".various").attr("href", "MsgSurveyResult.aspx?msg_id=" + Element.hidden_MsgID_Original.val());
                });
            },
            setMsgID: function (id) {
                this.MsgID = id;
                Element.hidden_MsgID_Original.val(id);
                Element.hidden_MsgID.val(id);
            },
            copy: function () {
                var url =  MDS.Utility.NUtility.HtmlEncode(window.location.href.replace("pushed=Y", "pushed=N")+ "&clone=Y") ;
                window.location.href =  MDS.Utility.NUtility.HtmlDecode(url);
            },
            init: function () {
                this.URLParameters = Common.getURLParameter();
                this.setMsgID(this.URLParameters.msg_id);
                this.initDateSelector();
                this.initTagSelector();
                this.setAddRow();
            }
        }

        var MsgDataControler = {
            init: function (id) {
                this.getMsgData(Ctrl.MsgID);
            },
            getMsgData: function (id) {
                $.blockUI();
                var url = ResolveUrl("~/MDSAPI/Msg/get_MsgInfo.ashx");
                var data = {
                    "msg_id": id
                };
                var success = function (d) {
                    _qryPushed = d.data["published"];
                    TableControler.genTable(d.data.survey);
                    MsgDataControler.setMsgData(d.data);
                    if (_qryPushed == "Y" && _qryCopyMsg != "Y") {
                        Ctrl.DisableContentField();
                    }
                    Ctrl.initStatusDataTable(); //隱藏or顯示[問卷結果檢視]的按鈕                    
                };
                var fail = function (d) {
                    MasterCtrl.showErrMsg(d.msg);
                };
                $.mds.ajaxload(url, data, success, fail);
            },
            setMsgData: function (data) {
                if (Ctrl.MsgID != "0") $("#msgid").val(data.msg_id);  //訊息編號
                $("input[name='msgkind'][value='" + data.msg_kind_id + "']").attr('checked', true);   //訊息類型
                if (data.msg_kind_id == "I") $("#liSurvey").show();
                $("input[name='priority'][value='" + data.priority + "']").attr('checked', true);  //重要性
                Element.msgtype.val(data.msg_type_id);  //訊息類別
                //部門人員                
                if (data.target_all == "Y") {
                    $("#targetall").attr("checked", true); //打勾                     
                }
                else {
                    $("#targetall").attr("checked", false);
                    _qryDepts = data.targets.depts;
                    $('#depts').editable('setValue', _qryDepts.split(','));
                    _qryUsers = data.targets.users;
                    $('#users').editable('setValue', _qryUsers.split(','));
                }
                $('#targetall').change();

                //發佈日期時間  
                if (_qryCopyMsg != "Y") {
                    if (data.publish_dttm != "" && data.publish_dttm != undefined) {
                        var pubdate = data.publish_dttm.split(" ")[0];
                        var pubtimes = data.publish_dttm.split(" ")[1].split(":");
                        var pubtime = pubtimes[0] + ":" + pubtimes[1];
                        $("#publishdate").datepicker('setDate', pubdate);  //發佈日期
                        $("#publishtime").timepicker('setTime', pubtime);  //發佈時間 
                    }
                    if (data.offshelf_date != "" && data.offshelf_date != undefined) {
                        $("#offshelfdate").datepicker('setDate', data.offshelf_date);  //下架日期
                    }
                }

                //推播通知
                $("#pushnotify").prop("checked", (data.push_notify == "Y"));

                //訊息主旨
                $("#subject").val(data.subject);

                //地點地址
                $("#title").val(data.place.title);  //地點名稱
                $("#form-field-job-location").val(data.place.address);  //地點地址
                $("#hidden_location_lat").val(data.place.lat);
                $("#hidden_location_lng").val(data.place.lng);
                updateLocationLabel();

                //整天
                $("#allday").prop("checked", (data.allday == "Y"));
                $("#allday").change();
                var dt = "";
                var tms = null;
                var tm = "";
                if (data.start_dttm != "") {
                    dt = data.start_dttm.split(" ")[0];
                    tms = data.start_dttm.split(" ")[1].split(":");
                    tm = tms[0] + ":" + tms[1];
                    $("#startdate").datepicker('setDate', dt);
                    $("#starttime").timepicker('setTime', tm);
                }
                if (data.end_dttm != "") {
                    dt = data.end_dttm.split(" ")[0];
                    tms = data.end_dttm.split(" ")[1].split(":");
                    tm = tms[0] + ":" + tms[1];
                    $("#enddate").datepicker('setDate', dt);
                    $("#endtime").timepicker('setTime', tm);
                }

                //聯絡人
                $("#name").val(data.contact.name);
                $("#phone").val(data.contact.phone);
                $("#email").val(data.contact.email);

                CKEDITOR.replace('taaEditContent');
                $("#taaEditContent").val(data.content);

                //紀錄編輯初始資料，有編輯權限使用者按"返回"按紐時比對用。
                //if (Ctrl.MsgID != "0" && Parameter.CanUpdate == "Y") 
                {
                    MsgOriginal = MsgDataControler.putDataToMsgObject();
                }
            },
            putDataToMsgObject: function () {
                var msg = Object.create(Msg);
                msg.msg_id = Element.hidden_MsgID.val();
                msg.msg_kind_id = $("input[name='msgkind']:checked").val();
                msg.msg_type_id = Element.msgtype.val();
                msg.priority = $("input[name='priority']:checked").val();
                msg.target_all = $("#targetall").prop('checked') ? "Y" : "N";
                var oTargets = {
                    depts: _qryDepts.toString(),
                    users: _qryUsers.toString()
                };
                msg.targets = oTargets;

                if ($("input[name='rdoPublishDttm']:checked").val() == "Y")
                    msg.publish_dttm = $("#publishdate").val() + " " + $("#publishtime").val() + ":00";
                else msg.publish_dttm = "";
                msg.offshelf_date = $("#offshelfdate").val();
                msg.push_notify = (!$("#pushnotify").prop('checked')) ? "N" : "Y";
                msg.subject = $("#subject").val();
                var oPlace = {
                    title: $("#title").val(),
                    address: $("#form-field-job-location").val(),
                    lat: $("#hidden_location_lat").val(),
                    lng: $("#hidden_location_lng").val()
                };
                msg.place = oPlace;
                msg.allday = $("#allday").prop('checked') ? "Y" : "N";
                if (msg.allday == "Y") {
                    msg.start_dttm = $("#startdate").val();
                    msg.end_dttm = $("#enddate").val()
                }
                else {
                    if ($("#startdate").val() != "") {
                        msg.start_dttm = $("#startdate").val() + " " + $("#starttime").val() + ":00";
                    }
                    if ($("#enddate").val() != "") {
                        msg.end_dttm = $("#enddate").val() + " " + $("#endtime").val() + ":00";
                    }
                }
                var oContact = {
                    name: $("#name").val(),
                    phone: $("#phone").val(),
                    email: $("#email").val()
                };
                msg.contact = oContact;
                msg.content = $('<div/>').text(CKEDITOR.instances["taaEditContent"].getData()).html();
                if ($("input[name='msgkind']:checked").val() == "I") {
                    var aSurvey = [];
                    var oSurvey = {
                        s_id: "",
                        question: "",
                        type: "",
                        options: "",
                        required: ""
                    };
                    var rows = $("#data-list").dataTable().fnGetNodes();
                    for (var i = 0; i < rows.length; i++) {
                        oSurvey["s_id"] = $.trim($(rows[i]).find("td:eq(0)").find("input").val());
                        oSurvey["question"] = $.trim($(rows[i]).find("td:eq(1)").find("input").val());
                        oSurvey["type"] = $(rows[i]).find("td:eq(2)").find("select").val();
                        oSurvey["options"] = $.trim($(rows[i]).find("td:eq(3)").find("input").val());
                        oSurvey["required"] = $(rows[i]).find("td:eq(4)").find("select").val();
                        aSurvey.push(oSurvey);
                        oSurvey = new Object();
                    }
                    msg.survey = aSurvey;
                }
                return msg;
            },
            saveMsgData: function () {
                $.blockUI();
                var msg = MsgDataControler.putDataToMsgObject();
                var SaveData = JSON.stringify(msg);

                var fail = function (d) {
                    MasterCtrl.showErrMsg(d.msg);
                };

                var success = function (d) {
                    MasterCtrl.showSucessMsg(d.msg);
                    window.location.replace("MsgList.aspx?msgtype=" + _qryMsgType + "&msgstatus=" + _qryMsgStatus + "&MsgTag=" + _qryMsgTag + "&startdate=" + _qryStartDate + "&enddate=" + _qryEndDate);
                }

                var data = {
                    'data': SaveData
                };

                var url = ResolveUrl("~/MDSAPI/Msg/upd_MsgInfo.ashx");
                $.mds.ajaxload(url, data, success, fail);
            }
        }        

        var TableControler = {
            DataList: $('#data-list'),
            genTable: function (data) {
               myDataTable = this.DataList.DataTable({
                    "ordering": true,
                    "info":false,
                    "dom": 'Rlfrtip',
                    "bDestroy": true,
                    "bAutoWidth": false,
                    "stateSave": false,
                    "data": data,
                    "order": [[0, 'asc']],
                    "bFilter": false,
                    "bPaginate":false,
                    columns: [
                        //編號
                        { data: 's_id', 'orderable': false },
                        //題目
                        { data: 'question', 'orderable': false },
                        //類型
                        { data: 'type', 'orderable': false },
                        //選項值
                        { data: 'options', 'orderable': false },
                        //是否必填
                        { data: 'required', 'orderable': false },
                    ],
                    "columnDefs": [
                        {
                            "targets": [0, 1, 2, 3, 4, 5],
                            "class": "center"
                            //"class": "hidden-480 center"
                        },
                        {
                            "targets": [0], //編號
                            "sClass": "center",
                            "render": function (data, type, row) {
                                var isDisabled = "";
                                if ((Ctrl.MsgID != "0" && Parameter.CanUpdate != "Y") || (_qryPushed == "Y" && _qryCopyMsg != "Y"))
                                    isDisabled = "disabled";
                                var s_id = "";
                                if (row.s_id == null) s_id = "<input type='text' name='s_id'  value='' style='width:100%;' maxlength='3' required " + isDisabled + "/>";
                                else return s_id = "<input type='text' value='" + row.s_id + "' style='width:100%' maxlength='3' required " + isDisabled + "/>";
                                s_id+='<div id="s_id-validator" class="validator-error"></div>';
                                return s_id;
                            }
                        },
                        {
                            "targets": [1], //題目
                            "sClass": "center",
                            "render": function (data, type, row) {
                                var isDisabled = "";
                                if ((Ctrl.MsgID != "0" && Parameter.CanUpdate != "Y") || (_qryPushed == "Y" && _qryCopyMsg != "Y"))
                                    isDisabled = "disabled";
                                if (row.question == null) return "<input type='text' name='question' value='' style='width:100%' maxlength='50' required " + isDisabled + "/>";
                                else return "<input type='text' value='" + row.question + "'  style='width:100%' maxlength='50' required " + isDisabled + "/>";
                            }
                        },
                        {
                            "targets": [2], //類型
                            "sClass": "center",
                            "render": function (data, type, row) {
                                var isDisabled = "";
                                if ((Ctrl.MsgID != "0" && Parameter.CanUpdate != "Y") || (_qryPushed == "Y" && _qryCopyMsg != "Y"))
                                    isDisabled = "disabled";
                                var typeList;
                                switch (row.type) {
                                    case 'S':
                                        typeList = "<select id='type' style='width:100%' onChange='setOptionsStatus(this)' " + isDisabled + "><option value='S' selected>單選</option><option value='M'>複選</option><option value='T'>文字框</option></select>";
                                        break;
                                    case 'M':
                                        typeList = "<select id='type' style='width:100%' onChange='setOptionsStatus(this)' " + isDisabled + "><option value='S'>單選</option><option value='M' selected>複選</option><option value='T'>文字框</option></select>";
                                        break;
                                    case 'T':
                                        typeList = "<select id='type' style='width:100%' onChange='setOptionsStatus(this)' " + isDisabled + "><option value='S'>單選</option><option value='M'>複選</option><option value='T' selected>文字框</option></select>";
                                        break;
                                    default:
                                        typeList = "<select id='type' style='width:100%' onChange='setOptionsStatus(this)' " + isDisabled + "><option value='S' selected>單選</option><option value='M'>複選</option><option value='T'>文字框</option></select>";
                                }
                                return typeList;
                            }
                        },
                        {
                            "targets": [3], //選項值
                            "sClass": "center",
                            "render": function (data, type, row) {
                                var isDisabled = "";
                                if ((Ctrl.MsgID != "0" && Parameter.CanUpdate != "Y") || (_qryPushed == "Y" && _qryCopyMsg != "Y"))
                                    isDisabled = "disabled";
                                if (row.type == "T") {
                                    isDisabled="disabled";
                                    return "<input type='text' id='options' name='options' value='' style='width:100%' maxlength='255' " + isDisabled + "/>";
                                }
                                else if (row.options == null) {
                                    return "<input type='text' id='options' name='options' value='' style='width:100%' maxlength='255' required " + isDisabled + "/>";
                                }
                                else {
                                    return "<input type='text' id='options' name='options' value='" + row.options + "' style='width:100%' maxlength='255' required " + isDisabled + "/>";
                                }
                            }
                        },
                        {
                            "targets": [4],         //是否必填
                            "sClass": "center",
                            "render": function (data, type, row) {
                                var isDisabled = "";
                                if ((Ctrl.MsgID != "0" && Parameter.CanUpdate != "Y") || (_qryPushed == "Y" && _qryCopyMsg != "Y"))
                                    isDisabled = "disabled";
                                if (row.type != "T") isDisabled = "disabled";
                                var requiredList;
                                switch (row.required) {
                                    case 'Y':
                                        requiredList = "<select id='required' style='width:100%' " + isDisabled + "><option value='Y' selected>是</option><option value='N'>否</option></select>";
                                        break;
                                    case 'N':
                                        requiredList = "<select id='required' style='width:100%' " + isDisabled + "><option value='Y'>是</option><option value='N' selected>否</option></select>";
                                        break;
                                    default:
                                        requiredList = "<select id='required' style='width:100%' " + isDisabled + "><option value='Y' selected>是</option><option value='N'>否</option></select>";
                                }
                                return requiredList;
                            }
                        },
                            {
                                "targets": [5], //移除
                                "sClass": "center",
                                "render": function (data, type, row) {
                                    if ((Ctrl.MsgID != "0" && Parameter.CanUpdate != "Y") || (_qryPushed == "Y" && _qryCopyMsg != "Y")) {  //判斷是否有刪除權限                                            
                                        return '<i class="ace-icon fa fa-trash-o bigger-130"></i>';    
                                    }
                                    else {                                        
                                        return '<a class="table-delete" data-id="' + row.s_id + '" onclick="delDataTableRow(this)"><i class="ace-icon fa fa-trash-o bigger-130"></i></a>';
                                    }
                                }
                            }
                    ]
                });//--datatable

                        myDataTable.column(5).visible(((Ctrl.MsgID != "0" && Parameter.CanUpdate != "Y") || (_qryPushed == "Y" && _qryCopyMsg != "Y")) == false);
            }

        }

        function delDataTableRow(element) {
            var tr = $(element).closest("tr");
            myDataTable.row(tr).remove().draw(false);
        }

        function setOptionsStatus(element) {
            var value = $(element).val();
            var tr = $(element).closest("tr");
            var options = $(tr).find("#options");
            var required = $(tr).find("#required");
            if (value == "T") {
                $(options).prop('required', false);
                $(options).prop('disabled', true);
                $(options).val("");
                $(required).prop('disabled', false);
            }
            else {
                $(options).prop('required', true);
                $(options).prop('disabled', false);
                $(required).prop('disabled', true);
                $(required)[0].selectedIndex = 0;
            }
        }

        function clearDateTime() {
            $("#startdate").val("");
            $("#starttime").val("");
            $("#enddate").val("");
            $("#endtime").val("");
        }

        function BackToList() {
            var isChanged = false;
            if ((Ctrl.MsgID != "0" && Parameter.CanUpdate == "Y" && _qryPushed != "Y")
                || (Ctrl.MsgID == "0" && Parameter.CanInsert == "Y")) {
                isChanged = hasDataChange();
                if (!isChanged) {
                    $.confirm({ content: '修改資料尚未儲存，是否確定返回？',
                        confirm: function () {
                            window.location.replace("MsgList.aspx?msgtype=" + _qryMsgType + "&msgstatus=" + _qryMsgStatus + "&MsgTag=" + _qryMsgTag + "&startdate=" + _qryStartDate + "&enddate=" + _qryEndDate);
                        }
                    });
                    return false;
                }
            }
                window.location.replace("MsgList.aspx?msgtype=" + _qryMsgType + "&msgstatus=" + _qryMsgStatus + "&MsgTag=" + _qryMsgTag + "&startdate=" + _qryStartDate + "&enddate=" + _qryEndDate);
        }

        function hasDataChange() { 
            if(CKEDITOR.instances["taaEditContent"].checkDirty()){
                return false;
            }           
            var msg = MsgDataControler.putDataToMsgObject();
            delete msg.content;
            var jsonMsg = JSON.stringify(msg);
            delete MsgOriginal.content;
            var jsonMsgOriginal = JSON.stringify(MsgOriginal);            
            return jsonMsg == jsonMsgOriginal;
        }
