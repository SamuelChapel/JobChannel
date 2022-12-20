﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.JobServices;
using JobChannel.Domain.BO;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService) => _jobService = jobService;

        [HttpGet]
        public async Task<IEnumerable<Job>> GetAll()
        {
            return await _jobService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Job?> GetById(int id)
        {
            return await _jobService.GetById(id);
        }
    }
}
