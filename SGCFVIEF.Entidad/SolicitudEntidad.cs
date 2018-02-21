using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class SolicitudEntidad
    {
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string FechaComision { get; set; }
        public decimal Tarifario { get; set; }
        public byte Tipo { get; set; }
        public string NombreCompletoVendedor { get { return this.Vendedor.Nombre + " " + this.Vendedor.Apellido + " " + this.Vendedor.Apellido2; } }
        public string NombreCompletoCliente { get { return this.Nombre_Cliente + " " + this.Apellido1_Cliente + " " + this.Apellido2_Cliente; } }
        public string Reclamo { get; set; }
        public decimal ComisionTarifario { get { return (this.Tipo == 1 ? ((this.Saldo_Act) * (this.Tarifario / 100)) : this.Tarifario); } }
    }
}
