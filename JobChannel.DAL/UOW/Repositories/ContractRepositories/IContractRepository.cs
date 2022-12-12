using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.ContractRepositories
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAllContracts();
    }
}
