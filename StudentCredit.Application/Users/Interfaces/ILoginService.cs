using StudentCredit.Application.Users.Dtos;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Application.Users.Interfaces
{
    public interface ILoginService : ITransientService
	{
        Task<UserLoginInfoDto> Login(UserCredentialsDto model, CancellationToken cancellationToken);
    }
}
