using System.Threading.Tasks;
using JobChannel.Domain.Base;

namespace JobChannel.BLL.Services.Base
{
    public interface IGenericWriteService<T, Tid> where T : BaseEntity<Tid>
    {
        /// <summary>
        /// Create a <typeparamref name="T"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="T"/> to create</param>
        /// <returns>The <typeparamref name="Tid"/> that corresponds to the id of the <typeparamref name="T"/> created</returns>
        public Task<Tid> Create(T request);

        /// <summary>
        /// Modify a <typeparamref name="T"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="T"/> modified</param>
        /// <returns>The number of <typeparamref name="T"/> modified</returns>
        public Task<int> Update(T request);

        /// <summary>
        /// Delete a <typeparamref name="T"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="Tid"/> who correspond to the <typeparamref name="T"/></param>
        /// <returns>The number of <typeparamref name="T"/> modified</returns>
        public Task<int> Delete(Tid id);
    }
}

