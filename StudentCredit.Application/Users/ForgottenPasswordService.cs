using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentCredit.Application.DomainValidation.Enums;
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
    public class ForgottenPasswordService : IForgottenPasswordService
    {
        private readonly IAppDbContext context;
        private readonly IEmailService emailService;
        private readonly IPasswordService passwordService;
        private readonly DomainValidationService validation;
        private readonly AuthConfiguration authConfig;

        public ForgottenPasswordService(
            IAppDbContext context,
            IEmailService emailService,
            IPasswordService passwordService,
            DomainValidationService validation,
            IOptions<AuthConfiguration> options)
        {
            this.context = context;
            this.emailService = emailService;
            this.passwordService = passwordService;
            this.validation = validation;
            this.authConfig = options.Value;
        }

        public async Task SendMail(EmailForgottenPasswordDto model, CancellationToken cancellationToken)
        {
            var user = await this.context.Set<User>()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Email.Trim().ToLower() == model.Mail.Trim().ToLower());

            if (user == null)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_InvalidEmail);
            }

            if (user.Status != UserStatus.Active)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_CannotRestoreUserPassword);
            }

            PasswordToken passwordToken = new PasswordToken(user.Id, 20160);
            this.context.Set<PasswordToken>().Add(passwordToken);

            var payload = new
            {
                Username = user.Username,
                ForgottenPasswordLink = $"{authConfig.Issuer}/passwordRecovery?token={passwordToken.Value}"
            };

            Email email = await this.emailService.ComposeEmailAsync(EmailTypeAlias.FORGOTTEN_PASSWORD, payload, user.Email);
            this.context.Set<Email>().Add(email);
            await this.context.SaveChangesAsync(cancellationToken);
        }

        public async Task RecoverPassword(ForgottenPasswordDto model, CancellationToken cancellationToken)
        {
            if (model.NewPassword != model.NewPasswordAgain)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_ChangePasswordNewPasswordMismatch);
            }

            if (string.IsNullOrWhiteSpace(model.Token) || string.IsNullOrWhiteSpace(model.NewPassword))
            {
                this.validation.ThrowErrorMessage(SystemErrorCode.System_IncorrectParameters);
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
            string hash = this.passwordService.HashPassword(model.NewPassword, salt);
            passwordToken.User.ChangePassword(hash, salt);
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
