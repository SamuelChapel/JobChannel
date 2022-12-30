using System.Threading.Tasks;

namespace JobChannel.BLL.Services.PoleEmploi.JobOffers
{
    public interface IJobOfferPoleEmploiService
    {
        Task<int> GetAndInsertPoleEmploiJobOffers(GetPoleEmploiJobOffersQuery query);
    }
}