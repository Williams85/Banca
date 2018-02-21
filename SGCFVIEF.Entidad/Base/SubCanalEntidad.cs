using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class SubCanalEntidad
    {
        public string Cod_SubCanal { get; set; }
        public CanalEntidad Canal { get; set; }
        public string SubCanal { get; set; }
        public string Direccion { get; set; }
        public RegionEntidad Region { get; set; }
        public ProvinciaEntidad Provincia { get; set; }
        public DistritoEntidad Distrito { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Cese { get; set; }
        public string Estado { get; set; }
        public string Fecha_Ult_Camb { get; set; }
        public UsuarioEntidad Usuario { get; set; }


    }
}
