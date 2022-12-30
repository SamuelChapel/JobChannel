using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.DAL.UOW.Repositories.ContractRepositories;
using JobChannel.DAL.UOW.Repositories.DepartmentRepositories;
using JobChannel.DAL.UOW.Repositories.JobOfferRepositories;
using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.DAL.UOW.Repositories.RegionRepositories;

namespace JobChannel.DAL.UOW
{
    public interface IUnitOfWork
    {
        ICityRepository CityRepository { get; }
        IContractRepository ContractRepository { get; }
        IDbSession DbSession { get; set; }
        IJobRepository JobRepository { get; }
        IDepartmentRepository DepartmentRepository { get; } 
        IRegionRepository RegionRepository { get; }
        IJobOfferRepository JobOfferRepository { get; }

        /// <summary>
        /// Start a Transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit a transaction
        /// </summary>
        void Commit();

        /// <summary>
        /// Dispose the 
        /// </summary>
        void Dispose();

        /// <summary>
        /// Rollback the transaction
        /// </summary>
        void Rollback();
    }
}