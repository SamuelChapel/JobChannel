using JobChannel.DAL.UOW;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.JobServices
{
    public class JobService : IJobService
    {
        public readonly IUnitOfWork _dbContext;

        public JobService(IUnitOfWork dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Job>> GetAll() => await _dbContext.JobRepository.GetAll();

        public async Task<Job?> GetById(int id) => await _dbContext.JobRepository.GetById(id) ?? throw new JobNotFoundException();
    }
}
