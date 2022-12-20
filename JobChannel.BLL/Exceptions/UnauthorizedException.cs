using System;

namespace JobChannel.BLL.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string? message) : base(message)
        {
        }
    }
}

