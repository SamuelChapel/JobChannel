using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.Base;

namespace JobChannel.BLL.Services.Base
{
    public interface IGenericReadService<T, Tid> where T : BaseEntity<Tid> where Tid : struct
    {
        Task<T> GetById(Tid id);

        Task<IEnumerable<T>> GetAll();
    }
}
