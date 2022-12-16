using Dapper;
using JobChannel.Domain.BO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobChannel.DAL.UOW.Repositories.CityRepositories
{
    public class CityRepository : ICityRepository
    {
        private readonly IDbSession _dbSession;

        public CityRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<City>> GetAll()
        {
            string query = @"SELECT c.Id, c.Name, c.Code, d.Id, d.Code, d.Name, r.Id, r.Code, r.Name
                             FROM JobChannel.City c
                             JOIN Department d ON d.Id = c.Id_Department
                             JOIN Region r ON r.Id = d.Id_Region";

            return await _dbSession.Connection.QueryAsync<City>(query);
        }

        public async Task<IEnumerable<City>> GetCitiesByDepartmentId(int departmentId)
        {
            string query = @"SELECT c.Id, c.Name, c.Code, d.Id, d.Code, d.Name, r.Id, r.Code, r.Name
                            FROM JobChannel.City c
                            JOIN JobChannel.Department d ON d.Id = c.Id_Department
                            JOIN JobChannel.Region r ON r.Id = d.Id_Region
                            WHERE c.Id_Department = @departmentId";

            return await _dbSession.Connection.QueryAsync<City>(query, new { departmentId });
        }

        public async Task<City?> GetById(int id)
        {
            string query = @"SELECT c.Id, c.Name, c.Code, c.Id, cpc.Postcode, d.Id, d.Name, d.Code, r.Id, r.Name, r.Code
                            FROM JobChannel.City c
                            JOIN JobChannel.CityPostcode cpc ON cpc.Id_City = c.Id
                            JOIN JobChannel.Department d ON d.Id = c.Id_Department
                            JOIN JobChannel.Region r ON r.Id = d.Id_Region
                            WHERE c.Id = @id";

            return (await _dbSession.Connection.QueryAsync<City, PostCode, Department, Region, City>(query, (city, postcode, department, region) =>
            {
                city.Department = department;
                city.Department.Region = region;
                city.Postcodes.Add(postcode.Postcode);
                return city;
            },
        param: new { id })).GroupBy(c => c.Id).Select(g =>
        {
            var groupedCity = g.First();
            groupedCity.Postcodes = g.Select(c => c.Postcodes.Single()).ToList();
            return groupedCity;
        }).Single();
        }
    }
}
