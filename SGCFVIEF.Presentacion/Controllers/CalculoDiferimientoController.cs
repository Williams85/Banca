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
    public class CalculoDiferimientoController : Controller
    {
        // GET: CalculoDiferimiento
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
        public ActionResult BuscarPagoComsiones(PagoComisionEntidad entidad)
        {
            PagoComisionDominio oPagoComisionDominio = new PagoComisionDominio();
            var ListaPagoComisiones = oPagoComisionDominio.FiltrarActivas(entidad);
            Session["ListaPagoDiferidos"] = null;
            Session["ListaPagoComisiones"] = ListaPagoComisiones;
            return PartialView("_ResultadosBusqueda", ListaPagoComisiones);
        }

        [HttpPost]
        public ActionResult BuscarPagosDiferidos(PagoComisionEntidad entidad)
        {
            PagoComisionDominio oPagoComisionDominio = new PagoComisionDominio();
            var ListaPagoDiferidos = oPagoComisionDominio.FiltrarDiferidos(entidad);
            Session["ListaPagoDiferidos"] = ListaPagoDiferidos;
            Session["ListaPagoComisiones"] = null;
            return PartialView("_ResultadosBusquedaDiferidos", ListaPagoDiferidos);
        }

        [HttpPost]
        public ActionResult GrabarDiferimientos()
        {
            PagoComisionDominio oPagoComisionDominio = new PagoComisionDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            var Lista = (List<PagoComisionEntidad>)Session["ListaPagoComisiones"];
            string Codigo = "";
            oResponseWeb.Estado = oPagoComisionDominio.grabarDiferido(Lista, ref Codigo);
            oResponseWeb.Valor = Codigo;
            return Json(oResponseWeb);
        }

        public ActionResult GenerarExcel()
        {
            List<PagoComisionEntidad> ListaPagoComisiones = new List<PagoComisionEntidad>();
            List<PagoComisionEntidad> ListaPagoDiferidos = new List<PagoComisionEntidad>();
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
            else if (Session["ListaPagoDiferidos"] != null)
            {
                ListaPagoDiferidos = (List<PagoComisionEntidad>)Session["ListaPagoDiferidos"];
                filename = "ListadoPagosDiferidos.xls";
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn { ColumnName = "Cod. Diferimiento" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Aprobación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cod. Cliente" });
                dt.Columns.Add(new DataColumn { ColumnName = "Operación" });
                dt.Columns.Add(new DataColumn { ColumnName = "Producto" });
                dt.Columns.Add(new DataColumn { ColumnName = "SubProducto" });
                dt.Columns.Add(new DataColumn { ColumnName = "Plazo" });
                dt.Columns.Add(new DataColumn { ColumnName = "Canal" });
                dt.Columns.Add(new DataColumn { ColumnName = "SubCanal" });
                dt.Columns.Add(new DataColumn { ColumnName = "Vendedor" });
                dt.Columns.Add(new DataColumn { ColumnName = "Estado Solic." });
                dt.Columns.Add(new DataColumn { ColumnName = "Moneda" });
                dt.Columns.Add(new DataColumn { ColumnName = "Importe (S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Comisión" });
                dt.Columns.Add(new DataColumn { ColumnName = "Comisión (S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Tarifa Comisión" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cuenta Comtable 1" });
                dt.Columns.Add(new DataColumn { ColumnName = "Fecha Diferido" });
                dt.Columns.Add(new DataColumn { ColumnName = "Monto Diferido(S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Pendiente(S/.)" });
                dt.Columns.Add(new DataColumn { ColumnName = "Saldo Pagado" });
                dt.Columns.Add(new DataColumn { ColumnName = "Cuenta Contable 2" });
                foreach (var item in ListaPagoDiferidos)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = item.Cod_Diferido;
                    dr[1] = item.Fecha_Aprob_Rech;
                    dr[2] = item.BT_Cliente;
                    dr[3] = item.N_Operación;
                    dr[4] = item.Producto.Producto;
                    dr[5] = item.SubProducto.SubProducto;
                    dr[6] = item.Plazo;
                    dr[7] = item.Canal.Canal;
                    dr[8] = item.Subcanal.SubCanal;
                    dr[9] = item.NombreCompletoVendedor;
                    dr[10] = item.Solicitud.Estado;
                    dr[11] = item.Moneda;
                    dr[12] = item.Línea_Desembolsos.ToString("###,##0.#0");
                    dr[13] = item.FechaComision;
                    dr[14] = item.MontoComision.ToString("###,##0.#0");
                    dr[15] = (item.TipoTarifa == 1 ? item.Tarifario.ToString("#0.#0") + "%" : "S/. " + item.Tarifario.ToString("#0.#0"));
                    dr[16] = item.CuentaContable1;
                    dr[17] = item.FechaDiferido;
                    dr[18] = item.MontoDiferido.ToString("###,##0.#0");
                    dr[19] = item.SaldoPendiente.ToString("###,##0.#0");
                    dr[20] = item.SaldoPagado.ToString("###,##0.#0");
                    dr[21] = item.CuentaContable2;
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