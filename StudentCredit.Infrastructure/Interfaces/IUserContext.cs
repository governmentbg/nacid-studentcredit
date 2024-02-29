using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Infrastructure.Interfaces
{
    public interface IUserContext : IScopedService
	{
        int UserId { get; }
        string Username { get; }
        string Role { get; }
        int? BankId { get; }
    }
}
