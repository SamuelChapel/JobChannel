using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.ContractRepositories;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.ContractServices
{
    public class ContractService : IContractService
    {
        public readonly IContractRepository _contractRepository;

        public ContractService(IContractRepository contractRepository) => _contractRepository = contractRepository;

        public async Task<IEnumerable<Contract>> GetAllContracts() => await _contractRepository.GetAllContracts();
    }
}
