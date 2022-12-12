using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.ContractService
{
    public interface IContractService
    {
        Task<IEnumerable<Contract>> GetAllContracts();
    }
}