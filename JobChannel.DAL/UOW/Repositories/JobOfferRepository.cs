﻿using System.Collections.Generic;
using System.Linq;
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
            string query = @"SELECT jo.Id, jo.Title, jo.Description, jo.PublicationDate, jo.ModificationDate,
                            jo.Url, jo.Salary, jo.Experience, jo.Company, jo.Id_Job, j.Id, j.Name, j.CodeRome, jo.Id_City, c.Id, c.Name,
                            c.Code, c.Population, cpc.Id_City, cpc.Postcode, c.Id_Department, d.Id, d.Name, d.Code,
                            d.Id_Region, r.Id, r.Name, r.Code, jo.Id_Contract, ct.Id, ct.Name, ct.Code
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
            },
            splitOn: "Id_Job, Id_City, Id_City, Id_Department, Id_Region, Id_Contract");

            return jobOffers.GroupBy(jo => jo.Id).Select(g =>
            {
                var groupedJobOffer = g.First();
                groupedJobOffer.City.Postcodes = g.Select(jo => jo.City.Postcodes.Single()).ToList();
                return groupedJobOffer;
            });
        }
    }
}
