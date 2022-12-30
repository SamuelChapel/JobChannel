using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.CityRepositories
{
    public interface ICityRepository : IGenericReadRepository<City, int>
    {
        /// <summary>
        /// Get the <typeparamref name="City"/> by it's PostCode
        /// </summary>
        /// <param name="postCode">the <typeparamref name="string"/> of the PostCode</param>
        /// <returns>The <typeparamref name="City"/> who corresponds to this PostCode</returns>
        Task<City> GetByPostCode(string postCode);

        /// <summary>
        /// Get all the <typeparamref name="City"/> for a <typeparamref name="Department"/>
        /// </summary>
        /// <param name="departmentId">The id of the <typeparamref name="Department"/></param>
        /// <returns>returns an <typeparamref name="IEnumerable"/> of all the <typeparamref name="City"/> in a <typeparamref name="Department"/></returns>
        Task<IEnumerable<City>> GetCitiesByDepartmentId(int departmentId);
    }
}