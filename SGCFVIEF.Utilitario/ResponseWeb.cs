using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Utilitario
{
    public class ResponseWeb<T>
    {
        public bool Estado { get; set; }
        public string Message { get; set; }
        public T Valor { get; set; }

    }
}
