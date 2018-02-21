using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class SubProductoDominio
    {
        private SubProductoRepositorio oSubProductoRepositorio = new SubProductoRepositorio();
        #region "Metodos No Transaccionales"
        public List<SubProductoEntidad> FiltrarxProducto(string codigo)
        {
            return oSubProductoRepositorio.FiltrarxProducto(codigo);
        }
        #endregion
    }
}
