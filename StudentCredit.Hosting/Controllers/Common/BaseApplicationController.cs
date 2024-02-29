using Microsoft.AspNetCore.Mvc;
using StudentCredit.Application.Applications.Common.Dtos;
using StudentCredit.Application.Applications.Common.Interfaces;
using StudentCredit.Application.Infrastructure.Services;
using StudentCredit.Data.Applications.Common.Enums;
using StudentCredit.Data.Common.Enums;
using StudentCredit.Data.Users.Constants;

namespace StudentCredit.Hosting.Controllers.Common
{
	[ApiController]
	[Route("api/[controller]")]
	public abstract class BaseApplicationController<TDto> : ControllerBase
		where TDto : ApplicationCommitDto
	{
		private readonly IApplicationService<TDto> service;
		private readonly RoleService roleService;
		private readonly ApplicationType type;

		public BaseApplicationController(IApplicationService<TDto> service, RoleService roleService, ApplicationType type)
		{
			this.service = service;
			this.roleService = roleService;
			this.type = type;
		}

		[HttpGet("{id:int}")]
		public async Task<TDto> GetById([FromRoute] int id)
		{
			this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			return await this.service.GetById(id);
		}

		[HttpGet("history/{rootId:int}")]
		public async Task<List<ApplicationHistoryDto>> GetHistory([FromRoute] int rootId)
		{
			this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			return await this.service.GetHistory(rootId, this.type);
		}

		[HttpPost]
		public async Task Create([FromBody] TDto dto)
		{
			this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			await this.service.Create(dto);
		}

		[HttpPost("returnForModification")]
		public async Task<TDto> ReturnForModification([FromBody] ApplicationHistoryDto dto, [FromQuery] CommitState state)
		{
			this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

			await this.service.CreateHistory(dto, this.type);

			await this.service.ChangeApplicationState(dto.ApplicationId.Value, state, dto.Description);

			return await this.service.GetById(dto.ApplicationId.Value);
		}

		[HttpPost("finishmodification")]
		public async Task FinishModification([FromBody] TDto dto, [FromQuery] CommitState state)
		{
			this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.BANK_ACTIVE);

			var newAppId = await this.service.Edit(dto);

			await this.service.ChangeApplicationState(newAppId, state, null);
		}

		[HttpPost("{applicationId:int}/approve")]
		public virtual async Task<TDto> Approve([FromRoute] int applicationId)
		{
			this.roleService.ValidateRoleException(UserRoleAliases.ADMINISTRATOR);

			await this.service.ChangeApplicationState(applicationId, CommitState.Approved, null);

			return await this.service.GetById(applicationId);
		}
	}
}
