using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;

namespace JobChannel.BLL.Services.JobOfferServices
{
    public interface IJobOfferService
    {
        public Task<IEnumerable<JobOffer>> GetAll();
        public Task<JobOffer> GetById(int id);
        public Task<bool> Create(JobOfferCreateRequest request);
        public Task<JobOffer> Update(JobOfferUpdateRequest request);
        public Task<int> Delete(int id);
    }
}
