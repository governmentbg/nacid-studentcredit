using StudentCredit.Application.Users.Dtos;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Users.Interfaces
{
    public interface IForgottenPasswordService : ITransientService
	{
        Task SendMail(EmailForgottenPasswordDto model, CancellationToken cancellationToken);
        Task RecoverPassword(ForgottenPasswordDto model, CancellationToken cancellationToken);
    }
}
