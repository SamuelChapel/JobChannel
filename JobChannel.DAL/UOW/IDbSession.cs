using System;
using System.Data;

namespace JobChannel.DAL.UOW
{
    public interface IDbSession : IDisposable
    {
        public IDbConnection Connection { get; set; }

        public IDbTransaction? Transaction { get; set; }
    }
}
