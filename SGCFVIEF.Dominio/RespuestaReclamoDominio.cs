using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class RespuestaReclamoDominio
    {
        private RespuestaReclamoRepositorio oRespuestaReclamoRepositorio = new RespuestaReclamoRepositorio();

        #region "Metodos No Transaccionales"

        public List<RespuestaReclamoEntidad> listarActivos()
        {
            return oRespuestaReclamoRepositorio.listarActivos();
        }

        #endregion
    }
}
