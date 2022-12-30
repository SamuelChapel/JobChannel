using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories.Base;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.DepartmentRepositories
{
    public interface IDepartmentRepository : IGenericReadRepository<Department, int>
    {
        /// <summary>
        /// Get all <typeparamref name="Department"/> of a <typeparamref name="Region"/>
        /// </summary>
        /// <param name="regionId">Id of the <typeparamref name="Region"/></param>
        /// <returns>All the <typeparamref name="Department"/> for this <typeparamref name="Region"/> id</returns>
        Task<IEnumerable<Department>> GetDepartmentsByRegionId(int regionId);
    }
}