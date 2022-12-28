using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.PoleEmploi.JobOffers
{
    public interface IJobOfferPoleEmploiService
    {
        Task<IEnumerable<JobOfferPoleEmploi>> GetJobOffers(GetPoleEmploiJobOffersQuery query);
        Task<bool> InsertJobOfferPoleEmploi(JobOfferPoleEmploi jobOfferPoleEmploi);
    }
}