using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.Base;

namespace JobChannel.BLL.Services.Base
{
    public interface IGenericReadService<T, Tid> where T : BaseEntity<Tid> where Tid : struct
    {
        /// <summary>
        /// Get a <typeparamref name="T"/> by his id
        /// </summary>
        /// <param name="id"><typeparamref name="Tid"/> of the id to find</param>
        /// <returns>returns a <typeparamref name="Tid"/> of the <typeparamref name="T"/> id</returns>
        Task<T> GetById(Tid id);

        /// <summary>
        /// Method for getting all the <typeparamref name="T"/> 
        /// </summary>
        /// <returns>returns an <typeparamref name="IEnumerable"/> of all the <typeparamref name="T"/></returns>
        Task<IEnumerable<T>> GetAll();
    }
}
