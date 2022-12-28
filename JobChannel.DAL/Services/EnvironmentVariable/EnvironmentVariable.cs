using System;
using JobChannel.DAL.Services.EnvironmentVariable.Exceptions;

namespace JobChannel.DAL.Services.EnvironmentVariable
{
    public class EnvironmentVariable : IEnvironmentVariable
    {
        public string GetEnvironmentVariable(string environmentVariableName)
        {
            var value = Environment.GetEnvironmentVariable(environmentVariableName) ?? throw new MissingEnvironmentVariableException(environmentVariableName);

            if (string.IsNullOrEmpty(value))
            {
                throw new MissingEnvironmentVariableException(environmentVariableName);
            }

            return value;
        }
    }
}