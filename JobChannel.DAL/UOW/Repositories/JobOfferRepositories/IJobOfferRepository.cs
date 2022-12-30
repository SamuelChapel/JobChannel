using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.JobOfferRepositories
{
    public interface IJobOfferRepository : IGenericWriteRepository<JobOffer, int>
    {
        /// <summary>
        /// Get a <typeparamref name="JobOffer"/> by his id 
        /// </summary>
        /// <param name="id"><typeparamref name="JobOffer"/> id to find</param>
        /// <returns>the <typeparamref name="JobOffer"/> matching the id</returns>
        /// <exception cref="JobOfferNotFoundException"></exception>
        Task<JobOffer> GetById(int id);

        /// <summary>
        /// Find all <typeparamref name="JobOffer"/>
        /// </summary>
        /// <param name="searchFields">Dictionary of search criteria</param>
        /// <returns>All the <typeparamref name="JobOffer"/> matching the criteria</returns>
        /// <exception cref="Exception"></exception>
        Task<IEnumerable<JobOffer>> GetAll(IReadOnlyDictionary<string, dynamic>? searchFields);
    }
}