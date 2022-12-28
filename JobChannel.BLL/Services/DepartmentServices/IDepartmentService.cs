using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.Base;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.DepartmentServices
{
    public interface IDepartmentService : IGenericReadService<Department, int>
    {
        Task<IEnumerable<Department>> GetDepartmentsByRegionId(int regionId);
    }
}