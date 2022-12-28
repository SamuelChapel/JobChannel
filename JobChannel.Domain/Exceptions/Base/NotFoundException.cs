using System;

namespace JobChannel.Domain.Exceptions.Base
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
