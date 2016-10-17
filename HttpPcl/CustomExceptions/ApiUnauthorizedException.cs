using System;

namespace HttpPcl.CustomExceptions
{
    public class ApiUnauthorizedException : Exception
    {
        
            public ApiUnauthorizedException() : base()
            {
            }

            public ApiUnauthorizedException(string message)
                : base(message)
            {
            }

            public ApiUnauthorizedException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
    
}
