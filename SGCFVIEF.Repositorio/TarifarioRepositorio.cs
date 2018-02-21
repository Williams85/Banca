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
    public class TarifarioRepositorio
    {
        #region "Metodos No Transaccionales"

        public List<TarifarioEntidad> obtenerDatosXFiltro(TarifarioEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Tarifario_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = (entidad.SubCanal.Cod_SubCanal != null ? entidad.SubCanal.Cod_SubCanal : "");
                cmd.Parameters.Add(new SqlParameter("@Ruc", SqlDbType.VarChar, 11)).Value = (entidad.Canal.RUC != null ? entidad.Canal.RUC : "");
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.CommandType = CommandType.StoredProcedure;
                List<TarifarioEntidad> ListaTarifario = new List<TarifarioEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TarifarioEntidad oTarifarioEntidad = new TarifarioEntidad();
                        oTarifarioEntidad.Cod_Tarifa = Reader.GetIntValue(reader, "Cod_Tarifa");
                        oTarifarioEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oTarifarioEntidad.SubCanal = new SubCanalEntidad
                        {
                            SubCanal = Reader.GetStringValue(reader, "SubCanal"),
                        };

                        oTarifarioEntidad.Producto = new ProductoEntidad
                        {
                            Cod_Producto = Reader.GetStringValue(reader, "Cod_Producto"),
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oTarifarioEntidad.SubProducto = new SubProductoEntidad
                        {
                            Cod_SubProducto = Reader.GetStringValue(reader, "Cod_SubProducto"),
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oTarifarioEntidad.Tarifa_Inicio = Reader.GetDecimalValue(reader, "Tarifa_Inicio");
                        oTarifarioEntidad.Tarifa_Fin = Reader.GetDecimalValue(reader, "Tarifa_Fin");
                        oTarifarioEntidad.Tipo = Reader.GetTinyIntValue(reader, "Tipo");
                        oTarifarioEntidad.Tarifario = Reader.GetDecimalValue(reader, "Tarifario");
                        oTarifarioEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oTarifarioEntidad.Fecha_Registro = Reader.GetDateTimeValue(reader, "Fecha_Registro").ToString("dd/MM/yyyy");
                        oTarifarioEntidad.Usuario = new UsuarioEntidad
                        {
                            Empleado = new EmpleadoEntidad
                            {
                                Nombre = Reader.GetStringValue(reader, "Nombre"),
                                Apellido = Reader.GetStringValue(reader, "Apellido"),
                                Apellido2 = Reader.GetStringValue(reader, "Apellido2"),

                            }
                        };

                        ListaTarifario.Add(oTarifarioEntidad);
                    }
                }
                return ListaTarifario;
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
        public TarifarioEntidad obtenerDatosXCodigo(string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Tarifario_FiltraxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Tarifa", SqlDbType.Int)).Value = Int32.Parse(codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                TarifarioEntidad oTarifarioEntidad = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oTarifarioEntidad = new TarifarioEntidad();
                        oTarifarioEntidad.Cod_Tarifa = Reader.GetIntValue(reader, "Cod_Tarifa");
                        oTarifarioEntidad.Canal = new CanalEntidad
                        {
                            Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal"),
                        };
                        oTarifarioEntidad.SubCanal = new SubCanalEntidad
                        {
                            Cod_SubCanal = Reader.GetStringValue(reader, "Cod_SubCanal"),
                        };
                        oTarifarioEntidad.Producto = new ProductoEntidad
                        {
                            Cod_Producto = Reader.GetStringValue(reader, "Cod_Producto"),
                        };
                        oTarifarioEntidad.SubProducto = new SubProductoEntidad
                        {
                            Cod_SubProducto = Reader.GetStringValue(reader, "Cod_SubProducto"),
                        };
                        oTarifarioEntidad.Tarifa_Inicio = Reader.GetDecimalValue(reader, "Tarifa_Inicio");
                        oTarifarioEntidad.Tarifa_Fin = Reader.GetDecimalValue(reader, "Tarifa_Fin");
                        oTarifarioEntidad.Tipo = Reader.GetTinyIntValue(reader, "Tipo");
                        oTarifarioEntidad.Tarifario = decimal.Parse((Reader.GetDecimalValue(reader, "Tarifario") * 100).ToString("#.#0"));
                        oTarifarioEntidad.Fecha_Ultimo = Reader.GetDateTimeValue(reader, "Fecha_Ultimo").ToString("dd/MM/yyyy");
                        oTarifarioEntidad.Fecha_Registro = Reader.GetDateTimeValue(reader, "Fecha_Registro").ToString("dd/MM/yyyy");
                        oTarifarioEntidad.TipoComision = Reader.GetTinyIntValue(reader, "Tipo_Comision");
                        oTarifarioEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oTarifarioEntidad.Usuario = new UsuarioEntidad
                        {
                            Nom_Usuario = Reader.GetStringValue(reader, "Nom_Usuario"),
                        };
                    }
                }
                return oTarifarioEntidad;
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
        public bool validarModificacionDatos(TarifarioEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Tarifario_ValidarModificacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Tarifa", SqlDbType.Int)).Value = entidad.Cod_Tarifa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubProducto", SqlDbType.VarChar, 15)).Value = (entidad.SubProducto.Cod_SubProducto != null ? entidad.SubProducto.Cod_SubProducto : "");
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) estado = true;

                return estado;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }
        public bool validarGrabacionDatos(TarifarioEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Tarifario_ValidarGrabacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubProducto", SqlDbType.VarChar, 15)).Value = (entidad.SubProducto.Cod_SubProducto != null ? entidad.SubProducto.Cod_SubProducto : "");
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) estado = true;

                return estado;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }
        #endregion

        #region "Metodos Transaccionales"
        public bool grabarDatos(TarifarioEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Tarifario_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = (entidad.SubCanal.Cod_SubCanal != null ? entidad.SubCanal.Cod_SubCanal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubProducto", SqlDbType.VarChar, 15)).Value = (entidad.SubProducto.Cod_SubProducto != null ? entidad.SubProducto.Cod_SubProducto : "");
                cmd.Parameters.Add(new SqlParameter("@Tarifa_Inicio", SqlDbType.Real)).Value = entidad.Tarifa_Inicio;
                cmd.Parameters.Add(new SqlParameter("@Tarifa_Fin", SqlDbType.Real)).Value = entidad.Tarifa_Fin;
                cmd.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.TinyInt)).Value = entidad.Tipo;
                cmd.Parameters.Add(new SqlParameter("@Tarifario", SqlDbType.Real)).Value = entidad.Tarifario;
                cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Char, 1)).Value = entidad.Estado;
                cmd.Parameters.Add(new SqlParameter("@Cod_Usuario", SqlDbType.TinyInt)).Value = entidad.Usuario.Cod_Usuario;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() > 0) estado = true;
                if (estado)
                    trans.Commit();
                else
                    trans.Rollback();

                return estado;
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                return false;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }

        public bool modificarDatos(TarifarioEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Tarifario_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Tarifa", SqlDbType.Int)).Value = entidad.Cod_Tarifa;
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = (entidad.SubCanal.Cod_SubCanal != null ? entidad.SubCanal.Cod_SubCanal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubProducto", SqlDbType.VarChar, 15)).Value = (entidad.SubProducto.Cod_SubProducto != null ? entidad.SubProducto.Cod_SubProducto : "");
                cmd.Parameters.Add(new SqlParameter("@Tarifa_Inicio", SqlDbType.Real)).Value = entidad.Tarifa_Inicio;
                cmd.Parameters.Add(new SqlParameter("@Tarifa_Fin", SqlDbType.Real)).Value = entidad.Tarifa_Fin;
                cmd.Parameters.Add(new SqlParameter("@Tipo", SqlDbType.TinyInt)).Value = entidad.Tipo;
                cmd.Parameters.Add(new SqlParameter("@Tarifario", SqlDbType.Real)).Value = entidad.Tarifario;
                cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Char, 1)).Value = entidad.Estado;
                cmd.Parameters.Add(new SqlParameter("@Cod_Usuario", SqlDbType.TinyInt)).Value = entidad.Usuario.Cod_Usuario;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() > 0) estado = true;

                if (estado)
                    trans.Commit();
                else
                    trans.Rollback();

                return estado;
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                return false;
            }
            finally
            {
                Conexion.cerrarConexion(cn);
            }
        }

        #endregion
    }
}
