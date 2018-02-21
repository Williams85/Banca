using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class TarifarioEntidad
    {
        public int Cod_Tarifa { get; set; }
        public CanalEntidad Canal { get; set; }
        public SubCanalEntidad SubCanal { get; set; }
        public ProductoEntidad Producto { get; set; }
        public SubProductoEntidad SubProducto { get; set; }
        public decimal Tarifa_Inicio { get; set; }
        public decimal Tarifa_Fin { get; set; }
        public byte Tipo { get; set; }
        public decimal Tarifario { get; set; }
        public string Fecha_Ultimo { get; set; }
        public string Fecha_Registro { get; set; }
        public int TipoComision { get; set; }
        public string Estado { get; set; }
        public UsuarioEntidad Usuario { get; set; }

    }
}
