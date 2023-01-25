using System.Threading.Tasks;
using JobChannel.Domain.Base;

namespace JobChannel.BLL.Services.Base
{
    public interface IGenericWriteService<TRequest, Tid> where TRequest : BaseEntity<Tid>
    {
        /// <summary>
        /// Create a <typeparamref name="TRequest"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="TRequest"/> to create</param>
        /// <returns>The <typeparamref name="Tid"/> that corresponds to the id of the <typeparamref name="TRequest"/> created</returns>
        public Task<Tid> Create(TRequest request);

        /// <summary>
        /// Modify a <typeparamref name="TRequest"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="TRequest"/> modified</param>
        /// <returns>The number of <typeparamref name="TRequest"/> modified</returns>
        public Task<int> Update(TRequest request);

        /// <summary>
        /// Delete a <typeparamref name="TRequest"/>
        /// </summary>
        /// <param name="t">The <typeparamref name="Tid"/> who correspond to the <typeparamref name="TRequest"/></param>
        /// <returns>The number of <typeparamref name="TRequest"/> modified</returns>
        public Task<int> Delete(Tid id);
    }
}

