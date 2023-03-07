using System.Collections.Generic;
using System.Threading.Tasks;
using JobChannel.BLL.Services.ContractServices;
using JobChannel.Domain.BO;
using Microsoft.AspNetCore.Mvc;

namespace JobChannel.API.Controllers.Contracts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService) => _contractService = contractService;

        [HttpGet]
        public async Task<IEnumerable<Contract>> GetAll()
        {
            return await _contractService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Contract> GetById(int id)
        {
            return await _contractService.GetById(id);
        }
    }
}
