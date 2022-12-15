using JobChannel.DAL.UOW;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        public readonly IUnitOfWork _dbContext;

        public DepartmentService(IUnitOfWork dbContext) 
            => _dbContext = dbContext;

        public async Task<IEnumerable<Department>> GetAllDepartments() 
            => await _dbContext.DepartmentRepository.GetAllDepartments();

        public async Task<IEnumerable<DepartmentGetResponse>?> GetDepartmentsByRegionId(int regionId) 
            => await _dbContext.DepartmentRepository.GetDepartmentsByRegionId(regionId);
    }
}
