using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _jobOfferService.GetAll());
        }

        [HttpPost("search")]
        public async Task<IActionResult> Find([FromBody] JobOfferFindRequest jobOfferFindRequest)
        {
            //TODO Find with parameters
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            JobOfferCreateRequest jobOfferCreateRequest,
            [FromServices] IJobService jobService,
            [FromServices] ICityService cityService,
            [FromServices] IContractService contractService)
        {
            var validationResult = new JobOfferCreateRequestValidator().Validate(jobOfferCreateRequest);

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
            [FromBody] JobOfferUpdateRequest jobOfferUpdateRequest)
        {
            var validationResult = new JobOfferUpdateRequestValidator().Validate(jobOfferUpdateRequest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());

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
