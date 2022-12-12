using JobChannel.DAL.UOW.Repositories.JobRepositories;
using JobChannel.DAL.UOW.Repositories.RegionRepositories;
using JobChannel.Domain.BO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.BLL.Services.RegionServices
{
    public class RegionService : IRegionService
    {
        public readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository) => _regionRepository = regionRepository;

        public async Task<IEnumerable<Region>> GetAllRegions() => await _regionRepository.GetAllRegions();
    }
}
