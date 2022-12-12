using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.DAL.UOW.Repositories.DepartmentRepositories;
using JobChannel.Domain.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.CityServices
{
    public class CityService : ICityService
    {
        public readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository) => _cityRepository = cityRepository;

        public async Task<IEnumerable<City>> GetAllCities() => await _cityRepository.GetAllCities();
    }
}
