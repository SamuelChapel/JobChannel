using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;

namespace JobChannel.BLL.Services.ContractServices
{
    public class ContractService : IContractService
    {
        public readonly IUnitOfWork _dbContext;

        public ContractService(IUnitOfWork dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Contract>> GetAll() => await _dbContext.ContractRepository.GetAll();

        public async Task<Contract> GetById(int id) 
            => await _dbContext.ContractRepository.GetById(id) ?? throw new ContractNotFoundException();
    }
}
