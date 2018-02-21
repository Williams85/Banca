using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class TipoReclamoDominio
    {
        TipoReclamoRepositorio oTipoReclamoRepositorio = new TipoReclamoRepositorio();

        #region "Metodos No Transaccionales"
        public List<TipoReclamoEntidad> listar()
        {
            return oTipoReclamoRepositorio.listar();
        }
        #endregion
    }
}
