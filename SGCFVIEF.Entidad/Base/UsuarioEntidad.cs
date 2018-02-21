using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public class UsuarioEntidad
    {
        public byte Cod_Usuario { get; set; }
        public string Nom_Usuario { get; set; }
        public string Pass_Usuario { get; set; }
        public EmpleadoEntidad Empleado { get; set; }
        public PerfilEntidad Perfil { get; set; }

    }
}
