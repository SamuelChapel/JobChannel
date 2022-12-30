using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.JobRepositories
{
    public interface IJobRepository : IGenericReadRepository<Job, int>
    {
        /// <summary>
        /// Get a <typeparamref name="Job"/> by it's romeCode
        /// </summary>
        /// <param name="romeCode">the <typeparamref name="string"/> of the romeCode</param>
        /// <returns>The <typeparamref name="Job"/> who corresponds to this romeCode</returns>
        Task<Job> GetByRomeCode(string romeCode);
    }
}