using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JobChannel.Domain.BO;
using JobChannel.Domain.Exceptions;

namespace JobChannel.DAL.UOW.Repositories.JobOfferRepositories
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly IDbSession _dbSession;

        public JobOfferRepository(IDbSession dbSession) => _dbSession = dbSession;

        /// <summary>
        /// Find all job offers 
        /// </summary>
        /// <param name="searchFields">Dictionary of search criteria</param>
        /// <returns>All the job offer's matching the criteria</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<JobOffer>> GetAll(IReadOnlyDictionary<string, dynamic>? searchFields)
        {
            string query = @"SELECT jo.Id, jo.Title, jo.Description, jo.PublicationDate, jo.ModificationDate,
                            jo.Url, jo.Salary, jo.Experience, jo.Company, j.Id, j.Name, j.CodeRome, c.Id, c.Name,
                            c.Code, c.Population, c.Id, cpc.Postcode, d.Id, d.Name, d.Code,
                            r.Id, r.Name, r.Code, ct.Id, ct.Name, ct.Code
                            FROM JobChannel.JobOffer jo
                            JOIN JobChannel.Job j ON jo.Id_Job = j.Id
                            JOIN JobChannel.City c ON c.Id = jo.Id_City
                            JOIN JobChannel.CityPostcode cpc ON c.Id = cpc.Id_City
                            JOIN JobChannel.Department d ON d.Id = c.Id_Department
                            JOIN JobChannel.Region r ON r.Id = d.Id_Region
                            JOIN JobChannel.Contract ct ON ct.Id = jo.Id_Contract";

            var parameters = new DynamicParameters();

            if (searchFields != null)
            {
                bool first = true;
                foreach (var field in searchFields)
                {
                    query += first ? " WHERE " : " AND ";
                    first = false;

                    query += field.Value switch
                    {
                        IEnumerable<int> list => field.Key switch
                        {
                            "Id_Region" => $"r.Id",
                            "Id_Department" => $"d.Id",
                            _ => $"jo.{field.Key}"
                        } + $" IN @{field.Key}",

                        ValueTuple<DateTime, DateTime> d => $"jo.{field.Key} BETWEEN @{d.Item1.Ticks} AND @{d.Item2.Ticks}",

                        string s => field.Key switch
                        {
                            "Url" => $"jo.Url COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key}",
                            _ => $"(jo.Title COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                 $"jo.Company COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                 $"c.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                 $"j.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                 $"d.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                 $"r.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                 $"ct.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                 $"jo.Url COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key})"
                        },

                        _ => throw new Exception("Valeur non prévue")
                    };

                    if (field.Value is ValueTuple<DateTime, DateTime> date)
                    {
                        parameters.Add(date.Item1.Ticks.ToString(), date.Item1.Date);
                        parameters.Add(date.Item2.Ticks.ToString(), date.Item2.Date);
                    }
                    else if (field.Value is string s)
                    {
                        parameters.Add(field.Key, "%" + s + "%");
                    }
                    else
                        parameters.Add(field.Key, field.Value);
                }
            }

            var jobOffers = await _dbSession.Connection.QueryAsync<JobOffer, Job, City, PostCode, Department, Region, Contract, JobOffer>(query, (jobOffer, job, city, postcode, department, region, contract) =>
            {
                jobOffer.Job = job;
                jobOffer.City = city;
                jobOffer.City.Postcodes.Add(postcode.Postcode);
                jobOffer.City.Department = department;
                jobOffer.City.Department.Region = region;
                jobOffer.Contract = contract;
                return jobOffer;
            }, parameters, _dbSession.Transaction);

            return jobOffers.GroupBy(jo => jo.Id).Select(g =>
            {
                var groupedJobOffer = g.First();
                groupedJobOffer.City.Postcodes = g.Select(jo => jo.City.Postcodes.Single()).ToList();
                return groupedJobOffer;
            });
        }

        /// <summary>
        /// Get a job offer by his id 
        /// </summary>
        /// <param name="id">job offer id to find</param>
        /// <returns>the job offer matching the id</returns>
        /// <exception cref="JobOfferNotFoundException"></exception>
        public async Task<JobOffer> GetById(int id)
        {
            string query = @"SELECT jo.Id, jo.Title, jo.Description, jo.PublicationDate, jo.ModificationDate,
                            jo.Url, jo.Salary, jo.Experience, jo.Company, j.Id, j.Name, j.CodeRome, c.Id, c.Name,
                            c.Code, c.Population, c.Id, cpc.Postcode, d.Id, d.Name, d.Code,
                            r.Id, r.Name, r.Code, ct.Id, ct.Name, ct.Code
                            FROM JobChannel.JobOffer jo
                            JOIN JobChannel.Job j ON jo.Id_Job = j.Id
                            JOIN JobChannel.City c ON c.Id = jo.Id_City
                            JOIN JobChannel.CityPostcode cpc ON c.Id = cpc.Id_City
                            JOIN JobChannel.Department d ON d.Id = c.Id_Department
                            JOIN JobChannel.Region r ON r.Id = d.Id_Region
                            JOIN JobChannel.Contract ct ON ct.Id = jo.Id_Contract
                            WHERE jo.Id = @Id";

            var jobOffers = await _dbSession.Connection.QueryAsync<JobOffer, Job, City, PostCode, Department, Region, Contract, JobOffer>(query, (jobOffer, job, city, postcode, department, region, contract) =>
            {
                jobOffer.Job = job;
                jobOffer.City = city;
                jobOffer.City.Postcodes.Add(postcode.Postcode);
                jobOffer.City.Department = department;
                jobOffer.City.Department.Region = region;
                jobOffer.Contract = contract;
                return jobOffer;
            }, new { id }, _dbSession.Transaction);

            return jobOffers.GroupBy(jo => jo.Id).Select(g =>
            {
                var groupedJobOffer = g.First();
                groupedJobOffer.City.Postcodes = g.Select(jo => jo.City.Postcodes.Single()).ToList();
                return groupedJobOffer;
            }).FirstOrDefault() ?? throw new JobOfferNotFoundException(id);
        }

        public async Task<int> Create(JobOffer jobOffer)
        {
            string query = @"INSERT INTO JobChannel.JobOffer (Title, Description, PublicationDate, ModificationDate, 
                             Url, Salary, Experience, Company, Id_Job, ID_Contract, Id_City)
                             OUTPUT INSERTED.ID
                             VALUES (@Title, @Description, @PublicationDate, @ModificationDate, @Url, @Salary, @Experience, @Company, 
                             @Id_Job, @ID_Contract, @Id_City)";

            var param = new DynamicParameters(jobOffer);
            param.Add("Id_Contract", jobOffer.Contract.Id);
            param.Add("Id_City", jobOffer.City.Id);
            param.Add("Id_Job", jobOffer.Job.Id);

            return await _dbSession.Connection.ExecuteScalarAsync<int>(query, param, _dbSession.Transaction);
        }

        public async Task<int> Update(JobOffer jobOffer)
        {
            string query = @"UPDATE JobChannel.JobOffer
                            SET Title = @Title, Description = @Description, PublicationDate = @PublicationDate, 
                            ModificationDate = @ModificationDate, Url = @Url, Salary = @Salary, Experience = @Experience, 
                            Company = @Company, Id_Job = @Id_Job, ID_Contract = @ID_Contract, Id_City = @Id_City
                            WHERE Id = @Id";

            var param = new DynamicParameters(jobOffer);
            param.Add("Id_Contract", jobOffer.Contract.Id);
            param.Add("Id_City", jobOffer.City.Id);
            param.Add("Id_Job", jobOffer.Job.Id);

            return await _dbSession.Connection.ExecuteAsync(query, param, _dbSession.Transaction);
        }

        public async Task<int> Delete(int id)
        {
            string query = @"DELETE FROM JobChannel.JobOffer
                             WHERE Id = @id";

            return await _dbSession.Connection.ExecuteAsync(query, new { id }, _dbSession.Transaction);
        }
    }
}
