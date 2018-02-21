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
    public class MotivoRechazoSolicitudRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<MotivoRechazoSolicitudEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_MotivoRechazoSolicitud_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<MotivoRechazoSolicitudEntidad> ListaMotivosRechazoSolicitud = new List<MotivoRechazoSolicitudEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MotivoRechazoSolicitudEntidad oMotivoRechazoSolicitudEntidad = new MotivoRechazoSolicitudEntidad();
                        oMotivoRechazoSolicitudEntidad.Cod_MotivoRechazoSolicitud = Reader.GetStringValue(reader, "Cod_MotivoRechazoSolicitud");
                        oMotivoRechazoSolicitudEntidad.Nom_MotivoRechazoSolicitud = Reader.GetStringValue(reader, "Nom_MotivoRechazoSolicitud");
                        ListaMotivosRechazoSolicitud.Add(oMotivoRechazoSolicitudEntidad);
                    }
                }
                return ListaMotivosRechazoSolicitud;
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
