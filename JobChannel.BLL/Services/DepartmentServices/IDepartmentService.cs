using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.Base;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;

namespace JobChannel.BLL.Services.DepartmentServices
{
    public interface IDepartmentService : IGenericReadService<Department, int>
    {
        Task<IEnumerable<DepartmentGetResponse>?> GetDepartmentsByRegionId(int regionId);
    }
}