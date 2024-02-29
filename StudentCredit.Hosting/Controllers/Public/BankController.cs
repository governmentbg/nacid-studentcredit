using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Banks.Dtos;
using StudentCredit.Application.Banks.Interfaces;
using StudentCredit.Application.Common.Dtos;

namespace StudentCredit.Hosting.Controllers.Public
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/public/[controller]")]
    public class BankController : Controller
    {
        private readonly IBankService bankService;

        public BankController(
            IBankService bankService
            )
        {
            this.bankService = bankService;
        }

        [HttpGet]
        public async Task<SearchResultItemDto<BankDto>> GetAll([FromQuery] BankSearchFilter filter)
           => await this.bankService.GetAll(filter);

        [HttpGet("{id:int}")]
        public async Task<BankDto> Get([FromRoute] int id)
            => await this.bankService.Get(id);
    }
}
