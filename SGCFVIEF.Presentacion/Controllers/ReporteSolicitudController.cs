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
    public class ReporteSolicitudController : Controller
    {
        // GET: ReporteSolicitud
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
        public ActionResult Buscar(SolicitudEntidad entidad)
        {
            SolicitudDominio oSolicitudDominio = new SolicitudDominio();
            var ListaSolicitudes = oSolicitudDominio.ReporteSolicitudesAprobadas(entidad);
            Session["ReporteSolicitudesAprobadas"] = ListaSolicitudes;
            return PartialView("_ResultadosBusqueda", ListaSolicitudes);
        }

        [HttpPost]
        public ActionResult Grabar(SolicitudEntidad entidad)
        {
            SolicitudDominio oSolicitudDominio = new SolicitudDominio();
            var ListaSolicitudes = (List<SolicitudEntidad>)Session["ReporteSolicitudesAprobadas"];
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            string Mensaje = string.Empty;
            string Codigo = string.Empty;
            oResponseWeb.Estado = oSolicitudDominio.GrabarReporteSolicitudesAprobadas(ListaSolicitudes, ref Codigo);
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
            List<VendedorEntidad> ListaVendedoresResult=new List<VendedorEntidad>();
            if (Codigo != null && Codigo != "")
                ListaVendedoresResult = ListaVendedores.Where(x => x.Canal.Cod_Canal == Codigo).OrderBy(x => x.NombreCompleto).ToList();
            else
                ListaVendedoresResult = ListaVendedores;
            return Json(ListaVendedoresResult);
        }


        public ActionResult GenerarExcel()
        {
            List<SolicitudEntidad> ReporteSolicitudesAprobadas = new List<SolicitudEntidad>();
            GridView gv = new GridView();
            string filename = string.Empty;
            if (Session["ReporteSolicitudesAprobadas"] != null)
            {
                ReporteSolicitudesAprobadas = (List<SolicitudEntidad>)Session["ReporteSolicitudesAprobadas"];
                filename = "ReporteSolicitudesAprobadas.xls";
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Reporte" });
                dt.Columns.Add(new DataColumn { ColumnName = "Canal" });
                dt.Columns.Add(new DataColumn { ColumnName = "Producto" });
                dt.Columns.Add(new DataColumn { ColumnName = "SubProducto" });
                dt.Columns.Add(new DataColumn { ColumnName = "N° Solicitud" });
                dt.Columns.Add(new DataColumn { ColumnName = "BT-Ciente" });
                dt.Columns.Add(new DataColumn { ColumnName = "N° Operación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Nombre Cliente" });
                dt.Columns.Add(new DataColumn { ColumnName = "Linea/Prestamo (S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Estado" });
                dt.Columns.Add(new DataColumn { ColumnName = "Plazo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Tarifa" });
                dt.Columns.Add(new DataColumn { ColumnName = "Vendedor" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Consulta" });

                foreach (var item in ReporteSolicitudesAprobadas)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Fecha_Reporte_Solicitud;
                    dr[1] = item.Canal.Canal;
                    dr[2] = item.Producto.Producto;
                    dr[3] = item.SubProducto.SubProducto;
                    dr[4] = item.N_Solicitud;
                    dr[5] = item.BT_Cliente;
                    dr[6] = item.N_Operación;
                    dr[7] = item.NombreCompletoCliente;
                    dr[8] = item.Línea_Desembolsos.ToString("#0.#0");
                    dr[9] = item.Estado;
                    dr[10] = item.Plazo;
                    dr[11] = (item.Tipo == 1 ? (item.Tarifario * 100).ToString("#0.#0") + "%" : "S/. " + item.Tarifario.ToString("#0.#0"));
                    dr[12] = item.NombreCompletoVendedor;
                    dr[13] = DateTime.Now.ToString("dd/MM/yyyy");
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