using System.Data.SqlClient;
using System;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace JobChannel.DAL.UOW
{
    internal class DbSession : IDbSession
    {
        public IDbConnection Connection { get; set; }

        public IDbTransaction? Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection
            {
                ConnectionString = configuration.GetConnectionString("OVH")
            };

            Connection.Open();
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
