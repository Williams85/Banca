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

    from1 = $("#Fecha_Inicio")
      .datepicker({
          defaultDate: new Date(),
          changeMonth: true,
          changeYear: true,
          yearRange: "1900:2023",
          numberOfMonths: 1
      })
      .on("change", function () {
          to1.datepicker("option", "minDate", getDate(this));
      }),
    to1 = $("#Fecha_Cese").datepicker({
        defaultDate: new Date(),
        changeMonth: true,
        changeYear: true,
        yearRange: "1900:2023",
        numberOfMonths: 1
    });

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }
        return date;
    }

    $("#Region_Cod_Region").on("change", function () {
        var parametros = { "Cod_Region": $(this).val() };
        BuscarProvincias(parametros);
        return false;
    });

    $("#Provincia_Cod_Provincia").on("change", function () {
        var parametros = { "Cod_Provincia": $(this).val() };
        BuscarDistritos(parametros);
        return false;
    });

    $("#btn-buscar").click(function () {
        var parametros = {
            "Canal": { "Cod_Canal": $("#Cod_Canal").val() },
            "Cod_SubCanal": $("#Cod_SubCanal").val(),
            "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
            "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
        };
        Buscar(parametros);

        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $('#Cod_SubCanal option:eq(0)').prop('selected', true)
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Cod_SubCanal").trigger("chosen:updated");
        $("#FechaInicio").val('');
        $("#FechaFin").val('');
        return false;
    });

    $("#btn-registrar").on("click", function () {
        var parametros = {
            "SubCanal": $("#SubCanal").val(),
            "Direccion": $("#Direccion").val(),
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val()
        };
        var mensaje = "";

        if (parametros.SubCanal == null || parametros.SubCanal == "")
            mensaje += Constantes.Message.FaltaNombreSubCanal + Constantes.SaltoHtml;
        else {
            if (parametros.SubCanal.match(Constantes.ExpresionRegular.NumerosLetrasEspacio) == null)
                mensaje += Constantes.Message.ErrorNombreSubCanal + Constantes.SaltoHtml;
        }

        if (parametros.Direccion == null || parametros.Direccion == "")
            mensaje += Constantes.Message.FaltaDireccionSubCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Direccion.match(Constantes.ExpresionRegular.Direccion) == null)
                mensaje += Constantes.Message.ErrorDireccionSubCanal + Constantes.SaltoHtml;
        }

        if (parametros.Fecha_Inicio == null || parametros.Fecha_Inicio == "")
            mensaje += Constantes.Message.FaltaFISubCanal + Constantes.SaltoHtml;

        if (parametros.Fecha_Cese == null || parametros.Fecha_Cese == "")
            mensaje += Constantes.Message.FaltaFCSubCanal + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeGrabar").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-modificar").on("click", function () {
        var parametros = {
            "Cod_SubCanal": $("#Cod_SubCanal").val(),
            "SubCanal": $("#SubCanal").val(),
            "Direccion": $("#Direccion").val(),
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val()
        };
        var mensaje = "";
        if (parametros.Cod_SubCanal == null || parametros.Cod_SubCanal == "")
            mensaje += Constantes.Message.FaltaCodigoSubCanal + Constantes.SaltoHtml;

        if (parametros.SubCanal == null || parametros.SubCanal == "")
            mensaje += Constantes.Message.FaltaNombreSubCanal + Constantes.SaltoHtml;
        else {
            if (parametros.SubCanal.match(Constantes.ExpresionRegular.NumerosLetrasEspacio) == null)
                mensaje += Constantes.Message.ErrorNombreSubCanal + Constantes.SaltoHtml;
        }

        if (parametros.Direccion == null || parametros.Direccion == "")
            mensaje += Constantes.Message.FaltaDireccionSubCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Direccion.match(Constantes.ExpresionRegular.Direccion) == null)
                mensaje += Constantes.Message.ErrorDireccionSubCanal + Constantes.SaltoHtml;
        }

        if (parametros.Fecha_Inicio == null || parametros.Fecha_Inicio == "")
            mensaje += Constantes.Message.FaltaFISubCanal + Constantes.SaltoHtml;

        if (parametros.Fecha_Cese == null || parametros.Fecha_Cese == "")
            mensaje += Constantes.Message.FaltaFCSubCanal + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeGrabar").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-grabar").on("click", function () {
        var parametros = {
            "SubCanal": $("#SubCanal").val(),
            "Canal": { "Cod_Canal": $("#Canal_Cod_Canal").val() },
            "Direccion": $("#Direccion").val(),
            "Region": { "Cod_Region": $("#Region_Cod_Region").val() },
            "Provincia": { "Cod_Provincia": $("#Provincia_Cod_Provincia").val() },
            "Distrito": { "Cod_Distrito": $("#Distrito_Cod_Distrito").val() },
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val(),
            "Estado": $("#Estado").val()
        };
        Grabar(parametros);
        return false;
    });

    $("#btn-actualizar").on("click", function () {
        var parametros = {
            "Cod_SubCanal": $("#Cod_SubCanal").val(),
            "SubCanal": $("#SubCanal").val(),
            "Canal": { "Cod_Canal": $("#Canal_Cod_Canal").val() },
            "Direccion": $("#Direccion").val(),
            "Region": { "Cod_Region": $("#Region_Cod_Region").val() },
            "Provincia": { "Cod_Provincia": $("#Provincia_Cod_Provincia").val() },
            "Distrito": { "Cod_Distrito": $("#Distrito_Cod_Distrito").val() },
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val(),
            "Estado": $("#Estado").val()
        };
        Modificar(parametros);
        return false;
    });

    $("#Cod_Canal").on("change", function () {
        var parametros = {
            "Codigo": $(this).val(),
        }

        FiltrarSubCanales(parametros);
    });


    $("#btn-cancelar").on("click", function () {
        $("#FechaInicio").val("");
        $("#FechaFin").val("");
        $("#CodigoReporte").val("");
        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $('#Cod_SubCanal option:eq(0)').prop('selected', true)
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Cod_SubCanal").trigger("chosen:updated");
        $("#Resultados").empty();
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
        if (data.Estado) {
            $("#ResultadoGrabacion #Id").empty();
            $("#ResultadoGrabacion #Id").html(data.Valor);
            $("#ResultadoGrabacion").modal('show');
            $("#Cod_SubCanal").val(data.Valor);
            $("#Message-Error").hide();
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
        $("#MensajeGrabar").modal('hide');
        if (data.Estado) {
            $("#ResultadoGrabacion").modal('show');
            $("#Message-Error").hide();
        } else {
            $("#ResultadoGrabacionErr #Mensaje").empty();
            $("#ResultadoGrabacionErr #Mensaje").append(data.Message);
            $("#ResultadoGrabacionErr").modal('show');
        }
    });
}

function FiltrarSubCanales(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.FiltrarxCanal;
    info.parametros = parametros;

    ajax(info, function (data) {

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

function BuscarProvincias(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarProvincias;
    info.parametros = parametros;
    ajax(info, function (data) {
        CargarProvincias(data, "#Provincia_Cod_Provincia");
    });
}

function CargarProvincias(data, objeto) {
    $(objeto + ' option').remove();
    if (data != null) {
        $.each(data, function (i, item) {
            $(objeto).append("<option value='"
               + item.Cod_Provincia + "'>" + item.Descripcion
               + "</option>");
        });
    }
    $(objeto).trigger("change");
    $(objeto).trigger("chosen:updated");

}

function BuscarDistritos(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarDistritos;
    info.parametros = parametros;
    ajax(info, function (data) {
        CargarDistritos(data, "#Distrito_Cod_Distrito");
    });
}

function CargarDistritos(data, objeto) {
    $(objeto + ' option').remove();
    if (data != null) {
        $.each(data, function (i, item) {
            $(objeto).append("<option value='"
               + item.Cod_Distrito + "'>" + item.Descripcion
               + "</option>");
        });
    }

    $(objeto).trigger("chosen:updated");
}

