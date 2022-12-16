using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.JobRepositories
{
    public interface IJobRepository : IGenericReadRepository<Job, int>
    {
    }
}