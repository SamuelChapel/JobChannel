using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.Base;

namespace JobChannel.DAL.UOW.Repositories.Base
{
    public interface IGenericReadRepository<T, Tid> where T : BaseEntity<Tid> where Tid : struct
    {
        Task<T> GetById(Tid id);

        Task<IEnumerable<T>> GetAll();
    }
}
