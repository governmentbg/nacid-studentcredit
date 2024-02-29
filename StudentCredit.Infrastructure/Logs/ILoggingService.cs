using Microsoft.AspNetCore.Http;
using StudentCredit.Data.Logs.Enums;
using StudentCredit.Infrastructure.Interfaces.Registration;

namespace StudentCredit.Infrastructure.Logs
{
    public interface ILoggingService : IScopedService
	{
        Task LogRequestAsync(HttpRequest request);

        Task LogExceptionAsync(Exception ex, LogType type, HttpRequest request = null);

		Task LogMessageAsync(string message, HttpRequest request = null);

		Task LogConsumerException(string message);
	}
}
