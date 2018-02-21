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
    public class VendedorDominio
    {
        private VendedorRepositorio oVendedorRepositorio = new VendedorRepositorio();

        #region "Metodos No Transaccionales"
        public List<VendedorEntidad> listar()
        {
            return oVendedorRepositorio.listar();
        }
        public List<VendedorEntidad> obtenerDatosXFiltro(VendedorEntidad entidad)
        {
            return oVendedorRepositorio.obtenerDatosXFiltro(entidad);
        }
        public VendedorEntidad obtenerDatosXCodigo(string codigo)
        {
            return oVendedorRepositorio.obtenerDatosXCodigo(codigo);
        }

        #endregion

        #region "Metodos Transaccionales"
        public bool grabarDatos(VendedorEntidad entidad, ref string codigo, ref string mensaje)
        {
            bool estado = false;
            if (oVendedorRepositorio.validarGrabacionDatos(entidad)) mensaje = Message.MTVendedorExiste;
            else
            {
                if (oVendedorRepositorio.grabarDatos(entidad, ref codigo))
                {
                    estado = true;
                    mensaje = Message.MTVendedorGraba;
                }
                else
                    mensaje = Message.MTVendedorNoGraba;
            }

            return estado;
        }

        public bool modificarDatos(VendedorEntidad entidad, ref string mensaje)
        {
            bool estado = false;
            if (oVendedorRepositorio.validarModificacionDatos(entidad)) mensaje = Message.MTVendedorExiste;
            else
            {
                if (oVendedorRepositorio.modificarDatos(entidad))
                {
                    estado = true;
                    mensaje = Message.MTVendedorModifica;
                }
                else
                    mensaje = Message.MTVendedorNoModifica;
            }

            return estado;
        }


        #endregion
    }
}
