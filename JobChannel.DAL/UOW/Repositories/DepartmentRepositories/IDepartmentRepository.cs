using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;

namespace JobChannel.DAL.UOW.Repositories.DepartmentRepositories
{
    public interface IDepartmentRepository : IGenericReadRepository<Department, int>
    {
        Task<IEnumerable<DepartmentGetResponse>?> GetDepartmentsByRegionId(int regionId);
    }
}