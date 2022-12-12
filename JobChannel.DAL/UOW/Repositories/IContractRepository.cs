using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAllContract();
    }
}
