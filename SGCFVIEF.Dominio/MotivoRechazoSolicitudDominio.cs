using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class MotivoRechazoSolicitudDominio
    {
        private MotivoRechazoSolicitudRepositorio oMotivoRechazoSolicitudRepositorio = new MotivoRechazoSolicitudRepositorio();

        #region "Metodos No Transaccionales"

        public List<MotivoRechazoSolicitudEntidad> listarActivos()
        {
            return oMotivoRechazoSolicitudRepositorio.listarActivos();
        }

        #endregion
    }
}
