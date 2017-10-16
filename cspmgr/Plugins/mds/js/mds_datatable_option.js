$.extend(true, $.fn.dataTable.defaults, {
    //"searching": false,
    //"ordering": false,
    "language": {
        "sEmptyTable": "查無資料",
        "sZeroRecords": "無過濾資料",
        "sInfoFiltered": "(過濾的資料來源總數：_MAX_)",
        "paginate": {
            "previous": "上一頁",
            "next": "下一頁"
        },
        "lengthMenu": '每頁顯示 <select>' +
                            '<option value="5">5</option>' +
                            '<option value="10">10</option>' +
                            '<option value="20">20</option>' +
                            '<option value="30">30</option>' +
                            '<option value="40">40</option>' +
                            '<option value="50">50</option>' +
                            '<option value="-1">All</option>' +
                            '</select> 筆　',
        "info": "目前顯示 _START_ 至 _END_ 筆(共 _TOTAL_ 筆)",
        "sInfoEmpty": "目前顯示 0 至 0 筆(共 0 筆)", //Showing 0 to 0 of 0 entries
        "search": '' //'<i class="ace-icon fa fa-search nav-search-icon gray"></i>'
    }
});
