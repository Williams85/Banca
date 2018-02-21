using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class VendedorEntidad
    {
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Cod_SubCanal { get; set; }
        public string Cod_Canal { get; set; }
        public string NombreCompleto { get { return this.Nombre + " " + this.Apellido + " " + this.Apellido2; } }

    }
}
