using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using SGCFVIEF.Utilitario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class TarifarioDominio
    {
        private TarifarioRepositorio oTarifarioRepositorio = new TarifarioRepositorio();

        #region "Metodos No Transaccionales"

        public List<TarifarioEntidad> obtenerDatosXFiltro(TarifarioEntidad entidad)
        {
            return oTarifarioRepositorio.obtenerDatosXFiltro(entidad);
        }
        public TarifarioEntidad obtenerDatosXCodigo(string codigo)
        {
            return oTarifarioRepositorio.obtenerDatosXCodigo(codigo);
        }

        #endregion

        #region "Metodos Transaccionales"
        public bool grabarDatos(TarifarioEntidad entidad, ref string mensaje)
        {
            bool estado = false;
            if (oTarifarioRepositorio.grabarDatos(entidad))
            {
                estado = true;
                mensaje = Message.MTTarifarioGraba;
            }
            //if (oTarifarioRepositorio.validarGrabacionDatos(entidad)) mensaje = Message.MTTarifarioExiste;
            //else
            //{
            //    if (oTarifarioRepositorio.grabarDatos(entidad))
            //    {
            //        estado = true;
            //        mensaje = Message.MTTarifarioGraba;
            //    }
            //    else
            //        mensaje = Message.MTTarifarioNoGraba;
            //}

            return estado;
        }

        public bool modificarDatos(TarifarioEntidad entidad, ref string mensaje)
        {
            bool estado = false;
            if (oTarifarioRepositorio.modificarDatos(entidad))
            {
                estado = true;
                mensaje = Message.MTTarifarioModifica;
            }
            //if (oTarifarioRepositorio.validarModificacionDatos(entidad)) mensaje = Message.MTTarifarioExiste;
            //else
            //{
            //    if (oTarifarioRepositorio.modificarDatos(entidad))
            //    {
            //        estado = true;
            //        mensaje = Message.MTTarifarioModifica;
            //    }
            //    else
            //        mensaje = Message.MTTarifarioNoModifica;
            //}

            return estado;
        }


        #endregion
    }
}
