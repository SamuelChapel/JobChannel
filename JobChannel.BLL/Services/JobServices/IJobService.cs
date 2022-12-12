using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.JobServices
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllJobs();
    }
}