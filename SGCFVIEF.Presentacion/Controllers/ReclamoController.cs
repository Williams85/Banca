using SGCFVIEF.Dominio;
using SGCFVIEF.Entidad;
using SGCFVIEF.Utilitario;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SGCFVIEF.Presentacion.Controllers
{
    [Authentication]
    public class ReclamoController : Controller
    {
        // GET: Reclamo
        public ActionResult Index()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            TipoDocumentoDominio oTipoDocumentoDominio = new TipoDocumentoDominio();

            var ListaCanales = oCanalDominio.listarActivos();
            var ListaSubCanales = oSubCanalDominio.listarActivos(ListaCanales[0].Cod_Canal);
            var ListaTipoDocumento = oTipoDocumentoDominio.listar();


            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaSubCanales = ListaSubCanales;
            ViewBag.ListaTipoDocumento = ListaTipoDocumento;


            return View();
        }

        public ActionResult BuscarSolicitudes()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            TipoDocumentoDominio oTipoDocumentoDominio = new TipoDocumentoDominio();

            var ListaCanales = oCanalDominio.listarActivos();
            var ListaSubCanales = oSubCanalDominio.listarActivos(ListaCanales[0].Cod_Canal);
            var ListaTipoDocumento = oTipoDocumentoDominio.listar();


            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaSubCanales = ListaSubCanales;
            ViewBag.ListaTipoDocumento = ListaTipoDocumento;
            return View();
        }

        public ActionResult BuscarReclamo()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            TipoDocumentoDominio oTipoDocumentoDominio = new TipoDocumentoDominio();
            var ListaCanales = oCanalDominio.listarActivos();
            var ListaSubCanales = oSubCanalDominio.listarActivos(ListaCanales[0].Cod_Canal);
            var ListaTipoDocumento = oTipoDocumentoDominio.listar();

            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaSubCanales = ListaSubCanales;
            ViewBag.ListaTipoDocumento = ListaTipoDocumento;

            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(string Codigo)
        {
            if (Codigo == null || Codigo == "") return RedirectToAction("Index", "Reclamo");
            MotivoRechazoSolicitudDominio oMotivoRechazoSolicitudDominio = new MotivoRechazoSolicitudDominio();
            ReclamoDominio oReclamoDominio = new ReclamoDominio();
            TipoReclamoDominio oTipoReclamoDominio = new TipoReclamoDominio();
            var ListaMotivosRechazo = oMotivoRechazoSolicitudDominio.listarActivos();
            var reclamosolicitud = oReclamoDominio.ObtenerxSolicitud(Codigo);
            var ListaTipoReclamo = oTipoReclamoDominio.listar();
            ViewBag.ListaMotivosRechazo = ListaMotivosRechazo;
            ViewBag.ListaTipoReclamo = ListaTipoReclamo;
            if (reclamosolicitud == null ) return RedirectToAction("Index", "Reclamo");
            return View(reclamosolicitud);
        }

        [HttpPost]
        public ActionResult Edicion(string Codigo)
        {
            ReclamoDominio oReclamoDominio = new ReclamoDominio();
            ReclamoEntidad oReclamoEntidad = new ReclamoEntidad();
            TipoReclamoDominio oTipoReclamoDominio = new TipoReclamoDominio();
            if (Codigo == null || Codigo == "") return RedirectToAction("Index", "Reclamo");
            MotivoRechazoSolicitudDominio oMotivoRechazoSolicitudDominio = new MotivoRechazoSolicitudDominio();
            oReclamoEntidad = oReclamoDominio.FiltrarxCodigo(Codigo);
            var ListaMotivosRechazo = oMotivoRechazoSolicitudDominio.listarActivos();
            var ListaTipoReclamo = oTipoReclamoDominio.listar();
            ViewBag.ListaMotivosRechazo = ListaMotivosRechazo;
            ViewBag.ListaTipoReclamo = ListaTipoReclamo;
            return View(oReclamoEntidad);
        }

        [HttpPost]
        public ActionResult RespuestaReclamo(string Codigo)
        {
            ReclamoDominio oReclamoDominio = new ReclamoDominio();
            ReclamoEntidad oReclamoEntidad = new ReclamoEntidad();
            TipoReclamoDominio oTipoReclamoDominio = new TipoReclamoDominio();
            if (Codigo == null || Codigo == "") return RedirectToAction("Index", "Reclamo");
            MotivoRechazoSolicitudDominio oMotivoRechazoSolicitudDominio = new MotivoRechazoSolicitudDominio();
            RespuestaReclamoDominio oRespuestaReclamoDominio = new RespuestaReclamoDominio();
            oReclamoEntidad = oReclamoDominio.FiltrarxCodigo(Codigo);
            var ListaMotivosRechazo = oMotivoRechazoSolicitudDominio.listarActivos();
            var ListaTipoReclamo = oTipoReclamoDominio.listar();
            ViewBag.ListaMotivosRechazo = ListaMotivosRechazo;
            var ListaRespuestaReclamo = oRespuestaReclamoDominio.listarActivos();
            ViewBag.ListaRespuestaReclamo = ListaRespuestaReclamo;
            ViewBag.ListaTipoReclamo = ListaTipoReclamo;
            return View(oReclamoEntidad);
        }
        
        [HttpPost]
        public ActionResult Grabar(ReclamoEntidad entidad)
        {
            ReclamoDominio oReclamoDominio = new ReclamoDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            entidad.Usuario = SessionManager.Usuario;
            string Mensaje = string.Empty;
            string Codigo = string.Empty;
            oResponseWeb.Estado = oReclamoDominio.grabarDatos(entidad, ref Codigo, ref Mensaje);
            oResponseWeb.Message = Mensaje;
            oResponseWeb.Valor = Codigo;

            return Json(oResponseWeb);
        }


        [HttpPost]
        public ActionResult Modificar(ReclamoEntidad entidad)
        {
            ReclamoDominio oReclamoDominio = new ReclamoDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            entidad.Usuario = SessionManager.Usuario;
            string Mensaje = string.Empty;
            string Codigo = string.Empty;
            oResponseWeb.Estado = oReclamoDominio.modificarDatos(entidad, ref Mensaje);
            oResponseWeb.Message = Mensaje;
            oResponseWeb.Valor = Codigo;

            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult GrabarRespuesta(ReclamoEntidad entidad)
        {
            ReclamoDominio oReclamoDominio = new ReclamoDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            entidad.Usuario = SessionManager.Usuario;
            string Mensaje = string.Empty;
            oResponseWeb.Estado = oReclamoDominio.grabarRespuesta(entidad,ref Mensaje);
            oResponseWeb.Message = Mensaje;

            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult Buscar(ReclamoEntidad entidad)
        {
            ReclamoDominio oReclamoDominio = new ReclamoDominio();
            var ListaReclamos = oReclamoDominio.obtenerDatosXFiltro(entidad);
            Session["ListaReclamos"] = ListaReclamos;
            return PartialView("_ResultadosBusqueda", ListaReclamos);
        }

        [HttpPost]
        public ActionResult ResultadoBuscarSolicitudes(SolicitudEntidad entidad)
        {
            SolicitudDominio oSolicitudDominio = new SolicitudDominio();
            var ListaSolicitudes = oSolicitudDominio.FiltrarforReclamos(entidad);
            return PartialView("_ResultadoBuscarSolicitudes", ListaSolicitudes);
        }

        [HttpPost]
        public ActionResult ResultadoBuscarReclamo(ReclamoEntidad entidad)
        {
            ReclamoDominio oReclamoDominio = new ReclamoDominio();
            var ListaReclamos = oReclamoDominio.obtenerDatosXFiltro(entidad);
            Session["ListaReclamos"] = ListaReclamos;
            return PartialView("_ResultadosBusquedaReclamos", ListaReclamos);
        }

        [HttpPost]
        public ActionResult ValidarVigenciaReclamo(string N_Solicitud)
        {
            SolicitudDominio oSolicitudDominio = new SolicitudDominio();
            var estado = oSolicitudDominio.ValidarVigenciaReclamo(N_Solicitud);
            return Json(estado);
        }



        public ActionResult GenerarExcel()
        {
            List<ReclamoEntidad> ListaReclamos = new List<ReclamoEntidad>();
            GridView gv = new GridView();
            string filename = string.Empty;
            if (Session["ListaReclamos"] != null)
            {
                ListaReclamos = (List<ReclamoEntidad>)Session["ListaReclamos"];
                filename = "ListaReclamos.xls";
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn { ColumnName = "Canal" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cód. Vendedor" });
                dt.Columns.Add(new DataColumn { ColumnName = "Nombre Vendedor" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cod. Cliente" });
                dt.Columns.Add(new DataColumn { ColumnName = "Producto" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Aprobación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Tipo Reclamo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Motivo Reclamo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Estado" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Inicio" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Fin" });

                foreach (var item in ListaReclamos)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Solicitud.Canal.Canal;
                    dr[1] = item.Solicitud.Vendedor.Cod_Vendedor;
                    dr[2] = item.Solicitud.NombreCompletoVendedor;
                    dr[3] = item.Solicitud.BT_Cliente;
                    dr[4] = item.Solicitud.Producto.Producto;
                    dr[5] = item.Solicitud.Fecha_Aprob_Rech;
                    dr[6] = item.TipoReclamo.Nom_TipoReclamo;
                    dr[7] = item.MotivoRechazoSolicitud.Nom_MotivoRechazoSolicitud;
                    dr[8] = item.Estado;
                    dr[9] = item.FechaInicio;
                    dr[10] = item.FechaFin;
                    dt.Rows.Add(dr);
                }
                gv.DataSource = dt;
                gv.DataBind();

            }
            


            HttpContext context = System.Web.HttpContext.Current;
            context.Response.ClearContent();
            context.Response.Buffer = true;
            context.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
            context.Response.ContentType = "application/ms-excel";
            context.Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            context.Response.Output.Write(sw.ToString());
            context.Response.Flush();
            htw.Close();
            sw.Close();
            context.Response.End();
            return Json(new { successCode = "1" });
        }


    }
}