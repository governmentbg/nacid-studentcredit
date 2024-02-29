using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Credits;
using StudentCredit.Application.Credits.Dtos;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Infrastructure.Interfaces;

namespace StudentCredit.Hosting.Controllers.Credits
{
	[ApiController]
	[Route("api/[controller]")]
	public class CreditController : Controller
	{
		private readonly ICreditService creditService;
		private readonly RoleService roleService;
		private readonly IUserContext userContext;

		public CreditController(
			ICreditService creditService,
			RoleService roleService,
			IUserContext userContext)
		{
			this.creditService = creditService;
			this.roleService = roleService;
			this.userContext = userContext;
		}

		[HttpGet]
		public async Task<SearchResultItemDto<CreditSearchResultItemDto>> GetCreditsFiltered([FromQuery] SearchCreditFilter filter)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				filter.BankId = this.userContext.BankId;
			}

			return await this.creditService.GetCreditsFiltered(filter);
		}

		[HttpGet("{id:int}")]
		public async Task<CreditInfoDto> GetApplicationOneById([FromRoute] int id)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            return await this.creditService.GetCreditInfo(id);
		}
	}
}
