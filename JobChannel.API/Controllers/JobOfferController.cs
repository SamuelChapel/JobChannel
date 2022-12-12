using System.Threading.Tasks;
using JobChannel.BLL.JobOfferService;
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
    }
}
