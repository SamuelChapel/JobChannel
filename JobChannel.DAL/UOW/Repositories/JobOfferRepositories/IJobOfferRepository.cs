using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.JobOfferRepositories
{
    public interface IJobOfferRepository
    {
        Task<IEnumerable<JobOffer>> GetJobOffers();

        Task<int> CreateJobOffer(JobOffer request);

        Task<int> DeleteJobOffer(int id);

        Task<int> UpdateJobOffer(JobOffer request);
    }
}