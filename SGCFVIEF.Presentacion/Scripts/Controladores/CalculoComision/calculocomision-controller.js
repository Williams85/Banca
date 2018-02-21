$(document).ready(function () {

    var config = {
        '.chosen-select': {},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }


    $("#IdGrabar").attr("disabled", true);
    $("#IdNuevo").attr("disabled", true);
    $("#IdExportar").attr("disabled", true);

    if ($("#Solicitudes").prop("checked") == true) {
        $("#SolRechazadas").prop("checked", false);
        $("#SolRechazadas").attr("disabled", false);
    }


    $("#SolRechazadas").on("click", function () {
        if ($(this).prop("checked") == true) {
            $("#IdGrabar").attr("disabled", true);
            $("#IdNuevo").attr("disabled", true);
        }
    });

    $("#Solicitudes").on("click", function () {
        ClearForm();
        if ($("#Solicitudes").prop("checked") == true) {
            $("#SolRechazadas").prop("checked", false);
            $("#SolRechazadas").attr("disabled", false);
            $("#IdExportar").attr("disabled", true);
            $("#IdGrabar").attr("disabled", true);
            $("#IdNuevo").attr("disabled", true);
        } else {
            $("#SolRechazadas").prop("checked", true);
            $("#SolRechazadas").attr("disabled", true);
            $("#IdExportar").attr("disabled", false);
        }
    });

    $("#Pagos").on("click", function () {
        ClearForm();
        if ($("#Pagos").prop("checked") == true) {
            $("#SolRechazadas").prop("checked", false);
            $("#SolRechazadas").attr("disabled", true);
            $("#TipoComision").attr("disabled", false);
            $("#Otros").attr("disabled", false);
            $("#Cod_Canal").attr("disabled", false);
            $("#IdExportar").attr("disabled", true);
            $("#IdGrabar").attr("disabled", true);
            $("#IdNuevo").attr("disabled", true);
        } else {
            $("#SolRechazadas").prop("checked", false);
            $("#SolRechazadas").attr("disabled", false);
            $("#TipoComision").attr("disabled", true);
            $("#Otros").attr("disabled", true);
            $("#Cod_Canal").attr("disabled", true);
            $("#IdExportar").attr("disabled", true);
        }
    });

    $("#IdBuscar").on("click", function () {
        if ($("#Solicitudes").prop("checked") == true) {
            var parametros = {
                "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
                "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
                "Canal": { "Cod_Canal": $("#Cod_Canal").val() },
                "Producto": { "Cod_Producto": $("#Cod_Producto").val() },
                "TipoComision": $("#TipoComision").val(),
            };

            $("#Fecha_Inicio").val(parametros.FechaInicio);
            $("#Fecha_Fin").val(parametros.FechaFin);
            $("#Tipo_Comision").val(parametros.TipoComision);
            $("#Canal").val(parametros.Canal.Cod_Canal);
            $("#Producto").val(parametros.Producto.Cod_Producto);


            if ($("#SolRechazadas").prop("checked") == true) {
                BuscarSolicitudesRechazdos(parametros);
                ClearForm();

            } else {
                BuscarSolicitudesActivos(parametros);
                ClearForm();
            }
            return false;
        } else if ($("#Pagos").prop("checked") == true) {
            var parametros = {
                "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
                "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
                "Canal": { "Cod_Canal": $("#Cod_Canal").val() },
                "Producto": { "Cod_Producto": $("#Cod_Producto").val() },
                "TipoComision": $("#TipoComision").val(),
                "Producto": { "Cod_Producto": "" },
            };
            ClearForm();
            BuscarPagoComsiones(parametros);
        }
    });

    $("#btn-cancelar").on("click",function () {
        ClearForm();
        $("#Resultados").empty();
    });

    $("#IdNuevo").on("click", function () {
        var parametros = {
            "FechaInicio": ($("#Fecha_Inicio").val() == '' ? '01/01/2009' : $("#Fecha_Inicio").val()),
            "FechaFin": ($("#Fecha_Fin").val() == '' ? '01/01/2029' : $("#Fecha_Fin").val()),
            "Canal": { "Cod_Canal": $("#Canal").val() },
            "Producto": { "Cod_Producto": $("#Producto").val() },
            "TipoComision": $("#Tipo_Comision").val(),
        };
        CalcularComisiones(parametros);
        return false;
    });

    $("#IdGrabar").on("click", function () {
        GrabarCalculoComisiones();
        return false;
    });

    $("#IdExportar").on("click", function () {
        ExportarExcel();
    });

    var myDate = null;
    myDate = $.datepicker.parseDate("dd/mm/yy", "01/01/2016");
    var dateFormat = "dd/mm/yy";
       from = $("#FechaInicio")
         .datepicker({
             defaultDate: new Date(),
             changeMonth: true,
             changeYear: true,
             yearRange: "1900:2023",
             numberOfMonths: 1
         })
         .on("change", function () {
             to.datepicker("option", "minDate", getDate(this));
         }),
       to = $("#FechaFin").datepicker({
           defaultDate: new Date(),
           changeMonth: true,
           changeYear: true,
           yearRange: "1900:2023",
           numberOfMonths: 1
       })

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }
        return date;
    }

});



