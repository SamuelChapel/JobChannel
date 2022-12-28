using JobChannel.DAL.UOW;
using JobChannel.Domain.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.DepartmentServices
{
    internal class DepartmentService : IDepartmentService
    {
        public readonly IUnitOfWork _dbContext;

        public DepartmentService(IUnitOfWork dbContext) 
            => _dbContext = dbContext;

        public async Task<IEnumerable<Department>> GetAll() 
            => await _dbContext.DepartmentRepository.GetAll();

        public async Task<Department> GetById(int id) 
            => await _dbContext.DepartmentRepository.GetById(id);

        public async Task<IEnumerable<Department>> GetDepartmentsByRegionId(int regionId) 
            => await _dbContext.DepartmentRepository.GetDepartmentsByRegionId(regionId);
    }
}
