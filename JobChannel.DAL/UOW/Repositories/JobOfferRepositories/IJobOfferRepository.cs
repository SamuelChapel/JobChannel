using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.JobOfferRepositories
{
    public interface IJobOfferRepository : IGenericWriteRepository<JobOffer, int>
    {
        Task<JobOffer> GetById(int id);

        Task<IEnumerable<JobOffer>> GetAll(IReadOnlyDictionary<string, dynamic>? searchFields);
    }
}