using System.Threading.Tasks;
using JobChannel.BLL.Services.JobServices;
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _jobService.GetAllJobs());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _jobService.GetById(id));
        }
    }
}
