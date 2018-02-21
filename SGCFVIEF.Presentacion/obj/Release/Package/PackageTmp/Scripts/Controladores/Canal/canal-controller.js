1903$(document).ready(function () {

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
            "Cod_Canal": $("#Cod_Canal").val(),
            "Cod_SubCanal": $("#Cod_SubCanal").val(),
            "RUC": ($("#RUC").val() == '' ? '' : $("#RUC").val()),
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
        if (parametros.RUC != null && parametros.RUC != "") {
            if (parametros.RUC.match(Constantes.ExpresionRegular.Ruc) == null)
                mensaje += Constantes.Message.ErrorRucCanal + Constantes.SaltoHtml;
        }

        if (mensaje == "")
            Buscar(parametros);
        else
            MostrarMensajeError(mensaje);
        return false;
    });

    $("#btn-cancelar").on("click", function () {
        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $('#Cod_SubCanal option:eq(0)').prop('selected', true)
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Cod_SubCanal").trigger("chosen:updated");
        $("#RUC").val(''),
        $("#FechaInicio").val('');
        $("#FechaFin").val('');
        $("#Resultados").empty();
    });

    $("#btn-registrar").on("click", function () {
        var parametros = {
            "Canal": $("#Canal").val(),
            "RUC": $("#RUC").val(),
            "Razon_Social": $("#Razon_Social").val(),
            "Direccion": $("#Direccion").val(),
            "Distrito": { "Cod_Distrito": $("#Distrito_Cod_Distrito").val() },
            "Telefono1": $("#Telefono1").val(),
            "Telefono2": $("#Telefono2").val(),
            "Celular": $("#Celular").val(),
            "Representante_Legal": $("#Representante_Legal").val(),
            "Email": $("#Email").val(),
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val(),
            "Estado": $("#Estado").val()
        };
        var mensaje = "";
        if (parametros.Canal == null || parametros.Canal == "")
            mensaje += Constantes.Message.FaltaNombreCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Canal.match(Constantes.ExpresionRegular.NumerosLetrasEspacio) == null)
                mensaje += Constantes.Message.ErrorNombreCanal + Constantes.SaltoHtml;
        }
        if (parametros.RUC == null || parametros.RUC == "")
            mensaje += Constantes.Message.FaltaRucCanal + Constantes.SaltoHtml;
        else {
            if (parametros.RUC.match(Constantes.ExpresionRegular.Ruc) == null)
                mensaje += Constantes.Message.ErrorRucCanal + Constantes.SaltoHtml;
        }

        if (parametros.Razon_Social == null || parametros.Razon_Social == "")
            mensaje += Constantes.Message.FaltaRzCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Razon_Social.match(Constantes.ExpresionRegular.Empresa) == null)
                mensaje += Constantes.Message.ErrorRzCanal + Constantes.SaltoHtml;
        }

        if (parametros.Direccion == null || parametros.Direccion == "")
            mensaje += Constantes.Message.FaltaDireccionCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Direccion.match(Constantes.ExpresionRegular.Direccion) == null)
                mensaje += Constantes.Message.ErrorDireccionCanal + Constantes.SaltoHtml;
        }

        if (parametros.Telefono1 == null || parametros.Telefono1 == "")
            mensaje += Constantes.Message.FaltaTelefono1Canal + Constantes.SaltoHtml;
        else {
            if (parametros.Telefono1.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorTelefono1Canal + Constantes.SaltoHtml;
        }

        if (parametros.Telefono2 == null || parametros.Telefono2 == "")
            mensaje += Constantes.Message.FaltaTelefono2Canal + Constantes.SaltoHtml;
        else {
            if (parametros.Telefono2.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorTelefono2Canal + Constantes.SaltoHtml;
        }

        if (parametros.Celular == null || parametros.Celular == "")
            mensaje += Constantes.Message.FaltaCelularCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Celular.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorCelularCanal + Constantes.SaltoHtml;
        }

        if (parametros.Representante_Legal == null || parametros.Representante_Legal == "")
            mensaje += Constantes.Message.FaltaRLCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Representante_Legal.match(Constantes.ExpresionRegular.NombresApellidos) == null)
                mensaje += Constantes.Message.ErrorRLCanal + Constantes.SaltoHtml;
        }

        if (parametros.Email == null || parametros.Email == "")
            mensaje += Constantes.Message.FaltaEmailCanal + Constantes.SaltoHtml;
        else if (isValidEmail(parametros.Email) == false)
            mensaje += Constantes.Message.ErrorEmailCanal + Constantes.SaltoHtml;

        if (parametros.Fecha_Inicio == null || parametros.Fecha_Inicio == "")
            mensaje += Constantes.Message.FaltaFICanal + Constantes.SaltoHtml;

        if (parametros.Fecha_Cese == null || parametros.Fecha_Cese == "")
            mensaje += Constantes.Message.FaltaFCCanal + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeGrabar").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-modificar").on("click", function () {
        var parametros = {
            "Canal": $("#Canal").val(),
            "RUC": $("#RUC").val(),
            "Razon_Social": $("#Razon_Social").val(),
            "Direccion": $("#Direccion").val(),
            "Region": { "Cod_Region": $("#Region_Cod_Region").val() },
            "Provincia": { "Cod_Provincia": $("#Provincia_Cod_Provincia").val() },
            "Distrito": { "Cod_Distrito": $("#Distrito_Cod_Distrito").val() },
            "Telefono1": $("#Telefono1").val(),
            "Telefono2": $("#Telefono2").val(),
            "Celular": $("#Celular").val(),
            "Representante_Legal": $("#Representante_Legal").val(),
            "Email": $("#Email").val(),
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val(),
            "Estado": $("#Estado").val()
        };
        var mensaje = "";
        if (parametros.Canal == null || parametros.Canal == "")
            mensaje += Constantes.Message.FaltaNombreCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Canal.match(Constantes.ExpresionRegular.NumerosLetrasEspacio) == null)
                mensaje += Constantes.Message.ErrorNombreCanal + Constantes.SaltoHtml;
        }
        if (parametros.RUC == null || parametros.RUC == "")
            mensaje += Constantes.Message.FaltaRucCanal + Constantes.SaltoHtml;
        else {
            if (parametros.RUC.match(Constantes.ExpresionRegular.Ruc) == null)
                mensaje += Constantes.Message.ErrorRucCanal + Constantes.SaltoHtml;
        }

        if (parametros.Razon_Social == null || parametros.Razon_Social == "")
            mensaje += Constantes.Message.FaltaRzCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Razon_Social.match(Constantes.ExpresionRegular.Empresa) == null)
                mensaje += Constantes.Message.ErrorRzCanal + Constantes.SaltoHtml;
        }

        if (parametros.Direccion == null || parametros.Direccion == "")
            mensaje += Constantes.Message.FaltaDireccionCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Direccion.match(Constantes.ExpresionRegular.Direccion) == null)
                mensaje += Constantes.Message.ErrorDireccionCanal + Constantes.SaltoHtml;
        }

        if (parametros.Telefono1 == null || parametros.Telefono1 == "")
            mensaje += Constantes.Message.FaltaTelefono1Canal + Constantes.SaltoHtml;
        else {
            if (parametros.Telefono1.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorTelefono1Canal + Constantes.SaltoHtml;
        }

        if (parametros.Telefono2 == null || parametros.Telefono2 == "")
            mensaje += Constantes.Message.FaltaTelefono2Canal + Constantes.SaltoHtml;
        else {
            if (parametros.Telefono2.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorTelefono2Canal + Constantes.SaltoHtml;
        }

        if (parametros.Celular == null || parametros.Celular == "")
            mensaje += Constantes.Message.FaltaCelularCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Celular.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorCelularCanal + Constantes.SaltoHtml;
        }

        if (parametros.Representante_Legal == null || parametros.Representante_Legal == "")
            mensaje += Constantes.Message.FaltaRLCanal + Constantes.SaltoHtml;
        else {
            if (parametros.Representante_Legal.match(Constantes.ExpresionRegular.NombresApellidos) == null)
                mensaje += Constantes.Message.ErrorRLCanal + Constantes.SaltoHtml;
        }

        if (parametros.Email == null || parametros.Email == "")
            mensaje += Constantes.Message.FaltaEmailCanal + Constantes.SaltoHtml;
        else if (isValidEmail(parametros.Email) == false)
            mensaje += Constantes.Message.ErrorEmailCanal + Constantes.SaltoHtml;

        if (parametros.Fecha_Inicio == null || parametros.Fecha_Inicio == "")
            mensaje += Constantes.Message.FaltaFICanal + Constantes.SaltoHtml;

        if (parametros.Fecha_Cese == null || parametros.Fecha_Cese == "")
            mensaje += Constantes.Message.FaltaFCCanal + Constantes.SaltoHtml;

        if (mensaje == "") { $("#MensajeGrabar").modal('show'); }
        else {
            MostrarMensajeError(mensaje);
        }
    });


    $("#btn-grabar").on("click", function () {
        var parametros = {
            "Canal": $("#Canal").val(),
            "RUC": $("#RUC").val(),
            "Razon_Social": $("#Razon_Social").val(),
            "Direccion": $("#Direccion").val(),
            "Region": { "Cod_Region": $("#Region_Cod_Region").val() },
            "Provincia": { "Cod_Provincia": $("#Provincia_Cod_Provincia").val() },
            "Distrito": { "Cod_Distrito": $("#Distrito_Cod_Distrito").val() },
            "Telefono1": $("#Telefono1").val(),
            "Telefono2": $("#Telefono2").val(),
            "Celular": $("#Celular").val(),
            "Representante_Legal": $("#Representante_Legal").val(),
            "Email": $("#Email").val(),
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val(),
            "Estado": $("#Estado").val()
        };
        Grabar(parametros);
        return false;
    });

    $("#btn-actualizar").on("click", function () {
        var parametros = {
            "Cod_Canal": $("#Cod_Canal").val(),
            "Canal": $("#Canal").val(),
            "RUC": $("#RUC").val(),
            "Razon_Social": $("#Razon_Social").val(),
            "Direccion": $("#Direccion").val(),
            "Region": { "Cod_Region": $("#Region_Cod_Region").val() },
            "Provincia": { "Cod_Provincia": $("#Provincia_Cod_Provincia").val() },
            "Distrito": { "Cod_Distrito": $("#Distrito_Cod_Distrito").val() },
            "Telefono1": $("#Telefono1").val(),
            "Telefono2": $("#Telefono2").val(),
            "Celular": $("#Celular").val(),
            "Representante_Legal": $("#Representante_Legal").val(),
            "Email": $("#Email").val(),
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
            $("#Cod_Canal").val(data.Valor);
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






