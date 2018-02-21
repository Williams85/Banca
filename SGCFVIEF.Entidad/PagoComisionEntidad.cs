using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class PagoComisionEntidad
    {
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string NombreCompletoVendedor { get { return this.Vendedor.Nombre + "" + this.Vendedor.Apellido + "" + this.Vendedor.Apellido2; } }
        public decimal ComisionTarifario { get { return (this.TipoTarifa == 1 ? ((this.Saldo_Act) * (this.Tarifario / 100)) : this.Tarifario); } }

    }
}
