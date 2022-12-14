using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.JobServices
{
    public class JobService : IJobService
    {
        public readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository) => _jobRepository = jobRepository;

        public async Task<IEnumerable<Job>> GetAllJobs() => await _jobRepository.GetAllJob();

        public async Task<Job> GetById(int id)
        {
            return await _jobRepository.GetJobById(id) ?? throw new JobNotFoundException();
        }
    }
}
