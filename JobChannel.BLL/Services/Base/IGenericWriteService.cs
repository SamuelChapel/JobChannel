using System.Threading.Tasks;
using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.Domain.Base;

namespace JobChannel.BLL.Services.Base
{
    public interface IGenericWriteService<T, Tid> where T : BaseEntity<Tid>
    {
        public Task<int> Create(T request);

        public Task<int> Update(T request);

        public Task<int> Delete(Tid id);
    }
}

