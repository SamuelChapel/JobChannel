using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JobChannel.Tests.Integrations.Fixtures
{
    public static class TestData
    {
        //Initialise un jeu de données dans la base de données utilisée pour les TI 
        public static void Initialize(IConfiguration configuration)
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("OVH"));
            connection.Open();
            string script = @"";
            SqlCommand command = new SqlCommand(script, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
