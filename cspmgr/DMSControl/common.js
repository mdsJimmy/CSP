$(document).ready(function() {
    /*固定ToolBar相關.fixed-header DOM START*/
    if($(".fixed-header").length != 0) {
        var _fixedheader = $('<div id="fixed-header"></div>').appendTo('body');
        _fixedheader.append($(".fixed-header"));
        $('body').prepend($('<table style="height:' + _fixedheader.height() + 'px;"><tr><td>&nbsp;</td></tr></table>'));
    }
    /*固定ToolBar相關.fixed-header DOM END*/
});
