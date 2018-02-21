using SGCFVIEF.Dominio;
using SGCFVIEF.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SGCFVIEF.Presentacion.Controllers
{
    public class HomeController : Controller
    {
        [Authentication]
        public ActionResult Index()
        {
            return View();
        }

        [Authentication]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authentication]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Login(string Login, string Password)
        {
            if (ModelState.IsValid && ((Login != null || Login != "") || (Password != null || Password != "")))
            {
                UsuarioDominio oUsuarioDominio = new UsuarioDominio();
                var oUsuarioEntidad = oUsuarioDominio.ValidarUsuario(new UsuarioEntidad
                {
                    Nom_Usuario = Login,
                    Pass_Usuario = Password

                });
                if (oUsuarioEntidad != null)
                {
                    FormsAuthentication.SetAuthCookie(Login, false);
                    SessionManager.Usuario = oUsuarioEntidad;
                    return RedirectToAction("Index", "Home");
                }
                else
                    return Redirect("~/Login/Login.aspx");

            }
            else
                return Redirect("~/Login/Login.aspx");
        }

        public ActionResult Autenticar()
        {
            return Redirect("~/Login/Login.aspx");
        }

        [Authentication]
        [HttpPost]
        public ActionResult BuscarProvincias(string Cod_Region)
        {
            ProvinciaDominio oProvinciaDominio = new ProvinciaDominio();
            var ListaProvincias = oProvinciaDominio.listar(Cod_Region);
            return Json(ListaProvincias, JsonRequestBehavior.AllowGet);
        }

        [Authentication]
        public ActionResult BuscarDistritos(string Cod_Provincia)
        {
            DistritoDominio oDistritoDominio = new DistritoDominio();
            var ListaDistritos = oDistritoDominio.listar(Cod_Provincia);
            return Json(ListaDistritos, JsonRequestBehavior.AllowGet);
        }

        [Authentication]
        public ActionResult End()
        {
            Session.Abandon();
            return Redirect("~/Login/Login.aspx");
        }

    }
}