using SGCFVIEF.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Repositorio
{
    public class TipoDocumentoRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<TipoDocumentoEntidad> listar()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_TipoDocumento_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<TipoDocumentoEntidad> ListaTipoDocumentos = new List<TipoDocumentoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoDocumentoEntidad oTipoDocumentoEntidad = new TipoDocumentoEntidad();
                        oTipoDocumentoEntidad.Tipo_Doc = Reader.GetTinyIntValue(reader, "Tipo_Doc");
                        oTipoDocumentoEntidad.Nomb_Doc = Reader.GetStringValue(reader, "Nomb_Doc");
                        ListaTipoDocumentos.Add(oTipoDocumentoEntidad);
                    }
                }
                return ListaTipoDocumentos;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }

        #endregion
    }
}
