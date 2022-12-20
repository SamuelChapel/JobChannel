using System;

namespace JobChannel.BLL.Exceptions
{
    public class BadRequestException : Exception
    {
        protected BadRequestException(string? message) : base(message)
        {
        }
    }
}

