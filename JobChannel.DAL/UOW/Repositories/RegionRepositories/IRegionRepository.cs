using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.RegionRepositories
{
    public interface IRegionRepository : IGenericReadRepository<Region, int>
    {
    }
}