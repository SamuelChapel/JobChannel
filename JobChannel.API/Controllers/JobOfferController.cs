using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

        }
    }
}
