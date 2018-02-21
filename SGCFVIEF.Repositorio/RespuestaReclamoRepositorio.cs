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
    public class RespuestaReclamoRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<RespuestaReclamoEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_RespuestaReclamo_listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<RespuestaReclamoEntidad> ListaRespuestaReclamo = new List<RespuestaReclamoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RespuestaReclamoEntidad oRespuestaReclamoEntidad = new RespuestaReclamoEntidad();
                        oRespuestaReclamoEntidad.Cod_RespuestaReclamo = Reader.GetStringValue(reader, "Cod_RespuestaReclamo");
                        oRespuestaReclamoEntidad.Nom_RespuestaReclamo = Reader.GetStringValue(reader, "Nom_RespuestaReclamo");
                        ListaRespuestaReclamo.Add(oRespuestaReclamoEntidad);
                    }
                }
                return ListaRespuestaReclamo;
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
