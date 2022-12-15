using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.CityServices
{
    public interface ICityService
    {
        /// <summary>
        /// Get all city
        /// </summary>
        /// <returns>All Cities</returns>
        Task<IEnumerable<City>> GetAllCities();

        /// <summary>
        /// Get cities for a department
        /// </summary>
        /// <param name="departmentId">Department Id for the cities</param>
        /// <returns>Cities for the department</returns>
        Task<IEnumerable<City>?> GetCitiesByDepartmentId(int departmentId);

        /// <summary>
        /// Get city by his id
        /// </summary>
        /// <param name="id">City id to find</param>
        /// <returns>City with this id</returns>
        Task<City> GetById(int id);
    }
}