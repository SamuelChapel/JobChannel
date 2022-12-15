using System;
using System.Data;

namespace JobChannel.DAL.UOW
{
    public interface IDbSession : IDisposable
    {
        public IDbConnection Connection { get; }

        public IDbTransaction? Transaction { get; set; }
    }
}
