using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobOfferServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.Domain.BO;
using JobChannel.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private readonly IJobOfferService _jobOfferService;

        public JobOfferController(IJobOfferService jobOfferService) => _jobOfferService = jobOfferService;

        [HttpPost("search")]
        public async Task<IActionResult> Find(
            [FromBody] JobOfferFindRequest jobOfferFindRequest
            )
        {
            var filters = new Dictionary<string, dynamic>();

            if (jobOfferFindRequest.Id_City?.Count() > 0) filters.Add("Id_City", jobOfferFindRequest.Id_City);
            if (jobOfferFindRequest.Id_Department?.Count() > 0) filters.Add("Id_Department", jobOfferFindRequest.Id_Department);
            if (jobOfferFindRequest.Id_Region?.Count() > 0) filters.Add("Id_Region", jobOfferFindRequest.Id_Region);
            if (jobOfferFindRequest.StartDate.HasValue && jobOfferFindRequest.EndDate.HasValue) filters.Add("PublicationDate", new ValueTuple<DateTime, DateTime>((DateTime)jobOfferFindRequest.StartDate, (DateTime)jobOfferFindRequest.EndDate));
            if (jobOfferFindRequest.Id_Job?.Count() > 0) filters.Add("Id_Job", jobOfferFindRequest.Id_Job);
            if (jobOfferFindRequest.Id_Contract?.Count() > 0) filters.Add("Id_Contract", jobOfferFindRequest.Id_Contract);
            if (jobOfferFindRequest.SearchString?.Length > 0) filters.Add("SearchString", jobOfferFindRequest.SearchString);

            return Ok(await _jobOfferService.GetAll(filters));
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            JobOfferCreateRequest jobOfferCreateRequest,
            [FromServices] IJobService jobService,
            [FromServices] ICityService cityService,
            [FromServices] IContractService contractService,
            [FromServices] IValidator<JobOfferCreateRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(jobOfferCreateRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

            JobOffer jobOffer = new()
            {
                Title = jobOfferCreateRequest.Title,
                Description = jobOfferCreateRequest.Description,
                PublicationDate = jobOfferCreateRequest.PublicationDate,
                ModificationDate = jobOfferCreateRequest.ModificationDate,
                Salary = jobOfferCreateRequest.Salary,
                Experience = jobOfferCreateRequest.Experience,
                Url = jobOfferCreateRequest.Url,
                Company = jobOfferCreateRequest.Company,
                Job = new() { Id = jobOfferCreateRequest.JobId },
                City = new() { Id = jobOfferCreateRequest.CityId },
                Contract = new() { Id = jobOfferCreateRequest.ContractId }
            };

            return Ok(await _jobOfferService.Create(jobOffer, jobService, cityService, contractService));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] JobOfferUpdateRequest jobOfferUpdateRequest,
            [FromServices] IValidator<JobOfferUpdateRequest> validator)
        {
            await validator.ValidateAndThrowAsync(jobOfferUpdateRequest);

            JobOffer jobOffer = new()
            {
                Id = id,
                Title = jobOfferUpdateRequest.Title,
                Description = jobOfferUpdateRequest.Description,
                Experience = jobOfferUpdateRequest.Experience,
                Url = jobOfferUpdateRequest.Url,
                Salary = jobOfferUpdateRequest.Salary,
                Company = jobOfferUpdateRequest.Company,
                PublicationDate = jobOfferUpdateRequest.PublicationDate,
                ModificationDate = jobOfferUpdateRequest.ModificationDate,
                Job = new() { Id = jobOfferUpdateRequest.JobId },
                City = new() { Id = jobOfferUpdateRequest.CityId },
                Contract = new() { Id = jobOfferUpdateRequest.ContractId }
            };

            return Ok(await _jobOfferService.Update(jobOffer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] int id)
        {
            return (await _jobOfferService.Delete(id) != 0) ? NoContent() : NotFound();
        }
    }
}
