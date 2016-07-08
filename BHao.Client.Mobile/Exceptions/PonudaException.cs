using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.Exceptions
{
    public class PonudaException : Exception
    {
        public PonudaException()
            : base()
        {

        }

        public PonudaException(string message)
            : base(message)
        {

        }

        public PonudaException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
