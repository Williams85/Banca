using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class CanalEntidad
    {
        public string Cod_Canal { get; set; }
        public string Canal { get; set; }
        public string RUC { get; set; }
        public string Razon_Social { get; set; }
        public string Direccion { get; set; }
        public RegionEntidad Region { get; set; }
        public ProvinciaEntidad Provincia { get; set; }
        public DistritoEntidad Distrito { get; set; }

        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Representante_Legal { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Cese { get; set; }
        public string Estado { get; set; }
        public string Fecha_Ult_Camb { get; set; }
        public UsuarioEntidad   Usuario { get; set; }

    }
}
