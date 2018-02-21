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
    public class CanalRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<CanalEntidad> listarActivos()
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Canal_Listar_Activos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                List<CanalEntidad> ListaCanal = new List<CanalEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CanalEntidad oCanalEntidad = new CanalEntidad();
                        oCanalEntidad.Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal");
                        oCanalEntidad.Canal = Reader.GetStringValue(reader, "Canal");
                        ListaCanal.Add(oCanalEntidad);
                    }
                }
                return ListaCanal.OrderBy(x=>x.Canal).ToList();
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
        public List<CanalEntidad> obtenerDatosXFiltro(CanalEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Canal_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Cod_Canal != null ? entidad.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = (entidad.Cod_SubCanal != null ? entidad.Cod_SubCanal : "");
                cmd.Parameters.Add(new SqlParameter("@Ruc", SqlDbType.VarChar, 11)).Value = (entidad.RUC != null ? entidad.RUC : "");
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.CommandType = CommandType.StoredProcedure;
                List<CanalEntidad> ListaCanal = new List<CanalEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CanalEntidad oCanalEntidad = new CanalEntidad();
                        oCanalEntidad.Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal");
                        oCanalEntidad.Canal = Reader.GetStringValue(reader, "Canal");
                        oCanalEntidad.Fecha_Inicio = Reader.GetDateTimeValue(reader, "Fecha_Inicio").ToString("dd/MM/yyyy");
                        oCanalEntidad.Fecha_Cese = Reader.GetDateTimeValue(reader, "Fecha_Cese").ToString("dd/MM/yyyy");
                        oCanalEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oCanalEntidad.Direccion = Reader.GetStringValue(reader, "Dirección");
                        oCanalEntidad.Telefono1 = Reader.GetStringValue(reader, "Teléfono1");
                        oCanalEntidad.Telefono2 = Reader.GetStringValue(reader, "Teléfono2");
                        oCanalEntidad.Celular = Reader.GetStringValue(reader, "Celular");
                        oCanalEntidad.Distrito = new DistritoEntidad
                        {
                            Descripcion = Reader.GetStringValue(reader, "Descripcion"),
                        };
                        oCanalEntidad.Representante_Legal = Reader.GetStringValue(reader, "Representante_Legal");
                        oCanalEntidad.Fecha_Ult_Camb = Reader.GetDateTimeValue(reader, "Fecha_Ult_Camb").ToString("dd/MM/yyyy");
                        oCanalEntidad.Usuario = new UsuarioEntidad
                        {
                            Nom_Usuario = Reader.GetStringValue(reader, "Nom_Usuario"),
                        };
                        ListaCanal.Add(oCanalEntidad);
                    }
                }
                return ListaCanal;
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
        public CanalEntidad obtenerDatosXCodigo(string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Canal_FiltrarxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = codigo;
                cmd.CommandType = CommandType.StoredProcedure;
                CanalEntidad oCanalEntidad = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oCanalEntidad = new CanalEntidad();
                        oCanalEntidad.Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal").Trim();
                        oCanalEntidad.Canal = Reader.GetStringValue(reader, "Canal");
                        oCanalEntidad.RUC = Reader.GetStringValue(reader, "RUC");
                        oCanalEntidad.Razon_Social = Reader.GetStringValue(reader, "Razon_Social");
                        oCanalEntidad.Direccion = Reader.GetStringValue(reader, "Dirección");
                        oCanalEntidad.Region = new RegionEntidad
                        {
                            Cod_Region = Reader.GetStringValue(reader, "Cod_Region"),
                        };
                        oCanalEntidad.Provincia = new ProvinciaEntidad
                        {
                            Cod_Provincia = Reader.GetStringValue(reader, "Cod_Provincia"),
                        };
                        oCanalEntidad.Distrito = new DistritoEntidad
                        {
                            Cod_Distrito = Reader.GetStringValue(reader, "Cod_Distrito"),
                        };
                        oCanalEntidad.Telefono1 = Reader.GetStringValue(reader, "Teléfono1");
                        oCanalEntidad.Telefono2 = Reader.GetStringValue(reader, "Teléfono2");
                        oCanalEntidad.Celular = Reader.GetStringValue(reader, "Celular");
                        oCanalEntidad.Email = Reader.GetStringValue(reader, "Email");
                        oCanalEntidad.Representante_Legal = Reader.GetStringValue(reader, "Representante_Legal");
                        oCanalEntidad.Fecha_Inicio = Reader.GetDateTimeValue(reader, "Fecha_Inicio").ToString("dd/MM/yyyy");
                        oCanalEntidad.Fecha_Cese = Reader.GetDateTimeValue(reader, "Fecha_Cese").ToString("dd/MM/yyyy");
                        oCanalEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oCanalEntidad.Fecha_Ult_Camb = Reader.GetDateTimeValue(reader, "Fecha_Ult_Camb").ToString("dd/MM/yyyy");
                        oCanalEntidad.Usuario = new UsuarioEntidad
                        {
                            Cod_Usuario = Reader.GetTinyIntValue(reader, "Cod_Usuario"),
                        };
                    }
                }
                return oCanalEntidad;
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
        public bool validarModificacionDatos(CanalEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Canal_ValidarModificacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = entidad.Cod_Canal;
                cmd.Parameters.Add(new SqlParameter("@Canal", SqlDbType.VarChar, 150)).Value = entidad.Canal;
                cmd.Parameters.Add(new SqlParameter("@RUC", SqlDbType.VarChar, 11)).Value = entidad.RUC;
                cmd.Parameters.Add(new SqlParameter("@Razon_Social", SqlDbType.VarChar, 150)).Value = entidad.Razon_Social;
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
        public bool validarGrabacionDatos(CanalEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Canal_ValidarGrabacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Canal", SqlDbType.VarChar, 150)).Value = entidad.Canal;
                cmd.Parameters.Add(new SqlParameter("@RUC", SqlDbType.VarChar, 11)).Value = entidad.RUC;
                cmd.Parameters.Add(new SqlParameter("@Razon_Social", SqlDbType.VarChar, 150)).Value = entidad.Razon_Social;
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
        public bool grabarDatos(CanalEntidad entidad,ref string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Canal_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@Canal", SqlDbType.VarChar, 150)).Value = entidad.Canal.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@RUC", SqlDbType.VarChar, 11)).Value = entidad.RUC;
                cmd.Parameters.Add(new SqlParameter("@Razon_Social", SqlDbType.VarChar, 150)).Value = entidad.Razon_Social.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Dirección", SqlDbType.VarChar, 250)).Value = entidad.Direccion.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Cod_Region", SqlDbType.VarChar, 6)).Value = entidad.Region.Cod_Region;
                cmd.Parameters.Add(new SqlParameter("@Cod_Provincia", SqlDbType.VarChar, 6)).Value = entidad.Provincia.Cod_Provincia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Distrito", SqlDbType.VarChar,6)).Value = entidad.Distrito.Cod_Distrito;
                cmd.Parameters.Add(new SqlParameter("@Teléfono1", SqlDbType.VarChar, 15)).Value = entidad.Telefono1;
                cmd.Parameters.Add(new SqlParameter("@Teléfono2", SqlDbType.VarChar, 15)).Value = entidad.Telefono2;
                cmd.Parameters.Add(new SqlParameter("@Celular", SqlDbType.VarChar, 15)).Value = entidad.Celular;
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150)).Value = entidad.Email;
                cmd.Parameters.Add(new SqlParameter("@Representante_Legal", SqlDbType.VarChar, 150)).Value = entidad.Representante_Legal.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Fecha_Inicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Inicio);
                cmd.Parameters.Add(new SqlParameter("@Fecha_Cese", SqlDbType.SmallDateTime)).Value =DateTime.Parse(entidad.Fecha_Cese);
                cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Char, 1)).Value = entidad.Estado;
                cmd.Parameters.Add(new SqlParameter("@Cod_Usuario", SqlDbType.TinyInt)).Value = entidad.Usuario.Cod_Usuario;
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 10)).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() > 0) estado = true;

                if (estado){
                    codigo = cmd.Parameters["@Cod_Canal"].Value.ToString();
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

        public bool modificarDatos(CanalEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Canal_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 10)).Value = entidad.Cod_Canal;
                cmd.Parameters.Add(new SqlParameter("@Canal", SqlDbType.VarChar, 150)).Value = entidad.Canal.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@RUC", SqlDbType.VarChar, 11)).Value = entidad.RUC;
                cmd.Parameters.Add(new SqlParameter("@Razon_Social", SqlDbType.VarChar, 150)).Value = entidad.Razon_Social.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Dirección", SqlDbType.VarChar, 250)).Value = entidad.Direccion.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Cod_Region", SqlDbType.VarChar, 6)).Value = entidad.Region.Cod_Region;
                cmd.Parameters.Add(new SqlParameter("@Cod_Provincia", SqlDbType.VarChar, 6)).Value = entidad.Provincia.Cod_Provincia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Distrito", SqlDbType.VarChar, 6)).Value = entidad.Distrito.Cod_Distrito;
                cmd.Parameters.Add(new SqlParameter("@Teléfono1", SqlDbType.VarChar, 15)).Value = entidad.Telefono1;
                cmd.Parameters.Add(new SqlParameter("@Teléfono2", SqlDbType.VarChar, 15)).Value = entidad.Telefono2;
                cmd.Parameters.Add(new SqlParameter("@Celular", SqlDbType.VarChar, 15)).Value = entidad.Celular;
                cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 150)).Value = entidad.Email.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Representante_Legal", SqlDbType.VarChar, 150)).Value = entidad.Representante_Legal.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Fecha_Inicio", SqlDbType.SmallDateTime)).Value =DateTime.Parse(entidad.Fecha_Inicio);
                cmd.Parameters.Add(new SqlParameter("@Fecha_Cese", SqlDbType.SmallDateTime)).Value =DateTime.Parse(entidad.Fecha_Cese);
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
