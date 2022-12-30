using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.ContractRepositories
{
    public interface IContractRepository : IGenericReadRepository<Contract, int>
    {
        /// <summary>
        /// Get a <typeparamref name="Contract"/> by it's code name
        /// </summary>
        /// <param name="code">The <typeparamref name="string"/> of the code</param>
        /// <returns>The <typeparamref name="Contract"/> who corresponds to this code</returns>
        Task<Contract> GetByCode(string code);
    }
}
