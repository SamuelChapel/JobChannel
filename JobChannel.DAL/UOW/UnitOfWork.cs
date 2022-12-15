using System;
using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.DAL.UOW.Repositories.ContractRepositories;
using JobChannel.DAL.UOW.Repositories.DepartmentRepositories;
using JobChannel.DAL.UOW.Repositories.JobOfferRepositories;
using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.DAL.UOW.Repositories.RegionRepositories;

namespace JobChannel.DAL.UOW
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        public IJobRepository JobRepository { get => new JobRepository(DbSession); }
        public IContractRepository ContractRepository { get => new ContractRepository(DbSession); }
        public ICityRepository CityRepository { get => new CityRepository(DbSession); }
        public IDepartmentRepository DepartmentRepository { get => new DepartmentRepository(DbSession); }
        public IRegionRepository RegionRepository { get => new RegionRepository(DbSession); }
        public IJobOfferRepository JobOfferRepository { get => new JobOfferRepository(DbSession); }

        public IDbSession DbSession { get; set; }

        public UnitOfWork(IDbSession dbSession) => DbSession = dbSession;

        public void BeginTransaction() => DbSession.Transaction = DbSession.Connection.BeginTransaction();

        public void Commit()
        {
            DbSession.Transaction?.Commit();
            DbSession.Transaction = null;
        }

        public void Rollback()
        {
            DbSession.Transaction?.Rollback();
            DbSession.Transaction = null;
        }

        public void Dispose() => Commit();
    }
}
