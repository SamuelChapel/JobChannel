using JobChannel.DAL.UOW.Repositories.DepartmentRepositories;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        public readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository) 
            => _departmentRepository = departmentRepository;

        public async Task<IEnumerable<Department>> GetAllDepartments() 
            => await _departmentRepository.GetAllDepartments();

        public async Task<IEnumerable<DepartmentGetResponse>?> GetDepartmentsByRegionId(int regionId) 
            => await _departmentRepository.GetDepartmentsByRegionId(regionId);
    }
}
