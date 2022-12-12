using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.CityRepositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllCities();
    }
}