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
    public class DistritoRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<DistritoEntidad> listar(string Cod_Provincia)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Distrito_Listar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Provincia", SqlDbType.VarChar, 6)).Value = Cod_Provincia;
                cmd.CommandType = CommandType.StoredProcedure;
                List<DistritoEntidad> ListaDistritos = new List<DistritoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DistritoEntidad oDistritoEntidad = new DistritoEntidad();
                        oDistritoEntidad.Cod_Distrito = Reader.GetStringValue(reader, "Cod_Distrito");
                        oDistritoEntidad.Descripcion = Reader.GetStringValue(reader, "Descripcion");
                        oDistritoEntidad.Provincia = new ProvinciaEntidad
                        {
                            Cod_Provincia= Reader.GetStringValue(reader, "Cod_Provincia"),
                        };
                        ListaDistritos.Add(oDistritoEntidad);
                    }
                }
                return ListaDistritos;
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
