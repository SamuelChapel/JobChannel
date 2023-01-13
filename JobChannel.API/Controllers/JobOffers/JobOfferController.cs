using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using JobChannel.API.Controllers.Base;
using JobChannel.API.Controllers.JobOffers.Requests;
using JobChannel.BLL.Services.CityServices;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.BLL.Services.JobOfferServices;
using JobChannel.BLL.Services.JobServices;
using JobChannel.BLL.Services.PoleEmploi.JobOffers;
using JobChannel.Domain.BO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers.JobOffers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobOfferController : ControllerBase, IGenericReadController<JobOffer, int, JobOfferFindRequest>
    {
        private readonly IJobOfferService _jobOfferService;

        public JobOfferController(IJobOfferService jobOfferService) => _jobOfferService = jobOfferService;

        [HttpGet("{id}")]
        public async Task<JobOffer> GetById(
            [FromRoute] int id
            ) 
            => await _jobOfferService.GetById(id);

        [HttpPost("search")]
        public async Task<IEnumerable<JobOffer>> Find(
            [FromBody] JobOfferFindRequest jobOfferFindRequest,
            [FromServices] IValidator<JobOfferFindRequest> validator
            )
        {
            await validator.ValidateAndThrowAsync(jobOfferFindRequest);

            var filters = new Dictionary<string, dynamic>();

            foreach (PropertyInfo property in jobOfferFindRequest.GetType().GetProperties())
            {
                var value = property.GetValue(jobOfferFindRequest);

                if (value is not null)
                {
                    filters.Add(property.Name, value);
                }
            }

            return await _jobOfferService.GetAll(filters);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            JobOfferCreateRequest jobOfferCreateRequest,
            [FromServices] IJobService jobService,
            [FromServices] ICityService cityService,
            [FromServices] IContractService contractService,
            [FromServices] IValidator<JobOfferCreateRequest> validator)
        {
            await validator.ValidateAndThrowAsync(jobOfferCreateRequest);

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
            var jobOffer = new JobOffer()
            {
                Id = id
            };

            return await _jobOfferService.Delete(jobOffer) != 0 ? NoContent() : NotFound();
        }

        [HttpPost("PoleEmploi")]
        public async Task<IActionResult> InsertPoleEmploi(
            [FromBody] GetPoleEmploiJobOffersRequest request,
            [FromServices] IJobOfferPoleEmploiService jobOfferPoleEmploiService,
            [FromServices] IValidator<GetPoleEmploiJobOffersRequest> validator
            )
        {
            await validator.ValidateAndThrowAsync(request);

            var result = await jobOfferPoleEmploiService.GetAndInsertPoleEmploiJobOffers(new GetPoleEmploiJobOffersQuery((request.Start, request.End), request.CodeRome, request.Commune, request.PublieeDepuis, request.EntreprisesAdaptees));

            return Ok(result);
        }
    }
}
