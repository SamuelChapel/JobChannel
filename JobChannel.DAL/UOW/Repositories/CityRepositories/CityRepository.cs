using Dapper;
using JobChannel.DAL.Services.SqlQueryBuilder;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobChannel.DAL.UOW.Repositories.CityRepositories
{
    internal class CityRepository : ICityRepository
    {
        private readonly IDbSession _dbSession;

        public CityRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<City>> GetAll()
        {
            var cityQuery = new SqlSelectBuilder(
                "c.Id, c.Name",
                "JobChannel.City AS c");

            return await cityQuery.QueryAsync<City>(_dbSession.Connection);
        }

        public async Task<IEnumerable<City>> GetCitiesByDepartmentId(int departmentId)
        {
            var query = @"SELECT c.Id, c.Name, c.Code, c.Population, c.Id, cpc.Postcode
                          FROM JobChannel.City c
                          JOIN JobChannel.CityPostcode cpc ON cpc.Id_City = c.Id
                          WHERE c.Id_Department = @departmentId
                          ORDER BY c.Population DESC
                          OFFSET 0 ROWS FETCH FIRST 100 ROWS ONLY";

            var cities = await _dbSession.Connection.QueryAsync<City, PostCode, City>(query, (city, postcode) =>
            {
                city.Postcodes.Add(postcode.Postcode);
                return city;
            },  
            param: new { departmentId });

            return cities.GroupBy(c => c.Id).Select(g =>
            {
                var groupedCity = g.First();
                groupedCity.Postcodes = g.Select(c => c.Postcodes.First()).ToList();
                return groupedCity;
            });

        }

        public async Task<City> GetById(int id)
        {
            string query = @"SELECT c.Id, c.Name, c.Code, c.Population, c.Id, cpc.Postcode, d.Id, d.Name, d.Code, r.Id, r.Name, r.Code
                            FROM JobChannel.City c
                            JOIN JobChannel.CityPostcode cpc ON cpc.Id_City = c.Id
                            JOIN JobChannel.Department d ON d.Id = c.Id_Department
                            JOIN JobChannel.Region r ON r.Id = d.Id_Region
                            WHERE c.Id = @id";

            var cities = await _dbSession.Connection.QueryAsync<City, PostCode, Department, Region, City>(query, (city, postcode, department, region) =>
            {
                city.Department = department;
                city.Department.Region = region;
                city.Postcodes.Add(postcode.Postcode);
                return city;
            },
            param: new { id });

            if (!cities.Any())
                throw new CityNotFoundException(id);

            return cities.GroupBy(c => c.Id).Select(g =>
            {
                var groupedCity = g.First();
                groupedCity.Postcodes = g.Select(c => c.Postcodes.First()).ToList();
                return groupedCity;
            }).Single();
        }

        public async Task<City> GetByPostCode(string postCode)
        {
            string query = @"SELECT c.Id, c.Name, c.Code, c.Population, c.Id, cpc.Postcode, d.Id, d.Name, d.Code, r.Id, r.Name, r.Code
                            FROM JobChannel.City c
                            JOIN JobChannel.CityPostcode cpc ON cpc.Id_City = c.Id
                            JOIN JobChannel.Department d ON d.Id = c.Id_Department
                            JOIN JobChannel.Region r ON r.Id = d.Id_Region
                            WHERE cpc.Postcode = @postCode";

            var cities = await _dbSession.Connection.QueryAsync<City, PostCode, Department, Region, City>(query, (city, postcode, department, region) =>
            {
                city.Department = department;
                city.Department.Region = region;
                city.Postcodes.Add(postcode.Postcode);
                return city;
            },
            param: new { postCode });

            return cities.First();
        }

        public async Task<IEnumerable<City>> GetByName(string name)
        {
            string query = $@"SELECT c.Id, c.Name, c.Code, c.Population, c.Id, cpc.Postcode, d.Id, d.Name, d.Code, r.Id, r.Name, r.Code
                            FROM JobChannel.City c
                            JOIN JobChannel.CityPostcode cpc ON cpc.Id_City = c.Id
                            JOIN JobChannel.Department d ON d.Id = c.Id_Department
                            JOIN JobChannel.Region r ON r.Id = d.Id_Region
                            WHERE c.Name COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '%{name}%'
                            ORDER BY
                                CASE
                                    WHEN c.Name LIKE '{name}' THEN 1
                                    WHEN c.Name LIKE '{name}%' THEN 2
                                    WHEN c.Name LIKE '%{name}' THEN 3
                                    ELSE 4
                                END
                            OFFSET 0 ROWS FETCH FIRST 100 ROWS ONLY";


            var cities = await _dbSession.Connection.QueryAsync<City, PostCode, Department, Region, City>(query, (city, postcode, department, region) =>
            {
                city.Department = department;
                city.Department.Region = region;
                city.Postcodes.Add(postcode.Postcode);
                return city;
            });

            return cities;
        }
    }
}
