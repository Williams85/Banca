using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public class ProvinciaEntidad
    {
        public string Cod_Provincia { get; set; }
        public string Descripcion { get; set; }
        public RegionEntidad Region { get; set; }
        public bool Estado { get; set; }
    }
}
