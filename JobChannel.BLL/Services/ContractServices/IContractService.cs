using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.ContractServices
{
    public interface IContractService
    {
        Task<IEnumerable<Contract>> GetAllContracts();

        Task<Contract> GetById(int id);
    }
}