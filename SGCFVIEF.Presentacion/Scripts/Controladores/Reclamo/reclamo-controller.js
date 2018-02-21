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

    from1 = $("#FechaInicioSolicitud")
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
    to1 = $("#FechaFinSolicitud").datepicker({
        defaultDate: new Date(),
        changeMonth: true,
        changeYear: true,
        yearRange: "1900:2023",
        numberOfMonths: 1
    })

    from2 = $("#Fecha_Inicio")
      .datepicker({
          defaultDate: new Date(),
          changeMonth: true,
          changeYear: true,
          yearRange: "1900:2023",
          numberOfMonths: 1
      })
      .on("change", function () {
          var myDate = getDate(this);
          var dayOfMonth = myDate.getDate();
          myDate.setDate(dayOfMonth + 5);
          var dia = (myDate.getDate() < 10 ? ("0" + (myDate.getDate())) : (myDate.getDate()));
          var mes = (myDate.getMonth() < 9 ? ("0" + (myDate.getMonth() + 1)) : (myDate.getMonth() + 1));
          var ano = myDate.getFullYear();
          var fecha = dia + "/" + mes + "/" + ano;
          $("#Fecha_Fin").val(fecha);
          console.log(fecha);
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


    //$('input:[readonly="true"]').css("background-color", "#FFE599");
    $("#Message-Error").hide();

    $("#Canal").on("change", function () {
        var parametros = {
            "Codigo": $(this).val(),
        }
        FiltrarSubCanales(parametros);
    });

    $("#IdExportar").prop("disabled", true);

    $("#BT_Cliente").on("keyup", function () {
        if ($(this).val() != null || $(this).val() != "") {
            var parametros = {
                "Solicitud": { "BT_Cliente": $(this).val() }
            }

            BuscarSolicitudxCliente(parametros);
        }
    });

    $("#btn-registrar").on("click", function () {

        $("#Message-Error").hide();
        var parametros = {
            "Solicitud": { "N_Solicitud": $("#Solicitud_N_Solicitud").val() },
            "TipoReclamo": { "Cod_TipoReclamo": $("#TipoReclamo").val() },
            "MotivoRechazoSolicitud": { "Cod_MotivoRechazoSolicitud": $("#MotivoRechazoSolicitud").val() },
            "Observaciones": $("#Observacion").val(),
            "FechaInicio": $("#Fecha_Inicio").val(),
            "Estado": $("#Estado").val(),
        };
        var mensaje = "";
        if (parametros.Solicitud == null || parametros.Solicitud.N_Solicitud == null || parametros.Solicitud.N_Solicitud == "")
            mensaje += Constantes.Message.FaltaReclamoCodigoSolicitud + Constantes.SaltoHtml;

        if (parametros.TipoReclamo == null || parametros.TipoReclamo == "")
            mensaje += Constantes.Message.FaltaReclamoTipoReclamo + Constantes.SaltoHtml;

        if (parametros.MotivoRechazoSolicitud == null || parametros.MotivoRechazoSolicitud.Cod_MotivoRechazoSolicitud == null || parametros.MotivoRechazoSolicitud.Cod_MotivoRechazoSolicitud == "")
            mensaje += Constantes.Message.FaltaReclamoMotivoRechazo + Constantes.SaltoHtml;

        if (parametros.Observaciones == null || parametros.Observaciones == "")
            mensaje += Constantes.Message.FaltaReclamoObservacion + Constantes.SaltoHtml;

        if (parametros.FechaInicio == null || parametros.FechaInicio == "")
            mensaje += Constantes.Message.FaltaReclamoFechaInicio + Constantes.SaltoHtml;

        if (parametros.Estado == null || parametros.Estado == "")
            mensaje += Constantes.Message.FaltaReclamoEstado + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeGrabar").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-grabar").on("click", function () {
        var parametros = {
            "Solicitud": { "N_Solicitud": $("#Solicitud_N_Solicitud").val() },
            "TipoReclamo": { "Cod_TipoReclamo": $("#TipoReclamo").val() },
            "MotivoRechazoSolicitud": { "Cod_MotivoRechazoSolicitud": $("#MotivoRechazoSolicitud").val() },
            "Observaciones": $("#Observacion").val(),
            "FechaInicio": $("#Fecha_Inicio").val(),
            "Estado": $("#Estado").val(),
        };
        Grabar(parametros);
        return false;
    });

    $("#btn-registrarrespuesta").on("click", function () {
        $("#Message-Error").hide();
        var parametros = {
            "Cod_Reclamo": $("#Cod_Reclamo").val(),
            "RespuestaReclamo": { "Cod_RespuestaReclamo": $("#RespuestaReclamo_Cod_RespuestaReclamo").val() },
        };
        var mensaje = "";
        if (parametros.RespuestaReclamo == null || parametros.RespuestaReclamo.Cod_RespuestaReclamo == null || parametros.RespuestaReclamo.Cod_RespuestaReclamo == "")
            mensaje += Constantes.Message.FaltaReclamoRespuesta + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeGrabarRespuesta").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-grabarrespuesta").on("click", function () {
        $("#Message-Error").hide();
        var parametros = {
            "Cod_Reclamo": $("#Cod_Reclamo").val(),
            "RespuestaReclamo": { "Cod_RespuestaReclamo": $("#RespuestaReclamo_Cod_RespuestaReclamo").val() },
        };

        GrabarRespuesta(parametros);
    });

    $("#btn-modificar").on("click", function () {
        $("#Message-Error").hide();
        var parametros = {
            "Cod_Reclamo": $("#Cod_Reclamo").val(),
            "Solicitud": { "N_Solicitud": $("#Solicitud_N_Solicitud").val() },
            "TipoReclamo": { "Cod_TipoReclamo": $("#TipoReclamo").val() },
            "MotivoRechazoSolicitud": { "Cod_MotivoRechazoSolicitud": $("#MotivoRechazoSolicitud_Cod_MotivoRechazoSolicitud").val() },
            "Observaciones": $("#Observaciones").val(),
            "FechaInicio": $("#Fecha_Inicio").val(),
            "Estado": $("#Estado").val(),
        };
        var mensaje = "";
        if (parametros.Solicitud == null || parametros.Solicitud.N_Solicitud == null || parametros.Solicitud.N_Solicitud == "")
            mensaje += Constantes.Message.FaltaReclamoCodigoSolicitud + Constantes.SaltoHtml;

        if (parametros.Tipo_Reclamo == null || parametros.Tipo_Reclamo == "")
            mensaje += Constantes.Message.FaltaReclamoTipoReclamo + Constantes.SaltoHtml;

        if (parametros.MotivoRechazoSolicitud == null || parametros.MotivoRechazoSolicitud.Cod_MotivoRechazoSolicitud == null || parametros.MotivoRechazoSolicitud.Cod_MotivoRechazoSolicitud == "")
            mensaje += Constantes.Message.FaltaReclamoMotivoRechazo + Constantes.SaltoHtml;

        if (parametros.Observaciones == null || parametros.Observaciones == "")
            mensaje += Constantes.Message.FaltaReclamoObservacion + Constantes.SaltoHtml;

        if (parametros.FechaInicio == null || parametros.FechaInicio == "")
            mensaje += Constantes.Message.FaltaReclamoFechaInicio + Constantes.SaltoHtml;

        if (parametros.Estado == null || parametros.Estado == "")
            mensaje += Constantes.Message.FaltaReclamoEstado + Constantes.SaltoHtml;

        if (mensaje == "") {
            $("#MensajeModificar").modal('show');
        }
        else {
            MostrarMensajeError(mensaje);
        }
    });

    $("#btn-actualizar").on("click", function () {
        var parametros = {
            "Cod_Reclamo": $("#Cod_Reclamo").val(),
            "Solicitud": { "N_Solicitud": $("#Solicitud_N_Solicitud").val() },
            "TipoReclamo": { "Cod_TipoReclamo": $("#TipoReclamo").val() },
            "MotivoRechazoSolicitud": { "Cod_MotivoRechazoSolicitud": $("#MotivoRechazoSolicitud_Cod_MotivoRechazoSolicitud").val() },
            "Observaciones": $("#Observaciones").val(),
            "FechaInicio": $("#Fecha_Inicio").val(),
            "Estado": $("#Estado").val(),
        };

        Modificar(parametros);
        return false;
    });

    $("#IdBuscar").on("click", function () {
        var parametros = {
            "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
            "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
            "Ruc": $("#Ruc").val(),
            "Solicitud": {
                "Canal":
                    {
                        "Cod_Canal": $("#Canal").val()
                    },
                "Subcanal":
                    {
                        "Cod_SubCanal": $("#SubCanal").val()
                    },
                "Vendedor":
                    {
                        "Cod_Vendedor": $("#CodigoVendedor").val(),
                        "Nombre": $("#NombreVendedor").val(),
                        "Apellido": $("#ApellidosVendedor").val(),
                        "Tipo_Doc":
                        {
                            "Tipo_Doc": $("#TipoDocumento").val()
                        },
                        "Num_Doc": $("#NumDocumento").val()
                    }
            },
        };

        $("#Message-Error").hide();
        var mensaje = "";

        if (parametros.Solicitud.Vendedor.Tipo_Doc.Tipo_Doc == "1") {
            if (parametros.Solicitud.Vendedor.Num_Doc.match(Constantes.ExpresionRegular.SoloNumeros) == null)
                mensaje += Constantes.Message.ErrorNumeroDoc + Constantes.SaltoHtml;
            else {
                if (parametros.Solicitud.Vendedor.Num_Doc.length != 8)
                    mensaje += Constantes.Message.ErrorLongNumeroDoc + Constantes.SaltoHtml;
            }
        } else if (parametros.Solicitud.Vendedor.Tipo_Doc.Tipo_Doc == "2" || parametros.Solicitud.Vendedor.Tipo_Doc.Tipo_Doc == "3") {
            if (parametros.Solicitud.Vendedor.Num_Doc.match(Constantes.ExpresionRegular.NumerosLetras) == null)
                mensaje += Constantes.Message.ErrorNumeroDoc + Constantes.SaltoHtml;
            else {
                if (parametros.Solicitud.Vendedor.Num_Doc.length < 10 || parametros.Solicitud.Vendedor.Num_Doc.length > 15)
                    mensaje += Constantes.Message.ErrorLongNumeroDoc + Constantes.SaltoHtml;
            }
        }

        if (parametros.Ruc != null && parametros.Ruc != "") {
            if (parametros.Ruc.match(Constantes.ExpresionRegular.Ruc) == null)
                mensaje += Constantes.Message.ErrorRucCanal + Constantes.SaltoHtml;
        }
        if (mensaje == "")
            Buscar(parametros)
        else
            MostrarMensajeError(mensaje);


    });

    $("#IdBuscarReclamo").on("click", function () {
        var parametros = {
            "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
            "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
            "Ruc": $("#Ruc").val(),
            "Solicitud": {
                "Canal":
                    {
                        "Cod_Canal": $("#Canal").val()
                    },
                "Subcanal":
                    {
                        "Cod_SubCanal": $("#SubCanal").val()
                    },
                "Vendedor":
                    {
                        "Cod_Vendedor": $("#CodigoVendedor").val(),
                        "Nombre": $("#NombreVendedor").val(),
                        "Apellido": $("#ApellidosVendedor").val(),
                        "Tipo_Doc":
                        {
                            "Tipo_Doc": $("#TipoDocumento").val()
                        },
                        "Num_Doc": $("#NumDocumento").val()
                    }
            },
        };

        $("#Message-Error").hide();
        var mensaje = "";
        if (parametros.Ruc != null && parametros.Ruc != "") {
            if (parametros.Ruc.match(Constantes.ExpresionRegular.Ruc) == null)
                mensaje += Constantes.Message.ErrorRucCanal + Constantes.SaltoHtml;
        }
        if (mensaje == "")
            ResultadoBuscarReclamo(parametros);
        else
            MostrarMensajeError(mensaje);
    });

    $("#IdBuscarSolicitudes").on("click", function () {
        //var parametros = {
        //    "FechaInicio": ($("#FechaInicioSolicitud").val() == '' ? '01/01/2009' : $("#FechaInicioSolicitud").val()),
        //    "FechaFin": ($("#FechaFinSolicitud").val() == '' ? '01/01/2029' : $("#FechaFinSolicitud").val()),
        //    "Producto": { "Cod_Producto": $("#ProductoSolicitud").val() },
        //    "Num_Doc": $("#DocumentoCliente").val(),
        //    "BT_Cliente": $("#DocumentoCliente").val(),
        //    "Nombre_Cliente": $("#NombresCliente").val(),
        //    "Apellido1_Cliente": $("#ApellidosCliente").val(),
        //};
        var parametros = {
            "FechaInicio": ($("#FechaInicio").val() == '' ? '01/01/2009' : $("#FechaInicio").val()),
            "FechaFin": ($("#FechaFin").val() == '' ? '01/01/2029' : $("#FechaFin").val()),
            "N_Solicitud": $("#N_Solicitud").val(),
            "BT_Cliente": $("#BT-Cliente").val(),
            "Canal": { "Cod_Canal": $("#Canal").val() },
            "Subcanal":{"Cod_SubCanal": $("#SubCanal").val()},
            "Vendedor":{
                    "Cod_Vendedor": $("#CodigoVendedor").val(),
                    "Nombre": $("#NombreVendedor").val(),
                    "Apellido": $("#ApellidosVendedor").val(),
                    "Tipo_Doc":{"Tipo_Doc": $("#TipoDocumento").val()},
                    "Num_Doc": $("#NumDocumento").val()
                }
        };

        $("#Message-Error").hide();
        var mensaje = "";

        if (parametros.Vendedor.Tipo_Doc.Tipo_Doc == "1") {
            if (parametros.Vendedor.Num_Doc.match(Constantes.ExpresionRegular.SoloNumeros) == null)
                mensaje += Constantes.Message.ErrorNumeroDoc + Constantes.SaltoHtml;
            else {
                if (parametros.Vendedor.Num_Doc.length != 8)
                    mensaje += Constantes.Message.ErrorLongNumeroDoc + Constantes.SaltoHtml;
            }
        } else if (parametros.Vendedor.Tipo_Doc.Tipo_Doc == "2" || parametros.Vendedor.Tipo_Doc.Tipo_Doc == "3") {
            if (parametros.Solicitud.Vendedor.Num_Doc.match(Constantes.ExpresionRegular.NumerosLetras) == null)
                mensaje += Constantes.Message.ErrorNumeroDoc + Constantes.SaltoHtml;
            else {
                if (parametros.Vendedor.Num_Doc.length < 10 || parametros.Vendedor.Num_Doc.length > 15)
                    mensaje += Constantes.Message.ErrorLongNumeroDoc + Constantes.SaltoHtml;
            }
        }

        if (mensaje == "")
            ResultadoBuscarSolicitudes(parametros)
        else
            MostrarMensajeError(mensaje);
        return false;

    });


    $("#btn-cancelar").on("click", function () {
        $("#FechaInicio").val("");
        $("#FechaFin").val("");
        $("#Ruc").val("");
        $("#N_Solicitud").val("");
        $("#NombreVendedor").val("");
        $("#ApellidosVendedor").val("");
        $("#NumDocumento").val("");
        $("#CodigoVendedor").val("");
        $('#Canal option:eq(0)').prop('selected', true)
        $('#SubCanal option:eq(0)').prop('selected', true)
        $('#TipoDocumento option:eq(0)').prop('selected', true)
        $("#Canal").trigger("chosen:updated");
        $("#SubCanal").trigger("chosen:updated");
        $("#TipoDocumento").trigger("chosen:updated");
        $("#Resultados").empty();
    });


    $("#IdExportar").on("click", function () {
        ExportarExcel();
    });


});

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
            //$("#Cod_Canal").val(data.Valor);
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
        $("#MensajeModificar").modal('hide');
        if (data.Estado) {
            $("#ResultadoModificacion").modal('show');
            //$("#Cod_Canal").val(data.Valor);
            $("#Message-Error").hide();
        } else {
            $("#ResultadoGrabacionErr #Mensaje").empty();
            $("#ResultadoGrabacionErr #Mensaje").append(data.Message);
            $("#ResultadoGrabacionErr").modal('show');
        }

    });
}

