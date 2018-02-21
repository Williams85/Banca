using SGCFVIEF.Entidad;
using SGCFVIEF.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Dominio
{
    public class TipoDocumentoDominio
    {
        private TipoDocumentoRepositorio oTipoDocumentoRepositorio = new TipoDocumentoRepositorio();

        #region "Metodos No Transaccionales"

        public List<TipoDocumentoEntidad> listar()
        {
            return oTipoDocumentoRepositorio.listar();
        }

        #endregion
    }
}
