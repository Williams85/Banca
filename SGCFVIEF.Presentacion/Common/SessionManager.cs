using SGCFVIEF.Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace SGCFVIEF.Presentacion
{
    public class SessionManager
    {
        public static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        #region "Atributos"
        const string _Usuario = "Usuario";
        const string _ListaVendedores = "ListaVendedores";
        #endregion

        #region "Propiedades"
        public static UsuarioEntidad Usuario
        {
            get { return (UsuarioEntidad)Session[_Usuario]; }
            set { Session[_Usuario] = value; }
        }

        public static List<VendedorEntidad> ListaVendedores
        {
            get { return (List<VendedorEntidad>)Session[_ListaVendedores]; }
            set { Session[_ListaVendedores] = value; }
        }


        #endregion
    }
}