using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.Base;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.CityServices
{
    public interface ICityService : IGenericReadService<City, int>
    {
        /// <summary>
        /// Get cities for a department
        /// </summary>
        /// <param name="departmentId">Department Id for the cities</param>
        /// <returns>Cities for the department</returns>
        Task<IEnumerable<City>> GetCitiesByDepartmentId(int departmentId);

        Task<IEnumerable<City>> GetByName(string name);
    }
}