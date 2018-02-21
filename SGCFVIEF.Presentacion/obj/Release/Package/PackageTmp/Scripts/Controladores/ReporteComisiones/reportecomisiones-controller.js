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

    //$("#IdNuevo").prop("disabled", true);
    //$("#IdExportar").prop("disabled", true);

    $("#IdBuscar").on("click", function () {
        var parametros = {
            "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
            "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
            "Canal": { "Cod_Canal": $("#Cod_Canal").val() },
            "Producto": { "Cod_Producto": $("#Cod_Producto").val() },
            "Vendedor": { "Cod_Vendedor": $("#Cod_Vendedor").val() },
            "TipoComision": $("#TipoComision").val(),
            "Cod_Reporte_Comision": $("#CodigoReporte").val(),
        };

        Buscar(parametros);
        return false;
    });

    $("#btn-cancelar").on("click", function () {
        $("#FechaInicio").val("");
        $("#FechaFin").val("");
        $("#CodigoReporte").val("");
        $('#TipoComision option:eq(0)').prop('selected', true)
        $('#Cod_Producto option:eq(0)').prop('selected', true)
        $('#Cod_Vendedor option:eq(0)').prop('selected', true)
        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $("#TipoComision").trigger("chosen:updated");
        $("#Cod_Producto").trigger("chosen:updated");
        $("#Cod_Vendedor").trigger("chosen:updated");
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Resultados").empty();
    });

    $("#IdNuevo").on("click", function () {
        var parametros = {};
        Grabar(parametros);
        return false;
    });

    $("#Cod_Canal").on("change", function () {
        var parametros = {
            "Codigo": $(this).val(),
        }

        FiltrarVendedores(parametros);
    });

    $("#IdExportar").on("click", function () {
        ExportarExcel();
    });
});


function Buscar(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.Buscar;
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
            PopInformativo("No se encontraron reportes generados.");
        }
    });
}

function Grabar(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.Grabar;
    info.parametros = parametros;

    ajax(info, function (data) {
        $("#MensajeGrabar").modal('hide');
        if (data.Estado) {
            $("#ResultadoGrabacion #Id").empty();
            $("#ResultadoGrabacion #Id").html(data.Valor);
            $("#ResultadoGrabacion").modal('show');
        } else {
            $("#ResultadoGrabacionErr #Mensaje").empty();
            $("#ResultadoGrabacionErr #Mensaje").append(data.Message);
            $("#ResultadoGrabacionErr").modal('show');
        }
    });
}

function FiltrarVendedores(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.FiltraVendedorxCanal;
    info.parametros = parametros;
    ajax(info, function (data) {
        CargarVendedores(data, "#Cod_Vendedor");
    });
}

function CargarVendedores(data, objeto) {
    $(objeto + ' option').remove();
    $(objeto).append("<option value=''>Todos</option>");
    if (data != null) {
        $.each(data, function (i, item) {
            $(objeto).append("<option value='"
               + item.Cod_Vendedor + "'>" + item.NombreCompleto
               + "</option>");
        });
    }

    $(objeto).trigger("chosen:updated");
}

function ExportarExcel(tipo) {
    window.open('/ReporteComisiones/GenerarExcel/');
}