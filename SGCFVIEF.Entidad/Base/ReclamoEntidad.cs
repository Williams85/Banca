using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public partial class ReclamoEntidad
    {
        public string Cod_Reclamo { get; set; }
        public SolicitudEntidad Solicitud { get; set; }
        public MotivoRechazoSolicitudEntidad MotivoRechazoSolicitud { get; set; }
        public string Observaciones { get; set; }
        public UsuarioEntidad Usuario { get; set; }
        public RespuestaReclamoEntidad RespuestaReclamo { get; set; }
        public TipoReclamoEntidad TipoReclamo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Estado { get; set; }


    }
}
