using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public class DistritoEntidad 
    {
        public string Cod_Distrito { get; set; }
        public ProvinciaEntidad Provincia { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
