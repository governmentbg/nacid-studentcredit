using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Banks.Dtos;
using StudentCredit.Application.Banks.Interfaces;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Application.Users.Interfaces;
using StudentCredit.Data.Users.Constants;

namespace StudentCredit.Hosting.Controllers.Banks
{
	[Route("api/[controller]")]
	[ApiController]
	public class BankController : ControllerBase
	{
		private readonly IBankService bankService;
		private readonly RoleService roleService;
		private readonly IUserService userService;

		public BankController(
			IBankService bankService,
			RoleService roleService,
			IUserService userService
			)
		{
			this.bankService = bankService;
			this.roleService = roleService;
			this.userService = userService;
		}

		[HttpGet]
		public async Task<SearchResultItemDto<BankDto>> GetAll([FromQuery] BankSearchFilter filter)
		{
			this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

			return await this.bankService.GetAll(filter);
		}

		[HttpGet("{id:int}")]
		public async Task<BankDto> Get([FromRoute] int id)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				this.userService.ValidateUserBank(id);
			}

			return await this.bankService.Get(id);
		}

		[HttpPost]
		public async Task Create([FromBody] BankDto model)
		{
			this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

			await this.bankService.Create(model);
		}

		[HttpPost("changeStatus/{id:int}")]
		public async Task<bool> ChangeStatus([FromRoute] int id)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			return await this.bankService.ChangeStatus(id);
        }

        [HttpPut]
		public async Task Edit([FromBody] BankDto model)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				this.userService.ValidateUserBank(model.Id.Value);
			}

			await this.bankService.Edit(model);
		}
	}
}
