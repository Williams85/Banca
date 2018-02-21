using SGCFVIEF.Dominio;
using SGCFVIEF.Entidad;
using SGCFVIEF.Utilitario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGCFVIEF.Presentacion.Controllers
{
    [Authentication]
    public class TarifaController : Controller
    {
        public ActionResult Index()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            var ListaCanales = oCanalDominio.listarActivos();
            var ListSubaCanales = oSubCanalDominio.listarActivos(ListaCanales[0].Cod_Canal);
            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaSubCanales = ListSubaCanales;
            return View();
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            ProductoDominio oProductoDominio = new ProductoDominio();
            SubProductoDominio oSubProductoDominio = new SubProductoDominio();
            var ListaCanales = oCanalDominio.listarActivos();
            var ListSubaCanales = oSubCanalDominio.listarActivos(ListaCanales[0].Cod_Canal);
            var ListaProductos = oProductoDominio.listarActivos();
            var ListaSubProductos = oSubProductoDominio.FiltrarxProducto(ListaProductos[0].Cod_Producto);
            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaSubCanales = ListSubaCanales;
            ViewBag.ListaProductos = ListaProductos;
            ViewBag.ListaSubProductos = ListaSubProductos;
            return View();
        }

        [HttpPost]
        public ActionResult Edicion(string Codigo)
        {
            TarifarioDominio oTarifarioDominio = new TarifarioDominio();
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            ProductoDominio oProductoDominio = new ProductoDominio();
            SubProductoDominio oSubProductoDominio = new SubProductoDominio();
            TarifarioEntidad entidad = new TarifarioEntidad();
            if (Codigo == null || Codigo == "") return RedirectToAction("Index", "Tarifa");
            entidad = oTarifarioDominio.obtenerDatosXCodigo(Codigo);
            var ListaCanales = oCanalDominio.listarActivos();
            var ListSubCanales = oSubCanalDominio.listarActivos(entidad.Canal.Cod_Canal);
            var ListaProductos = oProductoDominio.listarActivos();
            var ListaSubProductos = oSubProductoDominio.FiltrarxProducto(entidad.Producto.Cod_Producto);
            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListSubCanales = ListSubCanales;
            ViewBag.ListaProductos = ListaProductos;
            ViewBag.ListaSubProductos = ListaSubProductos;

            return View(entidad);
        }


        [HttpPost]
        public ActionResult Buscar(TarifarioEntidad entidad)
        {
            TarifarioDominio oTarifarioDominio = new TarifarioDominio();
            var ListaTarifario = oTarifarioDominio.obtenerDatosXFiltro(entidad);
            return PartialView("_ResultadosBusqueda", ListaTarifario);
        }


        [HttpPost]
        public ActionResult Grabar(TarifarioEntidad entidad)
        {
            TarifarioDominio oTarifarioDominio = new TarifarioDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            string Mensaje = string.Empty;
            entidad.Usuario = new UsuarioEntidad
            {
                Cod_Usuario = SessionManager.Usuario.Cod_Usuario,
            };
            oResponseWeb.Estado = oTarifarioDominio.grabarDatos(entidad,  ref Mensaje);
            oResponseWeb.Message = Mensaje;
            return Json(oResponseWeb);
        }


        [HttpPost]
        public ActionResult Modificar(TarifarioEntidad entidad)
        {
            TarifarioDominio oTarifarioDominio = new TarifarioDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            string Mensaje = string.Empty;
            entidad.Usuario = new UsuarioEntidad
            {
                Cod_Usuario = SessionManager.Usuario.Cod_Usuario,
            };
            oResponseWeb.Estado = oTarifarioDominio.modificarDatos(entidad, ref Mensaje);
            oResponseWeb.Message = Mensaje;
            return Json(oResponseWeb);
        }

        [HttpPost]
        public ActionResult BuscarSubProductos(string Codigo)
        {
            SubProductoDominio oSubProductoDominio = new SubProductoDominio();
            var ListaSubProductos = oSubProductoDominio.FiltrarxProducto(Codigo);
            return Json(ListaSubProductos, JsonRequestBehavior.AllowGet);
        }
    }
}