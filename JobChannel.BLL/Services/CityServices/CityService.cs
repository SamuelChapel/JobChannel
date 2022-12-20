using JobChannel.DAL.UOW;
using JobChannel.DAL.UOW.Repositories.CityRepositories;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.CityServices
{
    public class CityService : ICityService
    {
        public readonly IUnitOfWork _dbContext;

        public CityService(IUnitOfWork dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<City>> GetAll() => await _dbContext.CityRepository.GetAll();

        public async Task<IEnumerable<City>?> GetCitiesByDepartmentId(int departmentId) 
            => await _dbContext.CityRepository.GetCitiesByDepartmentId(departmentId);

        public async Task<City> GetById(int id)
            => await _dbContext.CityRepository.GetById(id) ?? throw new CityNotFoundException();
    }
}
