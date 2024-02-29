using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Applications.ApplicationsFive;
using StudentCredit.Application.Applications.ApplicationsFive.Dtos;
using StudentCredit.Application.Common.Dtos;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Users.Constants;
using StudentCredit.Hosting.Controllers.Common;
using StudentCredit.Infrastructure.Interfaces;

namespace StudentCredit.Hosting.Controllers.Applications
{
	public class ApplicationFiveController : BaseApplicationController<ApplicationFiveDto>
	{
		private readonly IApplicationFiveService service;
		private readonly RoleService roleService;
		private readonly IUserContext userContext;

		public ApplicationFiveController(
			IApplicationFiveService service,
			RoleService roleService,
			IUserContext userContext
			)
			: base(service, roleService, ApplicationType.ApplicationFive)
		{
			this.service = service;
			this.roleService = roleService;
			this.userContext = userContext;
		}

		[HttpGet]
		public async Task<SearchResultItemDto<ApplicationFiveSearchResultItemDto>> GetCreditsFiltered([FromQuery] SearchApplicationFiveFilter filter)
		{
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

            if (this.roleService.ValidateRole(UserRoleAliases.BANK_ACTIVE))
			{
				filter.BankId = this.userContext.BankId;
			}

			return await this.service.GetFiltered(filter);
		}

		[HttpPost("report")]
		public async Task<DividendReportResultDto> GetReport([FromBody] DividendReportSearchDto dto)
		{
			this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

			return await this.service.GenerateReport(dto);
		}
	}
}
