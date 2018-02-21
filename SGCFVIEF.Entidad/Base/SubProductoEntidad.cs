using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Entidad
{
    public class SubProductoEntidad
    {
        public string Cod_SubProducto { get; set; }
        public ProductoEntidad Producto { get; set; }
        public string SubProducto { get; set; }

    }
}
