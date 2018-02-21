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
    public class ProvinciaRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<ProvinciaEntidad> listar(string Cod_Region)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Provincia_Listar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Region", SqlDbType.VarChar, 6)).Value = Cod_Region;
                cmd.CommandType = CommandType.StoredProcedure;
                List<ProvinciaEntidad> ListaProvincias = new List<ProvinciaEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProvinciaEntidad oProvinciaEntidad = new ProvinciaEntidad();
                        oProvinciaEntidad.Cod_Provincia = Reader.GetStringValue(reader, "Cod_Provincia");
                        oProvinciaEntidad.Descripcion = Reader.GetStringValue(reader, "Descripcion");
                        oProvinciaEntidad.Region = new RegionEntidad
                        {
                            Cod_Region = Reader.GetStringValue(reader, "Cod_Region"),
                        };
                        ListaProvincias.Add(oProvinciaEntidad);
                    }
                }
                return ListaProvincias;
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
