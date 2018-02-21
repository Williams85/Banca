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

    $("#Canal_Cod_Canal").on("change", function () {
        var parametros = {
            "Codigo": $(this).val(),
        }

        FiltrarSubCanales(parametros);
    });

    $("#Cod_Canal").on("change", function () {
        var parametros = {
            "Codigo": $(this).val(),
        }

        FiltrarSubCanales(parametros);
    });

    $("#Cod_Producto").on("change", function () {
        var parametros = { "Codigo": $(this).val() };
        FiltrarSubProductos(parametros);
        return false;
    });

    $("#Producto_Cod_Producto").on("change", function () {
        var parametros = { "Codigo": $(this).val() };
        FiltrarSubProductos(parametros);
        return false;
    });

    if ($("#Tipo_Tarifa").val() == "2") {
        $("#Tarifa_Inicio").prop("disabled", true);
        $("#Tarifa_Fin").prop("disabled", true);
        $("#Tarifa_Inicio").val("0");
        $("#Tarifa_Fin").val("0");
    } else if ($("#Tipo_Tarifa").val() == "1") {
        $("#Tarifa_Inicio").prop("disabled", false);
        $("#Tarifa_Fin").prop("disabled", false);
        $("#Tarifa_Inicio").val("");
        $("#Tarifa_Fin").val("");
    }

    if ($("#Tipo").val() == "2") {
        $("#Tarifa_Inicio").prop("disabled", true);
        $("#Tarifa_Fin").prop("disabled", true);
        //$("#Tarifa_Inicio").val("0");
        //$("#Tarifa_Fin").val("0");
    } else if ($("#Tipo").val() == "1") {
        $("#Tarifa_Inicio").prop("disabled", false);
        $("#Tarifa_Fin").prop("disabled", false);
        //$("#Tarifa_Inicio").val("");
        //$("#Tarifa_Fin").val("");
    }

    $("#Tipo_Tarifa").on("change", function () {
        var Tipo = $(this).val();
        if (Tipo == "2") {
            $("#Tarifa_Inicio").prop("disabled", true);
            $("#Tarifa_Fin").prop("disabled", true);
            $("#Tarifa_Inicio").val("0");
            $("#Tarifa_Fin").val("0");
        } else if (Tipo == "1") {
            $("#Tarifa_Inicio").prop("disabled", false);
            $("#Tarifa_Fin").prop("disabled", false);
            $("#Tarifa_Inicio").val("");
            $("#Tarifa_Fin").val("");
        }

    });

    $("#Tipo").on("change", function () {
        var Tipo = $(this).val();
        if (Tipo == "2") {
            $("#Tarifa_Inicio").prop("disabled", true);
            $("#Tarifa_Fin").prop("disabled", true);
            //$("#Tarifa_Inicio").val("0");
            //$("#Tarifa_Fin").val("0");
        } else if (Tipo == "1") {
            $("#Tarifa_Inicio").prop("disabled", false);
            $("#Tarifa_Fin").prop("disabled", false);
            //$("#Tarifa_Inicio").val("");
            //$("#Tarifa_Fin").val("");
        }

    });

    $("#btn-cancelar").on("click", function () {
        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $('#Cod_SubCanal option:eq(0)').prop('selected', true)
        $('#TipoComision option:eq(0)').prop('selected', true)
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Cod_SubCanal").trigger("chosen:updated");
        $("#TipoComision").trigger("chosen:updated");
        $("#FechaInicio").val('');
        $("#FechaFin").val('');
        $("#RUC").val('');
        $("#Resultados").empty();
    });

    $("#btn-buscar").on("click", function () {
        var parametros = {
            "Canal": { "Cod_Canal": $("#Cod_Canal").val() },
            "SubCanal": { "Cod_SubCanal": $("#Cod_SubCanal").val() },
            "Canal": { "RUC": ($("#RUC").val() == '' ? '' : $("#RUC").val())},
            "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
            "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
        };

        $("#Message-Error").hide();
        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $('#Cod_SubCanal option:eq(0)').prop('selected', true)
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Cod_SubCanal").trigger("chosen:updated");
        $("#RUC").val(''),
        $("#FechaInicio").val('');
        $("#FechaFin").val('');
        var mensaje = "";
        if (parametros.Canal.RUC != null && parametros.Canal.RUC != "") {
            if (parametros.Canal.RUC.match(Constantes.ExpresionRegular.Ruc) == null)
                mensaje += Constantes.Message.ErrorRuc + Constantes.SaltoHtml;
        }

        if (mensaje == "")
            Buscar(parametros);
        else
            MostrarMensajeError(mensaje);

        return false;
    });

    $("#btn-registrar").on("click", function () {
        $("#Message-Error").hide();
        var parametros = {
            "Canal": { "Cod_Canal": $("#Canal_Cod_Canal").val(), },
            "SubCanal": { "Cod_SubCanal": $("#SubCanal_Cod_SubCanal").val(), },
            "Producto": { "Cod_Producto": $("#Producto_Cod_Producto").val(), },
            "SubProducto": { "Cod_SubProducto": $("#SubProducto_Cod_SubProducto").val(), },
            "Tipo": $("#Tipo_Tarifa").val(),
            "Tarifa_Inicio": $("#Tarifa_Inicio").val(),
            "Tarifa_Fin": $("#Tarifa_Fin").val(),
            "Tarifario": $("#Tarifa").val(),
            "Estado": $("#Estado").val(),
        };

        var mensaje = "";
        if (parametros.Canal == null || parametros.Canal.Cod_Canal == null || parametros.Canal.Cod_Canal == "")
            mensaje += Constantes.Message.FaltaTarifaCanal + Constantes.SaltoHtml;

        if (parametros.SubCanal == null || parametros.SubCanal.Cod_SubCanal == null || parametros.SubCanal.Cod_SubCanal == "")
            mensaje += Constantes.Message.FaltaTarifaSubCanal + Constantes.SaltoHtml;

        if (parametros.Producto == null || parametros.Producto.Cod_Producto == null || parametros.Producto.Cod_Producto == "")
            mensaje += Constantes.Message.FaltaTarifaProducto + Constantes.SaltoHtml;

        if (parametros.SubProducto == null || parametros.SubProducto.Cod_SubProducto == null || parametros.SubProducto.Cod_SubProducto == "")
            mensaje += Constantes.Message.FaltaTarifaSubProducto + Constantes.SaltoHtml;

        if (parametros.Tipo == null || parametros.Tipo == "")
            mensaje += Constantes.Message.FaltaTarifaTipoTarifa + Constantes.SaltoHtml;

        if (parametros.Tarifa_Inicio == null || parametros.Tarifa_Inicio == "")
            mensaje += Constantes.Message.FaltaTarifaTarifaInicio + Constantes.SaltoHtml;
        else {
            if (parametros.Tarifa_Inicio.match(Constantes.ExpresionRegular.Precio) == null)
                mensaje += Constantes.Message.ErrorTarifaTarifaInicio + Constantes.SaltoHtml;
        }

        if (parametros.Tarifa_Fin == null || parametros.Tarifa_Fin == "")
            mensaje += Constantes.Message.FaltaTarifaTarifaFin + Constantes.SaltoHtml;
        else {
            if (parametros.Tarifa_Fin.match(Constantes.ExpresionRegular.Precio) == null)
                mensaje += Constantes.Message.ErrorTarifaTarifaFin + Constantes.SaltoHtml;
        }

        if (parametros.Tarifario == null || parametros.Tarifario == "")
            mensaje += Constantes.Message.FaltaTarifaTarifa + Constantes.SaltoHtml;
        else {
            if (parametros.Tarifario.match(Constantes.ExpresionRegular.Precio) == null)
                mensaje += Constantes.Message.ErrorTarifaTarifa + Constantes.SaltoHtml;
        }

        if (parametros.Estado == null || parametros.Estado == "")
            mensaje += Constantes.Message.FaltaTarifaEstado + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeGrabar").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-grabar").on("click", function () {
        var parametros = {
            "Canal": { "Cod_Canal": $("#Canal_Cod_Canal").val(), },
            "SubCanal": { "Cod_SubCanal": $("#SubCanal_Cod_SubCanal").val(), },
            "Producto": { "Cod_Producto": $("#Producto_Cod_Producto").val(), },
            "SubProducto": { "Cod_SubProducto": $("#SubProducto_Cod_SubProducto").val(), },
            "Tipo": $("#Tipo_Tarifa").val(),
            "Tarifa_Inicio": $("#Tarifa_Inicio").val(),
            "Tarifa_Fin": $("#Tarifa_Fin").val(),
            "Tarifario": $("#Tarifa").val(),
            "Estado": $("#Estado").val(),
        };
        Grabar(parametros);
        return false;
    });

    $("#btn-modificar").on("click", function () {
        $("#Message-Error").hide();
        var parametros = {
            "Cod_Tarifa": $("#Cod_Tarifa").val(),
            "Canal": { "Cod_Canal": $("#Canal_Cod_Canal").val(), },
            "SubCanal": { "Cod_SubCanal": $("#SubCanal_Cod_SubCanal").val(), },
            "Producto": { "Cod_Producto": $("#Producto_Cod_Producto").val(), },
            "SubProducto": { "Cod_SubProducto": $("#SubProducto_Cod_SubProducto").val(), },
            "Tipo": $("#Tipo").val(),
            "Tarifa_Inicio": $("#Tarifa_Inicio").val(),
            "Tarifa_Fin": $("#Tarifa_Fin").val(),
            "Tarifario": $("#Tarifario").val(),
            "Estado": $("#Estado").val(),
        };

        var mensaje = "";
        if (parametros.Cod_Tarifa == null || parametros.Cod_Tarifa == "")
            mensaje += Constantes.Message.FaltaTarifaCodigo + Constantes.SaltoHtml;

        if (parametros.Canal == null || parametros.Canal.Cod_Canal == null || parametros.Canal.Cod_Canal == "")
            mensaje += Constantes.Message.FaltaTarifaCanal + Constantes.SaltoHtml;

        if (parametros.SubCanal == null || parametros.SubCanal.Cod_SubCanal == null || parametros.SubCanal.Cod_SubCanal == "")
            mensaje += Constantes.Message.FaltaTarifaSubCanal + Constantes.SaltoHtml;

        if (parametros.Producto == null || parametros.Producto.Cod_Producto == null || parametros.Producto.Cod_Producto == "")
            mensaje += Constantes.Message.FaltaTarifaProducto + Constantes.SaltoHtml;

        if (parametros.SubProducto == null || parametros.SubProducto.Cod_SubProducto == null || parametros.SubProducto.Cod_SubProducto == "")
            mensaje += Constantes.Message.FaltaTarifaSubProducto + Constantes.SaltoHtml;

        if (parametros.Tipo == null || parametros.Tipo == "")
            mensaje += Constantes.Message.FaltaTarifaTipoTarifa + Constantes.SaltoHtml;

        if (parametros.Tarifa_Inicio == null || parametros.Tarifa_Inicio == "")
            mensaje += Constantes.Message.FaltaTarifaTarifaInicio + Constantes.SaltoHtml;
        else {
            if (parametros.Tarifa_Inicio.match(Constantes.ExpresionRegular.Precio) == null)
                mensaje += Constantes.Message.ErrorTarifaTarifaInicio + Constantes.SaltoHtml;
        }

        if (parametros.Tarifa_Fin == null || parametros.Tarifa_Fin == "")
            mensaje += Constantes.Message.FaltaTarifaTarifaFin + Constantes.SaltoHtml;
        else {
            if (parametros.Tarifa_Fin.match(Constantes.ExpresionRegular.Precio) == null)
                mensaje += Constantes.Message.ErrorTarifaTarifaFin + Constantes.SaltoHtml;
        }

        if (parametros.Tarifario == null || parametros.Tarifario == "")
            mensaje += Constantes.Message.FaltaTarifaTarifa + Constantes.SaltoHtml;
        else {
            if (parametros.Tarifario.match(Constantes.ExpresionRegular.Precio) == null)
                mensaje += Constantes.Message.ErrorTarifaTarifa + Constantes.SaltoHtml;
        }

        if (parametros.Estado == null || parametros.Estado == "")
            mensaje += Constantes.Message.FaltaTarifaEstado + Constantes.SaltoHtml;

        if (mensaje == "") { $("#MensajeGrabar").modal('show'); }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-actualizar").on("click", function () {
        var parametros = {
            "Cod_Tarifa": $("#Cod_Tarifa").val(),
            "Canal": { "Cod_Canal": $("#Canal_Cod_Canal").val(), },
            "SubCanal": { "Cod_SubCanal": $("#SubCanal_Cod_SubCanal").val(), },
            "Producto": { "Cod_Producto": $("#Producto_Cod_Producto").val(), },
            "SubProducto": { "Cod_SubProducto": $("#SubProducto_Cod_SubProducto").val(), },
            "Tipo": $("#Tipo").val(),
            "Tarifa_Inicio": $("#Tarifa_Inicio").val(),
            "Tarifa_Fin": $("#Tarifa_Fin").val(),
            "Tarifario": $("#Tarifario").val(),
            "Estado": $("#Estado").val(),
        };
        Modificar(parametros);
        return false;
    });

    $("#Message-Error").hide();

})

