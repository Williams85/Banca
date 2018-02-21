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
            "SubCanal": { "Cod_SubCanal": $("#Cod_SubCanal").val() },
            "Tipo_Doc": { "Tipo_Doc": $("#Tipo_Doc").val() },
            "Num_Doc": $("#Num_Doc").val(),
            "Nombre": $("#Nombre").val(),
            "Apellido": $("#Apellido").val(),
            "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
            "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
        };

        $("#Message-Error").hide();
        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $('#Cod_SubCanal option:eq(0)').prop('selected', true)
        $('#Tipo_Doc option:eq(0)').prop('selected', true)
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Cod_SubCanal").trigger("chosen:updated");
        $("#Tipo_Doc").trigger("chosen:updated");
        $("#Num_Doc").val('');
        $("#Nombre").val('');
        $("#Apellido").val(''),
        $("#FechaInicio").val('');
        $("#FechaFin").val('');
        var mensaje = "";

        if (parametros.Tipo_Doc.Tipo_Doc == "1") {
            if (parametros.Num_Doc.match(Constantes.ExpresionRegular.SoloNumeros) == null)
                mensaje += Constantes.Message.ErrorNumeroDoc + Constantes.SaltoHtml;
            else {
                if (parametros.Num_Doc.length != 8)
                    mensaje += Constantes.Message.ErrorLongNumeroDoc + Constantes.SaltoHtml;
            }
        } else if (parametros.Tipo_Doc.Tipo_Doc == "2" || parametros.Tipo_Doc.Tipo_Doc == "3") {
            if (parametros.Num_Doc.match(Constantes.ExpresionRegular.NumerosLetras) == null)
                mensaje += Constantes.Message.ErrorNumeroDoc + Constantes.SaltoHtml;
            else {
                if (parametros.Num_Doc.length < 10 || parametros.Num_Doc.length > 15)
                    mensaje += Constantes.Message.ErrorLongNumeroDoc + Constantes.SaltoHtml;
            }
        }

        if (mensaje == "")
            Buscar(parametros)
        else
            MostrarMensajeError(mensaje);
        return false;
    });

    $("#btn-cancelar").on("click", function () {
        $("#Message-Error").hide();
        $('#Cod_Canal option:eq(0)').prop('selected', true)
        $('#Cod_SubCanal option:eq(0)').prop('selected', true)
        $('#Tipo_Doc option:eq(0)').prop('selected', true)
        $("#Cod_Canal").trigger("chosen:updated");
        $("#Cod_SubCanal").trigger("chosen:updated");
        $("#Tipo_Doc").trigger("chosen:updated");
        $("#Num_Doc").val('');
        $("#Nombre").val('');
        $("#Apellido").val(''),
        $("#FechaInicio").val('');
        $("#FechaFin").val('');
        $("#Resultados").empty();
    });

    $("#btn-registrar").on("click", function () {
        var parametros = {
            "Tipo_Doc": $("#Tipo_Doc_Tipo_Doc").val(),
            "Num_Doc": $("#Num_Doc").val(),
            "Nombre": $("#Nombre").val(),
            "Apellido": $("#Apellido").val(),
            "Apellido2": $("#Apellido2").val(),
            "Direccion": $("#Direccion").val(),
            "Telefono1": $("#Telefono1").val(),
            "Telefono2": $("#Telefono2").val(),
            "Celular": $("#Celular").val(),
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val()
        };
        var mensaje = "";

        if (parametros.Num_Doc == null || parametros.Num_Doc == "")
            mensaje += Constantes.Message.FaltaNumDocVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Num_Doc.match(Constantes.ExpresionRegular.DNI) == null)
                mensaje += Constantes.Message.ErrorNumDocVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Nombre == null || parametros.Nombre == "")
            mensaje += Constantes.Message.FaltaNombreVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Nombre.match(Constantes.ExpresionRegular.NombresApellidos) == null)
                mensaje += Constantes.Message.ErrorNombreVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Apellido == null || parametros.Apellido == "")
            mensaje += Constantes.Message.FaltaAPVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Apellido.match(Constantes.ExpresionRegular.NombresApellidos) == null)
                mensaje += Constantes.Message.ErrorAPVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Apellido2 == null || parametros.Apellido2 == "")
            mensaje += Constantes.Message.FaltaAMVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Apellido2.match(Constantes.ExpresionRegular.NombresApellidos) == null)
                mensaje += Constantes.Message.ErrorAMVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Num_Doc == null || parametros.Num_Doc == "")
            mensaje += Constantes.Message.FaltaNumDocVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Tipo_Doc != null && parametros.Tipo_Doc != "") {
                if (parametros.Tipo_Doc == "1") {
                    if (parametros.Num_Doc.match(Constantes.ExpresionRegular.SoloNumeros) == null)
                        mensaje += Constantes.Message.ErrorNumDocVendedor + Constantes.SaltoHtml;
                    else {
                        if (parametros.Num_Doc.length != 8) {
                            mensaje += Constantes.Message.ErrorLongNumDocVendedor + Constantes.SaltoHtml;
                        }
                    }
                } else {
                    if (parametros.Num_Doc.match(Constantes.ExpresionRegular.NumerosLetras) == null)
                        mensaje += Constantes.Message.ErrorNumDocVendedor + Constantes.SaltoHtml;
                    else {
                        if (parametros.Num_Doc.length < 10 || parametros.Num_Doc.length > 15) {
                            mensaje += Constantes.Message.ErrorLongNumDocVendedor + Constantes.SaltoHtml;
                        }
                    }
                }
            }
        }


        if (parametros.Direccion == null || parametros.Direccion == "")
            mensaje += Constantes.Message.FaltaDireccionVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Direccion.match(Constantes.ExpresionRegular.Direccion) == null)
                mensaje += Constantes.Message.ErrorDireccionVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Telefono1 == null || parametros.Telefono1 == "")
            mensaje += Constantes.Message.FaltaTelefono1Vendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Telefono1.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorTelefono1Vendedor + Constantes.SaltoHtml;
        }

        if (parametros.Telefono2 == null || parametros.Telefono2 == "")
            mensaje += Constantes.Message.FaltaTelefono2Vendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Telefono2.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorTelefono2Vendedor + Constantes.SaltoHtml;
        }

        if (parametros.Celular == null || parametros.Celular == "")
            mensaje += Constantes.Message.FaltaCelularVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Celular.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorCelularVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Fecha_Inicio == null || parametros.Fecha_Inicio == "")
            mensaje += Constantes.Message.FaltaFIVendedor + Constantes.SaltoHtml;

        if (parametros.Fecha_Cese == null || parametros.Fecha_Cese == "")
            mensaje += Constantes.Message.FaltaFCVendedor + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeGrabar").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-modificar").on("click", function () {
        var parametros = {
            "Cod_Vendedor": $("#Cod_Vendedor").val(),
            "Tipo_Doc": $("#Tipo_Doc_Tipo_Doc").val(),
            "Num_Doc": $("#Num_Doc").val(),
            "Nombre": $("#Nombre").val(),
            "Apellido": $("#Apellido").val(),
            "Apellido2": $("#Apellido2").val(),
            "Direccion": $("#Direccion").val(),
            "Telefono1": $("#Telefono1").val(),
            "Telefono2": $("#Telefono2").val(),
            "Celular": $("#Celular").val(),
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val()
        };
        var mensaje = "";
        if (parametros.Cod_Vendedor == null || parametros.Cod_Vendedor == "")
            mensaje += Constantes.Message.FaltaCodigoVendedor + Constantes.SaltoHtml;

        if (parametros.Num_Doc == null || parametros.Num_Doc == "")
            mensaje += Constantes.Message.FaltaNumDocVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Num_Doc.match(Constantes.ExpresionRegular.DNI) == null)
                mensaje += Constantes.Message.ErrorNumDocVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Nombre == null || parametros.Nombre == "")
            mensaje += Constantes.Message.FaltaNombreVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Nombre.match(Constantes.ExpresionRegular.NombresApellidos) == null)
                mensaje += Constantes.Message.ErrorNombreVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Apellido == null || parametros.Apellido == "")
            mensaje += Constantes.Message.FaltaAPVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Apellido.match(Constantes.ExpresionRegular.NombresApellidos) == null)
                mensaje += Constantes.Message.ErrorAPVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Apellido2 == null || parametros.Apellido2 == "")
            mensaje += Constantes.Message.FaltaAMVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Apellido2.match(Constantes.ExpresionRegular.NombresApellidos) == null)
                mensaje += Constantes.Message.ErrorAMVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Num_Doc == null || parametros.Num_Doc == "")
            mensaje += Constantes.Message.FaltaNumDocVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Tipo_Doc != null && parametros.Tipo_Doc != "") {
                if (parametros.Tipo_Doc == "1") {
                    if (parametros.Num_Doc.match(Constantes.ExpresionRegular.SoloNumeros) == null)
                        mensaje += Constantes.Message.ErrorNumDocVendedor + Constantes.SaltoHtml;
                    else {
                        if (parametros.Num_Doc.length != 8) {
                            mensaje += Constantes.Message.ErrorLongNumDocVendedor + Constantes.SaltoHtml;
                        }
                    }
                } else {
                    if (parametros.Num_Doc.match(Constantes.ExpresionRegular.NumerosLetras) == null)
                        mensaje += Constantes.Message.ErrorNumDocVendedor + Constantes.SaltoHtml;
                    else {
                        if (parametros.Num_Doc.length < 10 || parametros.Num_Doc.length > 15) {
                            mensaje += Constantes.Message.ErrorLongNumDocVendedor + Constantes.SaltoHtml;
                        }
                    }
                }
            }
        }

        if (parametros.Direccion == null || parametros.Direccion == "")
            mensaje += Constantes.Message.FaltaDireccionVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Direccion.match(Constantes.ExpresionRegular.Direccion) == null)
                mensaje += Constantes.Message.ErrorDireccionVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Telefono1 == null || parametros.Telefono1 == "")
            mensaje += Constantes.Message.FaltaTelefono1Vendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Telefono1.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorTelefono1Vendedor + Constantes.SaltoHtml;
        }

        if (parametros.Telefono2 == null || parametros.Telefono2 == "")
            mensaje += Constantes.Message.FaltaTelefono2Vendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Telefono2.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorTelefono2Vendedor + Constantes.SaltoHtml;
        }

        if (parametros.Celular == null || parametros.Celular == "")
            mensaje += Constantes.Message.FaltaCelularVendedor + Constantes.SaltoHtml;
        else {
            if (parametros.Celular.match(Constantes.ExpresionRegular.Telefono) == null)
                mensaje += Constantes.Message.ErrorCelularVendedor + Constantes.SaltoHtml;
        }

        if (parametros.Fecha_Inicio == null || parametros.Fecha_Inicio == "")
            mensaje += Constantes.Message.FaltaFIVendedor + Constantes.SaltoHtml;

        if (parametros.Fecha_Cese == null || parametros.Fecha_Cese == "")
            mensaje += Constantes.Message.FaltaFCVendedor + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeGrabar").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-grabar").on("click", function () {
        var parametros = {
            "Cod_Canal": $("#Canal_Cod_Canal").val(),
            "Cod_SubCanal": $("#SubCanal_Cod_SubCanal").val(),
            "Region": { "Cod_Region": $("#Region_Cod_Region").val() },
            "Provincia": { "Cod_Provincia": $("#Provincia_Cod_Provincia").val() },
            "Distrito": { "Cod_Distrito": $("#Distrito_Cod_Distrito").val() },
            "Tipo_Doc": { "Tipo_Doc": $("#Tipo_Doc_Tipo_Doc").val() },
            "Num_Doc": $("#Num_Doc").val(),
            "Nombre": $("#Nombre").val(),
            "Apellido": $("#Apellido").val(),
            "Apellido2": $("#Apellido2").val(),
            "Direccion": $("#Direccion").val(),
            "Telefono1": $("#Telefono1").val(),
            "Telefono2": $("#Telefono2").val(),
            "Celular": $("#Celular").val(),
            "Fecha_Inicio": $("#Fecha_Inicio").val(),
            "Fecha_Cese": $("#Fecha_Cese").val(),
            "Estado": $("#Estado").val()
        };
        Grabar(parametros);
        return false;
    });

    $("#btn-actualizar").on("click", function () {
        var parametros = {
            "Cod_Vendedor": $("#Cod_Vendedor").val(),
            "Cod_Canal": $("#Canal_Cod_Canal").val(),
            "Cod_SubCanal": $("#SubCanal_Cod_SubCanal").val(),
            "Region": { "Cod_Region": $("#Region_Cod_Region").val() },
            "Provincia": { "Cod_Provincia": $("#Provincia_Cod_Provincia").val() },
            "Distrito": { "Cod_Distrito": $("#Distrito_Cod_Distrito").val() },
            "Tipo_Doc": { "Tipo_Doc": $("#Tipo_Doc_Tipo_Doc").val() },
            "Num_Doc": $("#Num_Doc").val(),
            "Nombre": $("#Nombre").val(),
            "Apellido": $("#Apellido").val(),
            "Apellido2": $("#Apellido2").val(),
            "Direccion": $("#Direccion").val(),
            "Telefono1": $("#Telefono1").val(),
            "Telefono2": $("#Telefono2").val(),
            "Celular": $("#Celular").val(),
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

    $("#Canal_Cod_Canal").on("change", function () {
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
            $("#Cod_Vendedor").val(data.Valor);
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
        CargarSubCanales2(data, "#SubCanal_Cod_SubCanal");
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

function CargarSubCanales2(data, objeto) {
    $(objeto + ' option').remove();
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
