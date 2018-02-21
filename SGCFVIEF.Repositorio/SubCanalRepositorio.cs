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
    public class SubCanalRepositorio
    {


        #region "Metodos No Transaccionales"
        public List<SubCanalEntidad> listarXCanal(string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_SubCanal_Listar_Filtro", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = codigo;
                cmd.CommandType = CommandType.StoredProcedure;
                List<SubCanalEntidad> ListaSubCanal = new List<SubCanalEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SubCanalEntidad oSubCanalEntidad = new SubCanalEntidad();
                        oSubCanalEntidad.Cod_SubCanal = Reader.GetStringValue(reader, "Cod_Subcanal");
                        oSubCanalEntidad.SubCanal = Reader.GetStringValue(reader, "Subcanal");
                        ListaSubCanal.Add(oSubCanalEntidad);
                    }
                }
                return ListaSubCanal;
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
        public List<SubCanalEntidad> obtenerDatosXFiltro(SubCanalEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_SubCanal_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = (entidad.Cod_SubCanal != null ? entidad.Cod_SubCanal : "");
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.CommandType = CommandType.StoredProcedure;
                List<SubCanalEntidad> ListaCanal = new List<SubCanalEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SubCanalEntidad oSubCanalEntidad = new SubCanalEntidad();
                        oSubCanalEntidad.Cod_SubCanal = Reader.GetStringValue(reader, "Cod_SubCanal");
                        oSubCanalEntidad.Canal = new CanalEntidad {
                            Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal"),
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oSubCanalEntidad.SubCanal = Reader.GetStringValue(reader, "SubCanal");
                        oSubCanalEntidad.Direccion = Reader.GetStringValue(reader, "Dirección");
                        oSubCanalEntidad.Fecha_Inicio = Reader.GetDateTimeValue(reader, "Fecha_Inicio").ToString("dd/MM/yyyy");
                        oSubCanalEntidad.Fecha_Cese = Reader.GetDateTimeValue(reader, "Fecha_Cese").ToString("dd/MM/yyyy");
                        oSubCanalEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oSubCanalEntidad.Distrito = new DistritoEntidad
                        {
                            Descripcion = Reader.GetStringValue(reader, "Descripcion"),
                        };
                        oSubCanalEntidad.Fecha_Ult_Camb = Reader.GetDateTimeValue(reader, "Fecha_Ult_Camb").ToString("dd/MM/yyyy");
                        oSubCanalEntidad.Usuario = new UsuarioEntidad
                        {
                            Nom_Usuario = Reader.GetStringValue(reader, "Nom_Usuario"),
                        };
                        ListaCanal.Add(oSubCanalEntidad);
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
        public SubCanalEntidad obtenerDatosXCodigo(string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_SubCanal_FiltrarxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = codigo;
                cmd.CommandType = CommandType.StoredProcedure;
                SubCanalEntidad oSubCanalEntidad = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oSubCanalEntidad = new SubCanalEntidad();
                        oSubCanalEntidad.Cod_SubCanal = Reader.GetStringValue(reader, "Cod_SubCanal");
                        oSubCanalEntidad.SubCanal = Reader.GetStringValue(reader, "SubCanal");

                        oSubCanalEntidad.Canal = new CanalEntidad { 
                            Cod_Canal=Reader.GetStringValue(reader, "Cod_Canal"),
                        };
                        oSubCanalEntidad.Region = new RegionEntidad
                        {
                            Cod_Region = Reader.GetStringValue(reader, "Cod_Region"),
                        };
                        oSubCanalEntidad.Provincia = new ProvinciaEntidad
                        {
                            Cod_Provincia = Reader.GetStringValue(reader, "Cod_Provincia"),
                        };
                        oSubCanalEntidad.Distrito = new DistritoEntidad
                        {
                            Cod_Distrito = Reader.GetStringValue(reader, "Cod_Distrito"),
                        };
                        oSubCanalEntidad.Direccion = Reader.GetStringValue(reader, "Dirección");
                        oSubCanalEntidad.Fecha_Inicio = Reader.GetDateTimeValue(reader, "Fecha_Inicio").ToString("dd/MM/yyyy");
                        oSubCanalEntidad.Fecha_Cese = Reader.GetDateTimeValue(reader, "Fecha_Cese").ToString("dd/MM/yyyy");
                        oSubCanalEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oSubCanalEntidad.Fecha_Ult_Camb = Reader.GetDateTimeValue(reader, "Fecha_Ult_Camb").ToString("dd/MM/yyyy");
                        oSubCanalEntidad.Usuario = new UsuarioEntidad
                        {
                            Cod_Usuario = Reader.GetTinyIntValue(reader, "Cod_Usuario"),
                        };
                    }
                }
                return oSubCanalEntidad;
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
        public bool validarModificacionDatos(SubCanalEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_SubCanal_ValidarModificacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = entidad.Canal.Cod_Canal;
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = entidad.Cod_SubCanal;
                cmd.Parameters.Add(new SqlParameter("@SubCanal", SqlDbType.VarChar, 150)).Value = entidad.SubCanal;
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
        public bool validarGrabacionDatos(SubCanalEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_SubCanal_ValidarGrabacion", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = entidad.Canal.Cod_Canal;
                cmd.Parameters.Add(new SqlParameter("@SubCanal", SqlDbType.VarChar, 150)).Value = entidad.SubCanal;
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
        public bool grabarDatos(SubCanalEntidad entidad, ref string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_SubCanal_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@SubCanal", SqlDbType.VarChar, 150)).Value = entidad.SubCanal.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = entidad.Canal.Cod_Canal;
                cmd.Parameters.Add(new SqlParameter("@Dirección", SqlDbType.VarChar, 250)).Value = entidad.Direccion.ToUpper();
                cmd.Parameters.Add(new SqlParameter("@Cod_Region", SqlDbType.VarChar, 6)).Value = entidad.Region.Cod_Region;
                cmd.Parameters.Add(new SqlParameter("@Cod_Provincia", SqlDbType.VarChar, 6)).Value = entidad.Provincia.Cod_Provincia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Distrito", SqlDbType.VarChar, 6)).Value = entidad.Distrito.Cod_Distrito;
                cmd.Parameters.Add(new SqlParameter("@Fecha_Inicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Inicio);
                cmd.Parameters.Add(new SqlParameter("@Fecha_Cese", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Cese);
                cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Char, 1)).Value = entidad.Estado;
                cmd.Parameters.Add(new SqlParameter("@Cod_Usuario", SqlDbType.TinyInt)).Value = entidad.Usuario.Cod_Usuario;
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() > 0) estado = true;

                if (estado)
                {
                    codigo = cmd.Parameters["@Cod_SubCanal"].Value.ToString();
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

        public bool modificarDatos(SubCanalEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_SubCanal_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = entidad.Cod_SubCanal;
                cmd.Parameters.Add(new SqlParameter("@SubCanal", SqlDbType.VarChar, 150)).Value = entidad.SubCanal;
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = entidad.Canal.Cod_Canal;
                cmd.Parameters.Add(new SqlParameter("@Dirección", SqlDbType.VarChar, 250)).Value = entidad.Direccion;
                cmd.Parameters.Add(new SqlParameter("@Cod_Region", SqlDbType.VarChar, 6)).Value = entidad.Region.Cod_Region;
                cmd.Parameters.Add(new SqlParameter("@Cod_Provincia", SqlDbType.VarChar, 6)).Value = entidad.Provincia.Cod_Provincia;
                cmd.Parameters.Add(new SqlParameter("@Cod_Distrito", SqlDbType.VarChar, 6)).Value = entidad.Distrito.Cod_Distrito;
                cmd.Parameters.Add(new SqlParameter("@Fecha_Inicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Inicio);
                cmd.Parameters.Add(new SqlParameter("@Fecha_Cese", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Cese);
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
