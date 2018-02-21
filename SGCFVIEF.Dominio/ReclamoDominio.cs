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
    public class ReclamoDominio
    {
        ReclamoRepositorio oReclamoRepositorio = new ReclamoRepositorio();

        #region "Metodos No Transaccionales"

        public ReclamoEntidad FiltrarxCodigo(string codigo)
        {
            return oReclamoRepositorio.FiltrarxCodigo(codigo);
        }
        public ReclamoEntidad ObtenerxSolicitud(string codigo)
        {
            return oReclamoRepositorio.ObtenerxSolicitud(codigo);
        }

        public List<ReclamoEntidad> obtenerDatosXFiltro(ReclamoEntidad entidad)
        {
            return oReclamoRepositorio.obtenerDatosXFiltro(entidad);
        }




        #endregion

        #region "Metodos Transaccionales"
        public bool grabarDatos(ReclamoEntidad entidad, ref string codigo, ref string mensaje)
        {
            bool estado = false;

            if (oReclamoRepositorio.grabarDatos(entidad, ref codigo))
            {
                estado = true;
                mensaje = Message.MTReclamoGraba;
            }
            else
                mensaje = Message.MTReclamoNoGraba;
            return estado;

        }
        public bool grabarRespuesta(ReclamoEntidad entidad, ref string mensaje)
        {
            bool estado = false;

            if (oReclamoRepositorio.grabarRespuesta(entidad))
            {
                estado = true;
                mensaje = Message.MTReclamoGrabaRespuesta;
            }
            else
                mensaje = Message.MTReclamoNoGrabaRespuesta;
            return estado;

        }

        public bool modificarDatos(ReclamoEntidad entidad, ref string mensaje)
        {
            bool estado = false;

            if (oReclamoRepositorio.modificarDatos(entidad))
            {
                estado = true;
                mensaje = Message.MTReclamoModifica;
            }
            else
                mensaje = Message.MTReclamoNoModifica;
            return estado;
        }
        #endregion
    }
}
