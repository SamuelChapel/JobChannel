using Dapper;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.DAL.UOW.Repositories.DepartmentRepositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbSession _dbSession;

        public DepartmentRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            string query = @"SELECT d.Id, d.Name, d.Code
                            FROM JobChannel.Department d";

            return await _dbSession.Connection.QueryAsync<Department>(query);
        }

        public async Task<IEnumerable<DepartmentGetResponse>?> GetDepartmentsByRegionId(int regionId)
        {
            string query = @"SELECT d.Id, d.Name, d.Code
                            FROM JobChannel.Department d
                            WHERE d.Id_Region = @regionId";

            return await _dbSession.Connection.QueryAsync<DepartmentGetResponse>(query, new { regionId });
        }
    }
}
