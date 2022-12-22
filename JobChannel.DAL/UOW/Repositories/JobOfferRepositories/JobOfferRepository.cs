using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories.JobOfferRepositories
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly IDbSession _dbSession;

        public JobOfferRepository(IDbSession dbSession) => _dbSession = dbSession;

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

                    var t = field.Value.GetType();
                    query += field.Value switch
                    {
                        // TODO gérer la table dans l'enumerable
                        IEnumerable<int> list => field.Key switch
                        {
                            "Id_Region" => $"r.Id",
                            "Id_Department" => $"d.Id",
                            _ => $"jo.{field.Key}"
                        } + $" IN @{field.Key}",
                        ValueTuple<DateTime, DateTime> d => $"jo.{field.Key} BETWEEN {d.Item1} AND {d.Item2}",
                        string s => $"jo.Title COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key}",
                                    //$"jo.Company COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                    //$"c.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                    //$"j.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                    //$"d.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                    //$"r.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key} OR " +
                                    //$"ct.Name COLLATE SQL_Latin1_General_CP1_CI_AS LIKE @{field.Key}",
                        _ => throw new Exception()
                    };

                    // TODO gerer les tuple pour les params de dapper
                    if (field.Value is ValueTuple<DateTime, DateTime> date)
                    {
                        parameters.Add(date.Item1.ToString(), date.Item1);
                        parameters.Add(date.Item2.ToString(), date.Item2);
                    }
                    else parameters.Add(field.Key, field.Value);
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

        public async Task<int> Create(JobOffer jobOffer)
        {
            string query = @"INSERT INTO JobChannel.JobOffer (Title, Description, PublicationDate, ModificationDate, 
                             Url, Salary, Experience, Id_Job, ID_Contract, Id_City)
                             OUTPUT INSERTED.ID
                             VALUES (@Title, @Description, @PublicationDate, @ModificationDate, @Url, @Salary, @Experience, 
                             @Id_Job, @ID_Contract, @Id_City)";

            var param = new DynamicParameters(jobOffer);
            param.Add("Id_Contract", jobOffer.Contract.Id);
            param.Add("Id_City", jobOffer.City.Id);
            param.Add("Id_Job", jobOffer.Job.Id);

            return await _dbSession.Connection.ExecuteAsync(query, param);
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

            return await _dbSession.Connection.ExecuteAsync(query, param);
        }

        public async Task<int> Delete(int id)
        {
            string query = @"DELETE FROM JobChannel.JobOffer
                             WHERE Id = @id";

            return await _dbSession.Connection.ExecuteAsync(query, new { id });
        }
    }
}
