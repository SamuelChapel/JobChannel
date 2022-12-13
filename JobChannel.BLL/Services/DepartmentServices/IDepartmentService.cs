using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;

namespace JobChannel.BLL.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartments();

        Task<IEnumerable<DepartmentGetResponse>?> GetDepartmentsByRegionId(int regionId);
    }
}