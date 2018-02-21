using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class ProvinciaDominio
    {
        private ProvinciaRepositorio oProvinciaRepositorio = new ProvinciaRepositorio();

        #region "Metodos No Transaccionales"

        public List<ProvinciaEntidad> listar(string Cod_Region)
        {
            return oProvinciaRepositorio.listar(Cod_Region);
        }

        #endregion
    }
}
