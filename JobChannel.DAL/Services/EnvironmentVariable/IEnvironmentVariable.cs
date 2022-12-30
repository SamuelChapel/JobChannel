namespace JobChannel.DAL.Services.EnvironmentVariable
{
    public interface IEnvironmentVariable
    {
        /// <summary>
        /// Get an Environment variable by it's name
        /// </summary>
        /// <param name="environmentVariableName">Name of the environment variable</param>
        /// <returns>The environment variable</returns>
        public string GetEnvironmentVariable(string environmentVariableName);
    }
}