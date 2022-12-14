using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.JobRepositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllJob();

        Task<Job?> GetJobById(int id);
    }
}