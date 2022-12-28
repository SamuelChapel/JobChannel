using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.DepartmentRepositories
{
    public interface IDepartmentRepository : IGenericReadRepository<Department, int>
    {
        Task<IEnumerable<Department>> GetDepartmentsByRegionId(int regionId);
    }
}