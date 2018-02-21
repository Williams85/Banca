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
    public class VendedorRepositorio
    {

        #region "Metodos No Transaccionales"
        public List<VendedorEntidad> listar()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conector.abrirConexion();
                SqlCommand cmd = new SqlCommand("usp_Listar_Vendedores", Conector.Conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                List<VendedorEntidad> ListaVendedor = new List<VendedorEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VendedorEntidad oVendedorEntidad = new VendedorEntidad();
                        oVendedorEntidad.Cod_Vendedor = Reader.GetStringValue(reader, "Cod_Vendedor");
                        oVendedorEntidad.Canal = new CanalEntidad
                        {
                            Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal"),
                        };
                        oVendedorEntidad.Nombre = Reader.GetStringValue(reader, "Nombre");
                        oVendedorEntidad.Apellido = Reader.GetStringValue(reader, "Apellido");
                        oVendedorEntidad.Apellido2 = Reader.GetStringValue(reader, "Apellido2");
                        ListaVendedor.Add(oVendedorEntidad);
                    }
                }
                return ListaVendedor;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Conector.cerrarConexion();
            }
        }
        public List<VendedorEntidad> obtenerDatosXFiltro(VendedorEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Vendedor_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = (entidad.SubCanal.Cod_SubCanal != null ? entidad.SubCanal.Cod_SubCanal : "");
                cmd.Parameters.Add(new SqlParameter("@Tipo_Doc", SqlDbType.TinyInt)).Value = (entidad.Tipo_Doc.Tipo_Doc != null ? entidad.Tipo_Doc.Tipo_Doc : 0);
                cmd.Parameters.Add(new SqlParameter("@Num_Doc", SqlDbType.VarChar, 15)).Value = (entidad.Num_Doc != null ? entidad.Num_Doc : "");
                cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150)).Value = (entidad.Nombre != null ? entidad.Nombre : "");
                cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 150)).Value = (entidad.Apellido != null ? entidad.Apellido : "");
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.CommandType = CommandType.StoredProcedure;
                List<VendedorEntidad> ListaVendedor = new List<VendedorEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VendedorEntidad oVendedorEntidad = new VendedorEntidad();
                        oVendedorEntidad.Cod_Vendedor = Reader.GetStringValue(reader, "Cod_Vendedor");
                        oVendedorEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oVendedorEntidad.SubCanal = new SubCanalEntidad
                        {
                            SubCanal = Reader.GetStringValue(reader, "SubCanal"),
                        };
                        oVendedorEntidad.Distrito = new DistritoEntidad
                        {
                            Descripcion = Reader.GetStringValue(reader, "Descripcion"),
                        };
                        oVendedorEntidad.Tipo_Doc = new TipoDocumentoEntidad
                        {
                            Nomb_Doc = Reader.GetStringValue(reader, "Nomb_Doc"),
                        };
                        oVendedorEntidad.Num_Doc = Reader.GetStringValue(reader, "Num_Doc");
                        oVendedorEntidad.Nombre = Reader.GetStringValue(reader, "Nombre");
                        oVendedorEntidad.Apellido = Reader.GetStringValue(reader, "Apellido");
                        oVendedorEntidad.Apellido2 = Reader.GetStringValue(reader, "Apellido2");
                        oVendedorEntidad.Direccion = Reader.GetStringValue(reader, "Dirección");
                        oVendedorEntidad.Telefono1 = Reader.GetStringValue(reader, "Telefono1");
                        oVendedorEntidad.Telefono2 = Reader.GetStringValue(reader, "Telefono2");
                        oVendedorEntidad.Celular = Reader.GetStringValue(reader, "Celular");
                        oVendedorEntidad.Fecha_Inicio = Reader.GetDateTimeValue(reader, "Fecha_Inicio").ToString("dd/MM/yyyy");
                        oVendedorEntidad.Fecha_Cese = Reader.GetDateTimeValue(reader, "Fecha_Cese").ToString("dd/MM/yyyy");
                        oVendedorEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oVendedorEntidad.Fecha_Ult_Camb = Reader.GetDateTimeValue(reader, "Fecha_Ult_Camb").ToString("dd/MM/yyyy");
                        oVendedorEntidad.Usuario = new UsuarioEntidad
                        {
                            Nom_Usuario = Reader.GetStringValue(reader, "Nom_Usuario"),
                        };
                        ListaVendedor.Add(oVendedorEntidad);
                    }
                }
                return ListaVendedor;
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
        public VendedorEntidad obtenerDatosXCodigo(string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Vendedor_FiltrarxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Value = codigo;
                cmd.CommandType = CommandType.StoredProcedure;
                VendedorEntidad oVendedorEntidad = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oVendedorEntidad = new VendedorEntidad();
                        oVendedorEntidad.Cod_Vendedor = Reader.GetStringValue(reader, "Cod_Vendedor");
                        oVendedorEntidad.Canal = new CanalEntidad
                        {
                            Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal"),
                        };
                        oVendedorEntidad.SubCanal = new SubCanalEntidad
                        {
                            Cod_SubCanal = Reader.GetStringValue(reader, "Cod_SubCanal"),
                        };
                        oVendedorEntidad.Region = new RegionEntidad
                        {
                            Cod_Region = Reader.GetStringValue(reader, "Cod_Region"),
                        };
                        oVendedorEntidad.Provincia = new ProvinciaEntidad
                        {
                            Cod_Provincia = Reader.GetStringValue(reader, "Cod_Provincia"),
                        };
                        oVendedorEntidad.Distrito = new DistritoEntidad
                        {
                            Cod_Distrito = Reader.GetStringValue(reader, "Cod_Distrito"),
                        };
                        oVendedorEntidad.Tipo_Doc = new TipoDocumentoEntidad
                        {
                            Tipo_Doc = Reader.GetTinyIntValue(reader, "Tipo_Doc"),
                        };
                        oVendedorEntidad.Num_Doc = Reader.GetStringValue(reader, "Num_Doc");
                        oVendedorEntidad.Nombre = Reader.GetStringValue(reader, "Nombre");
                        oVendedorEntidad.Apellido = Reader.GetStringValue(reader, "Apellido");
                        oVendedorEntidad.Apellido2 = Reader.GetStringValue(reader, "Apellido2");
                        oVendedorEntidad.Direccion = Reader.GetStringValue(reader, "Dirección");
                        oVendedorEntidad.Telefono1 = Reader.GetStringValue(reader, "Telefono1");
                        oVendedorEntidad.Telefono2 = Reader.GetStringValue(reader, "Telefono2");
                        oVendedorEntidad.Celular = Reader.GetStringValue(reader, "Celular");
                        oVendedorEntidad.Fecha_Inicio = Reader.GetDateTimeValue(reader, "Fecha_Inicio").ToString("dd/MM/yyyy");
                        oVendedorEntidad.Fecha_Cese = Reader.GetDateTimeValue(reader, "Fecha_Cese").ToString("dd/MM/yyyy");
                        oVendedorEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oVendedorEntidad.Fecha_Ult_Camb = Reader.GetDateTimeValue(reader, "Fecha_Ult_Camb").ToString("dd/MM/yyyy");
                        oVendedorEntidad.Usuario = new UsuarioEntidad
                        {
                            Cod_Usuario = Reader.GetTinyIntValue(reader, "Cod_Usuario"),
                        };
                    }
                }
                return oVendedorEntidad;
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
        public bool validarModificacionDatos(VendedorEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Vendedor_ValidarModificacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Value = entidad.Cod_Vendedor.Trim();
                cmd.Parameters.Add(new SqlParameter("@Tipo_Doc", SqlDbType.TinyInt)).Value = entidad.Tipo_Doc.Tipo_Doc;
                cmd.Parameters.Add(new SqlParameter("@Num_Doc", SqlDbType.VarChar, 15)).Value = entidad.Num_Doc.Trim();
                cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150)).Value = entidad.Nombre.Trim();
                cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 150)).Value = entidad.Apellido.Trim();
                cmd.Parameters.Add(new SqlParameter("@Apellido2", SqlDbType.VarChar, 150)).Value = entidad.Apellido2.Trim();
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
        public bool validarGrabacionDatos(VendedorEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Vendedor_ValidarGrabacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Tipo_Doc", SqlDbType.TinyInt)).Value = entidad.Tipo_Doc.Tipo_Doc;
                cmd.Parameters.Add(new SqlParameter("@Num_Doc", SqlDbType.VarChar, 15)).Value = entidad.Num_Doc.Trim();
                cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150)).Value = entidad.Nombre.Trim();
                cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 150)).Value = entidad.Apellido.Trim();
                cmd.Parameters.Add(new SqlParameter("@Apellido2", SqlDbType.VarChar, 150)).Value = entidad.Apellido2.Trim();
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
        public bool grabarDatos(VendedorEntidad entidad, ref string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Vendedor_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = entidad.Cod_Canal.Trim();
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = entidad.Cod_SubCanal.Trim();
                cmd.Parameters.Add(new SqlParameter("@Cod_Region", SqlDbType.VarChar, 6)).Value = entidad.Region.Cod_Region;
                cmd.Parameters.Add(new SqlParameter("@Cod_Provincia", SqlDbType.VarChar, 6)).Value = entidad.Provincia.Cod_Provincia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Distrito", SqlDbType.VarChar, 6)).Value = entidad.Distrito.Cod_Distrito;
                cmd.Parameters.Add(new SqlParameter("@Tipo_Doc", SqlDbType.TinyInt)).Value = entidad.Tipo_Doc.Tipo_Doc;
                cmd.Parameters.Add(new SqlParameter("@Num_Doc", SqlDbType.VarChar, 15)).Value = entidad.Num_Doc.Trim();
                cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150)).Value = entidad.Nombre.Trim().ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 150)).Value = entidad.Apellido.Trim().ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Apellido2", SqlDbType.VarChar, 150)).Value = entidad.Apellido2.Trim().ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 250)).Value = entidad.Direccion.Trim().ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Telefono1", SqlDbType.VarChar, 15)).Value = entidad.Telefono1.Trim();
                cmd.Parameters.Add(new SqlParameter("@Telefono2", SqlDbType.VarChar, 15)).Value = entidad.Telefono2;
                cmd.Parameters.Add(new SqlParameter("@Celular", SqlDbType.VarChar, 15)).Value = entidad.Celular.Trim();
                cmd.Parameters.Add(new SqlParameter("@Fecha_Inicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Inicio.Trim());
                cmd.Parameters.Add(new SqlParameter("@Fecha_Cese", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Cese.Trim());
                cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Char, 1)).Value = entidad.Estado.Trim();
                cmd.Parameters.Add(new SqlParameter("@Cod_Usuario", SqlDbType.TinyInt)).Value = entidad.Usuario.Cod_Usuario;
                cmd.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() > 0) estado = true;

                if (estado)
                {
                    codigo = cmd.Parameters["@Cod_Vendedor"].Value.ToString();
                    trans.Commit();
                }

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

        public bool modificarDatos(VendedorEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Vendedor_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Value = entidad.Cod_Vendedor.Trim();
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = entidad.Cod_Canal.Trim();
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = entidad.Cod_SubCanal.Trim();
                cmd.Parameters.Add(new SqlParameter("@Cod_Region", SqlDbType.VarChar, 6)).Value = entidad.Region.Cod_Region;
                cmd.Parameters.Add(new SqlParameter("@Cod_Provincia", SqlDbType.VarChar, 6)).Value = entidad.Provincia.Cod_Provincia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Distrito", SqlDbType.VarChar, 6)).Value = entidad.Distrito.Cod_Distrito;
                cmd.Parameters.Add(new SqlParameter("@Tipo_Doc", SqlDbType.TinyInt)).Value = entidad.Tipo_Doc.Tipo_Doc;
                cmd.Parameters.Add(new SqlParameter("@Num_Doc", SqlDbType.VarChar, 15)).Value = entidad.Num_Doc.Trim();
                cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 150)).Value = entidad.Nombre.Trim();
                cmd.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 150)).Value = entidad.Apellido.Trim();
                cmd.Parameters.Add(new SqlParameter("@Apellido2", SqlDbType.VarChar, 150)).Value = entidad.Apellido2.Trim();
                cmd.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 250)).Value = entidad.Direccion.Trim();
                cmd.Parameters.Add(new SqlParameter("@Telefono1", SqlDbType.VarChar, 15)).Value = entidad.Telefono1.Trim();
                cmd.Parameters.Add(new SqlParameter("@Telefono2", SqlDbType.VarChar, 15)).Value = entidad.Telefono2.Trim();
                cmd.Parameters.Add(new SqlParameter("@Celular", SqlDbType.VarChar, 15)).Value = entidad.Celular.Trim();
                cmd.Parameters.Add(new SqlParameter("@Fecha_Inicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Inicio.Trim());
                cmd.Parameters.Add(new SqlParameter("@Fecha_Cese", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Cese.Trim());
                cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Char, 1)).Value = entidad.Estado.Trim();
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
