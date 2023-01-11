using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace JobChannel.Tests.Integrations.Fixtures
{
    public static class TestData
    {
        //Initialise un jeu de données dans la base de données utilisée pour les TI 
        public static void Initialize(IConfiguration configuration)
        {
            SqlConnection connection = new SqlConnection(configuration.GetConnectionString("default"));
            connection.Open();
            string script = @"";
            SqlCommand command = new SqlCommand(script, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
