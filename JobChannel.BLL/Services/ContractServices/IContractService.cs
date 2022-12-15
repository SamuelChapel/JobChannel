using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.ContractServices
{
    public interface IContractService
    {
        /// <summary>
        /// Get all the contracts
        /// </summary>
        /// <returns>IEnumerable of contracts</returns>
        Task<IEnumerable<Contract>> GetAllContracts();

        /// <summary>
        /// Get a contract by his id
        /// </summary>
        /// <param name="id">Contract Id to find</param>
        /// <returns>Contract with correponds with this id</returns>
        Task<Contract> GetById(int id);
    }
}