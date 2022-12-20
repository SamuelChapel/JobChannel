using System;

namespace JobChannel.Domain.Exceptions
{
    public class JobNotFoundException : Exception
    {
        public JobNotFoundException() : base ("Le job n'existe pas") { }
    }
}
