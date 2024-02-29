using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.Interfaces;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Infrastructure.Services
{
	public class RoleService : IScopedService
	{
		private readonly DomainValidationService validation;
		private readonly IUserContext userContext;

		public RoleService(DomainValidationService validation, IUserContext userContext)
		{
			this.validation = validation;
			this.userContext = userContext;
		}

		public void ValidateRoleException(string roleAlias)
		{
			if (roleAlias != this.userContext.Role)
			{
				this.validation.ThrowErrorMessage(UserErrorCode.User_NotEnoughPermission);
			}
		}

		public void ValidateRolesException(params string[] roleAliases)
		{
			var hasPermission = roleAliases.Any(s => s == this.userContext.Role);

			if (!hasPermission)
			{
				this.validation.ThrowErrorMessage(UserErrorCode.User_NotEnoughPermission);
            }
		}

		public bool ValidateRole(string roleAlias)
		{
			return roleAlias == this.userContext.Role;
		}
	}
}
