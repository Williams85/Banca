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
    public class RegionRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<RegionEntidad> listar()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Region_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<RegionEntidad> ListaRegiones = new List<RegionEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RegionEntidad oRegionEntidad = new RegionEntidad();
                        oRegionEntidad.Cod_Region = Reader.GetStringValue(reader, "Cod_Region");
                        oRegionEntidad.Descripcion = Reader.GetStringValue(reader, "Descripcion");
                        ListaRegiones.Add(oRegionEntidad);
                    }
                }
                return ListaRegiones;
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
