using Dapper;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobChannel.DAL.UOW.Repositories.DepartmentRepositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbSession _dbSession;

        public DepartmentRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<Department>> GetAll()
        {
            string query = @"SELECT d.Id, d.Name, d.Code
                            FROM JobChannel.Department d";

            return await _dbSession.Connection.QueryAsync<Department>(query);
        }

        public async Task<Department> GetById(int id)
        {
            string query = @"SELECT d.Id, d.Name, d.Code, r.Id, r.Name, r.Code
                            FROM JobChannel.Department d
                            JOIN JobChannel.Region r ON d.Id_Region = r.Id
                            WHERE d.Id = @Id";

            return (await _dbSession.Connection.QueryAsync<Department, Region, Department>(query, (department, region) =>
            {
                department.Region = region;
                return department;
            }, param: new { id })).FirstOrDefault() ?? throw new DepartmentNotFoundException(id);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsByRegionId(int regionId)
        {
            string query = @"SELECT d.Id, d.Name, d.Code
                            FROM JobChannel.Department d
                            WHERE d.Id_Region = @regionId";

            return await _dbSession.Connection.QueryAsync<Department>(query, new { regionId });
        }
    }
}
