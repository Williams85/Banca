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
    public class ReporteComisionesController : Controller
    {
        // GET: ReporteComisiones
        public ActionResult Index()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            ProductoDominio oProductoDominio = new ProductoDominio();
            VendedorDominio oVendedorDominio = new VendedorDominio();
            var ListaCanales = oCanalDominio.listarActivos();
            var ListaProductos = oProductoDominio.listarActivos();
            SessionManager.ListaVendedores = oVendedorDominio.listar();
            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaProductos = ListaProductos;
            ViewBag.ListaVendedores = SessionManager.ListaVendedores;
            return View();
        }

        [HttpPost]
        public ActionResult Buscar(PagoComisionEntidad entidad)
        {
            PagoComisionDominio oPagoComisionDominio = new PagoComisionDominio();
            var ListaPagoComisiones = oPagoComisionDominio.ReporteComisiones(entidad);
            Session["ReporteComisiones"] = ListaPagoComisiones;
            return PartialView("_ResultadosBusqueda", ListaPagoComisiones);
        }

        [HttpPost]
        public ActionResult Grabar(SolicitudEntidad entidad)
        {
            PagoComisionDominio oPagoComisionDominio = new PagoComisionDominio();
            var ListaPagoComisiones = (List<PagoComisionEntidad>)Session["ReporteComisiones"];
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            string Mensaje = string.Empty;
            string Codigo = string.Empty;
            oResponseWeb.Estado = oPagoComisionDominio.GrabarReporteComisiones(ListaPagoComisiones, ref Codigo);
            oResponseWeb.Valor = Codigo;
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult FiltraVendedorxCanal(string Codigo)
        {
            if (SessionManager.ListaVendedores == null || SessionManager.ListaVendedores.Count == 0)
            {
                VendedorDominio oVendedorDominio = new VendedorDominio();
                SessionManager.ListaVendedores = oVendedorDominio.listar();

            }
            var ListaVendedores = SessionManager.ListaVendedores;
            List<VendedorEntidad> ListaVendedoresResult = new List<VendedorEntidad>();
            if (Codigo != null && Codigo != "")
                ListaVendedoresResult = ListaVendedores.Where(x => x.Canal.Cod_Canal == Codigo).OrderBy(x => x.NombreCompleto).ToList();
            else
                ListaVendedoresResult = ListaVendedores;
            return Json(ListaVendedoresResult);
        }


        public ActionResult GenerarExcel()
        {
            List<PagoComisionEntidad> ReporteComisiones = new List<PagoComisionEntidad>();
            GridView gv = new GridView();
            string filename = string.Empty;
            if (Session["ReporteComisiones"] != null)
            {
                ReporteComisiones = (List<PagoComisionEntidad>)Session["ReporteComisiones"];
                filename = "ReporteComisiones.xls";
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Reporte" });
                dt.Columns.Add(new DataColumn { ColumnName = "Código Comisión" });
                dt.Columns.Add(new DataColumn { ColumnName = "Producto" });
                dt.Columns.Add(new DataColumn { ColumnName = "Canal" });
                dt.Columns.Add(new DataColumn { ColumnName = "Importe" });
                dt.Columns.Add(new DataColumn { ColumnName = "Código Reporte" });

                foreach (var item in ReporteComisiones)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Fecha_Reporte_Comision;
                    dr[1] = item.Cod_Comision;
                    dr[2] = item.Producto.Producto;
                    dr[3] = item.Canal.Canal;
                    dr[4] = item.MontoComision;
                    dr[5] = item.Cod_Reporte_Comision;
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