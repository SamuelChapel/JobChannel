using JobChannel.DAL.UOW;
using JobChannel.Domain.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.RegionServices
{
    internal class RegionService : IRegionService
    {
        public readonly IUnitOfWork _dbContext;

        public RegionService(IUnitOfWork dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Region>> GetAll() => await _dbContext.RegionRepository.GetAll();
        public async Task<Region> GetById(int id) => await _dbContext.RegionRepository.GetById(id);
    }
}
