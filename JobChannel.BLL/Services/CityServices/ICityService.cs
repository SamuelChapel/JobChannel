using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;

namespace JobChannel.BLL.Services.CityServices
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCities();

        Task<IEnumerable<City>?> GetCitiesByDepartmentId(int departmentId);

        Task<City> GetById(int id);
    }
}