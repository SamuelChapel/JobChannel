﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobChannel.DAL.UOW.Repositories;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;

namespace JobChannel.BLL.JobOfferService
{
    public class JobOfferService : IJobOfferService
    {
        public readonly IJobOfferRepository _jobOfferRepository;

        public JobOfferService(IJobOfferRepository jobOfferRepository) => _jobOfferRepository = jobOfferRepository;

        public Task<bool> Create(JobOfferCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobOffer>> GetAll()
        {
            return _jobOfferRepository.GetJobOffersAsync();
        }

        public Task<JobOffer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JobOffer> Update(JobOfferUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
