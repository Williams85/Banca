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
    public class SubProductoRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<SubProductoEntidad> FiltrarxProducto(string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_SubProducto_FiltrarxProducto", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = codigo;
                cmd.CommandType = CommandType.StoredProcedure;
                List<SubProductoEntidad> ListaSubProductos = new List<SubProductoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SubProductoEntidad oSubProductoEntidad = new SubProductoEntidad();
                        oSubProductoEntidad.Cod_SubProducto = Reader.GetStringValue(reader, "Cod_SubProducto");
                        oSubProductoEntidad.SubProducto = Reader.GetStringValue(reader, "SubProducto");
                        ListaSubProductos.Add(oSubProductoEntidad);
                    }
                }
                return ListaSubProductos;
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
