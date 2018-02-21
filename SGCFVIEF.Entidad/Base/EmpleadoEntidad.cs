using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class EmpleadoEntidad
    {
        public string Cod_Empleado { get; set; }
        public AgenciaEntidad Agencia { get; set; }
        public TipoDocumentoEntidad TipoDocumento { get; set; }
        public string Num_Doc { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Apellido2 { get; set; }
        public string Direccion { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Celular { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Cese { get; set; }
        public string Estado { get; set; }

    }
}
