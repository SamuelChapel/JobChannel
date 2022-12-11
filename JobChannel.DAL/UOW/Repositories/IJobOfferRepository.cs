using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories
{
    public interface IJobOfferRepository
    {
        Task<IEnumerable<JobOffer>> GetJobOffersAsync();
    }
}