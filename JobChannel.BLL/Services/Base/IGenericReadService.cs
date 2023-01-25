using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.Base;

namespace JobChannel.BLL.Services.Base
{
    public interface IGenericReadService<TReturn, Tid> where TReturn : BaseEntity<Tid> where Tid : struct
    {
        /// <summary>
        /// Get a <typeparamref name="TReturn"/> by his id
        /// </summary>
        /// <param name="id"><typeparamref name="Tid"/> of the id to find</param>
        /// <returns>returns a <typeparamref name="Tid"/> of the <typeparamref name="TReturn"/> id</returns>
        Task<TReturn> GetById(Tid id);

        /// <summary>
        /// Method for getting all the <typeparamref name="TReturn"/> 
        /// </summary>
        /// <returns>returns an <typeparamref name="IEnumerable"/> of all the <typeparamref name="TReturn"/></returns>
        Task<IEnumerable<TReturn>> GetAll();
    }
}
