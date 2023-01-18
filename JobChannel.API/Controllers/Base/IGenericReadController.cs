using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using JobChannel.Domain.Base;
using JobChannel.Domain.Contracts;

namespace JobChannel.API.Controllers.Base
{
    /// <summary>
    /// Generic Controller Interface for reading
    /// </summary>
    /// <typeparam name="T">Entity Type for the read controller</typeparam>
    /// <typeparam name="Tid">Id Type </typeparam>
    /// <typeparam name="TRequest">Request Type</typeparam>
    /// <typeparam name="TResponse">Response Type</typeparam>
    public interface IGenericReadController<T, Tid, TRequest, TResponse> where TRequest : IRequest where T : BaseEntity<Tid> where TResponse : IResponse
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
        /// <param name="request"><typeparamref name="TRequest"/> who contains data for the search</param>
        /// <param name="validator">Validator for validating the data of <typeparamref name="TRequest"/></param>
        /// <returns>returns an IEnumerable of <typeparamref name="T"/> correspondant to <typeparamref name="TRequest"/> criteria</returns>
        Task<IEnumerable<TResponse>> Find(TRequest request, IValidator<TRequest> validator);
    }
}
