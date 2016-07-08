using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Common.Exceptions
{
    public class PonudaCreateException : ApplicationException
    {
        public PonudaCreateException(string message)
            : base(message)
        {

        }

        public PonudaCreateException(string message, Exception ex)
            : base(message, ex)
        {

        }
    }
}