function BuscarSolicitudesActivos(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarSolicitudesActivos;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        $("#Resultados").empty();
        if ($.trim(data) != null && $.trim(data) != "") {
            $("#Resultados").html(data);
            $("#TablaResultados").dataTable({
                "pageLength": 5,
                "ordering": false,
                "info": false,
                "searching": false
            });
            $("#IdNuevo").attr("disabled", false);
            $("#IdExportar").attr("disabled", false);
        } else {
            PopInformativo("No se encontraron solicitudes aprobadas.");
        }
    });
}

function BuscarSolicitudesRechazdos(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarSolicitudesRechazdos;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        $("#Resultados").empty();
        if ($.trim(data) != null && $.trim(data) != "") {
            $("#Resultados").html(data);
            $("#TablaResultados").dataTable({
                "pageLength": 5,
                "ordering": false,
                "info": false,
                "searching": false
            });
            $("#IdExportar").attr("disabled", false);
            $(".btn-activar").on("click", function () {
                var Tipo = $(this).attr("data-tipo");
                var Estado = "";
                if (Tipo == 1)
                    Estado = 'D'
                else
                    Estado = 'A'

                var parametros = {
                    "N_Solicitud": $(this).attr("data-nsolicitud"),
                    "Estado": Estado,
                };
                ActivarSolicitud(parametros);
                return false;
            });
        } else {
            PopInformativo("No se encontraron solicitudes rechazadas.");
        }
    });
}

function BuscarPagoComsiones(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarPagoComsiones;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        $("#Resultados").empty();
        if ($.trim(data) != null && $.trim(data) != "") {
            $("#Resultados").html(data);
            $("#TablaResultados").dataTable({
                "pageLength": 5,
                "ordering": false,
                "info": false,
                "searching": false
            });
            $("#IdNuevo").attr("disabled", false);
            $("#IdExportar").attr("disabled", false);
        } else {
            $("#IdExportar").attr("disabled", true);
            PopInformativo("No se encontraron los pagos de comisiones.");
        }
    });
}

function CalcularComisiones(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.CalcularComisiones;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        $("#Resultados").empty();
        if ($.trim(data) != null && $.trim(data) != "") {
            $("#Resultados").html(data);
            $("#TablaResultados").dataTable({
                "pageLength": 5,
                "ordering": false,
                "info": false,
                "searching": false
            });
            $("#IdGrabar").attr("disabled", false);
        } else {
            PopInformativo("No se calcularon las comisiones...");
        }
    });
}

function ActivarSolicitud(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ActivarSolicitud;
    info.parametros = parametros;

    ajax(info, function (data) {
        PopInformativo(data.Message)
        var parametros = {
            "FechaInicio": ($("#Fecha_Inicio").val() == '' ? '01/01/2009' : $("#Fecha_Inicio").val()),
            "FechaFin": ($("#Fecha_Fin").val() == '' ? '01/01/2029' : $("#Fecha_Fin").val()),
            "Canal": { "Cod_Canal": $("#Canal").val() },
            "Producto": { "Cod_Producto": $("#Producto").val() },
            "TipoComision": $("#Tipo_Comision").val(),
        };
        BuscarSolicitudesRechazdos(parametros);
    });
}

function GrabarCalculoComisiones(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GrabarCalculoComisiones;
    info.parametros = {};

    ajax(info, function (data) {
        if (data.Estado) {
            $("#Resultados").empty();
            $("#ResultadoGrabacion #Id").empty();
            $("#ResultadoGrabacion #Id").html(data.Valor);
            $("#ResultadoGrabacion").modal('show');
            $("#IdGrabar").attr("disabled", true);
            $("#IdNuevo").attr("disabled", true);
            ClearForm();
        } else {
            PopInformativo(data.Message);
        }

    });
}

function ClearForm() {
    $("#FechaInicio").val("");
    $("#FechaFin").val("");
    $("#FechaInicio").attr('value', "");
    $("#FechaFin").attr('value', "");
    $("#Cod_Canal option:eq(0)").prop('selected', true);
    $("#TipoComision option:eq(0)").prop('selected', true);
    $("#Otros option:eq(0)").prop('selected', true);
    $("#Cod_Canal").trigger("chosen:updated");
    $("#TipoComision").trigger("chosen:updated");
    $("#Otros").trigger("chosen:updated");

}

function ExportarExcel(tipo) {
    window.open('/CalculoComision/GenerarExcel/');
}