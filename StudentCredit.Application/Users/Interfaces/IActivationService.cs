using StudentCredit.Application.Users.Dtos;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Users.Interfaces
{
    public interface IActivationService : ITransientService
	{
        Task SendActivationLink(int userId, CancellationToken cancellationToken);
        Task CheckToken(string token, CancellationToken cancellationToken);
        Task ActivateUser(UserActivationDto model, CancellationToken cancellationToken);
    }
}
