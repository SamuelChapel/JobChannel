using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using JobChannel.Domain.BO;

namespace JobChannel.DAL.UOW.Repositories
{
    public class JobOfferRepository : IJobOfferRepository
    {
        private readonly IDbSession _dbSession;

        public JobOfferRepository(IDbSession dbSession) => _dbSession = dbSession;

        public async Task<IEnumerable<JobOffer>> GetJobOffersAsync()
        {
            string query = @"SELECT jo.Title, jo.Description, jo.PublicationDate, jo.ModificationDate, 
                            jo.Url, jo.Salary, jo.Experience, jo.Id_Job, j.Name, j.Code, jo.Id_City, c.Name, 
                            c.Code, jo.Id_Contract, ct.Name, ct.Code, jo.Id_Company, cp.Name, cp.Description
                            FROM JobChannel.JobOffer jo
                            JOIN JobChannel.Job j ON jo.Id_Job = j.Id
                            JOIN JobChannel.City c on c.Id = jo.Id_City
                            JOIN JobChannel.Contract ct on ct.Id = jo.Id_Contract
                            JOIN JobChannel.Company cp on cp.Id = jo.Id_Company";

            return await _dbSession.Connection.QueryAsync<JobOffer, Job, City, Contract, Company, JobOffer>(query, (jobOffer, job, city, contract, company) =>
            {
                jobOffer.Job = job;
                jobOffer.City = city;
                jobOffer.Contract = contract;
                jobOffer.Company = company;
                return jobOffer;
            },
            splitOn: "Id_Job, Id_City, Id_Contract, Id_Company");
        }
    }
}
