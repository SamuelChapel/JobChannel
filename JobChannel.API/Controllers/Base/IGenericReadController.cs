using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using JobChannel.Domain.Base;
using JobChannel.Domain.Contracts;

namespace JobChannel.API.Controllers.Base
{
    public interface IGenericReadController<T, Tid, Trequest> where Trequest : IRequest where T : BaseEntity<Tid> where Tid : struct
    {
        /// <summary>
        /// Get a <typeparamref name="T"/> by his id
        /// </summary>
        /// <param name="id"><typeparamref name="Tid"/> of the <typeparamref name="T"/> id to find</param>
        /// <returns>returns a <typeparamref name="Tid"/> of the <typeparamref name="T"/> id</returns>
        Task<T> GetById(Tid id);

        /// <summary>
        /// Method for finding the <typeparamref name="T"/> with search parameters
        /// </summary>
        /// <param name="request"><typeparamref name="Trequest"/> who contains data for the search</param>
        /// <param name="validator">Validator for validating the data of <typeparamref name="Trequest"/></param>
        /// <returns>returns an <typeparamref name="IEnumerable"/> of <typeparamref name="T"/>correspondant to <typeparamref name="Trequest"/> criteria</returns>
        Task<IEnumerable<T>> Find(Trequest request, IValidator<Trequest> validator);
    }
}
