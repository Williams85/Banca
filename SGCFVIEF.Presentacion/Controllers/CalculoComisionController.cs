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
    public class CalculoComisionController : Controller
    {
        // GET: CalculoComision
        public ActionResult Index()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            ProductoDominio oProductoDominio = new ProductoDominio();
            var ListaCanales = oCanalDominio.listarActivos();
            var ListaProductos = oProductoDominio.listarActivos();
            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaProductos = ListaProductos;
            return View();
        }

        [HttpPost]
        public ActionResult BuscarSolicitudesActivos(SolicitudEntidad entidad)
        {
            SolicitudDominio oSolicitudDominio = new SolicitudDominio();
            var ListaSolicitudesActivas = oSolicitudDominio.FiltrarActivas(entidad);
            Session["ListaCalculoComisiones"] = null;
            Session["ListaPagoComisiones"] = null;
            Session["ListaSolicitudesRechazadas"] = null;
            Session["ListaSolicitudes"] = ListaSolicitudesActivas;
            return PartialView("_ResultadosBusqueda", ListaSolicitudesActivas);
        }

        [HttpPost]
        public ActionResult BuscarSolicitudesRechazdos(SolicitudEntidad entidad)
        {
            SolicitudDominio oSolicitudDominio = new SolicitudDominio();
            var ListaSolicitudesActivas = oSolicitudDominio.FiltrarRechazados(entidad);
            Session["ListaSolicitudes"] = null;
            Session["ListaCalculoComisiones"] = null;
            Session["ListaPagoComisiones"] = null;
            Session["ListaSolicitudesRechazadas"] = ListaSolicitudesActivas;
            return PartialView("_ResultadosBusquedaRechazados", ListaSolicitudesActivas);
        }

        [HttpPost]
        public ActionResult BuscarPagoComsiones(PagoComisionEntidad entidad)
        {
            PagoComisionDominio oPagoComisionDominio = new PagoComisionDominio();
            var ListaPagoComisiones = oPagoComisionDominio.FiltrarActivas(entidad);
            Session["ListaSolicitudes"] = null;
            Session["ListaSolicitudesRechazadas"] = null;
            Session["ListaCalculoComisiones"] = null;
            Session["ListaPagoComisiones"] = ListaPagoComisiones;
            return PartialView("_ResultadosBusquedaPagoComisiones", ListaPagoComisiones);
        }

        [HttpPost]
        public ActionResult CalcularComisiones(SolicitudEntidad entidad)
        {
            SolicitudDominio oSolicitudDominio = new SolicitudDominio();
            var ListaCalculoComisiones = oSolicitudDominio.CalcularComisiones(entidad);
            Session["ListaSolicitudes"] = null;
            Session["ListaSolicitudesRechazadas"] = null;
            Session["ListaPagoComisiones"] = null;
            Session["ListaCalculoComisiones"] = ListaCalculoComisiones;
            return PartialView("_ResultadosBusquedaCalculoComision", ListaCalculoComisiones);
        }

        [HttpPost]
        public ActionResult ActivarSolicitud(SolicitudEntidad entidad)
        {
            SolicitudDominio oSolicitudDominio = new SolicitudDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            oResponseWeb.Estado = oSolicitudDominio.Activar(entidad);
            if (oResponseWeb.Estado)
                oResponseWeb.Message = "Se activo la solicitud...";
            else
                oResponseWeb.Message = "No se activo la solicitud...";
            
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult GrabarCalculoComisiones()
        {
            PagoComisionDominio oPagoComisionDominio = new PagoComisionDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            var Lista=(List<SolicitudEntidad>) Session["ListaCalculoComisiones"];
            string Codigo = "";
            oResponseWeb.Estado = oPagoComisionDominio.grabarDatos(Lista, ref Codigo);
            oResponseWeb.Valor = Codigo;
            return Json(oResponseWeb);
        }

        public ActionResult GenerarExcel()
        {
            List<PagoComisionEntidad> ListaPagoComisiones = new List<PagoComisionEntidad>();
            List<SolicitudEntidad> ListaSolicitudes = new List<SolicitudEntidad>();
            GridView gv = new GridView();
            string filename = string.Empty;
            if (Session["ListaPagoComisiones"] != null)
            {
                ListaPagoComisiones = (List<PagoComisionEntidad>)Session["ListaPagoComisiones"];
                filename = "ListadoPagoComisiones.xls";
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn { ColumnName = "Cod. Cálculo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cuenta Contable" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Aprobación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cod. Cliente" });
                dt.Columns.Add(new DataColumn { ColumnName = "Operación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Producto" });
                dt.Columns.Add(new DataColumn { ColumnName = "SubProducto" });
                dt.Columns.Add(new DataColumn { ColumnName = "Moneda" });
                dt.Columns.Add(new DataColumn { ColumnName = "Linea/Prestamo (S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Plazo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Actual." });
                dt.Columns.Add(new DataColumn { ColumnName = "Canal" });
                dt.Columns.Add(new DataColumn { ColumnName = "Vendedor" });
                dt.Columns.Add(new DataColumn { ColumnName = "Estado" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Comisión" });
                dt.Columns.Add(new DataColumn { ColumnName = "Comisión" });
                dt.Columns.Add(new DataColumn { ColumnName = "Tarifa Comisión" });

                foreach (var item in ListaPagoComisiones)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Cod_Comision;
                    dr[1] = item.CuentaContable1;
                    dr[2] = item.Fecha_Aprob_Rech;
                    dr[3] = item.BT_Cliente;
                    dr[4] = item.N_Operación;
                    dr[5] = item.Producto.Producto;
                    dr[6] = item.SubProducto.SubProducto;
                    dr[7] = item.Moneda;
                    dr[8] = item.Línea_Desembolsos.ToString("#0.#0");
                    dr[9] = item.Plazo;
                    dr[10] = item.Saldo_Act.ToString("#0.#0");
                    dr[11] = item.Canal.Canal;
                    dr[12] = item.NombreCompletoVendedor;
                    dr[13] = item.Estado;
                    dr[14] = item.FechaComision;
                    dr[15] = item.MontoComision.ToString("#0.#0");
                    dr[16] = (item.TipoTarifa == 1 ? item.Tarifario.ToString("#0.#0") + "%" : "S/. " + item.Tarifario.ToString("#0.#0"));
                    dt.Rows.Add(dr);
                }
                gv.DataSource = dt;
                gv.DataBind();

            }
            else if (Session["ListaCalculoComisiones"] != null)
            {
                ListaSolicitudes = (List<SolicitudEntidad>)Session["ListaCalculoComisiones"];
                filename = "ListadoCalculoComisiones.xls";

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Aprobación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cod. Cliente" });
                dt.Columns.Add(new DataColumn { ColumnName = "Operacion" });
                dt.Columns.Add(new DataColumn { ColumnName = "Producto" });
                dt.Columns.Add(new DataColumn { ColumnName = "SubProducto" });
                dt.Columns.Add(new DataColumn { ColumnName = "Moneda" });
                dt.Columns.Add(new DataColumn { ColumnName = "Linea/Prestamo (S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Plazo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Mes Ant." });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Actual." });
                dt.Columns.Add(new DataColumn { ColumnName = "Canal" });
                dt.Columns.Add(new DataColumn { ColumnName = "Vendedor" });
                dt.Columns.Add(new DataColumn { ColumnName = "Estado" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Comisión" });
                dt.Columns.Add(new DataColumn { ColumnName = "Comisión" });
                dt.Columns.Add(new DataColumn { ColumnName = "Tarifa Comisión" });
                foreach (var item in ListaSolicitudes)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Fecha_Aprob_Rech;
                    dr[1] = item.BT_Cliente;
                    dr[2] = item.N_Operación;
                    dr[3] = item.Producto.Producto;
                    dr[4] = item.SubProducto.SubProducto;
                    dr[5] = item.Moneda;
                    dr[6] = item.Línea_Desembolsos.ToString("#0.#0");
                    dr[7] = item.Plazo;
                    dr[8] = item.Saldo_Ant.ToString("#0.#0");
                    dr[9] = item.Saldo_Act.ToString("#0.#0");
                    dr[10] = item.Canal.Canal;
                    dr[11] = item.NombreCompletoVendedor;
                    dr[12] = item.Estado;
                    dr[13] = item.FechaComision;
                    dr[14] = item.ComisionTarifario.ToString("#0.#0");
                    dr[15] = (item.Tipo == 1 ? item.Tarifario.ToString("#0.#0") + "%" : "S/. " + item.Tarifario.ToString("#0.#0"));
                    dt.Rows.Add(dr);
                }
                gv.DataSource = dt;
                gv.DataBind();
            }
            else if (Session["ListaSolicitudes"] != null)
            {
                ListaSolicitudes = (List<SolicitudEntidad>)Session["ListaSolicitudes"];
                filename = "ListadoSolicitudes.xls";

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Aprobación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cod. Cliente" });
                dt.Columns.Add(new DataColumn { ColumnName = "Operacion" });
                dt.Columns.Add(new DataColumn { ColumnName = "Producto" });
                dt.Columns.Add(new DataColumn { ColumnName = "SubProducto" });
                dt.Columns.Add(new DataColumn { ColumnName = "Moneda" });
                dt.Columns.Add(new DataColumn { ColumnName = "Linea/Prestamo (S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Plazo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Mes Ant." });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Actual." });
                dt.Columns.Add(new DataColumn { ColumnName = "Canal" });
                dt.Columns.Add(new DataColumn { ColumnName = "Vendedor" });
                dt.Columns.Add(new DataColumn { ColumnName = "Estado" });

                foreach (var item in ListaSolicitudes)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Fecha_Aprob_Rech;
                    dr[1] = item.BT_Cliente;
                    dr[2] = item.N_Operación;
                    dr[3] = item.Producto.Producto;
                    dr[4] = item.SubProducto.SubProducto;
                    dr[5] = item.Moneda;
                    dr[6] = item.Línea_Desembolsos.ToString("#0.#0");
                    dr[7] = item.Plazo;
                    dr[8] = item.Saldo_Ant.ToString("#0.#0");
                    dr[9] = item.Saldo_Act.ToString("#0.#0");
                    dr[10] = item.Canal.Canal;
                    dr[11] = item.NombreCompletoVendedor;
                    dr[12] = item.Estado;
                    dt.Rows.Add(dr);
                }
                gv.DataSource = dt;
                gv.DataBind();
            }
            else if (Session["ListaSolicitudesRechazadas"] != null)
            {
                ListaSolicitudes = (List<SolicitudEntidad>)Session["ListaSolicitudesRechazadas"];
                filename = "ListadoSolicitudesRechazadas.xls";

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Aprobación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cod. Cliente" });
                dt.Columns.Add(new DataColumn { ColumnName = "Operacion" });
                dt.Columns.Add(new DataColumn { ColumnName = "Producto" });
                dt.Columns.Add(new DataColumn { ColumnName = "SubProducto" });
                dt.Columns.Add(new DataColumn { ColumnName = "Moneda" });
                dt.Columns.Add(new DataColumn { ColumnName = "Linea/Prestamo (S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Plazo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Mes Ant." });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Actual." });
                dt.Columns.Add(new DataColumn { ColumnName = "Canal" });
                dt.Columns.Add(new DataColumn { ColumnName = "Vendedor" });
                dt.Columns.Add(new DataColumn { ColumnName = "Estado" });

                foreach (var item in ListaSolicitudes)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Fecha_Aprob_Rech;
                    dr[1] = item.BT_Cliente;
                    dr[2] = item.N_Operación;
                    dr[3] = item.Producto.Producto;
                    dr[4] = item.SubProducto.SubProducto;
                    dr[5] = item.Moneda;
                    dr[6] = item.Línea_Desembolsos.ToString("#0.#0");
                    dr[7] = item.Plazo;
                    dr[8] = item.Saldo_Ant.ToString("#0.#0");
                    dr[9] = item.Saldo_Act.ToString("#0.#0");
                    dr[10] = item.Canal.Canal;
                    dr[11] = item.NombreCompletoVendedor;
                    dr[12] = item.Estado;
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