function GrabarRespuesta(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.GrabarRespuesta;
    info.parametros = parametros;

    ajax(info, function (data) {
        $("#MensajeGrabarRespuesta").modal('hide');
        if (data.Estado) {
            $("#ResultadoGrabacionRespuesta").modal('show');
            $("#Message-Error").hide();
        } else {
            $("#ResultadoGrabacionRespuestaErr #Mensaje").empty();
            $("#ResultadoGrabacionRespuestaErr #Mensaje").append(data.Message);
            $("#ResultadoGrabacionRespuestaErr").modal('show');
        }

    });
}

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
            $("#IdExportar").prop("disabled", false);
        } else {
            $("#IdExportar").prop("disabled", true);
            PopInformativo("La consulta no presenta resultados para el filtro seleccionado.");
        }

    });
}

function ResultadoBuscarSolicitudes(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ResultadoBuscarSolicitudes;
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
            $(".btn-reclamo").on("click", function () {
                var solicitud = $(this).attr("data-solicitud");
                var parametros = { "N_Solicitud": solicitud };
                var valor = ValidarVigenciaReclamo(parametros);
                //console.log(valor);
                if (valor == "0")
                    return false;
            });

        } else {
            PopInformativo("La consulta no presenta resultados para el filtro seleccionado.");
        }

    });
}

