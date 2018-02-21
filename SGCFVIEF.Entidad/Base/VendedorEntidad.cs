using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class VendedorEntidad
    {
        public string Cod_Vendedor { get; set; }
        public CanalEntidad Canal { get; set; }
        public SubCanalEntidad SubCanal { get; set; }
        public RegionEntidad Region { get; set; }
        public ProvinciaEntidad Provincia { get; set; }
        public DistritoEntidad Distrito { get; set; }
        public TipoDocumentoEntidad Tipo_Doc { get; set; }
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
        public string Fecha_Ult_Camb { get; set; }
        public UsuarioEntidad   Usuario { get; set; }


    }
}
