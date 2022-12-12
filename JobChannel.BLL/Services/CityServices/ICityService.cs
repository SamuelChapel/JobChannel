using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.CityServices
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCities();
    }
}