function ResultadoBuscarReclamo(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ResultadoBuscarReclamo;
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
            $("#IdExportar").prop("disabled", true);
            PopInformativo("La consulta no presenta resultados para el filtro seleccionado.");
        }

    });
}

function BuscarSolicitudxCliente(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.BuscarSolicitudxCliente;
    info.parametros = parametros;

    ajax(info, function (data) {
        if (data != null)
            CargarDatosSolicitud(data);
        else
            PopInformativo("No se encontro la solicitud del cliente");

    });
}

function CargarDatosSolicitud(data) {

    $("#N_Solicitud").val(data.N_Solicitud);
    $("#Cod_Vendedor").val(data.Vendedor.Cod_Vendedor);
    $("#Nombre").val(data.Vendedor.Nombre);
    $("#Apellido").val(data.Vendedor.Apellido);
    $("#Apellido2").val(data.Vendedor.Apellido2);
    $("#Canal").val(data.Canal.Canal);
    $("#Sub_Canal").val(data.Subcanal.SubCanal);
    $("#Region").val(data.Region.Descripcion);
    $("#Provincia").val(data.Provincia.Descripcion);
    $("#Distrito").val(data.Distrito.Descripcion);
    $("#NomTipoDocumentoCliente").val(data.Tipo_Doc.Nomb_Doc);
    $("#NumDocumentoCliente").val(data.Num_Doc);
    $("#NombreCliente").val(data.Nombre_Cliente + ' ' + data.Apellido1_Cliente + ' ' + data.Apellido2_Cliente);
    $("#N_Operacion").val(data.N_Operación);
    $("#FechaAprobacion").val(data.Fecha_Aprob_Rech);
    $("#Producto").val(data.Producto.Producto);
    $("#Sub_Producto").val(data.SubProducto.SubProducto);

}

function FiltrarSubCanales(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.FiltrarxCanal;
    info.parametros = parametros;

    ajax(info, function (data) {

        CargarSubCanales(data, "#SubCanal");
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

function ValidarVigenciaReclamo(parametros) {
    //Consultar Controlador
    var info = new Object();
    info.metodo = "POST";
    info.serviceURL = rutas.ValidarVigenciaReclamo;
    info.parametros = parametros;

    //var resultado = false;
    ajax(info, function (data) {
        var valor = "0";
        //resultado = data;
        if (data == true)
            valor = "1";
        else
            PopInformativo("La vigencia del periodo de reclamo ha caducado.");

        console.log(valor);
        return "0";
    });
}

function ExportarExcel() {
    window.open('/Reclamo/GenerarExcel/');
}
