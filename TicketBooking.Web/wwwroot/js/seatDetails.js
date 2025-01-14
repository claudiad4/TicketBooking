$(document).ready(function () {
    $("#seatTable").dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/MiejscaDetails/GetSeatDetails", // Dopasowanie do kontrolera i metody
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0], // Ukrycie kolumny Id
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true }, // Identyfikator miejsca
            { "data": "numerMiejsca", "name": "NumerMiejsca", "autoWidth": true }, // Numer miejsca
            { "data": "nazwaKoncertu", "name": "NazwaKoncertu", "autoWidth": true }, // Nazwa koncertu
            { "data": "statusMiejsca", "name": "StatusMiejsca", "autoWidth": true } // Status miejsca
        ]
    });
});
