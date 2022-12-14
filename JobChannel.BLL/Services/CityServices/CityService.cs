using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;
using JobChannel.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.CityServices
{
    public class CityService : ICityService
    {
        public readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository) => _cityRepository = cityRepository;

        public async Task<IEnumerable<City>> GetAllCities() => await _cityRepository.GetAllCities();

        public async Task<IEnumerable<City>?> GetCitiesByDepartmentId(int departmentId) 
            => await _cityRepository.GetCitiesByDepartmentId(departmentId);

        public async Task<City> GetById(int id)
            => await _cityRepository.GetCityById(id) ?? throw new CityNotFoundException();
    }
}
