using System.Data.Common;
using System.Data.SqlClient;
using System;
using Microsoft.Extensions.Configuration;

namespace JobChannel.DAL.UOW
{
    public class DbSession : IDbSession
    {
        public DbConnection Connection { get; private set; }

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
