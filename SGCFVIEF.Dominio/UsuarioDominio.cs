using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class UsuarioDominio
    {
        private UsuarioRepositorio oUsuarioRepositorio = new UsuarioRepositorio();

        #region "Metodos No Transaccionales"
        public UsuarioEntidad ValidarUsuario(UsuarioEntidad entidad)
        {
            return oUsuarioRepositorio.ValidarUsuario(entidad);
        }
        #endregion
    }
}
