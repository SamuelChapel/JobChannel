using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.JobOfferServices
{
    public interface IJobOfferService
    {
        public Task<JobOffer> GetById(int id);
        public Task<IEnumerable<JobOffer>> GetAll(IReadOnlyDictionary<string, dynamic>? searchFields);
        public Task<int> Create(
            JobOffer request,
            IJobService jobService,
            ICityService cityService,
            IContractService contractService);
        public Task<int> Update(JobOffer request);
        public Task<int> Delete(int id);
    }
}
