using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using JobChannel.DAL.UOW.Repositories.Base;
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

            var jobOffers = await _dbSession.Connection.QueryAsync<JobOffer, Job, City, PostCode, Department, Region, Contract, JobOffer>(query, (jobOffer, job, city, postcode, department, region, contract) =>
            {
                jobOffer.Job = job;
                jobOffer.City = city;
                jobOffer.City.Postcodes.Add(postcode.Postcode);
                jobOffer.City.Department = department;
                jobOffer.City.Department.Region = region;
                jobOffer.Contract = contract;
                return jobOffer;
            }, _dbSession.Transaction);

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
