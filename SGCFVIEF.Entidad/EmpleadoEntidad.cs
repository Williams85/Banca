using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class EmpleadoEntidad
    {
        public string NombreCompleto { get { return this.Nombre + " " + this.Apellido + " " + this.Apellido2; } }
    }
}
