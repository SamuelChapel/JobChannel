using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.DAL.UOW;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.JobOfferServices
{
    internal class JobOfferService : IJobOfferService
    {
        public readonly IUnitOfWork _dbContext;

        public JobOfferService(IUnitOfWork dbContext) => _dbContext = dbContext;

        public async Task<int> Create(
            JobOffer jobOffer,
            IJobService jobService,
            ICityService cityService,
            IContractService contractService)
        {
            jobOffer.Job = await jobService.GetById(jobOffer.Job.Id);
            jobOffer.City = await cityService.GetById(jobOffer.City.Id);
            jobOffer.Contract = await contractService.GetById(jobOffer.Contract.Id);

            return await _dbContext.JobOfferRepository.Create(jobOffer);
        }

        public async Task<IEnumerable<JobOffer>> GetAll(IReadOnlyDictionary<string, dynamic>? searchFields) 
            => await _dbContext.JobOfferRepository.GetAll(searchFields);

        public async Task<JobOffer> GetById(int id) 
            => await _dbContext.JobOfferRepository.GetById(id);

        public Task<int> Update(JobOffer jobOffer) 
            => _dbContext.JobOfferRepository.Update(jobOffer);

        public Task<int> Delete(JobOffer jobOffer) 
            => _dbContext.JobOfferRepository.Delete(jobOffer);
    }
}
