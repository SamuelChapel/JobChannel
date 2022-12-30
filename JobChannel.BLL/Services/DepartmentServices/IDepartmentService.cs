using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.Base;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.DepartmentServices
{
    public interface IDepartmentService : IGenericReadService<Department, int>
    {
        /// <summary>
        /// Get all <typeparamref name="Department"/> of a <typeparamref name="Region"/>
        /// </summary>
        /// <param name="regionId">Id of the <typeparamref name="Region"/></param>
        /// <returns>All the <typeparamref name="Department"/> for this <typeparamref name="Region"/> id</returns>
        Task<IEnumerable<Department>> GetDepartmentsByRegionId(int regionId);
    }
}