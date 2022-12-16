using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.CityRepositories
{
    public interface ICityRepository : IGenericReadRepository<City, int>
    {
        Task<IEnumerable<City>> GetCitiesByDepartmentId(int departmentId);
    }
}