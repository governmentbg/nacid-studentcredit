using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentCredit.Application.Users.Dtos;
using StudentCredit.Application.Users.Interfaces;
using StudentCredit.Data.Emails;
using StudentCredit.Data.Nomenclatures.Constants;
using StudentCredit.Data.Users;
using StudentCredit.Data.Users.Enums;
using StudentCredit.Infrastructure.ConfigurationExtensions.Models;
using StudentCredit.Infrastructure.DomainValidation;
using StudentCredit.Infrastructure.DomainValidation.Enums;
using StudentCredit.Infrastructure.Emails;
using StudentCredit.Infrastructure.Interfaces.Contexts;
using StudentCredit.Infrastructure.Users.Interfaces;

namespace StudentCredit.Application.Users
{
    public class ActivationService : IActivationService
    {
        private readonly IAppDbContext context;
        private readonly IPasswordService passwordService;
        private readonly IEmailService emailService;
        private readonly DomainValidationService validation;
        private readonly AuthConfiguration authConfig;
        private readonly EmailsConfiguration emailConfiguration;

        public ActivationService(
            IAppDbContext context,
            IPasswordService passwordService,
            IEmailService emailService,
            DomainValidationService validation,
            IOptions<AuthConfiguration> options,
            IOptions<EmailsConfiguration> emailOptions)
        {
            this.context = context;
            this.passwordService = passwordService;
            this.emailService = emailService;
            this.validation = validation;
            this.authConfig = options.Value;
            this.emailConfiguration = emailOptions.Value;
        }

        public async Task SendActivationLink(int userId, CancellationToken cancellationToken)
        {
            var user = await this.context.Set<User>()
                    .AsNoTracking()
                    .Include(e => e.Role)
                    .SingleOrDefaultAsync(e => e.Id == userId, cancellationToken);

            if (user.Status != UserStatus.Inactive)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_UserAlreadyUnlocked);
            }

            var oldTokens = await this.context.Set<PasswordToken>()
                .Where(e => !e.IsUsed && e.UserId == userId)
                .ToListAsync(cancellationToken);

            foreach (var oldToken in oldTokens)
            {
                oldToken.Use();
            }

            await this.context.SaveChangesAsync(cancellationToken);

            PasswordToken passwordToken = new PasswordToken(user.Id, 20160);
            this.context.Set<PasswordToken>().Add(passwordToken);

            var payload = new
            {
                FullName = user.FirstName + " " + user.LastName,
                Role = user.Role.Name,
                ActivationLink = $"{authConfig.Issuer}/user/activation?token={passwordToken.Value}",
            };

            Email email = await this.emailService.ComposeEmailAsync(EmailTypeAlias.USER_ACTIVATION, payload, user.Email);
            this.context.Set<Email>().Add(email);

            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task CheckToken(string token, CancellationToken cancellationToken)
        {
            PasswordToken passwordToken = await this.context.Set<PasswordToken>()
                .SingleOrDefaultAsync(e => e.Value == token, cancellationToken);

            if (passwordToken == null)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_InvalidToken);
            }

            if (passwordToken.IsUsed)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_ActivationTokenAlreadyUsed);
            }

            if (passwordToken.ExpirationTime < DateTime.UtcNow)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_TokenExpired);
            }
        }

        public async Task ActivateUser(UserActivationDto model, CancellationToken cancellationToken)
        {
            if (model.Password != model.PasswordAgain)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_PasswordsMissmatch);
            }

            PasswordToken passwordToken = await this.context.Set<PasswordToken>()
                .Include(e => e.User)
                .SingleOrDefaultAsync(e => e.Value == model.Token, cancellationToken);

            if (passwordToken.IsUsed)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_ActivationTokenAlreadyUsed);
            }

            if (passwordToken.ExpirationTime < DateTime.UtcNow)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_TokenExpired);
            }

            passwordToken.Use();

            string salt = this.passwordService.GenerateSalt(128);
            string hash = this.passwordService.HashPassword(model.Password, salt);
            passwordToken.User.Activate(hash, salt);

            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
