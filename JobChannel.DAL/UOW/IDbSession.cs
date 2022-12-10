using System;
using System.Data.Common;

namespace JobChannel.DAL.UOW
{
    public interface IDbSession : IDisposable
    {
        public DbConnection Connection { get; }
    }
}
