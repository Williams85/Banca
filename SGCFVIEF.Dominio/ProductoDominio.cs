using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class ProductoDominio
    {
        private ProductoRepositorio oProductoRepositorio = new ProductoRepositorio();

        #region "Metodos No Transaccionales"

        public List<ProductoEntidad> listarActivos()
        {
            return oProductoRepositorio.listarActivos();
        }


        #endregion
    }
}
