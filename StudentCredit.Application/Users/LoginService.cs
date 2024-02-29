using Microsoft.EntityFrameworkCore;
using StudentCredit.Application.Nomenclatures.Dtos;
using StudentCredit.Application.Users.Dtos;
using StudentCredit.Application.Users.Interfaces;
using StudentCredit.Data.Users;
using StudentCredit.Data.Users.Enums;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.Helpers.Extensions;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.Users.Interfaces;

namespace StudentCredit.Application.Users
{
    public class LoginService : ILoginService
    {
        private readonly IAppDbContext context;
        private readonly IPasswordService passwordService;
        private readonly IJWTService jwtService;
        private readonly DomainValidationService validation;

        public LoginService(
            IAppDbContext context,
            IPasswordService passwordService,
            IJWTService jwtService,
            DomainValidationService validation
            )
        {
            this.context = context;
            this.passwordService = passwordService;
            this.jwtService = jwtService;
            this.validation = validation;
        }

        public async Task<UserLoginInfoDto> Login(UserCredentialsDto model, CancellationToken cancellationToken)
        {
            var user = await this.context.Set<User>()
                    .AsNoTracking()
                    .Include(u => u.Role)
                    .Include(u => u.Bank)
                    .SingleOrDefaultAsync(u => u.Username.Trim() == model.Username.Trim(), cancellationToken);

            if (user == null || user.Status != UserStatus.Active)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_InvalidCredentials);
            }

            bool isSamePassword = this.passwordService.VerifyHashedPassword(user.Password, model.Password, user.PasswordSalt);
            if (!isSamePassword)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_InvalidCredentials);
            }

            var result = new UserLoginInfoDto
            {
                Fullname = user.FirstName + " " + user.LastName,
                RoleAlias = user.Role.Alias,
                Token = this.jwtService.CreateToken(user.Id, user.Username, user.Role.Alias, user.BankId),
                Bank = user.Bank != null ? new BankNomenclatureDto { Id = user.Bank.Id, Name = user.Bank.Name, Bulstat = user.Bank.Bulstat, IsActive = user.Bank.IsActive } : null,
            };

            return result;
        }
    }
}
