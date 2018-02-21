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
    public class ProductoRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<ProductoEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Producto_Listar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<ProductoEntidad> ListaProductos = new List<ProductoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductoEntidad oProductoEntidad = new ProductoEntidad();
                        oProductoEntidad.Cod_Producto = Reader.GetStringValue(reader, "Cod_Producto");
                        oProductoEntidad.Producto = Reader.GetStringValue(reader, "Producto");
                        ListaProductos.Add(oProductoEntidad);
                    }
                }
                return ListaProductos;
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
