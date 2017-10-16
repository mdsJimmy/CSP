var Common = {
    clearForm: function (obj) {
        var $form = obj;

        $form = $form.find('[disabled]').removeAttr('disabled').end();
        $form = $form.find(':input[type=text]').val('').end();
        $form = $form.find(':input[type=checkbox], :input[type=radio]').attr('checked', false).end();
        $form = $form.find('select option:selected').removeAttr('selected').end();
        $form = $form.find('option:first').attr('selected', 'selected').end();
        return $form;
    },
    getURLParameter: function () {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        var Parameter = {};
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterArray = sURLVariables[i].split('=');
            Parameter[sParameterArray[0]] = sParameterArray[1];
        }
        return Parameter;
    },
    ObjectArrayToJSON: function (objAry) {
        var object = {};
        for (var i = 0; i < objAry.length; i++) {
            var key = objAry[i].name.split('$').pop();
            object[key] = objAry[i].value;
        }
        return object;
    },
    StringToJSON: function (str) {
        var AllSet = decodeURIComponent(str).split('&');
        
        var obj = {};
        for (var i = 0; i < AllSet.length; i++) {
            var DataArray = AllSet[i].split('=');
            var key = DataArray.shift().split('$').pop();
            var value = DataArray.pop();
            obj[key] = value;
        }
        return obj;
    }
};
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray({ checkboxesAsBools: true });
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });

    return o;
};

jQuery.fn.serializeArray = function (options) {
    var o = $.extend({
        checkboxesAsBools: false
    }, options || {});

    var rselectTextarea = /select|textarea/i;
    var rinput = /text|hidden|password|search/i;

    return this.map(function () {
        return this.elements ? $.makeArray(this.elements) : this;
    })
    .filter(function () {
        return this.name && !this.disabled &&
            (this.checked
            || (o.checkboxesAsBools && this.type === 'checkbox')
            || rselectTextarea.test(this.nodeName)
            || rinput.test(this.type));
    })
        .map(function (i, elem) {
            
            var val = $(this).val();
            return val == null ?
            null :
            $.isArray(val) ?
            $.map(val, function (val, i) {
                var n = elem.name.split('$').pop();
                return { name: n, value: val };
            }) :
            {
                name: elem.name.split('$').pop(),
                value: (o.checkboxesAsBools && this.type === 'checkbox') ? //moar ternaries!
                    (this.checked ? 'true' : 'false') :
                    val
            };
        }).get();
};