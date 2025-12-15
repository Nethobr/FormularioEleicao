using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormularioEleicao.Dominio.Conceito
{
    public class DominioException : Exception
    {
        public DominioException() { }

        public DominioException(string message) : base(message) { }

        public DominioException(string message, Exception innerException) : base(message, innerException) { }
    }
}
