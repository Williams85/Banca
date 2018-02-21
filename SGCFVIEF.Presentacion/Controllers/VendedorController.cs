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
    public class VendedorController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            TipoDocumentoDominio oTipoDocumentoDominio = new TipoDocumentoDominio();
            var ListaTipoDocumentos = oTipoDocumentoDominio.listar();
            var ListaCanales = oCanalDominio.listarActivos();
            var ListSubaCanales = oSubCanalDominio.listarActivos(ListaCanales[0].Cod_Canal);
            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaSubCanales = ListSubaCanales;
            ViewBag.ListaTipoDocumentos = ListaTipoDocumentos;
            return View();
        }

        [HttpGet]
        public ActionResult Nuevo()
        {
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            TipoDocumentoDominio oTipoDocumentoDominio = new TipoDocumentoDominio();
            RegionDominio oRegionDominio = new RegionDominio();
            ProvinciaDominio oProvinciaDominio = new ProvinciaDominio();
            DistritoDominio oDistritoDominio = new DistritoDominio();

            var ListaTipoDocumentos = oTipoDocumentoDominio.listar();
            var ListaCanales = oCanalDominio.listarActivos();
            var ListSubaCanales = oSubCanalDominio.listarActivos(ListaCanales[0].Cod_Canal);
            var ListaRegiones = oRegionDominio.listar();
            var ListaProvincias = oProvinciaDominio.listar(ListaRegiones[0].Cod_Region);
            var ListaDistritos = oDistritoDominio.listar(ListaProvincias[0].Cod_Provincia);

            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaSubCanales = ListSubaCanales;
            ViewBag.ListaTipoDocumentos = ListaTipoDocumentos;
            ViewBag.ListaRegiones = ListaRegiones;
            ViewBag.ListaProvincias = ListaProvincias;
            ViewBag.ListaDistritos = ListaDistritos;
            return View();
        }

        [HttpPost]
        public ActionResult Edicion(string Codigo)
        {
            VendedorEntidad entidad = new VendedorEntidad();
            if (Codigo == null || Codigo == "") return RedirectToAction("Index", "Vendedor");
            CanalDominio oCanalDominio = new CanalDominio();
            SubCanalDominio oSubCanalDominio = new SubCanalDominio();
            TipoDocumentoDominio oTipoDocumentoDominio = new TipoDocumentoDominio();
            RegionDominio oRegionDominio = new RegionDominio();
            ProvinciaDominio oProvinciaDominio = new ProvinciaDominio();
            DistritoDominio oDistritoDominio = new DistritoDominio();
            VendedorDominio oVendedorDominio = new VendedorDominio();
            entidad = oVendedorDominio.obtenerDatosXCodigo(Codigo);
            var ListaTipoDocumentos = oTipoDocumentoDominio.listar();
            var ListaCanales = oCanalDominio.listarActivos();
            var ListSubaCanales = oSubCanalDominio.listarActivos(entidad.Canal.Cod_Canal);
            var ListaRegiones = oRegionDominio.listar();


            ViewBag.ListaCanales = ListaCanales;
            ViewBag.ListaSubCanales = ListSubaCanales;
            ViewBag.ListaTipoDocumentos = ListaTipoDocumentos;
            ViewBag.ListaRegiones = ListaRegiones;
            var ListaProvincias = oProvinciaDominio.listar(entidad.Region.Cod_Region);
            ViewBag.ListaProvincias = ListaProvincias;
            var ListaDistritos = oDistritoDominio.listar(entidad.Provincia.Cod_Provincia);
            ViewBag.ListaDistritos = ListaDistritos;
            return View(entidad);

        }


        [HttpPost]
        public ActionResult Buscar(VendedorEntidad entidad)
        {
            VendedorDominio oVendedorDominio = new VendedorDominio();
            var ListaVendedores = oVendedorDominio.obtenerDatosXFiltro(entidad);
            return PartialView("_ResultadosBusqueda", ListaVendedores);
        }


        [HttpPost]
        public ActionResult Grabar(VendedorEntidad entidad)
        {
            VendedorDominio oVendedorDominio = new VendedorDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            entidad.Usuario = new UsuarioEntidad
            {
                Cod_Usuario = 1,
            };
            string Mensaje = string.Empty;
            string Codigo = string.Empty;
            oResponseWeb.Estado = oVendedorDominio.grabarDatos(entidad, ref Codigo, ref Mensaje);
            oResponseWeb.Message = Mensaje;
            oResponseWeb.Valor = Codigo;

            return Json(oResponseWeb);
        }


        [HttpPost]
        public ActionResult Modificar(VendedorEntidad entidad)
        {
            VendedorDominio oVendedorDominio = new VendedorDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            entidad.Usuario = new UsuarioEntidad
            {
                Cod_Usuario = 1,
            };
            string Mensaje = string.Empty;
            oResponseWeb.Estado = oVendedorDominio.modificarDatos(entidad, ref Mensaje);
            oResponseWeb.Message = Mensaje;

            return Json(oResponseWeb);
        }
    }
}