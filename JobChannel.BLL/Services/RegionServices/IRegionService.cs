using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.RegionServices
{
    public interface IRegionService
    {
        Task<IEnumerable<Region>> GetAllRegions();
    }
}