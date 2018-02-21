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
    public class ReclamoRepositorio
    {

        #region "Metodos No Transaccionales"

        public ReclamoEntidad FiltrarxCodigo(string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Reclamo_FiltrarxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Reclamo", SqlDbType.VarChar, 15)).Value = codigo;
                cmd.CommandType = CommandType.StoredProcedure;
                ReclamoEntidad oReclamoEntidad = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oReclamoEntidad = new ReclamoEntidad();
                        oReclamoEntidad.Cod_Reclamo = Reader.GetStringValue(reader, "Cod_Reclamo");
                        oReclamoEntidad.Solicitud = new SolicitudEntidad
                        {
                            N_Solicitud = Reader.GetStringValue(reader, "N_Solicitud"),
                            Vendedor = new VendedorEntidad
                            {
                                Cod_Vendedor = Reader.GetStringValue(reader, "Cod_Vendedor"),
                                Nombre = Reader.GetStringValue(reader, "Nombre_Vendedor"),
                                Apellido = Reader.GetStringValue(reader, "ApellidoPaterno_Vendedor"),
                                Apellido2 = Reader.GetStringValue(reader, "ApellidoMaterno_Vendedor"),
                            },
                            Canal = new CanalEntidad
                            {
                                Canal = Reader.GetStringValue(reader, "Canal"),
                            },
                            Subcanal = new SubCanalEntidad
                            {
                                SubCanal = Reader.GetStringValue(reader, "Subcanal"),
                            },
                            Region = new RegionEntidad
                            {
                                Descripcion = Reader.GetStringValue(reader, "Region"),
                            },
                            Provincia = new ProvinciaEntidad
                            {
                                Descripcion = Reader.GetStringValue(reader, "Provincia"),
                            },
                            Distrito = new DistritoEntidad
                            {
                                Descripcion = Reader.GetStringValue(reader, "Distrito"),
                            },
                            BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente"),
                            Tipo_Doc = new TipoDocumentoEntidad
                            {
                                Nomb_Doc = Reader.GetStringValue(reader, "NomTipoDocumento_Cliente"),
                            },
                            Num_Doc = Reader.GetStringValue(reader, "NumeroDocumento_Cliente"),
                            Nombre_Cliente = Reader.GetStringValue(reader, "Nombre_Cliente"),
                            Apellido1_Cliente = Reader.GetStringValue(reader, "Apellido1_Cliente"),
                            Apellido2_Cliente = Reader.GetStringValue(reader, "Apellido2_Cliente"),
                            N_Operación = Reader.GetStringValue(reader, "N_Operación"),
                            Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy"),
                            Producto = new ProductoEntidad
                            {
                                Producto = Reader.GetStringValue(reader, "Producto"),
                            },
                            SubProducto = new SubProductoEntidad
                            {
                                SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                            },


                        };
                        oReclamoEntidad.TipoReclamo = new TipoReclamoEntidad
                        {
                            Cod_TipoReclamo = Reader.GetTinyIntValue(reader, "Tipo_Reclamo"),
                            //Nom_TipoReclamo = Reader.GetStringValue(reader, "Nom_TipoReclamo"),
                        };
                        oReclamoEntidad.MotivoRechazoSolicitud = new MotivoRechazoSolicitudEntidad
                        {
                            Cod_MotivoRechazoSolicitud = Reader.GetStringValue(reader, "Cod_MotivoRechazoSolicitud"),
                        };
                        oReclamoEntidad.RespuestaReclamo = new RespuestaReclamoEntidad
                        {
                            Cod_RespuestaReclamo = Reader.GetStringValue(reader, "Cod_RespuestaReclamo"),
                        };
                        oReclamoEntidad.Observaciones = Reader.GetStringValue(reader, "Observaciones");
                        oReclamoEntidad.Usuario = new UsuarioEntidad
                        {
                            Nom_Usuario = Reader.GetStringValue(reader, "Nom_Usuario"),
                        };
                        oReclamoEntidad.FechaInicio = Reader.GetDateTimeValue(reader, "FechaInicio").ToString("dd/MM/yyyy");
                        oReclamoEntidad.FechaFin = Reader.GetDateTimeValue(reader, "FechaFin").ToString("dd/MM/yyyy");
                        oReclamoEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                    }
                }
                return oReclamoEntidad;
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
        public ReclamoEntidad ObtenerxSolicitud(string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Solicitud_ObtenerxCodigo", cn);
                cmd.Parameters.Add(new SqlParameter("@N_Solicitud", SqlDbType.VarChar, 15)).Value = codigo;
                cmd.CommandType = CommandType.StoredProcedure;
                ReclamoEntidad oReclamoEntidad = null;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oReclamoEntidad = new ReclamoEntidad();
                        //oReclamoEntidad.Cod_Reclamo = Reader.GetStringValue(reader, "Cod_Reclamo");
                        oReclamoEntidad.Solicitud = new SolicitudEntidad
                        {
                            N_Solicitud = Reader.GetStringValue(reader, "N_Solicitud"),
                            Vendedor = new VendedorEntidad
                            {
                                Cod_Vendedor = Reader.GetStringValue(reader, "Cod_Vendedor"),
                                Nombre = Reader.GetStringValue(reader, "Nombre_Vendedor"),
                                Apellido = Reader.GetStringValue(reader, "ApellidoPaterno_Vendedor"),
                                Apellido2 = Reader.GetStringValue(reader, "ApellidoMaterno_Vendedor"),
                            },
                            Canal = new CanalEntidad
                            {
                                Canal = Reader.GetStringValue(reader, "Canal"),
                            },
                            Subcanal = new SubCanalEntidad
                            {
                                SubCanal = Reader.GetStringValue(reader, "Subcanal"),
                            },
                            Region = new RegionEntidad
                            {
                                Descripcion = Reader.GetStringValue(reader, "Region"),
                            },
                            Provincia = new ProvinciaEntidad
                            {
                                Descripcion = Reader.GetStringValue(reader, "Provincia"),
                            },
                            Distrito = new DistritoEntidad
                            {
                                Descripcion = Reader.GetStringValue(reader, "Distrito"),
                            },
                            BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente"),
                            Tipo_Doc = new TipoDocumentoEntidad
                            {
                                Nomb_Doc = Reader.GetStringValue(reader, "NomTipoDocumento_Cliente"),
                            },
                            Num_Doc = Reader.GetStringValue(reader, "NumeroDocumento_Cliente"),
                            Nombre_Cliente = Reader.GetStringValue(reader, "Nombre_Cliente"),
                            Apellido1_Cliente = Reader.GetStringValue(reader, "Apellido1_Cliente"),
                            Apellido2_Cliente = Reader.GetStringValue(reader, "Apellido2_Cliente"),
                            N_Operación = Reader.GetStringValue(reader, "N_Operación"),
                            Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy"),
                            Producto = new ProductoEntidad
                            {
                                Producto = Reader.GetStringValue(reader, "Producto"),
                            },
                            SubProducto = new SubProductoEntidad
                            {
                                SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                            },


                        };
                    }
                }
                return oReclamoEntidad;
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
        public List<ReclamoEntidad> obtenerDatosXFiltro(ReclamoEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Reclamo_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = entidad.FechaInicio;
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = entidad.FechaFin;
                cmd.Parameters.Add(new SqlParameter("@Ruc", SqlDbType.VarChar, 11)).Value = (entidad.Ruc != null ? entidad.Ruc : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Solicitud.Canal.Cod_Canal != null ? entidad.Solicitud.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Subcanal", SqlDbType.VarChar, 15)).Value = (entidad.Solicitud.Subcanal.Cod_SubCanal != null ? entidad.Solicitud.Subcanal.Cod_SubCanal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Value = (entidad.Solicitud.Vendedor.Cod_Vendedor != null ? entidad.Solicitud.Vendedor.Cod_Vendedor : "");
                cmd.Parameters.Add(new SqlParameter("@NombresVendedor", SqlDbType.VarChar, 150)).Value = (entidad.Solicitud.Vendedor.Nombre != null ? entidad.Solicitud.Vendedor.Nombre : "");
                cmd.Parameters.Add(new SqlParameter("@ApellidosVendedor", SqlDbType.VarChar, 150)).Value = (entidad.Solicitud.Vendedor.Apellido != null ? entidad.Solicitud.Vendedor.Apellido : "");
                cmd.Parameters.Add(new SqlParameter("@Tipo_Doc", SqlDbType.TinyInt)).Value = (entidad.Solicitud.Vendedor.Tipo_Doc.Tipo_Doc != null ? entidad.Solicitud.Vendedor.Tipo_Doc.Tipo_Doc : 0);
                cmd.Parameters.Add(new SqlParameter("@Num_Doc", SqlDbType.VarChar, 15)).Value = (entidad.Solicitud.Vendedor.Num_Doc != null ? entidad.Solicitud.Vendedor.Num_Doc : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<ReclamoEntidad> ListaReclamo = new List<ReclamoEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ReclamoEntidad oReclamoEntidad = new ReclamoEntidad();
                        oReclamoEntidad.Cod_Reclamo = Reader.GetStringValue(reader, "Cod_Reclamo");
                        oReclamoEntidad.Solicitud = new SolicitudEntidad
                        {
                            Canal = new CanalEntidad
                            {
                                Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal"),
                                Canal = Reader.GetStringValue(reader, "Canal"),
                            },
                            Vendedor = new VendedorEntidad
                            {
                                Cod_Vendedor = Reader.GetStringValue(reader, "Cod_Vendedor"),
                                Nombre = Reader.GetStringValue(reader, "Nombre_Vendedor"),
                                Apellido = Reader.GetStringValue(reader, "ApellidoPaterno_Vendedor"),
                                Apellido2 = Reader.GetStringValue(reader, "ApellidoMaterno_Vendedor"),
                            },

                            BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente"),
                            Producto = new ProductoEntidad
                            {
                                Cod_Producto = Reader.GetStringValue(reader, "Cod_Producto"),
                                Producto = Reader.GetStringValue(reader, "Producto"),
                            },
                            Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy"),
                        };
                        oReclamoEntidad.TipoReclamo = new TipoReclamoEntidad
                        {
                            Cod_TipoReclamo = Reader.GetTinyIntValue(reader, "Tipo_Reclamo"),
                            Nom_TipoReclamo = Reader.GetStringValue(reader, "Nom_TipoReclamo"),
                        };
                        oReclamoEntidad.MotivoRechazoSolicitud = new MotivoRechazoSolicitudEntidad
                        {
                            Cod_MotivoRechazoSolicitud = Reader.GetStringValue(reader, "Cod_MotivoRechazoSolicitud"),
                            Nom_MotivoRechazoSolicitud = Reader.GetStringValue(reader, "Nom_MotivoRechazoSolicitud"),
                        };
                        oReclamoEntidad.FechaInicio = Reader.GetDateTimeValue(reader, "FechaInicio").ToString("dd/MM/yyyy");
                        oReclamoEntidad.FechaFin = Reader.GetDateTimeValue(reader, "FechaFin").ToString("dd/MM/yyyy");
                        oReclamoEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        ListaReclamo.Add(oReclamoEntidad);
                    }
                }
                return ListaReclamo;
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

        #region "Metodos Transaccionales"
        public bool grabarDatos(ReclamoEntidad entidad, ref string codigo)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Reclamo_Grabar", cn);
                cmd.Parameters.Add(new SqlParameter("@N_Solicitud", SqlDbType.VarChar, 15)).Value = entidad.Solicitud.N_Solicitud;
                cmd.Parameters.Add(new SqlParameter("@Tipo_Reclamo", SqlDbType.TinyInt)).Value = entidad.TipoReclamo.Cod_TipoReclamo;
                cmd.Parameters.Add(new SqlParameter("@Cod_MotivoRechazoSolicitud", SqlDbType.VarChar, 15)).Value = entidad.MotivoRechazoSolicitud.Cod_MotivoRechazoSolicitud;
                cmd.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.VarChar, 500)).Value = entidad.Observaciones;
                cmd.Parameters.Add(new SqlParameter("@Cod_Usuario", SqlDbType.TinyInt)).Value = entidad.Usuario.Cod_Usuario;
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = entidad.FechaInicio;
                cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.Char, 1)).Value = entidad.Estado;
                cmd.Parameters.Add(new SqlParameter("@Cod_Reclamo", SqlDbType.VarChar, 15)).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trans;
                if (cmd.ExecuteNonQuery() > 0) estado = true;

                if (estado)
                {
                    codigo = cmd.Parameters["@Cod_Reclamo"].Value.ToString();
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

        public bool grabarRespuesta(ReclamoEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Reclamo_GrabarRespuesta", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Reclamo", SqlDbType.VarChar, 15)).Value = entidad.Cod_Reclamo;
                cmd.Parameters.Add(new SqlParameter("@Cod_RespuestaReclamo", SqlDbType.VarChar, 15)).Value = entidad.RespuestaReclamo.Cod_RespuestaReclamo;
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

        public bool modificarDatos(ReclamoEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Reclamo_Modificar", cn);
                cmd.Parameters.Add(new SqlParameter("@Cod_Reclamo", SqlDbType.VarChar, 15)).Value = entidad.Cod_Reclamo;
                cmd.Parameters.Add(new SqlParameter("@Tipo_Reclamo", SqlDbType.TinyInt)).Value = entidad.TipoReclamo.Cod_TipoReclamo;
                cmd.Parameters.Add(new SqlParameter("@Cod_MotivoRechazoSolicitud", SqlDbType.VarChar, 15)).Value = entidad.MotivoRechazoSolicitud.Cod_MotivoRechazoSolicitud;
                cmd.Parameters.Add(new SqlParameter("@Observaciones", SqlDbType.VarChar, 500)).Value = entidad.Observaciones;
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = entidad.FechaInicio;
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
