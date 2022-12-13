using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.JobOfferRepositories
{
    public interface IJobOfferRepository
    {
        Task<IEnumerable<JobOffer>> GetJobOffersAsync();

        Task<JobOffer> CreateJobOffer(JobOffer request);

        Task<int> DeleteJobOffer(int id);

        Task<JobOffer> UpdateJobOffer(JobOffer request);
    }
}