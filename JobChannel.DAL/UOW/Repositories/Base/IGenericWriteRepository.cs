using System.Threading.Tasks;
using JobChannel.Domain.Base;

namespace JobChannel.DAL.UOW.Repositories.Base
{
    public interface IGenericWriteRepository<T, Tid>  where T : BaseEntity<Tid>
    {
        /// <summary>
        /// Create a <typeparamref name="T"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="T"/> to create</param>
        /// <returns>The <typeparamref name="Tid"/> that corresponds to the id of the <typeparamref name="T"/> created</returns>
        Task<Tid> Create(T t);

        /// <summary>
        /// Modify a <typeparamref name="T"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="T"/> modified</param>
        /// <returns>The number of <typeparamref name="T"/> modified</returns>
        Task<int> Update(T t);

        /// <summary>
        /// Delete a <typeparamref name="T"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="Tid"/> who correspond to the <typeparamref name="T"/></param>
        /// <returns>The number of <typeparamref name="T"/> modified</returns>
        Task<int> Delete(Tid id);
    }
}
