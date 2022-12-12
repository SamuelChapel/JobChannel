using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.RegionRepositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegions();
    }
}