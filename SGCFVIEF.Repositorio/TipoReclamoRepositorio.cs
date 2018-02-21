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
    public class TipoReclamoRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<TipoReclamoEntidad> listar()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_TipoReclamo_listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<TipoReclamoEntidad> ListaTipoReclamo = new List<TipoReclamoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoReclamoEntidad oTipoReclamoEntidad = new TipoReclamoEntidad();
                        oTipoReclamoEntidad.Cod_TipoReclamo = Reader.GetTinyIntValue(reader, "Cod_TipoReclamo");
                        oTipoReclamoEntidad.Nom_TipoReclamo = Reader.GetStringValue(reader, "Nom_TipoReclamo");
                        ListaTipoReclamo.Add(oTipoReclamoEntidad);
                    }
                }
                return ListaTipoReclamo;
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
