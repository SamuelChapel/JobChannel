using System;

namespace JobChannel.DAL.Services.EnvironmentVariable.Exceptions
{
    public class MissingEnvironmentVariableException : Exception
    {
        public MissingEnvironmentVariableException(string? environmentVariable) : base($"'{environmentVariable}' is missing in the environment.")
        {

        }
    }
}