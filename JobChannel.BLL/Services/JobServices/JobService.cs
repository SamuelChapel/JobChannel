using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.Domain.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.JobServices
{
    public class JobService : IJobService
    {
        public readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository) => _jobRepository = jobRepository;

        public async Task<IEnumerable<Job>> GetAllJobs() => await _jobRepository.GetAllJob();
    }
}
