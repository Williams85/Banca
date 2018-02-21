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
    public class PagoComisionRepositorio
    {
        #region "Metodos No Transaccionales"
        public List<PagoComisionEntidad> FiltrarActivas(PagoComisionEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_PagoComision_Filtrar", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@TipoComision", SqlDbType.TinyInt)).Value = entidad.TipoComision;
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<PagoComisionEntidad> ListaPagoComisiones = new List<PagoComisionEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PagoComisionEntidad oPagoComisionEntidad = new PagoComisionEntidad();
                        oPagoComisionEntidad.IdPagoComision = Reader.GetIntValue(reader, "IdPagoComision");
                        oPagoComisionEntidad.Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy");
                        oPagoComisionEntidad.BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente");
                        oPagoComisionEntidad.N_Operación = Reader.GetStringValue(reader, "N_Operación");
                        oPagoComisionEntidad.Producto = new ProductoEntidad
                        {
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oPagoComisionEntidad.SubProducto = new SubProductoEntidad
                        {
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oPagoComisionEntidad.Moneda = Reader.GetStringValue(reader, "Moneda");
                        oPagoComisionEntidad.Línea_Desembolsos = Reader.GetDecimalValue(reader, "Línea_Desembolsos");
                        oPagoComisionEntidad.Plazo = Reader.GetSmallIntValue(reader, "Plazo");
                        oPagoComisionEntidad.Saldo_Act = Reader.GetDecimalValue(reader, "Saldo_Act");
                        oPagoComisionEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oPagoComisionEntidad.Subcanal = new SubCanalEntidad
                        {
                            SubCanal = Reader.GetStringValue(reader, "SubCanal"),
                        };

                        oPagoComisionEntidad.Vendedor = new VendedorEntidad
                        {
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),
                        };
                        oPagoComisionEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oPagoComisionEntidad.FechaComision = Reader.GetDateTimeValue(reader, "FechaComision").ToString("dd/MM/yyyy");
                        oPagoComisionEntidad.MontoComision = Reader.GetDecimalValue(reader, "MontoComision");
                        oPagoComisionEntidad.Tarifario = Reader.GetDecimalValue(reader, "Tarifario");
                        oPagoComisionEntidad.TipoTarifa = Reader.GetTinyIntValue(reader, "TipoTarifa");
                        oPagoComisionEntidad.Cod_Comision = Reader.GetStringValue(reader, "Cod_Comision");
                        oPagoComisionEntidad.CuentaContable1 = Reader.GetStringValue(reader, "CuentaContable1");
                        oPagoComisionEntidad.FechaDiferido = Reader.GetStringValue(reader, "FechaDiferido");
                        oPagoComisionEntidad.Solicitud = new SolicitudEntidad
                        {
                            Estado = Reader.GetStringValue(reader, "Estado_Solicitud")
                        };
                        oPagoComisionEntidad.MontoDiferido = Reader.GetDecimalValue(reader, "MontoDiferido");
                        oPagoComisionEntidad.SaldoPendiente = Reader.GetDecimalValue(reader, "MontoComision");
                        oPagoComisionEntidad.SaldoPagado = 0;
                        oPagoComisionEntidad.CuentaContable2 = "4525180000";

                        ListaPagoComisiones.Add(oPagoComisionEntidad);
                    }
                }
                return ListaPagoComisiones;
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

        public List<PagoComisionEntidad> FiltrarDiferidos(PagoComisionEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_PagoComision_FiltrarDiferido", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@TipoComision", SqlDbType.TinyInt)).Value = entidad.TipoComision;
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<PagoComisionEntidad> ListaPagoComisiones = new List<PagoComisionEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PagoComisionEntidad oPagoComisionEntidad = new PagoComisionEntidad();
                        oPagoComisionEntidad.Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy");
                        oPagoComisionEntidad.BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente");
                        oPagoComisionEntidad.N_Operación = Reader.GetStringValue(reader, "N_Operación");
                        oPagoComisionEntidad.Producto = new ProductoEntidad
                        {
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oPagoComisionEntidad.SubProducto = new SubProductoEntidad
                        {
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oPagoComisionEntidad.Moneda = Reader.GetStringValue(reader, "Moneda");
                        oPagoComisionEntidad.Línea_Desembolsos = Reader.GetDecimalValue(reader, "Línea_Desembolsos");
                        oPagoComisionEntidad.Plazo = Reader.GetSmallIntValue(reader, "Plazo");
                        oPagoComisionEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oPagoComisionEntidad.Subcanal = new SubCanalEntidad
                        {
                            SubCanal = Reader.GetStringValue(reader, "SubCanal"),
                        };

                        oPagoComisionEntidad.Vendedor = new VendedorEntidad
                        {
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),
                        };
                        oPagoComisionEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oPagoComisionEntidad.FechaComision = Reader.GetDateTimeValue(reader, "FechaComision").ToString("dd/MM/yyyy");
                        oPagoComisionEntidad.MontoComision = Reader.GetDecimalValue(reader, "MontoComision");
                        oPagoComisionEntidad.Tarifario = Reader.GetDecimalValue(reader, "Tarifario");
                        oPagoComisionEntidad.TipoTarifa = Reader.GetTinyIntValue(reader, "TipoTarifa");
                        oPagoComisionEntidad.Cod_Comision = Reader.GetStringValue(reader, "Cod_Comision");
                        oPagoComisionEntidad.CuentaContable1 = Reader.GetStringValue(reader, "CuentaContable1");
                        oPagoComisionEntidad.FechaDiferido = Reader.GetDateTimeValue(reader, "FechaDiferido").ToString("dd/MM/yyyy");
                        oPagoComisionEntidad.Solicitud = new SolicitudEntidad
                        {
                            Estado = Reader.GetStringValue(reader, "Estado_Solicitud")
                        };
                        oPagoComisionEntidad.MontoDiferido = Reader.GetDecimalValue(reader, "MontoDiferido");
                        oPagoComisionEntidad.SaldoPendiente = Reader.GetDecimalValue(reader, "SaldoPendiente");
                        oPagoComisionEntidad.SaldoPagado = Reader.GetDecimalValue(reader, "SaldoPagado");
                        oPagoComisionEntidad.CuentaContable2 = Reader.GetStringValue(reader, "CuentaContable2");
                        oPagoComisionEntidad.Cod_Diferido = Reader.GetStringValue(reader, "Cod_Diferido");

                        ListaPagoComisiones.Add(oPagoComisionEntidad);
                    }
                }
                return ListaPagoComisiones;
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

        public List<PagoComisionEntidad> ReporteComisiones(PagoComisionEntidad entidad)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            try
            {
                Conexion.abrirConexion(cn);
                SqlCommand cmd = new SqlCommand("usp_PagoComision_Reporte_Comisiones", cn);
                cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaInicio);
                cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaFin);
                cmd.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = (entidad.Canal.Cod_Canal != null ? entidad.Canal.Cod_Canal : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = (entidad.Producto.Cod_Producto != null ? entidad.Producto.Cod_Producto : "");
                cmd.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Value = (entidad.Vendedor.Cod_Vendedor != null ? entidad.Vendedor.Cod_Vendedor : "");
                cmd.Parameters.Add(new SqlParameter("@TipoComision", SqlDbType.TinyInt)).Value = entidad.TipoComision;
                cmd.Parameters.Add(new SqlParameter("@Cod_Reporte", SqlDbType.VarChar, 15)).Value = (entidad.Cod_Reporte_Comision != null ? entidad.Cod_Reporte_Comision : "");
                cmd.CommandType = CommandType.StoredProcedure;
                List<PagoComisionEntidad> ListaPagoComisiones = new List<PagoComisionEntidad>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PagoComisionEntidad oPagoComisionEntidad = new PagoComisionEntidad();
                        oPagoComisionEntidad.IdPagoComision = Reader.GetIntValue(reader, "IdPagoComision");
                        oPagoComisionEntidad.Fecha_Aprob_Rech = Reader.GetDateTimeValue(reader, "Fecha_Aprob_Rech").ToString("dd/MM/yyyy");
                        oPagoComisionEntidad.BT_Cliente = Reader.GetStringValue(reader, "BT_Cliente");
                        oPagoComisionEntidad.N_Operación = Reader.GetStringValue(reader, "N_Operación");
                        oPagoComisionEntidad.Producto = new ProductoEntidad
                        {
                            Producto = Reader.GetStringValue(reader, "Producto"),
                        };
                        oPagoComisionEntidad.SubProducto = new SubProductoEntidad
                        {
                            SubProducto = Reader.GetStringValue(reader, "SubProducto"),
                        };
                        oPagoComisionEntidad.Moneda = Reader.GetStringValue(reader, "Moneda");
                        oPagoComisionEntidad.Línea_Desembolsos = Reader.GetDecimalValue(reader, "Línea_Desembolsos");
                        oPagoComisionEntidad.Plazo = Reader.GetSmallIntValue(reader, "Plazo");
                        oPagoComisionEntidad.Saldo_Act = Reader.GetDecimalValue(reader, "Saldo_Act");
                        oPagoComisionEntidad.Canal = new CanalEntidad
                        {
                            Canal = Reader.GetStringValue(reader, "Canal"),
                        };
                        oPagoComisionEntidad.Subcanal = new SubCanalEntidad
                        {
                            SubCanal = Reader.GetStringValue(reader, "SubCanal"),
                        };

                        oPagoComisionEntidad.Vendedor = new VendedorEntidad
                        {
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),
                        };
                        oPagoComisionEntidad.Estado = Reader.GetStringValue(reader, "Estado");
                        oPagoComisionEntidad.FechaComision = Reader.GetDateTimeValue(reader, "FechaComision").ToString("dd/MM/yyyy");
                        oPagoComisionEntidad.MontoComision = Reader.GetDecimalValue(reader, "MontoComision");
                        oPagoComisionEntidad.Tarifario = Reader.GetDecimalValue(reader, "Tarifario");
                        oPagoComisionEntidad.TipoTarifa = Reader.GetTinyIntValue(reader, "TipoTarifa");
                        oPagoComisionEntidad.Cod_Comision = Reader.GetStringValue(reader, "Cod_Comision");
                        oPagoComisionEntidad.CuentaContable1 = Reader.GetStringValue(reader, "CuentaContable1");
                        oPagoComisionEntidad.FechaDiferido = Reader.GetStringValue(reader, "FechaDiferido");
                        oPagoComisionEntidad.Solicitud = new SolicitudEntidad
                        {
                            Estado = Reader.GetStringValue(reader, "Estado_Solicitud")
                        };
                        oPagoComisionEntidad.MontoDiferido = Reader.GetDecimalValue(reader, "MontoDiferido");
                        oPagoComisionEntidad.SaldoPendiente = Reader.GetDecimalValue(reader, "MontoComision");
                        oPagoComisionEntidad.SaldoPagado = 0;
                        oPagoComisionEntidad.CuentaContable2 = "4525180000";
                        oPagoComisionEntidad.Fecha_Reporte_Comision = Reader.GetDateTimeValue(reader, "Fecha_Reporte_Comision").ToString("dd/MM/yyyy");
                        oPagoComisionEntidad.Cod_Reporte_Comision = Reader.GetStringValue(reader, "Cod_Reporte_Comision");

                        ListaPagoComisiones.Add(oPagoComisionEntidad);
                    }
                }
                return ListaPagoComisiones.OrderBy(x => x.Fecha_Aprob_Rech).ToList();
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

        public bool GrabarReporteComisiones(List<PagoComisionEntidad> lista, ref string codigo)
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
                cmd_01.Parameters.Add(new SqlParameter("@Flag", SqlDbType.VarChar, 3)).Value = "CRC";
                cmd_01.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar, 15)).Direction = ParameterDirection.Output;
                if (cmd_01.ExecuteNonQuery() > 0)
                    Cod_Reporte = cmd_01.Parameters["@Codigo"].Value.ToString();
                else
                    estado = false;

                if (string.IsNullOrWhiteSpace(Cod_Reporte) == false)
                {

                    foreach (var entidad in lista)
                    {
                        if (string.IsNullOrWhiteSpace(entidad.Cod_Reporte_Comision))
                        {
                            SqlCommand cmd_02 = new SqlCommand("usp_PagoComision_Generar_Reporte_Comisiones", Conector.Conexion);
                            cmd_02.CommandType = CommandType.StoredProcedure;
                            cmd_02.Transaction = trans;
                            cmd_02.Parameters.Add(new SqlParameter("@IdPagoComision", SqlDbType.Int)).Value = entidad.IdPagoComision;
                            cmd_02.Parameters.Add(new SqlParameter("@Cod_Reporte_Comision", SqlDbType.VarChar, 15)).Value = Cod_Reporte;
                            if (cmd_02.ExecuteNonQuery() < 1) { estado = false; break; }
                        }
                    }
                }
                if (estado)
                {
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
        public bool grabarDatos(List<SolicitudEntidad> lista, ref string Cod_Comision)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Cod_Comision = "";
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd_01 = new SqlCommand("usp_Correlativo_CrearCorrelativo", cn);
                cmd_01.CommandType = CommandType.StoredProcedure;
                cmd_01.Transaction = trans;
                cmd_01.Parameters.Add(new SqlParameter("@Flag", SqlDbType.VarChar, 2)).Value = "CC";
                cmd_01.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar, 15)).Direction = ParameterDirection.Output;
                if (cmd_01.ExecuteNonQuery() > 0)
                    Cod_Comision = cmd_01.Parameters["@Codigo"].Value.ToString();
                else
                    estado = false;

                if (string.IsNullOrWhiteSpace(Cod_Comision) == false)
                {

                    foreach (var entidad in lista)
                    {
                        SqlCommand cmd_02 = new SqlCommand("usp_PagoComision_Grabar", cn);
                        cmd_02.CommandType = CommandType.StoredProcedure;
                        cmd_02.Transaction = trans;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_Comision", SqlDbType.VarChar, 15)).Value = Cod_Comision;
                        cmd_02.Parameters.Add(new SqlParameter("@N_Solicitud", SqlDbType.VarChar, 15)).Value = entidad.N_Solicitud;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_Canal", SqlDbType.VarChar, 15)).Value = entidad.Canal.Cod_Canal;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_SubCanal", SqlDbType.VarChar, 15)).Value = entidad.Subcanal.Cod_SubCanal;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_Region", SqlDbType.VarChar,6)).Value = entidad.Region.Cod_Region;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_Provincia", SqlDbType.VarChar, 6)).Value = entidad.Provincia.Cod_Provincia;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_Distrito", SqlDbType.VarChar, 6)).Value = entidad.Distrito.Cod_Distrito;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_Vendedor", SqlDbType.VarChar, 15)).Value = entidad.Vendedor.Cod_Vendedor;
                        cmd_02.Parameters.Add(new SqlParameter("@Fecha_Ingreso", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Ingreso);
                        cmd_02.Parameters.Add(new SqlParameter("@Fecha_Aprob_Rech", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.Fecha_Aprob_Rech);
                        //cmd_02.Parameters.Add(new SqlParameter("@Estado", SqlDbType.VarChar, 2)).Value = entidad.Estado;
                        cmd_02.Parameters.Add(new SqlParameter("@Línea_Desembolsos", SqlDbType.Real)).Value = entidad.Línea_Desembolsos;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_Producto", SqlDbType.VarChar, 15)).Value = entidad.Producto.Cod_Producto;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_SubProducto", SqlDbType.VarChar, 15)).Value = entidad.SubProducto.Cod_SubProducto;
                        cmd_02.Parameters.Add(new SqlParameter("@Plazo", SqlDbType.SmallInt)).Value = entidad.Plazo;
                        cmd_02.Parameters.Add(new SqlParameter("@Saldo_Act", SqlDbType.Real)).Value = entidad.Saldo_Act;
                        cmd_02.Parameters.Add(new SqlParameter("@BT_Cliente", SqlDbType.VarChar, 15)).Value = entidad.BT_Cliente;
                        cmd_02.Parameters.Add(new SqlParameter("@N_Operación", SqlDbType.VarChar, 15)).Value = entidad.N_Operación;
                        cmd_02.Parameters.Add(new SqlParameter("@FechaComision", SqlDbType.SmallDateTime)).Value = entidad.FechaComision;
                        cmd_02.Parameters.Add(new SqlParameter("@MontoComision", SqlDbType.Real)).Value = entidad.ComisionTarifario;
                        cmd_02.Parameters.Add(new SqlParameter("@Tarifario", SqlDbType.Real)).Value = entidad.Tarifario;
                        cmd_02.Parameters.Add(new SqlParameter("@TipoTarifa", SqlDbType.TinyInt)).Value = entidad.Tipo;
                        cmd_02.Parameters.Add(new SqlParameter("@TipoComision", SqlDbType.TinyInt)).Value = entidad.TipoComision;
                        if (cmd_02.ExecuteNonQuery() < 1) { estado = false; break; }
                    }
                }




                if (estado)
                    trans.Commit();
                else
                {
                    estado = false;
                    Cod_Comision = "";
                    trans.Rollback();
                }


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
        public bool grabarDiferido(List<PagoComisionEntidad> lista, ref string Cod_Diferido)
        {
            SqlConnection cn = new SqlConnection(Conexion.CnBanca);
            SqlTransaction trans = null;
            try
            {
                bool estado = true;
                Cod_Diferido = "";
                Conexion.abrirConexion(cn);
                trans = cn.BeginTransaction();
                SqlCommand cmd_01 = new SqlCommand("usp_Correlativo_CrearCorrelativo", cn);
                cmd_01.CommandType = CommandType.StoredProcedure;
                cmd_01.Transaction = trans;
                cmd_01.Parameters.Add(new SqlParameter("@Flag", SqlDbType.VarChar, 2)).Value = "CD";
                cmd_01.Parameters.Add(new SqlParameter("@Codigo", SqlDbType.VarChar, 15)).Direction = ParameterDirection.Output;
                if (cmd_01.ExecuteNonQuery() > 0)
                    Cod_Diferido = cmd_01.Parameters["@Codigo"].Value.ToString();
                else
                    estado = false;

                if (string.IsNullOrWhiteSpace(Cod_Diferido) == false)
                {

                    foreach (var entidad in lista)
                    {
                        SqlCommand cmd_02 = new SqlCommand("usp_PagoComision_GrabarDiferido", cn);
                        cmd_02.CommandType = CommandType.StoredProcedure;
                        cmd_02.Transaction = trans;
                        cmd_02.Parameters.Add(new SqlParameter("@IdPagoComision", SqlDbType.Int)).Value = entidad.IdPagoComision;
                        cmd_02.Parameters.Add(new SqlParameter("@MontoDiferido", SqlDbType.Real)).Value = entidad.MontoDiferido;
                        cmd_02.Parameters.Add(new SqlParameter("@FechaDiferido", SqlDbType.SmallDateTime)).Value = DateTime.Parse(entidad.FechaDiferido);
                        cmd_02.Parameters.Add(new SqlParameter("@SaldoPendiente", SqlDbType.Real)).Value = entidad.SaldoPendiente;
                        cmd_02.Parameters.Add(new SqlParameter("@Cod_Diferido", SqlDbType.VarChar, 15)).Value = Cod_Diferido;
                        cmd_02.Parameters.Add(new SqlParameter("@CuentaContable2", SqlDbType.VarChar, 20)).Value = entidad.CuentaContable2;

                        if (cmd_02.ExecuteNonQuery() < 1) { estado = false; break; }
                    }
                }




                if (estado)
                    trans.Commit();
                else
                {
                    estado = false;
                    Cod_Diferido = "";
                    trans.Rollback();
                }


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
