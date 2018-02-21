using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class DistritoDominio
    {
        private DistritoRepositorio oDistritoRepositorio = new DistritoRepositorio();

        #region "Metodos No Transaccionales"

        public List<DistritoEntidad> listar(string Cod_Provincia)
        {
            return oDistritoRepositorio.listar(Cod_Provincia);
        }

        #endregion
    }
}
