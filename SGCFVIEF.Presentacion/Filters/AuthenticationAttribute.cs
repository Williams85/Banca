using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGCFVIEF.Presentacion
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SessionManager.Usuario == null)
            {
                // Si la información es nula, redireccionar a 
                // página de error u otra página deseada.
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                    { "Controller", "Home" },
                    { "Action", "Autenticar" },

            });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}