using Dapper;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobChannel.DAL.UOW.Repositories.CityRepositories
{
    public class CityRepository : ICityRepository
    {
        private readonly IDbSession _dbSession;

        public CityRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<City>> GetAllCities()
        {
            string query = @"SELECT c.Id, c.Name, c.Code
                            FROM JobChannel.City c";

            return await _dbSession.Connection.QueryAsync<City>(query);
        }

        public async Task<IEnumerable<CityGetResponse>?> GetCitiesByDepartmentId(int departmentId)
        {
            string query = @"SELECT c.Id, c.Name, c.Code
                            FROM JobChannel.City c
                            WHERE c.Id_Department = @departmentId";

            return await _dbSession.Connection.QueryAsync<CityGetResponse>(query, new { departmentId });
        }
    }
}
