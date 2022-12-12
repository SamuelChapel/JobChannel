using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;

namespace JobChannel.DAL.UOW.Repositories.DepartmentRepositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartments();

        Task<IEnumerable<DepartmentGetResponse>?> GetDepartmentsByRegionId(int regionId);
    }
}