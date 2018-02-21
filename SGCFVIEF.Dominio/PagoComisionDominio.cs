using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class PagoComisionDominio
    {
        PagoComisionRepositorio oPagoComisionRepositorio = new PagoComisionRepositorio();
        #region "Metodos No Transaccionales"
        public List<PagoComisionEntidad> FiltrarActivas(PagoComisionEntidad entidad)
        {
            return oPagoComisionRepositorio.FiltrarActivas(entidad);

        }
        public List<PagoComisionEntidad> FiltrarDiferidos(PagoComisionEntidad entidad)
        {
            return oPagoComisionRepositorio.FiltrarDiferidos(entidad);

        }
        public List<PagoComisionEntidad> ReporteComisiones(PagoComisionEntidad entidad)
        {
            return oPagoComisionRepositorio.ReporteComisiones(entidad);

        }

        #endregion

        #region "Metodos Transaccionales"
        public bool GrabarReporteComisiones(List<PagoComisionEntidad> lista, ref string Codigo)
        {
            return oPagoComisionRepositorio.GrabarReporteComisiones(lista, ref Codigo);
        }
        public bool grabarDatos(List<SolicitudEntidad> lista, ref string Cod_Comision)
        {
            return oPagoComisionRepositorio.grabarDatos(lista, ref Cod_Comision);
        }

        public bool grabarDiferido(List<PagoComisionEntidad> lista, ref string Cod_Diferido)
        {
            return oPagoComisionRepositorio.grabarDiferido(lista, ref Cod_Diferido);
        }

        #endregion
    }
}
