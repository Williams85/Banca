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
    public class CanalDominio
    {
        private CanalRepositorio oCanalRepositorio = new CanalRepositorio();

        #region "Metodos No Transaccionales"

        public List<CanalEntidad> listarActivos()
        {
            return oCanalRepositorio.listarActivos();
        }
        public List<CanalEntidad> obtenerDatosXFiltro(CanalEntidad entidad)
        {
            return oCanalRepositorio.obtenerDatosXFiltro(entidad);
        }
        public CanalEntidad obtenerDatosXCodigo(string codigo)
        {
            return oCanalRepositorio.obtenerDatosXCodigo(codigo);
        }

        #endregion

        #region "Metodos Transaccionales"
        public bool grabarDatos(CanalEntidad entidad, ref string codigo, ref string mensaje)
        {
            bool estado = false;
            if (oCanalRepositorio.validarGrabacionDatos(entidad)) mensaje = Message.MTCanalesExiste;
            else
            {
                if (oCanalRepositorio.grabarDatos(entidad,ref codigo))
                {
                    estado = true;
                    mensaje = Message.MTCanalesGraba;
                }
                else
                    mensaje = Message.MTCanalesNoGraba;
            }

            return estado;
        }

        public bool modificarDatos(CanalEntidad entidad, ref string mensaje)
        {
            bool estado = false;
            if (oCanalRepositorio.validarModificacionDatos(entidad)) mensaje = Message.MTCanalesExiste;
            else
            {
                if (oCanalRepositorio.modificarDatos(entidad))
                {
                    estado = true;
                    mensaje = Message.MTCanalesModifica;
                }
                else
                    mensaje = Message.MTCanalesNoModifica;
            }

            return estado;
        }


        #endregion
    }
}
