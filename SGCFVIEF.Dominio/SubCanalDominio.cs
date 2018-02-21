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
    public class SubCanalDominio
    {
        private SubCanalRepositorio oSubCanalRepositorio = new SubCanalRepositorio();

        #region "Metodos No Transaccionales"

        public List<SubCanalEntidad> listarActivos(string codigo)
        {
            return oSubCanalRepositorio.listarXCanal(codigo);
        }
        public List<SubCanalEntidad> obtenerDatosXFiltro(SubCanalEntidad entidad)
        {
            return oSubCanalRepositorio.obtenerDatosXFiltro(entidad);
        }
        public SubCanalEntidad obtenerDatosXCodigo(string codigo)
        {
            return oSubCanalRepositorio.obtenerDatosXCodigo(codigo);
        }

        #endregion

        #region "Metodos Transaccionales"
        public bool grabarDatos(SubCanalEntidad entidad, ref string codigo, ref string mensaje)
        {
            bool estado = false;
            if (oSubCanalRepositorio.validarGrabacionDatos(entidad)) mensaje = Message.MTSubCanalesExiste;
            else
            {
                if (oSubCanalRepositorio.grabarDatos(entidad, ref codigo))
                {
                    estado = true;
                    mensaje = Message.MTSubCanalesGraba;
                }
                else
                    mensaje = Message.MTSubCanalesNoGraba;
            }

            return estado;
        }

        public bool modificarDatos(SubCanalEntidad entidad, ref string mensaje)
        {
            bool estado = false;
            if (oSubCanalRepositorio.validarModificacionDatos(entidad)) mensaje = Message.MTSubCanalesExiste;
            else
            {
                if (oSubCanalRepositorio.modificarDatos(entidad))
                {
                    estado = true;
                    mensaje = Message.MTSubCanalesModifica;
                }
                else
                    mensaje = Message.MTSubCanalesNoModifica;
            }

            return estado;
        }


        #endregion
    }
}
