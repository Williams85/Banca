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
    public class CanalController : Controller
    {
        // GET: Canal
        [HttpGet]
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
            RegionDominio oRegionDominio = new RegionDominio();
            ProvinciaDominio oProvinciaDominio = new ProvinciaDominio();
            DistritoDominio oDistritoDominio = new DistritoDominio();
            var ListaRegiones = oRegionDominio.listar();
            ViewBag.ListaRegiones = ListaRegiones;
            var ListaProvincias = oProvinciaDominio.listar(ViewBag.ListaRegiones[0].Cod_Region);
            ViewBag.ListaProvincias = ListaProvincias;
            var ListaDistritos = oDistritoDominio.listar(ViewBag.ListaProvincias[0].Cod_Provincia);
            ViewBag.ListaDistritos = ListaDistritos;

            return View();
        }

        [HttpPost]
        public ActionResult Edicion(string Codigo)
        {
            CanalEntidad entidad = new CanalEntidad();
            if (Codigo == null || Codigo == "") return RedirectToAction("Index", "Canal");
            RegionDominio oRegionDominio = new RegionDominio();
            ProvinciaDominio oProvinciaDominio = new ProvinciaDominio();
            DistritoDominio oDistritoDominio = new DistritoDominio();

            CanalDominio oCanalDominio = new CanalDominio();
            entidad = oCanalDominio.obtenerDatosXCodigo(Codigo);
            var ListaRegiones = oRegionDominio.listar();
            ViewBag.ListaRegiones = ListaRegiones;
            var ListaProvincias = oProvinciaDominio.listar(entidad.Region.Cod_Region);
            ViewBag.ListaProvincias = ListaProvincias;
            var ListaDistritos = oDistritoDominio.listar(entidad.Provincia.Cod_Provincia);
            ViewBag.ListaDistritos = ListaDistritos;
            return View(entidad);

        }


        [HttpPost]
        public ActionResult Buscar(CanalEntidad entidad)
        {
            CanalDominio oCanalDominio = new CanalDominio();
            var ListaCanales = oCanalDominio.obtenerDatosXFiltro(entidad);
            return PartialView("_ResultadosBusqueda", ListaCanales);
        }


        [HttpPost]
        public ActionResult Grabar(CanalEntidad entidad)
        {
            CanalDominio oCanalDominio = new CanalDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            entidad.Usuario = new UsuarioEntidad
            {
                Cod_Usuario = 1,
            };
            string Mensaje = string.Empty;
            string Codigo = string.Empty;
            oResponseWeb.Estado = oCanalDominio.grabarDatos(entidad,ref Codigo, ref Mensaje);
            oResponseWeb.Message = Mensaje;
            oResponseWeb.Valor = Codigo;

            return Json(oResponseWeb);
        }


        [HttpPost]
        public ActionResult Modificar(CanalEntidad entidad)
        {
            CanalDominio oCanalDominio = new CanalDominio();
            ResponseWeb<string> oResponseWeb = new ResponseWeb<string>();
            entidad.Usuario = new UsuarioEntidad
            {
                Cod_Usuario = 1,
            };
            string Mensaje = string.Empty;
            oResponseWeb.Estado = oCanalDominio.modificarDatos(entidad, ref Mensaje);
            oResponseWeb.Message = Mensaje;

            return Json(oResponseWeb);
        }

    }
}