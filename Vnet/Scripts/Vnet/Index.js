$(document).ready(function () {
    function RenovarIdForAction() {
        idForAction = null;
        tablenormal.$('tr.selected').removeClass('selected');
        tablecorporativo.$('tr.selected').removeClass('selected');
    }
    $("#example-tabs-t-0").on("click", function () {
        RenovarIdForAction();
    });
    $("#example-tabs-t-1").on("click", function () {
        RenovarIdForAction();
    });
    $('#tablenormal tbody').onclick
    var tablenormal = $('#tablenormal').DataTable({
        "ajax": {
            "url": "/Suscriptores/ListaSuscriptores?tiposuscriptor=" + 1,
            "dataSrc": ""
        },
        "columns": [
            { "data": "Id" },
            { "data": "NumeroSuscriptor" },
            { "data": "Nombre" },
            { "data": "Dni" },
            { "data": "Celular" }
        ],
        "order": [[1, "asc"]],
        "aoColumnDefs": [
            {
                "bSearchable": false,
                "bVisible": false,
                "aTargets": [0]
            }
        ],
        "language": {
            "url": '/Scripts/DataTables/spanish.json'
        }
    });
    $('#tablenormal tbody').on('click',
        'tr',
        function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
                idForAction = null;
            } else {
                tablenormal.$('tr.selected').removeClass('selected');

                $(this).addClass('selected');
                idForAction = tablenormal.row($(this)).data()["Id"];

            }
            console.log("Id capturado", idForAction);
        });

    var tablecorporativo = $('#tablecorporativo').DataTable({
        "ajax": {
            "url": "/Suscriptores/ListaSuscriptores?tiposuscriptor=" + 2,
            "dataSrc": ""
        },
        "columns": [
            { "data": "Id" },
            { "data": "NumeroSuscriptor" },
            { "data": "Nombre" },
            { "data": "Dni" },
            { "data": "Celular" }
        ],
        "order": [[1, "asc"]],
        "aoColumnDefs": [
            {
                "bSearchable": false,
                "bVisible": false,
                "aTargets": [0]
            }
        ],
        "language": {
            "url": '/Scripts/DataTables/spanish.json'
        }
    });
    $('#tablecorporativo tbody').on('click',
        'tr',
        function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
                idForAction = null;
            } else {
                tablecorporativo.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
                idForAction = tablecorporativo.row($(this)).data()["Id"];

            }
            console.log("Id capturado ", idForAction);
        });
});


function verRegistro() {
    if (verificarSeleccion()) {
        window.location.href = '/Suscriptores/Details/' + idForAction;
    }
}
function agregarRegistro() {
    if (verificarSeleccion()) {
        window.location.href = '/Suscriptores/Create';
    }
}
function eliminarRegistro() {
    if (verificarSeleccion()) {
        window.location.href = '/Suscriptores/Delete/' + idForAction;
    }
}
