using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class SolicitudDominio
    {
        SolicitudRepositorio oSolicitudRepositorio = new SolicitudRepositorio();
        #region "Metodos No Transaccionales"

        public bool ValidarVigenciaReclamo(string N_Solicitud)
        {
            return oSolicitudRepositorio.ValidarVigenciaReclamo(N_Solicitud);
        }

        public List<SolicitudEntidad> FiltrarActivas(SolicitudEntidad entidad)
        {
            return oSolicitudRepositorio.FiltrarActivas(entidad);
        }

        public List<SolicitudEntidad> FiltrarforReclamos(SolicitudEntidad entidad)
        {
            return oSolicitudRepositorio.FiltrarforReclamos(entidad);
        }

        public List<SolicitudEntidad> FiltrarRechazados(SolicitudEntidad entidad)
        {
            return oSolicitudRepositorio.FiltrarRechazados(entidad);    
        }

        public List<SolicitudEntidad> ReporteSolicitudesAprobadas(SolicitudEntidad entidad)
        {
            return oSolicitudRepositorio.ReporteSolicitudesAprobadas(entidad);
        }
        public bool GrabarReporteSolicitudesAprobadas(List<SolicitudEntidad> Lista, ref string codigo)
        {
            return oSolicitudRepositorio.GrabarReporteSolicitudesAprobadas(Lista,ref codigo);
        }

        public List<SolicitudEntidad> CalcularComisiones(SolicitudEntidad entidad)
        {
            return oSolicitudRepositorio.CalcularComisiones(entidad);    
           
        }
        #endregion

        #region "Metodos Transaccionales"
        public bool Activar(SolicitudEntidad entidad)
        {
            return oSolicitudRepositorio.Activar(entidad);    
        }
        #endregion
    }
}
