using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.DAL.UOW.Repositories.JobOfferRepositories;
using JobChannel.Domain.BO;

namespace JobChannel.BLL.Services.JobOfferServices
{
    public class JobOfferService : IJobOfferService
    {
        public readonly IJobOfferRepository _jobOfferRepository;

        public JobOfferService(IJobOfferRepository jobOfferRepository) => _jobOfferRepository = jobOfferRepository;

        public async Task<int> Create(
            JobOffer jobOffer,
            IJobService jobService,
            ICityService cityService,
            IContractService contractService)
        {
            var jobTask = await jobService.GetById(jobOffer.Job.Id);
            var cityTask = await cityService.GetById(jobOffer.City.Id);
            var contractTask = await contractService.GetById(jobOffer.Contract.Id);

            //await Task.WhenAll(jobTask, cityTask, contractTask);

            jobOffer.Job = jobTask/*.Result*/;
            jobOffer.City = cityTask/*.Result*/;
            jobOffer.Contract = contractTask/*.Result*/;

            return await _jobOfferRepository.CreateJobOffer(jobOffer);
        }

        public Task<IEnumerable<JobOffer>> GetAll()
        {
            return _jobOfferRepository.GetJobOffersAsync();
        }

        public Task<JobOffer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(JobOffer jobOffer)
        {
            return _jobOfferRepository.UpdateJobOffer(jobOffer);
        }

        public Task<int> Delete(int id)
        {
            return _jobOfferRepository.DeleteJobOffer(id);
        }
    }
}
