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
    public class SolicitudRepositorio
    {
        #region "Metodos No Transaccionales"


        public bool ValidarVigenciaReclamo(string N_Solicitud)
        {
            bool estado = false;
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Solicitud_ValidarVigenciaReclamo", cn);
                cmd.Parameters.Add(new SqlParameter("@N_Solicitud", SqlDbType.VarChar, 15)).Value = N_Solicitud;
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            estado = true;
                        }
                    }
                }
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
        public List<SolicitudEntidad> FiltrarActivas(SolicitudEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Solicitud_Filtrar_Activos", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@TipoComision", SqlDbType.TinyInt)).Value = entidad.TipoComision;
                cmd.CommandType = CommandType.StoredProcedure;
                List<SolicitudEntidad> ListaSolicitudes = new List<SolicitudEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SolicitudEntidad oSolicitudEntidad = new SolicitudEntidad();
                        oSolicitudEntidad.Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy");
                        oSolicitudEntidad.BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente");
                        oSolicitudEntidad.N_Operación = Reader.GetStringValue(reader, "N_Operación");
                        oSolicitudEntidad.Producto = new ProductoEntidad
                        {
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oSolicitudEntidad.SubProducto = new SubProductoEntidad
                        {
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oSolicitudEntidad.Moneda = Reader.GetStringValue(reader, "Moneda");
                        oSolicitudEntidad.Línea_Desembolsos = Reader.GetDecimalValue(reader, "Línea_Desembolsos");
                        oSolicitudEntidad.Plazo = Reader.GetSmallIntValue(reader, "Plazo");
                        oSolicitudEntidad.Saldo_Ant = Reader.GetDecimalValue(reader, "Saldo_Ant");
                        oSolicitudEntidad.Saldo_Act = Reader.GetDecimalValue(reader, "Saldo_Act");
                        oSolicitudEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oSolicitudEntidad.Vendedor = new VendedorEntidad
                        {
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),
                        };
                        oSolicitudEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oSolicitudEntidad.TipoComision = Reader.GetTinyIntValue(reader, "TipoComision");
                        oSolicitudEntidad.Reclamo = Reader.GetStringValue(reader, "Reclamo");
                        ListaSolicitudes.Add(oSolicitudEntidad);
                    }
                }
                return ListaSolicitudes.OrderBy(x => x.Fecha_Aprob_Rech).ToList();
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

        public List<SolicitudEntidad> FiltrarforReclamos(SolicitudEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Solicitud_FiltrarforReclamo", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = entidad.FechaInicio;
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = entidad.FechaFin;
                cmd.Parameters.Add(new SqlParameter("@N_Solicitud", SqlDbType.VarChar, 11)).Value = (entidad.N_Solicitud != null ? entidad.N_Solicitud : "");
                cmd.Parameters.Add(new SqlParameter("@BT_Cliente", SqlDbType.VarChar, 15)).Value = (entidad.BT_Cliente != null ? entidad.BT_Cliente : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Subcanal", SqlDbType.VarChar, 15)).Value = (entidad.Subcanal.Cod_SubCanal != null ? entidad.Subcanal.Cod_SubCanal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Value = (entidad.Vendedor.Cod_Vendedor != null ? entidad.Vendedor.Cod_Vendedor : "");
                cmd.Parameters.Add(new SqlParameter("@NombresVendedor", SqlDbType.VarChar, 150)).Value = (entidad.Vendedor.Nombre != null ? entidad.Vendedor.Nombre : "");
                cmd.Parameters.Add(new SqlParameter("@ApellidosVendedor", SqlDbType.VarChar, 150)).Value = (entidad.Vendedor.Apellido != null ? entidad.Vendedor.Apellido : "");
                cmd.Parameters.Add(new SqlParameter("@Tipo_Doc", SqlDbType.TinyInt)).Value = (entidad.Vendedor.Tipo_Doc.Tipo_Doc != null ? entidad.Vendedor.Tipo_Doc.Tipo_Doc : 0);
                cmd.Parameters.Add(new SqlParameter("@Num_Doc", SqlDbType.VarChar, 15)).Value = (entidad.Vendedor.Num_Doc != null ? entidad.Vendedor.Num_Doc : "");
                //cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                //cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                //cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                //cmd.Parameters.Add(new SqlParameter("@Dni_Cliente", SqlDbType.VarChar, 15)).Value = (entidad.Num_Doc != null ? entidad.Num_Doc : "");
                //cmd.Parameters.Add(new SqlParameter("@BT_Cliente", SqlDbType.VarChar, 15)).Value = (entidad.BT_Cliente != null ? entidad.BT_Cliente : "");
                //cmd.Parameters.Add(new SqlParameter("@Nombres_Cliente", SqlDbType.VarChar, 15)).Value = (entidad.Nombre_Cliente != null ? entidad.Nombre_Cliente : "");
                //cmd.Parameters.Add(new SqlParameter("@Apellidos_Cliente", SqlDbType.VarChar, 15)).Value = (entidad.Apellido1_Cliente != null ? entidad.Apellido1_Cliente : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<SolicitudEntidad> ListaSolicitudes = new List<SolicitudEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SolicitudEntidad oSolicitudEntidad = new SolicitudEntidad();
                        oSolicitudEntidad.N_Solicitud = Reader.GetStringValue(reader, "N_Solicitud");
                        oSolicitudEntidad.Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy");
                        oSolicitudEntidad.BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente");
                        oSolicitudEntidad.N_Operación = Reader.GetStringValue(reader, "N_Operación");
                        oSolicitudEntidad.Producto = new ProductoEntidad
                        {
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oSolicitudEntidad.SubProducto = new SubProductoEntidad
                        {
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oSolicitudEntidad.Moneda = Reader.GetStringValue(reader, "Moneda");
                        oSolicitudEntidad.Línea_Desembolsos = Reader.GetDecimalValue(reader, "Línea_Desembolsos");
                        oSolicitudEntidad.Plazo = Reader.GetSmallIntValue(reader, "Plazo");
                        oSolicitudEntidad.Saldo_Ant = Reader.GetDecimalValue(reader, "Saldo_Ant");
                        oSolicitudEntidad.Saldo_Act = Reader.GetDecimalValue(reader, "Saldo_Act");
                        oSolicitudEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oSolicitudEntidad.Vendedor = new VendedorEntidad
                        {
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),
                        };
                        oSolicitudEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oSolicitudEntidad.TipoComision = Reader.GetTinyIntValue(reader, "TipoComision");
                        ListaSolicitudes.Add(oSolicitudEntidad);
                    }
                }
                return ListaSolicitudes.OrderBy(x => x.Fecha_Aprob_Rech).ToList(); ;
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

        public List<SolicitudEntidad> FiltrarRechazados(SolicitudEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Solicitud_Filtrar_Rechazados", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@TipoComision", SqlDbType.TinyInt)).Value = entidad.TipoComision;
                cmd.CommandType = CommandType.StoredProcedure;
                List<SolicitudEntidad> ListaSolicitudes = new List<SolicitudEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SolicitudEntidad oSolicitudEntidad = new SolicitudEntidad();
                        oSolicitudEntidad.N_Solicitud = Reader.GetStringValue(reader, "N_Solicitud");
                        oSolicitudEntidad.Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy");
                        oSolicitudEntidad.BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente");
                        oSolicitudEntidad.N_Operación = Reader.GetStringValue(reader, "N_Operación");
                        oSolicitudEntidad.Producto = new ProductoEntidad
                        {
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oSolicitudEntidad.SubProducto = new SubProductoEntidad
                        {
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oSolicitudEntidad.Moneda = Reader.GetStringValue(reader, "Moneda");
                        oSolicitudEntidad.Línea_Desembolsos = Reader.GetDecimalValue(reader, "Línea_Desembolsos");
                        oSolicitudEntidad.Plazo = Reader.GetSmallIntValue(reader, "Plazo");
                        oSolicitudEntidad.Saldo_Ant = Reader.GetDecimalValue(reader, "Saldo_Ant");
                        oSolicitudEntidad.Saldo_Act = Reader.GetDecimalValue(reader, "Saldo_Act");
                        oSolicitudEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oSolicitudEntidad.Vendedor = new VendedorEntidad
                        {
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),
                        };
                        oSolicitudEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        //oSolicitudEntidad.Tipo = Reader.GetTinyIntValue(reader, "Tipo");
                        oSolicitudEntidad.Reclamo = Reader.GetStringValue(reader, "Reclamo");
                        ListaSolicitudes.Add(oSolicitudEntidad);
                    }
                }
                return ListaSolicitudes.OrderBy(x => x.Fecha_Aprob_Rech).ToList(); ;
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

        public List<SolicitudEntidad> CalcularComisiones(SolicitudEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Solicitud_Calcular_Comision", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@TipoComision", SqlDbType.TinyInt)).Value = entidad.TipoComision;
                cmd.CommandType = CommandType.StoredProcedure;
                List<SolicitudEntidad> ListaSolicitudes = new List<SolicitudEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SolicitudEntidad oSolicitudEntidad = new SolicitudEntidad();
                        oSolicitudEntidad.N_Solicitud = Reader.GetStringValue(reader, "N_Solicitud");
                        oSolicitudEntidad.Fecha_Ingreso = Reader.GetDateTimeValue(reader, "Fecha_Ingreso").ToString("dd/MM/yyyy");
                        oSolicitudEntidad.Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy");
                        oSolicitudEntidad.BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente");
                        oSolicitudEntidad.N_Operación = Reader.GetStringValue(reader, "N_Operación");
                        oSolicitudEntidad.Region = new RegionEntidad
                        {
                            Cod_Region = Reader.GetStringValue(reader, "Cod_Region"),
                        };
                        oSolicitudEntidad.Provincia = new ProvinciaEntidad
                        {
                            Cod_Provincia = Reader.GetStringValue(reader, "Cod_Provincia"),
                        };
                        oSolicitudEntidad.Distrito = new DistritoEntidad
                        {
                            Cod_Distrito = Reader.GetStringValue(reader, "Cod_Distrito"),
                        };

                        oSolicitudEntidad.Producto = new ProductoEntidad
                        {
                            Cod_Producto = Reader.GetStringValue(reader, "Cod_Producto"),
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oSolicitudEntidad.SubProducto = new SubProductoEntidad
                        {
                            Cod_SubProducto = Reader.GetStringValue(reader, "Cod_SubProducto"),
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oSolicitudEntidad.Moneda = Reader.GetStringValue(reader, "Moneda");
                        oSolicitudEntidad.Línea_Desembolsos = Reader.GetDecimalValue(reader, "Línea_Desembolsos");
                        oSolicitudEntidad.Plazo = Reader.GetSmallIntValue(reader, "Plazo");
                        oSolicitudEntidad.Saldo_Ant = Reader.GetDecimalValue(reader, "Saldo_Ant");
                        oSolicitudEntidad.Saldo_Act = Reader.GetDecimalValue(reader, "Saldo_Act");
                        oSolicitudEntidad.Canal = new CanalEntidad
                        {
                            Cod_Canal = Reader.GetStringValue(reader, "Cod_Canal"),
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oSolicitudEntidad.Subcanal = new SubCanalEntidad
                        {
                            Cod_SubCanal = Reader.GetStringValue(reader, "Cod_SubCanal"),
                        };
                        oSolicitudEntidad.Vendedor = new VendedorEntidad
                        {
                            Cod_Vendedor = Reader.GetStringValue(reader, "Cod_Vendedor"),
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),
                        };
                        oSolicitudEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oSolicitudEntidad.FechaComision = Reader.GetStringValue(reader, "FechaComision");
                        oSolicitudEntidad.Tarifario = Reader.GetDecimalValue(reader, "Tarifario");
                        oSolicitudEntidad.Tipo = Reader.GetTinyIntValue(reader, "Tipo");
                        oSolicitudEntidad.TipoComision = Reader.GetTinyIntValue(reader, "TipoComision");
                        oSolicitudEntidad.Reclamo = Reader.GetStringValue(reader, "Reclamo");
                        ListaSolicitudes.Add(oSolicitudEntidad);
                    }
                }
                return ListaSolicitudes.OrderBy(x => x.Fecha_Aprob_Rech).ToList(); ;
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

        public List<SolicitudEntidad> ReporteSolicitudesAprobadas(SolicitudEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_Solicitud_Reporte_Solicitudes", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Value = (entidad.Vendedor.Cod_Vendedor != null ? entidad.Vendedor.Cod_Vendedor : "");
                cmd.Parameters.Add(new SqlParameter("@TipoComision", SqlDbType.TinyInt)).Value = entidad.TipoComision;
                cmd.Parameters.Add(new SqlParameter("@Cod_Reporte", SqlDbType.VarChar, 15)).Value = (entidad.Cod_Reporte_Solicitud != null ? entidad.Cod_Reporte_Solicitud : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<SolicitudEntidad> ListaSolicitudes = new List<SolicitudEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SolicitudEntidad oSolicitudEntidad = new SolicitudEntidad();
                        oSolicitudEntidad.Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy");
                        oSolicitudEntidad.N_Solicitud = Reader.GetStringValue(reader, "N_Solicitud");
                        oSolicitudEntidad.BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente");
                        oSolicitudEntidad.Nombre_Cliente = Reader.GetStringValue(reader, "Nombre_Cliente");
                        oSolicitudEntidad.Apellido1_Cliente = Reader.GetStringValue(reader, "Apellido1_Cliente");
                        oSolicitudEntidad.Apellido2_Cliente = Reader.GetStringValue(reader, "Apellido2_Cliente");
                        oSolicitudEntidad.N_Operación = Reader.GetStringValue(reader, "N_Operación");
                        oSolicitudEntidad.Producto = new ProductoEntidad
                        {
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oSolicitudEntidad.SubProducto = new SubProductoEntidad
                        {
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oSolicitudEntidad.Moneda = Reader.GetStringValue(reader, "Moneda");
                        oSolicitudEntidad.Línea_Desembolsos = Reader.GetDecimalValue(reader, "Línea_Desembolsos");
                        oSolicitudEntidad.Plazo = Reader.GetSmallIntValue(reader, "Plazo");
                        oSolicitudEntidad.Saldo_Ant = Reader.GetDecimalValue(reader, "Saldo_Ant");
                        oSolicitudEntidad.Saldo_Act = Reader.GetDecimalValue(reader, "Saldo_Act");
                        oSolicitudEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oSolicitudEntidad.Vendedor = new VendedorEntidad
                        {
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),
                        };
                        oSolicitudEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oSolicitudEntidad.TipoComision = Reader.GetTinyIntValue(reader, "TipoComision");
                        oSolicitudEntidad.Tipo = Reader.GetTinyIntValue(reader, "Tipo");
                        oSolicitudEntidad.Tarifario = Reader.GetDecimalValue(reader, "Tarifario");
                        oSolicitudEntidad.Cod_Reporte_Solicitud = Reader.GetStringValue(reader, "Cod_Reporte_Solicitud");
                        oSolicitudEntidad.Fecha_Reporte_Solicitud = Reader.GetDateTimeValue(reader, "Fecha_Reporte_Solicitud").ToString("dd/MM/yyyy");
                        ListaSolicitudes.Add(oSolicitudEntidad);
                    }
                }
                return ListaSolicitudes.OrderBy(x => x.Fecha_Aprob_Rech).ToList();
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
        public bool Activar(SolicitudEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = false;
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("usp_Solicitud_Activar", cn);
                cmd.Parameters.Add(new SqlParameter("@N_Solicitud", SqlDbType.VarChar, 15)).Value = entidad.N_Solicitud;
                cmd.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 2)).Value = entidad.Estado;
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

        public bool GrabarReporteSolicitudesAprobadas(List<SolicitudEntidad> lista,ref string codigo)
        {
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                string Cod_Reporte = "";
                Conector.abrirConexion();
                trans = Conector.Conexion.BeginTransaction();
                SqlCommand cmd_01 = new SqlCommand("usp_Correlativo_CrearCorrelativo", Conector.Conexion);
                cmd_01.CommandType = CommandType.StoredProcedure;
                cmd_01.Transaction = trans;
                cmd_01.Parameters.Add(new SqlParameter("@Flag", SqlDbType.VarChar, 3)).Value = "CRS";
                cmd_01.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar, 15)).Direction = ParameterDirection.Output;
                if (cmd_01.ExecuteNonQuery() > 0)
                    Cod_Reporte = cmd_01.Parameters["@Codigo"].Value.ToString();
                else
                    estado = false;

                if (string.IsNullOrWhiteSpace(Cod_Reporte) == false)
                {

                    foreach (var entidad in lista)
                    {
                        if (string.IsNullOrWhiteSpace(entidad.Cod_Reporte_Solicitud))
                        {
                            SqlCommand cmd_02 = new SqlCommand("usp_Solicitud_Generar_Reporte_Solicitudes", Conector.Conexion);
                            cmd_02.CommandType = CommandType.StoredProcedure;
                            cmd_02.Transaction = trans;
                            cmd_02.Parameters.Add(new SqlParameter("@N_Solicitud", SqlDbType.VarChar, 15)).Value = entidad.N_Solicitud;
                            cmd_02.Parameters.Add(new SqlParameter("@Cod_Reporte_Solicitud", SqlDbType.VarChar, 15)).Value = Cod_Reporte;
                            if (cmd_02.ExecuteNonQuery() < 1) { estado = false; break; }
                        }
                    }
                }
                if (estado){
                    codigo = Cod_Reporte;
                    trans.Commit();
                }

                else
                {
                    estado = false;
                    trans.Rollback();
                }
                return estado;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                Conector.cerrarConexion();
            }
        }
        #endregion
    }
}
