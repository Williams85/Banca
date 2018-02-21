using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class SolicitudEntidad
    {
        public string N_Solicitud { get; set; }
        public CanalEntidad Canal { get; set; }
        public SubCanalEntidad Subcanal { get; set; }
        public RegionEntidad Region { get; set; }
        public ProvinciaEntidad Provincia { get; set; }
        public DistritoEntidad Distrito { get; set; }
        public VendedorEntidad Vendedor { get; set; }
        public TipoDocumentoEntidad Tipo_Doc { get; set; }
        public string Num_Doc { get; set; }
        public string Moneda { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Apellido1_Cliente { get; set; }
        public string Apellido2_Cliente { get; set; }
        public string Dirección { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Celular { get; set; }
        public string Fecha_Ingreso { get; set; }
        public string Fecha_Aprob_Rech { get; set; }
        public string Estado { get; set; }
        public decimal Línea_Desembolsos { get; set; }
        public ProductoEntidad Producto { get; set; }
        public SubProductoEntidad SubProducto { get; set; }
        public short Plazo { get; set; }
        public decimal Saldo_Ant { get; set; }
        public decimal Saldo_Act { get; set; }
        public string Observaciones { get; set; }
        public string BT_Cliente { get; set; }
        public string N_Operación { get; set; }
        public byte TipoComision { get; set; }
        public string Cod_Reporte_Solicitud { get; set; }
        public string Fecha_Reporte_Solicitud { get; set; }
    }
}
