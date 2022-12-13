using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;

namespace JobChannel.DAL.UOW.Repositories.CityRepositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllCities();

        Task<IEnumerable<CityGetResponse>?> GetCitiesByDepartmentId(int departmentId);
    }
}