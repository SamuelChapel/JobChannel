using System.Threading.Tasks;
using JobChannel.BLL.Services.ContractServices;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService) => _contractService = contractService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _contractService.GetAllContracts());
        }
    }
}
