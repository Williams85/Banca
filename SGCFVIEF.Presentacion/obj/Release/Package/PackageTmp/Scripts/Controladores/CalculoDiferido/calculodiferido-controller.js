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

    $("#IdGrabar").attr("disabled", true);

    $("#IdExportar").attr("disabled", true);

    $("#IdBuscar").on("click", function () {
        var parametros = {
            "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
            "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
            "Canal": { "Cod_Canal": $("#Cod_Canal").val() },
            "TipoComision": $("#TipoComision").val(),
            "Producto": { "Cod_Producto": $("#Cod_Producto").val() },
        };
        ClearForm();
        if ($("#Pagos").prop("checked") == true) {
            BuscarPagoComsiones(parametros);
            return false;
        } else if ($("#Diferidos").prop("checked") == true) {
            BuscarPagoDiferidos(parametros);
            return false;
        }

    });

    $("#IdGrabar").on("click", function () {
        GrabarDiferimientos();
        return false;
    });

    $("#Diferidos").on("click", function () {
        if ($(this).prop("disabled") == false) {
            $("#IdGrabar").prop("disabled", true);
            $("#IdExportar").prop("disabled", true);
            $("#IdBuscar").prop("disabled", false);
        } else {
            $("#IdGrabar").prop("disabled", false);
            $("#IdExportar").prop("disabled", true);
            $("#IdBuscar").prop("disabled", false);
        }

    });

    $("#Pagos").on("click", function () {
        if ($(this).prop("disabled") == false) {
            $("#IdGrabar").prop("disabled", true);
            $("#IdExportar").prop("disabled", true);
            $("#IdBuscar").prop("disabled", false);
        } else {
            $("#IdGrabar").prop("disabled", true);
            $("#IdExportar").prop("disabled", true);
            $("#IdBuscar").prop("disabled", false);
        }

    });


    $("#btn-cancelar").on("click", function () {
        $("#FechaInicio").val("");
        $("#FechaFin").val("");
        $('#TipoComision option:eq(0)').prop('selected', true)
        $('#Otros option:eq(0)').prop('selected', true)
        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $('#Cod_Producto option:eq(0)').prop('selected', true)
        $("#TipoComision").trigger("chosen:updated");
        $("#Otros").trigger("chosen:updated");
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Cod_Producto").trigger("chosen:updated");
        $("#Resultados").empty();
        $("#Pagos").prop("checked", true);
        $("#Diferidos").prop("checked", false);

    });


    $("#IdExportar").on("click", function () {
        ExportarExcel();
    });

    var myDate = null;
    myDate = $.datepicker.parseDate("dd/mm/yy", "01/01/2016");
    var dateFormat = "dd/mm/yy",
   from = $("#FechaInicio")
     .datepicker({
         defaultDate: new Date(),
         changeMonth: true,
         numberOfMonths: 1
     })
     .on("change", function () {
         to.datepicker("option", "minDate", getDate(this));
     }),
   to = $("#FechaFin").datepicker({
       defaultDate: new Date(),
       changeMonth: true,
       numberOfMonths: 1
   })
   //.on("change", function () {
   //    from.datepicker("option", "maxDate", getDate(this));
   //});

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
            $("#IdExportar").prop("disabled", false);
            $("#IdGrabar").attr("disabled", false);
        } else {
            PopInformativo("No se encontraron los filtros de búsqueda.");
        }
    });
}

function BuscarPagoDiferidos(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarPagosDiferidos;
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
            $("#IdExportar").prop("disabled", false);
        } else {
            PopInformativo("No se encontraron cálculos diferidos");
        }
    });
}

function GrabarDiferimientos() {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GrabarDiferimientos;
    info.parametros = {};

    ajax(info, function (data) {
        if (data.Estado) {
            $("#Resultados").empty();
            $("#ResultadoGrabacion #Id").empty();
            $("#ResultadoGrabacion #Id").html(data.Valor);
            $("#ResultadoGrabacion").modal('show');
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
    $("#Cod_Producto option:eq(0)").prop('selected', true);
    $("#Cod_Canal").trigger("chosen:updated");
    $("#TipoComision").trigger("chosen:updated");
    $("#Otros").trigger("chosen:updated");
    $("#Cod_Producto").trigger("chosen:updated");

}

function ExportarExcel(tipo) {
    window.open('/CalculoDiferimiento/GenerarExcel/');
}