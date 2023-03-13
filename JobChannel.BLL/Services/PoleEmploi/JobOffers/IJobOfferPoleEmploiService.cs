using System.Threading.Tasks;

namespace JobChannel.BLL.Services.PoleEmploi.JobOffers
{
    public interface IJobOfferPoleEmploiService
    {
        Task CleanUpJobOffers();

        /// <summary>
        /// Get job offer's from "pôle emploi" and insert it
        /// </summary>
        /// <param name="query">The <typeparamref name="GetPoleEmploiJobOffersQuery"/> who contains the parameters for the job offer search</param>
        /// <returns>The number of job offer's added</returns>
        Task<int> GetAndInsertPoleEmploiJobOffers(GetPoleEmploiJobOffersQuery query);
    }
}