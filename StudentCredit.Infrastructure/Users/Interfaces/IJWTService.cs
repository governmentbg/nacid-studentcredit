using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Infrastructure.Users.Interfaces
{
    public interface IJWTService : IScopedService
	{
        string CreateToken(int userId, string username, string roleAlias, int? bankId);
    }
}
