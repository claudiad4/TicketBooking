$(document).ready(function () {
    $("#seatTable").dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/MiejscaDetails/GetSeatDetails",
            "type": "Post",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],

        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "numerMiejsca", "name": "NumerMiejsca", "autoWidth": true },
            { "data": "nazwaKoncertu", "name": "NazwaKoncertu", "autoWidth": true },
            { "data": "statusMiejsca", "name": "statusMiejsca", "autoWidth": true }
        ]

    });
})