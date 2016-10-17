using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpPcl.CustomExceptions
{
    public class ApiGenericException : Exception
    {

        public ApiGenericException() : base()
        {
        }

        public ApiGenericException(string message)
            : base(message)
        {
        }

        public ApiGenericException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
