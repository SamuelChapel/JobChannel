using Dapper;
using JobChannel.Domain.BO;
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
    }
}