function Buscar(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.Buscar;
    info.parametros = parametros;

    ajaxPartialView(info, function (data) {
        if (data != null && $.trim(data) != "") {
            $("#Resultados").html(data);
            $("#TablaResultados").dataTable({
                "pageLength": 5,
                "ordering": false,
                "info": false,
                "searching": false
            });
        } else {
            $("#Resultados").html("");
            PopInformativo("La consulta no presenta resultado para el filtro seleccionado.");
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
        $("#Message-Error").hide();
        if (data.Estado) {
            $("#ResultadoGrabacion #Mensaje").empty();
            $("#ResultadoGrabacion #Mensaje").html(data.Message);
            $("#ResultadoGrabacion").modal('show');
        } else {
            $("#ResultadoGrabacionErr #Mensaje").empty();
            $("#ResultadoGrabacionErr #Mensaje").append(data.Message);
            $("#ResultadoGrabacionErr").modal('show');
        }
    });
}

function Modificar(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.Modificar;
    info.parametros = parametros;

    ajax(info, function (data) {
        $("#Message-Error").hide();
        $("#MensajeGrabar").modal('hide');
        if (data.Estado) {
            $("#ResultadoGrabacion #Mensaje").empty();
            $("#ResultadoGrabacion #Mensaje").html(data.Message);
            $("#ResultadoGrabacion").modal('show');
        } else {
            $("#ResultadoGrabacionErr #Mensaje").empty();
            $("#ResultadoGrabacionErr #Mensaje").append(data.Message);
            $("#ResultadoGrabacionErr").modal('show');
        }
    });
}

function FiltrarSubProductos(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarSubProductos;
    info.parametros = parametros;
    ajax(info, function (data) {
        CargarSubProductos(data, "#SubProducto_Cod_SubProducto");
    });
}

function CargarSubProductos(data, objeto) {
    $(objeto + ' option').remove();
    $(objeto).append("<option value=''>Todos</option>");
    if (data != null) {
        $.each(data, function (i, item) {
            $(objeto).append("<option value='"
               + item.Cod_SubProducto + "'>" + item.SubProducto
               + "</option>");
        });
    }
    $(objeto).trigger("chosen:updated");
}

function FiltrarSubCanales(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.FiltrarxCanal;
    info.parametros = parametros;

    ajax(info, function (data) {
        CargarSubCanales(data, "#SubCanal_Cod_SubCanal");
        CargarSubCanales(data, "#Cod_SubCanal");
    });
}

function CargarSubCanales(data, objeto) {
    $(objeto + ' option').remove();
    $(objeto).append("<option value=''>Todos</option>");
    if (data != null) {
        $.each(data, function (i, item) {
            $(objeto).append("<option value='"
               + item.Cod_SubCanal + "'>" + item.SubCanal
               + "</option>");
        });
    }
    $(objeto).trigger("chosen:updated");
}