using System.Threading.Tasks;
using JobChannel.Domain.Base;

namespace JobChannel.DAL.UOW.Repositories.Base
{
    public interface IGenericWriteRepository<T, Tid>  where T : BaseEntity<Tid>
    {
        Task<int> Create(T t);

        Task<int> Update(T t);

        Task<int> Delete(Tid t);
    }
}
